using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class Login : Form
    {
        #region SQL Server Connection
        private SqlConnection connection = new SqlConnection(Assets.ConStr);

        private void ChkCon()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        #endregion
        public Login()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                textBox2.Focus();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            ChkCon();
            string commandText =
                $"select stuff_level from market_stuff where stuff_id='{textBox1.Text}' and stuff_password='{textBox2.Text}'";
            SqlCommand command = new SqlCommand(commandText, connection);
            SqlDataReader reader = command.ExecuteReader();
            object level = new object();
            while (reader.Read())
            {
                level = reader[0];
            }
            reader.Close();
            try
            {
                string a = (string)level;
            }
            catch
            {
                MessageBox.Show("登录失败");
                return;
            }
            if ((string)level == "front-end")
            {
                Assets.FrontEnd.Show();
                this.Hide();
            }
        }

        private void TextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Button1_Click(sender, e);
            }
        }
    }
}
