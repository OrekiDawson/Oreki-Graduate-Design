using System;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    public partial class BackEnd : Form
    {
        public BackEnd()
        {
            InitializeComponent();
        }

        private void 商品管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var itemManage = new ItemManage {MdiParent = this};
            itemManage.Show();
        }

        private void 会员管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var memberManage = new MemberManage {MdiParent = this};
            memberManage.Show();
        }

        private void 优惠券管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var couponManage = new CouponManage {MdiParent = this};
            couponManage.Show();
        }

        private void BackEnd_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}