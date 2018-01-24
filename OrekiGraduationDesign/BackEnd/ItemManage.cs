using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class ItemManage : Form
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

        public ItemManage()
        {
            InitializeComponent();
            textBox1.Text = textBox1.Text.Replace("'", "");
            var commandText = $"select * from market_items where item_barcode='{textBox1.Text}' or item_name like '%{textBox1.Text}%'";
            var dataAdapter = new SqlDataAdapter(commandText, Assets.ConStr);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "item_barcode");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "item_barcode";
            dataGridView1.Columns[0].DataPropertyName = dataSet.Tables[0].Columns[0].ToString();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            ChkCon();
            if (e.KeyChar==Convert.ToChar(Keys.Enter))
            {
                

            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            ItemSearch();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        void ItemSearch()
        {
            textBox1.Text = textBox1.Text.Replace("'", "");
            var commandText = $"select * from market_items where item_barcode='{textBox1.Text}' or item_name like '%{textBox1.Text}%'";
            var dataAdapter = new SqlDataAdapter(commandText, Assets.ConStr);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, "item_barcode");
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "item_barcode";
            dataGridView1.Columns[0].DataPropertyName = dataSet.Tables[0].Columns[0].ToString();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var itemAddorChange = new ItemAddorChange
            {
                MdiParent = Assets.BackEnd,
                Text = "添加商品",
                button1 =
                {
                    Text = "添加",
                    Enabled = false
                },
                textBox1 = {Enabled = true}
            };
            itemAddorChange.Show();
            itemAddorChange.textBox1.Focus();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var itemAddorChange = new ItemAddorChange
            {
                MdiParent = Assets.BackEnd,
                Text = "商品修改"
            };
            itemAddorChange.button1.Text = "修改";
            itemAddorChange.textBox1.Text = Assets.ItemBarcode;
            itemAddorChange.textBox2.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value);
            itemAddorChange.textBox3.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value);
            itemAddorChange.textBox4.Text = Convert.ToString(dataGridView1.CurrentRow.Cells[3].Value);
            itemAddorChange.textBox1.Enabled = false;
            itemAddorChange.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            ChkCon();
            Assets.ShowInput("删除", "扫描条码以确认删除");
            if (Assets.OkCancel == 0)
            {
                return;
            }
            if (Assets.Temp == Assets.ItemBarcode)
            {
                var commandText = $"delete from market_items where item_barcode='{Assets.ItemBarcode}'";
                var command = new SqlCommand(commandText, _connection);
                command.ExecuteNonQuery();
                MessageBox.Show(@"不存在此会员条码");
            }
            else
            {
                MessageBox.Show(@"不存在此会员条码");
            }
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            Assets.ItemBarcode = Convert.ToString(dataGridView1.CurrentRow.Cells[0].Value);

        }
    }
}
