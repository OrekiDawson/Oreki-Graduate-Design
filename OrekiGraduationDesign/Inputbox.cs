using System;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class Inputbox : Form
    {
        public Inputbox()
        {
            InitializeComponent();
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                Button1_Click(sender, e);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text.Replace("'", "");
            Assets.Temp = textBox1.Text;
            Assets.OkCancel = 1;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Assets.OkCancel = 0;
            Close();
        }
    }
}