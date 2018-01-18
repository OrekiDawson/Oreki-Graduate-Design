using System;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class FrontEnd : Form
    {
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private DataTable dataTable = new DataTable();

        public Guid _guid;

        public string CouponID = "";

        public decimal CouponUseif = 0;

        public decimal couponPrice = 0;

        public bool useCoupon = false;

        public string MemberID = "0";

        public decimal total = 0;

        public bool isMember = false;

        #region SQL Server Connection
        private SqlConnection _connection = new SqlConnection(Assets.ConStr);

        private void ChkCon()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }
        #endregion

        public FrontEnd()
        {
            InitializeComponent();
            DataColumn dataColumn1 = new DataColumn("条码", Type.GetType("System.String") ?? throw new InvalidOperationException());
            DataColumn dataColumn2 = new DataColumn("品名", Type.GetType("System.String") ?? throw new InvalidOperationException());
            DataColumn dataColumn3 = new DataColumn("单价", Type.GetType("System.Decimal") ?? throw new InvalidOperationException());
            DataColumn dataColumn4 = new DataColumn("折扣", Type.GetType("System.Int32") ?? throw new InvalidOperationException());
            DataColumn dataColumn5 = new DataColumn("数量", Type.GetType("System.Decimal") ?? throw new InvalidOperationException());
            DataColumn dataColumn6 = new DataColumn("总价", Type.GetType("System.Decimal") ?? throw new InvalidOperationException());
            dataTable.Columns.Add(dataColumn1);
            dataTable.Columns.Add(dataColumn2);
            dataTable.Columns.Add(dataColumn3);
            dataTable.Columns.Add(dataColumn4);
            dataTable.Columns.Add(dataColumn5);
            dataTable.Columns.Add(dataColumn6);
            //label1.Location = new Point(10, Height - 10);
            NewGUID();
        }

        private void NewGUID()
        {
            _guid = Guid.NewGuid();
            StatusLabelGUID.Text = $"流水号：{_guid}                       ";
        }

        private void FrontEnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void FrontEnd_Resize(object sender, EventArgs e)
        {
            //label1.Location = new Point(10, Height - 70);
            textBox1.Location = new Point(10, Height - 100);
            textBox1.Width = Width - 37;
            dataGridView1.Height = Height - 130;
            dataGridView1.Width = Width - 37;
            dataGridView1.DataSource = dataTable;
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                AddItem(textBox1.Text);
            }
        }

        private void AddItem(string itemBarcode)
        {
            decimal tempQuantity = 0;
            if (dataGridView1.RowCount > 1)
            {
                for (int i = 0; i <= dataGridView1.RowCount - 2; i++)
                {
                    if ((string)dataGridView1.Rows[i].Cells["条码"].Value == itemBarcode)
                    {
                        tempQuantity = (decimal)dataGridView1.Rows[i].Cells["数量"].Value;
                        dataGridView1.Rows.RemoveAt(i);
                    }
                }

            }
            ChkCon();
            string commandText = $"select item_name from market_items where item_barcode='{itemBarcode}'";
            string commandText2 = $"select item_price from market_items where item_barcode='{itemBarcode}'";
            string commandText3 = $"select item_discount from market_items where item_barcode='{itemBarcode}'";
            SqlCommand
                command = new SqlCommand(commandText, _connection),
                command2 = new SqlCommand(commandText2, _connection),
                command3 = new SqlCommand(commandText3, _connection);
            object
                name = new object(),
                price = new object(),
                discount = new object();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                name = reader[0];
            }
            reader.Close();
            try
            {
                string xyz = (string)name;
            }
            catch (Exception)
            {
                MessageBox.Show("商品不存在");
                textBox1.Text = string.Empty;
                return;
            }
            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                price = reader2[0];
            }
            //reader2.Close();
            reader2.Close();
            SqlDataReader reader3 = command3.ExecuteReader();
            while (reader3.Read())
            {
                discount = reader3[0];
            }
            reader3.Close();
            DataRow dataRow = dataTable.NewRow();
            dataRow["条码"] = itemBarcode;
            dataRow["品名"] = (string)name;
            dataRow["单价"] = (decimal)price;
            dataRow["折扣"] = (int)discount;
            dataRow["数量"] = tempQuantity + 1;
            dataRow["总价"] = (tempQuantity + 1) * (decimal)price * (int)discount / 100;
            dataTable.Rows.Add(dataRow);
            textBox1.Text = String.Empty;
            CalcTotal();
        }

        private void Coupon()
        {
            ChkCon();
            Assets.ShowInput("优惠券", "输入优惠券编码：");
            if (Assets.OkCancel == 1)
            {
                string commandText = $"select coupon_barcode from market_coupons where coupon_barcode='{Assets.Temp}'";
                SqlCommand command = new SqlCommand(commandText, _connection);
                SqlDataReader reader = command.ExecuteReader();
                object
                    couponID = new object(),
                    useif = new object(),
                    price = new object();
                while (reader.Read())
                {
                    couponID = reader[0];
                }
                reader.Close();
                try
                {
                    string x = (string)couponID;
                }
                catch (Exception)
                {
                    MessageBox.Show("优惠码不存在");
                    return;
                }
                this.CouponID = (string)couponID;
                StatusLabelCoupon.Text = $"优惠码：{this.CouponID}";
                useCoupon = true;
                string commandText2 = $"select coupon_useif from market_coupons where coupon_barcode='{Assets.Temp}'";
                string commandText3 = $"select coupon_price from market_coupons where coupon_barcode='{Assets.Temp}'";
                SqlCommand command2 = new SqlCommand(commandText2, _connection);
                SqlCommand command3 = new SqlCommand(commandText3, _connection);
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    useif = reader2[0];
                }
                reader2.Close();
                SqlDataReader reader3 = command3.ExecuteReader();
                while (reader3.Read())
                {
                    price = reader3[0];
                }
                reader3.Close();
                this.CouponUseif = (decimal)useif;
                this.couponPrice = (decimal)price;
            }
        }

        private void DelOne()
        {
            if (dataGridView1.RowCount == 1)
            {
                if (MessageBox.Show("列表为空，是否初始化？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Init();
                }
                return;
            }
            if (MessageBox.Show("确定要删除一项？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.RowCount - 2);
                CalcTotal();
                if (dataGridView1.RowCount == 1)
                {

                    Init();
                }
            }
        }

        private void DelAll()
        {
            if (dataGridView1.RowCount == 1)
            {
                if (MessageBox.Show("列表为空，是否初始化？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //init();
                }
                return;
            }
            if (MessageBox.Show("确定要初始化？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                /*
                for (int i = dataGridView1.RowCount - 2; i >= 0; i--)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }*/

                Init();
            }
        }

        private void Quantity()
        {
            if (dataGridView1.RowCount != 1)
            {
                Assets.ShowInput("数量", "输入数量：");
                if (Assets.OkCancel == 1)
                {
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
                        else
                        {

                        }
                    }
                    catch
                    { }
                }
            }
        }

        private void Member()
        {
            ChkCon();
            Assets.ShowInput("会员卡", "请输入会员卡号：");
            if (Assets.OkCancel == 1)
            {
                string commandText = $"select member_barcode from market_members where member_barcode='{Assets.Temp}'";
                SqlCommand command = new SqlCommand(commandText, _connection);
                SqlDataReader reader = command.ExecuteReader();
                object memberID = new object();
                while (reader.Read())
                {
                    memberID = reader[0];
                }
                reader.Close();
                try
                {
                    string x = (string)memberID;
                }
                catch (Exception)
                {
                    MessageBox.Show("会员卡号不存在");
                    return;
                }
                this.MemberID = (string)memberID;
                StatusLabelMember.Text = $"会员卡号：{this.MemberID}";
                this.isMember = true;
            }
        }

        private void CheckitOut()
        {
            if (this.total>0)
            {
                MarketCheckout marketCheckout = new MarketCheckout();
                marketCheckout.ShowDialog();
            }
            
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Coupon();
            }
            if (e.KeyCode == Keys.F3)
            {
                DelOne();
            }
            if (e.KeyCode == Keys.F4)
            {
                DelAll();
            }
            if (e.KeyCode == Keys.F5)
            {
                Quantity();
            }
            if (e.KeyCode == Keys.F8)
            {
                Member();
            }
            if (e.KeyCode == Keys.F12)
            {
                CheckitOut();
            }
        }

        private void CalcTotal()
        {
            this.total = 0;
            if (dataGridView1.RowCount > 1)
            {
                for (int i = 0; i <= dataGridView1.RowCount - 2; i++)
                    total +=(decimal) dataGridView1.Rows[i].Cells["总价"].Value;
            }
        }

        public void Init()
        {
            NewGUID();
            StatusLabelMember.Text = "会员卡号：空";
            StatusLabelCoupon.Text = "优惠码：空";
            useCoupon = false;
            isMember = false;
            if (dataGridView1.RowCount>1)
            {
                for (int i = dataGridView1.RowCount - 2; i >= 0; i--)
                {
                    dataGridView1.Rows.RemoveAt(i);
                }
            }
            textBox1.Focus();
        }
    }
}
