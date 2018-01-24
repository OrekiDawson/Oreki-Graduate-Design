using System.ComponentModel;
using System.Windows.Forms;

namespace OrekiGraduationDesign
{
    partial class BackEnd
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.商品管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.会员管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.优惠券管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.商品管理ToolStripMenuItem,
            this.会员管理ToolStripMenuItem,
            this.优惠券管理ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(711, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 商品管理ToolStripMenuItem
            // 
            this.商品管理ToolStripMenuItem.Name = "商品管理ToolStripMenuItem";
            this.商品管理ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.商品管理ToolStripMenuItem.Text = "商品管理";
            this.商品管理ToolStripMenuItem.Click += new System.EventHandler(this.商品管理ToolStripMenuItem_Click);
            // 
            // 会员管理ToolStripMenuItem
            // 
            this.会员管理ToolStripMenuItem.Name = "会员管理ToolStripMenuItem";
            this.会员管理ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.会员管理ToolStripMenuItem.Text = "会员管理";
            this.会员管理ToolStripMenuItem.Click += new System.EventHandler(this.会员管理ToolStripMenuItem_Click);
            // 
            // 优惠券管理ToolStripMenuItem
            // 
            this.优惠券管理ToolStripMenuItem.Name = "优惠券管理ToolStripMenuItem";
            this.优惠券管理ToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.优惠券管理ToolStripMenuItem.Text = "优惠券管理";
            this.优惠券管理ToolStripMenuItem.Click += new System.EventHandler(this.优惠券管理ToolStripMenuItem_Click);
            // 
            // BackEnd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(711, 406);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BackEnd";
            this.Text = "Back_end";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.BackEnd_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 商品管理ToolStripMenuItem;
        private ToolStripMenuItem 会员管理ToolStripMenuItem;
        private ToolStripMenuItem 优惠券管理ToolStripMenuItem;
    }
}