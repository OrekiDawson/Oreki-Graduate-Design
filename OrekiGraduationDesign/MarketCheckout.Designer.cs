namespace OrekiGraduationDesign
{
    partial class MarketCheckout
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.labelTotal = new System.Windows.Forms.Label();
            this.labelCoupon = new System.Windows.Forms.Label();
            this.labelPrice = new System.Windows.Forms.Label();
            this.labelCash = new System.Windows.Forms.Label();
            this.buttonCheckOut = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelRefund = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.labelMember = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTotal
            // 
            this.labelTotal.AutoSize = true;
            this.labelTotal.Location = new System.Drawing.Point(56, 57);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(89, 13);
            this.labelTotal.TabIndex = 0;
            this.labelTotal.Text = "总价：labelTotal";
            // 
            // labelCoupon
            // 
            this.labelCoupon.AutoSize = true;
            this.labelCoupon.Location = new System.Drawing.Point(56, 113);
            this.labelCoupon.Name = "labelCoupon";
            this.labelCoupon.Size = new System.Drawing.Size(114, 13);
            this.labelCoupon.TabIndex = 2;
            this.labelCoupon.Text = "优惠券：labelCoupon";
            // 
            // labelPrice
            // 
            this.labelPrice.AutoSize = true;
            this.labelPrice.Location = new System.Drawing.Point(56, 165);
            this.labelPrice.Name = "labelPrice";
            this.labelPrice.Size = new System.Drawing.Size(89, 13);
            this.labelPrice.TabIndex = 3;
            this.labelPrice.Text = "应收：labelPrice";
            // 
            // labelCash
            // 
            this.labelCash.AutoSize = true;
            this.labelCash.Location = new System.Drawing.Point(56, 254);
            this.labelCash.Name = "labelCash";
            this.labelCash.Size = new System.Drawing.Size(43, 13);
            this.labelCash.TabIndex = 4;
            this.labelCash.Text = "现金：";
            // 
            // buttonCheckOut
            // 
            this.buttonCheckOut.Enabled = false;
            this.buttonCheckOut.Location = new System.Drawing.Point(56, 293);
            this.buttonCheckOut.Name = "buttonCheckOut";
            this.buttonCheckOut.Size = new System.Drawing.Size(75, 23);
            this.buttonCheckOut.TabIndex = 1;
            this.buttonCheckOut.Text = "结账";
            this.buttonCheckOut.UseVisualStyleBackColor = true;
            this.buttonCheckOut.Click += new System.EventHandler(this.ButtonCheckOut_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Location = new System.Drawing.Point(215, 293);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // labelRefund
            // 
            this.labelRefund.AutoSize = true;
            this.labelRefund.Location = new System.Drawing.Point(56, 344);
            this.labelRefund.Name = "labelRefund";
            this.labelRefund.Size = new System.Drawing.Size(100, 13);
            this.labelRefund.TabIndex = 7;
            this.labelRefund.Text = "找零：labelRefund";
            this.labelRefund.Visible = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(106, 251);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(184, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBox1_KeyPress);
            // 
            // labelMember
            // 
            this.labelMember.AutoSize = true;
            this.labelMember.Location = new System.Drawing.Point(56, 213);
            this.labelMember.Name = "labelMember";
            this.labelMember.Size = new System.Drawing.Size(139, 13);
            this.labelMember.TabIndex = 9;
            this.labelMember.Text = "会员卡扣款：labelMember";
            // 
            // MarketCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(397, 406);
            this.Controls.Add(this.labelMember);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.labelRefund);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonCheckOut);
            this.Controls.Add(this.labelCash);
            this.Controls.Add(this.labelPrice);
            this.Controls.Add(this.labelCoupon);
            this.Controls.Add(this.labelTotal);
            this.Name = "MarketCheckout";
            this.Text = "结账";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTotal;
        private System.Windows.Forms.Label labelCoupon;
        private System.Windows.Forms.Label labelPrice;
        private System.Windows.Forms.Label labelCash;
        private System.Windows.Forms.Button buttonCheckOut;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelRefund;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label labelMember;
    }
}