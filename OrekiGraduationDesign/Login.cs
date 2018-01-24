using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                textBox2.Focus();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ChkCon();
            textBox1.Text = textBox1.Text.Replace("'", "");
            textBox2.Text = textBox2.Text.Replace("'", "");
            var commandText =
                $"select stuff_level from market_stuff where stuff_id='{textBox1.Text}' and stuff_password='{textBox2.Text}'";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            var level = new object();
            while (reader.Read())
                level = reader[0];
            reader.Close();
            try
            {
                var unused = (string) level;
            }
            catch
            {
                MessageBox.Show(@"不存在此会员条码");
                return;
            }
            if ((string) level == "front_end")
            {
                Assets.FrontEnd.Text = $@"超市信息管理系统 v1.0 - [{textBox1.Text}]";
                Assets.FrontEnd.Show();
                Hide();
            }
            else
            {
                Assets.BackEnd.Text = $@"超市信息管理系统 v1.0 - [{textBox1.Text}]";
                Assets.BackEnd.Show();
                Hide();
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                Button1_Click(sender, e);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Assets.IsAutomatic = true;
            Assets.FrontEnd.Text = @"无人超市自助收银系统";
            Assets.FrontEnd.Show();
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