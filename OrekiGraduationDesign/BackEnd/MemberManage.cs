using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class MemberManage : Form
    {
        public MemberManage()
        {
            InitializeComponent();
        }

        public string Temp { get; private set; } = string.Empty;

        private void Button3_Click(object sender, EventArgs e)
        {
            ChkCon();
            Assets.ShowInput("会员卡号", "请扫描会员卡号");
            if (Assets.OkCancel == 0)
                return;
            Temp = Assets.Temp;
            var commandText = $"select member_name from market_members where member_barcode='{Assets.Temp}'";
            var commandText2 = $"select member_balance from market_members where member_barcode='{Assets.Temp}'";
            var command = new SqlCommand(commandText, _connection);
            var reader = command.ExecuteReader();
            var name = new object();
            while (reader.Read())
                name = reader[0];
            reader.Close();
            try
            {
                var unused = (string) name;
            }
            catch (Exception)
            {
                MessageBox.Show(@"不存在此会员条码");
                return;
            }
            label1.Text = $@"当前会员名称：{(string) name}";
            tabControl1.Enabled = true;
            label2.Text = $@"条码：{Assets.Temp}";
            label3.Text = $@"名称：{(string) name}";
            var command2 = new SqlCommand(commandText2, _connection);
            var reader2 = command2.ExecuteReader();
            var balance = new object();
            while (reader2.Read())
                balance = reader2[0];
            reader2.Close();
            button5.Enabled = (decimal) balance > 0;
            label4.Text = $@"余额：{(decimal) balance}";
            var commandText3 = $"select * from market_receipts where member_barcode='{Assets.Temp}'";
            var dataAdapter = new SqlDataAdapter(commandText3, Assets.ConStr);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "member_barcode");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "member_barcode";
            dataGridView1.Columns[0].DataPropertyName = dataSet.Tables[0].Columns[0].ToString();
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