namespace UserControlSamples.UserControls
{
    partial class Card1Uc
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Card1Uc));
            this.label1 = new System.Windows.Forms.Label();
            this.txtIO = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.plContainer.SuspendLayout();
            this.plImage.SuspendLayout();
            this.SuspendLayout();
            // 
            // picLogo
            // 
            this.picLogo.Image = ((System.Drawing.Image)(resources.GetObject("picLogo.Image")));
            this.picLogo.Location = new System.Drawing.Point(19, 15);
            this.picLogo.Size = new System.Drawing.Size(145, 97);
            // 
            // plContainer
            // 
            this.plContainer.Controls.Add(this.txtIO);
            this.plContainer.Controls.Add(this.label1);
            this.plContainer.Location = new System.Drawing.Point(0, 159);
            this.plContainer.Size = new System.Drawing.Size(184, 47);
            // 
            // plImage
            // 
            this.plImage.Size = new System.Drawing.Size(184, 127);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "I/O：";
            // 
            // txtIO
            // 
            this.txtIO.Location = new System.Drawing.Point(70, 8);
            this.txtIO.Name = "txtIO";
            this.txtIO.Size = new System.Drawing.Size(100, 25);
            this.txtIO.TabIndex = 1;
            // 
            // Card1Uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Card1Uc";
            this.Size = new System.Drawing.Size(192, 214);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.plContainer.ResumeLayout(false);
            this.plContainer.PerformLayout();
            this.plImage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIO;
    }
}
