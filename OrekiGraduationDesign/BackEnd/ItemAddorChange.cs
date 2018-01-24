using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class ItemAddorChange : Form
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

        public ItemAddorChange()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "添加")
            {
                AddItem();
            }
            else if (button1.Text == "修改")
            {
                ChangeItem();
            }
        }

        private void ChkEnable()
        {
            textBox1.Text = textBox1.Text.Replace("'", "");
            textBox2.Text = textBox2.Text.Replace("'", "");
            textBox3.Text = textBox3.Text.Replace("'", "");
            textBox4.Text = textBox4.Text.Replace("'", "");
            button1.Enabled = textBox1.Text != String.Empty && textBox2.Text != String.Empty &&
                              textBox3.Text != String.Empty && textBox4.Text != String.Empty;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            ChkEnable();
        }

        private void AddItem()
        {
            ChkCon();
            var commandText3 = $"select item_name from market_items where item_barcode='{textBox1.Text}'";
            var command3 = new SqlCommand(commandText3, _connection);
            var bookname = new object();
            {
                var reader = command3.ExecuteReader();

                while (reader.Read())
                {
                    bookname = reader[0];
                }
                reader.Close();
            }
            try
            {
                var aaa = (string)bookname;
            }
            catch (Exception)
            {
                /*商品不存在，可添加*/
                var commandText = $"insert into market_items values ('{textBox1.Text}','{textBox2.Text}',{textBox3.Text},{textBox4.Text})";
                var command = new SqlCommand(commandText, _connection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    MessageBox.Show(@"不存在此会员条码");
                    return;
                }
                MessageBox.Show(@"不存在此会员条码");
                Close();
                return;
            }
            MessageBox.Show(@"不存在此会员条码");
        }

        private void ChangeItem()
        {
            ChkCon();
            var commandText =
                $"update market_items set item_name='{textBox2.Text}',item_price={textBox3.Text},item_discount={textBox4.Text} where item_barcode='{textBox1.Text}'";
            var command = new SqlCommand(commandText, _connection);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                MessageBox.Show(@"不存在此会员条码");
            }
            MessageBox.Show(@"不存在此会员条码");
            Close();
        }
    }
}
