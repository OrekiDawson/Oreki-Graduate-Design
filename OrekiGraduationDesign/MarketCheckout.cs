using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class MarketCheckout : Form
    {
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

        public decimal total = 0;

        public bool useCouopnTruly = false;

        public decimal price = 0;

        public decimal memberPrice = 0;

        public decimal memberBalance = 0;

        public MarketCheckout()
        {

            InitializeComponent();
            ChkCon();
            labelTotal.Text = $"总价：{Assets.FrontEnd.total}";
            if (Assets.FrontEnd.useCoupon == false)
            {
                labelCoupon.Text = "优惠券：无";
                useCouopnTruly = false;
                price = Assets.FrontEnd.total;
            }
            else
            {
                if (Assets.FrontEnd.CouponUseif > Assets.FrontEnd.total)
                {
                    labelCoupon.Text = "优惠券：不可用";
                    useCouopnTruly = false;
                    price = Assets.FrontEnd.total;
                }
                else 
                {
                    labelCoupon.Text = $"优惠券：{Assets.FrontEnd.couponPrice}";
                    useCouopnTruly = true;
                    price = Assets.FrontEnd.total - Assets.FrontEnd.couponPrice;
                }
            }
            labelPrice.Text = $"应收：{price}";
            if (Assets.FrontEnd.isMember)
            {
                string commandText =
                    $"select member_balance from market_members where member_barcode={Assets.FrontEnd.MemberID}";
                SqlCommand command=new SqlCommand(commandText,_connection);
                SqlDataReader reader = command.ExecuteReader();
                object member=new object();
                while (reader.Read())
                {
                    member = reader[0];
                }
                reader.Close();
                memberBalance = (decimal) member;
                if (memberBalance>=price)
                {
                    memberPrice = price;
                    labelMember.Text = $"会员卡扣款：{memberPrice}";
                    textBox1.Text = "0";
                    textBox1.Visible = false;
                    labelCash.Visible = false;
                    buttonCheckOut.Enabled = true;
                    buttonCheckOut.Focus();
                }
                else
                {
                    memberPrice = memberBalance;
                    labelMember.Text = $"会员卡扣款：{memberPrice}";
                    textBox1.Text = $"{memberPrice}";
                    textBox1.SelectAll();
                    buttonCheckOut.Enabled = true;
                }
            }
            else
            {
                labelMember.Text = "";
                textBox1.Text = $"{price}";
                textBox1.SelectAll();
                buttonCheckOut.Enabled = true;
            }
        }

        private void ButtonCheckOut_Click(object sender, EventArgs e)
        {
            ChkCon();
            decimal cash = Convert.ToDecimal(textBox1.Text);
            if (cash+memberPrice>=price)
            {
                if (useCouopnTruly)
                {
                    string commandText =
                        $"delete from market_coupons where coupon_barcode='{Assets.FrontEnd.CouponID}'";
                    SqlCommand command=new SqlCommand(commandText,_connection);
                    command.ExecuteNonQuery();
                }
                if (Assets.FrontEnd.isMember)
                {
                    string commandText =
                        $"update market_members set member_balance=member_balance-{memberPrice} where member_barcode='{Assets.FrontEnd.MemberID}'";
                    SqlCommand command=new SqlCommand(commandText,_connection);
                    command.ExecuteNonQuery();
                }
                for (int i = 0; i <= Assets.FrontEnd.dataGridView1.RowCount - 2; i++)
                {
                    string commandText =
                        $"insert into market_records values('{Assets.FrontEnd._guid}','{Assets.FrontEnd.dataGridView1.Rows[i].Cells["条码"].Value}',{Assets.FrontEnd.dataGridView1.Rows[i].Cells["数量"].Value},{Assets.FrontEnd.dataGridView1.Rows[i].Cells["单价"].Value},0)";
                    //MessageBox.Show(commandText);
                    SqlCommand command=new SqlCommand(commandText,_connection);
                    command.ExecuteNonQuery();
                }
                string commCheckoutText =
                    $"insert into market_receipts values('{Assets.FrontEnd._guid}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',{price},{price},'{Assets.FrontEnd.MemberID}')";
                SqlCommand commCheckout=new SqlCommand(commCheckoutText,_connection);
                commCheckout.ExecuteNonQuery();
                buttonCheckOut.Enabled = false;
                button2.Text = "关闭";
                Assets.FrontEnd.Init();
                if (cash+memberPrice>price)
                {
                    labelRefund.Visible = true;
                    labelRefund.Text = $"找零：{cash + memberPrice - price}";
                }
            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==Convert.ToChar(Keys.Enter))
            {
                ButtonCheckOut_Click(sender,e);
            }
        }
    }
}
