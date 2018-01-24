using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class FrontEnd : Form
    {
        private readonly DataTable _dataTable = new DataTable();

        public string CouponId = "";

        public decimal CouponPrice;

        public decimal CouponUseif;

        public Guid Guid;

        public bool IsMember;

        public string MemberId = "0";

        public decimal Total;

        public bool UseCoupon;

        public FrontEnd()
        {
            InitializeComponent();
            var dataColumn1 =
                new DataColumn("条码", Type.GetType("System.String") ?? throw new InvalidOperationException());
            var dataColumn2 =
                new DataColumn("品名", Type.GetType("System.String") ?? throw new InvalidOperationException());
            var dataColumn3 = new DataColumn("单价",
                Type.GetType("System.Decimal") ?? throw new InvalidOperationException());
            var dataColumn4 =
                new DataColumn("折扣", Type.GetType("System.Int32") ?? throw new InvalidOperationException());
            var dataColumn5 = new DataColumn("数量",
                Type.GetType("System.Decimal") ?? throw new InvalidOperationException());
            var dataColumn6 = new DataColumn("总价",
                Type.GetType("System.Decimal") ?? throw new InvalidOperationException());
            _dataTable.Columns.Add(dataColumn1);
            _dataTable.Columns.Add(dataColumn2);
            _dataTable.Columns.Add(dataColumn3);
            _dataTable.Columns.Add(dataColumn4);
            _dataTable.Columns.Add(dataColumn5);
            _dataTable.Columns.Add(dataColumn6);
            NewGuid();
        }

        private void NewGuid()
        {
            Guid = Guid.NewGuid();
            StatusLabelGUID.Text = $"流水号：{Guid}                       ";
        }

        private void FrontEnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrontEnd_Resize(object sender, EventArgs e)
        {
            textBox1.Location = new Point(10, Height - 100);
            textBox1.Width = Width - 37;
            dataGridView1.Height = Height - 130;
            dataGridView1.Width = Width - 37;
            dataGridView1.DataSource = _dataTable;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                AddItem(textBox1.Text);
        }

        private void AddItem(string itemBarcode)
        {
            decimal tempQuantity = 0;
            if (dataGridView1.RowCount > 1)
                for (var i = 0; i <= dataGridView1.RowCount - 2; i++)
                    if ((string) dataGridView1.Rows[i].Cells["条码"].Value == itemBarcode)
                    {
                        tempQuantity = (decimal) dataGridView1.Rows[i].Cells["数量"].Value;
                        dataGridView1.Rows.RemoveAt(i);
                    }
            ChkCon();
            var commandText = $"select item_name from market_items where item_barcode='{itemBarcode}'";
            var commandText2 = $"select item_price from market_items where item_barcode='{itemBarcode}'";
            var commandText3 = $"select item_discount from market_items where item_barcode='{itemBarcode}'";
            SqlCommand
                command = new SqlCommand(commandText, _connection),
                command2 = new SqlCommand(commandText2, _connection),
                command3 = new SqlCommand(commandText3, _connection);
            object
                name = new object(),
                price = new object(),
                discount = new object();
            var reader = command.ExecuteReader();
            while (reader.Read())
                name = reader[0];
            reader.Close();
            try
            {
                var xyz = (string) name;
            }
            catch (Exception)
            {
                MessageBox.Show(@"不存在此会员条码");
                textBox1.Text = string.Empty;
                return;
            }
            var reader2 = command2.ExecuteReader();
            while (reader2.Read())
                price = reader2[0];
            //reader2.Close();
            reader2.Close();
            var reader3 = command3.ExecuteReader();
            while (reader3.Read())
                discount = reader3[0];
            reader3.Close();
            var dataRow = _dataTable.NewRow();
            dataRow["条码"] = itemBarcode;
            dataRow["品名"] = (string) name;
            dataRow["单价"] = (decimal) price;
            dataRow["折扣"] = (int) discount;
            dataRow["数量"] = tempQuantity + 1;
            dataRow["总价"] = (tempQuantity + 1) * (decimal) price * (int) discount / 100;
            _dataTable.Rows.Add(dataRow);
            textBox1.Text = string.Empty;
            CalcTotal();
        }

        private void Coupon()
        {
            ChkCon();
            Assets.ShowInput("优惠券", "输入优惠券编码：");
            if (Assets.OkCancel == 1)
            {
                var commandText = $"select coupon_barcode from market_coupons where coupon_barcode='{Assets.Temp}'";
                var command = new SqlCommand(commandText, _connection);
                var reader = command.ExecuteReader();
                object
                    couponId = new object(),
                    useif = new object(),
                    price = new object();
                while (reader.Read())
                    couponId = reader[0];
                reader.Close();
                try
                {
                    var x = (string) couponId;
                }
                catch (Exception)
                {
                    MessageBox.Show(@"不存在此会员条码");
                    return;
                }
                CouponId = (string) couponId;
                StatusLabelCoupon.Text = $"优惠码：{CouponId}";
                UseCoupon = true;
                var commandText2 = $"select coupon_useif from market_coupons where coupon_barcode='{Assets.Temp}'";
                var commandText3 = $"select coupon_price from market_coupons where coupon_barcode='{Assets.Temp}'";
                var command2 = new SqlCommand(commandText2, _connection);
                var command3 = new SqlCommand(commandText3, _connection);
                var reader2 = command2.ExecuteReader();
                while (reader2.Read())
                    useif = reader2[0];
                reader2.Close();
                var reader3 = command3.ExecuteReader();
                while (reader3.Read())
                    price = reader3[0];
                reader3.Close();
                CouponUseif = (decimal) useif;
                CouponPrice = (decimal) price;
            }
        }

        private void DelOne()
        {
            if (dataGridView1.RowCount == 1)
            {
                if (MessageBox.Show("列表为空，是否初始化？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Init();
                return;
            }
            if (MessageBox.Show("确定要删除一项？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.RowCount - 2);
                CalcTotal();
                if (dataGridView1.RowCount == 1)
                    Init();
            }
        }

        private void DelAll()
        {
            if (dataGridView1.RowCount == 1)
            {
                if (MessageBox.Show("列表为空，是否初始化？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    Init();
                return;
            }
            if (MessageBox.Show("确定要初始化？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
                Init();
        }

        private void Quantity()
        {
            if (dataGridView1.RowCount != 1)
            {
                Assets.ShowInput("数量", "输入数量：");
                if (Assets.OkCancel == 1)
                    try
                    {
                        if (Convert.ToDecimal(Assets.Temp) == 0)
                        {
                            DelOne();
                        }
                        else if (Convert.ToDecimal(Assets.Temp) > 0)
                        {
                            dataGridView1.Rows[dataGridView1.RowCount - 2].Cells["数量"].Value = Assets.Temp;
                            CalcTotal();
                        }
                    }
                    catch
                    {
                    }
            }
        }

        private void Member()
        {
            ChkCon();
            Assets.ShowInput("会员卡", "请输入会员卡号：");
            if (Assets.OkCancel == 1)
            {
                var commandText = $"select member_barcode from market_members where member_barcode='{Assets.Temp}'";
                var command = new SqlCommand(commandText, _connection);
                var reader = command.ExecuteReader();
                var memberId = new object();
                while (reader.Read())
                    memberId = reader[0];
                reader.Close();
                try
                {
                    var x = (string) memberId;
                }
                catch (Exception)
                {
                    MessageBox.Show(@"不存在此会员条码");
                    return;
                }
                MemberId = (string) memberId;
                StatusLabelMember.Text = $"会员卡号：{MemberId}";
                IsMember = true;
            }
        }

        private void CheckitOut()
        {
            if (Total > 0)
            {
                var marketCheckout = new MarketCheckout();
                marketCheckout.ShowDialog();
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                Coupon();
            if (e.KeyCode == Keys.F3)
                DelOne();
            if (e.KeyCode == Keys.F4)
                DelAll();
            if (e.KeyCode == Keys.F5)
                Quantity();
            if (e.KeyCode == Keys.F8)
                Member();
            if (e.KeyCode == Keys.F12)
                CheckitOut();
        }

        private void CalcTotal()
        {
            Total = 0;
            if (dataGridView1.RowCount > 1)
                for (var i = 0; i <= dataGridView1.RowCount - 2; i++)
                    Total += (decimal) dataGridView1.Rows[i].Cells["总价"].Value;
        }

        public void Init()
        {
            NewGuid();
            StatusLabelMember.Text = "会员卡号：空";
            StatusLabelCoupon.Text = "优惠码：空";
            UseCoupon = false;
            IsMember = false;
            if (dataGridView1.RowCount > 1)
                for (var i = dataGridView1.RowCount - 2; i >= 0; i--)
                    dataGridView1.Rows.RemoveAt(i);
            CalcTotal();
            textBox1.Focus();
            if (Assets.IsAutomatic)
                while (!IsMember)
                    Member();
        }

        private void FrontEnd_Load(object sender, EventArgs e)
        {
            if (Assets.IsAutomatic)
                while (!IsMember)
                    Member();
        }

        #region SQL Server Connection

        private readonly SqlConnection _connection = new SqlConnection(Assets.ConStr);

        private void ChkCon()
        {
            if (_connection.State == ConnectionState.Closed)
                _connection.Open();
        }

        #endregion
    }
}