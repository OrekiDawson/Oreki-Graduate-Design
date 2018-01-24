using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
// ReSharper disable All

namespace OrekiGraduationDesign
{
    public partial class MarketCheckout : Form
    {
        private bool _autoOk;

        public decimal MemberBalance;

        public decimal MemberPrice;

        public decimal Price;

        public decimal Total = 0;

        public bool UseCouopnTruly;

        public MarketCheckout()
        {
            InitializeComponent();
            if (Assets.IsAutomatic)
            {
                labelCash.Visible = false;
                textBox1.Visible = false;
            }

            ChkCon();
            labelTotal.Text = $"总价：{Assets.FrontEnd.Total}";
            if (Assets.FrontEnd.UseCoupon == false)
            {
                labelCoupon.Text = "优惠券：无";
                UseCouopnTruly = false;
                Price = Assets.FrontEnd.Total;
            }
            else
            {
                if (Assets.FrontEnd.CouponUseif > Assets.FrontEnd.Total)
                {
                    labelCoupon.Text = "优惠券：不可用";
                    UseCouopnTruly = false;
                    Price = Assets.FrontEnd.Total;
                }
                else
                {
                    labelCoupon.Text = $"优惠券：{Assets.FrontEnd.CouponPrice}";
                    UseCouopnTruly = true;
                    Price = Assets.FrontEnd.Total - Assets.FrontEnd.CouponPrice;
                }
            }
            labelPrice.Text = $"应收：{Price}";
            if (Assets.FrontEnd.IsMember)
            {
                var commandText =
                    $"select member_balance from market_members where member_barcode={Assets.FrontEnd.MemberId}";
                var command = new SqlCommand(commandText, _connection);
                var reader = command.ExecuteReader();
                var member = new object();
                while (reader.Read())
                    member = reader[0];
                reader.Close();
                MemberBalance = (decimal) member;
                if (MemberBalance >= Price)
                {
                    MemberPrice = Price;
                    labelMember.Text = $"会员卡扣款：{MemberPrice}";
                    textBox1.Text = "0";
                    textBox1.Visible = false;
                    labelCash.Visible = false;
                    buttonCheckOut.Enabled = true;
                    buttonCheckOut.Focus();
                }
                else
                {
                    MemberPrice = MemberBalance;
                    labelMember.Text = $"会员卡扣款：{MemberPrice}";
                    if (Assets.IsAutomatic)
                        textBox1.Text = "0";
                    else
                        textBox1.Text = $"{Price - MemberPrice}";

                    textBox1.SelectAll();
                    buttonCheckOut.Enabled = true;
                }
            }
            else
            {
                labelMember.Text = "";
                textBox1.Text = $"{Price}";
                textBox1.SelectAll();
                buttonCheckOut.Enabled = true;
            }
        }

        private void ButtonCheckOut_Click(object sender, EventArgs e)
        {
            ChkCon();
            textBox1.Text = textBox1.Text.Replace("'", "");
            var cash = Convert.ToDecimal(textBox1.Text);
            if (cash + MemberPrice >= Price)
            {
                if (UseCouopnTruly)
                {
                    var commandText =
                        $"delete from market_coupons where coupon_barcode='{Assets.FrontEnd.CouponId}'";
                    var command = new SqlCommand(commandText, _connection);
                    command.ExecuteNonQuery();
                }
                if (Assets.FrontEnd.IsMember)
                {
                    var commandText =
                        $"update market_members set member_balance=member_balance-{MemberPrice} where member_barcode='{Assets.FrontEnd.MemberId}'";
                    var command = new SqlCommand(commandText, _connection);
                    command.ExecuteNonQuery();
                }
                for (var i = 0; i <= Assets.FrontEnd.dataGridView1.RowCount - 2; i++)
                {
                    var commandText =
                        $"insert into market_records values('{Assets.FrontEnd.Guid}','{Assets.FrontEnd.dataGridView1.Rows[i].Cells["条码"].Value}',{Assets.FrontEnd.dataGridView1.Rows[i].Cells["数量"].Value},{Assets.FrontEnd.dataGridView1.Rows[i].Cells["单价"].Value},0)";
                    //MessageBox.Show(commandText);
                    var command = new SqlCommand(commandText, _connection);
                    command.ExecuteNonQuery();
                }
                var commCheckoutText =
                    $"insert into market_receipts values('{Assets.FrontEnd.Guid}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',{Price},{Price},'{Assets.FrontEnd.MemberId}')";
                var commCheckout = new SqlCommand(commCheckoutText, _connection);
                commCheckout.ExecuteNonQuery();

                if (Assets.IsAutomatic)
                {
                    _autoOk = true;

                    labelRefund.Visible = true;
                    labelRefund.Text = "结账成功，祝您购物愉快！";
                    //System.Threading.Thread.Sleep(3000);
                    labelCoupon.Visible = false;
                    labelTotal.Visible = false;
                    labelPrice.Visible = false;
                    labelMember.Visible = false;
                    Close();
                }
                else
                {
                    buttonCheckOut.Enabled = false;
                    textBox1.ReadOnly = true;
                    button2.Text = "关闭";
                    button2.Focus();
                    Assets.FrontEnd.Init();
                    if (cash + MemberPrice > Price)
                    {
                        labelRefund.Visible = true;
                        labelRefund.Text = $"找零：{cash + MemberPrice - Price}";
                    }
                }
            }
            else
            {
                if (Assets.IsAutomatic)
                {
                    MessageBox.Show(@"不存在此会员条码");
                }
                else
                {
                    MessageBox.Show(@"不存在此会员条码");
                    textBox1.SelectAll();
                }
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                ButtonCheckOut_Click(sender, e);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
        }

        private void MarketCheckout_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_autoOk)
            {
                Refresh();
                Assets.FrontEnd.Init();
            }
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