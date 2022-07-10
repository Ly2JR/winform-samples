namespace UserControlSamples.UI.UserControls
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
            this.plContainer.SuspendLayout();
            this.plLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // plContainer
            // 
            this.plContainer.Controls.Add(this.txtIO);
            this.plContainer.Controls.Add(this.label1);
            this.plContainer.Location = new System.Drawing.Point(0, 194);
            this.plContainer.Size = new System.Drawing.Size(197, 65);
            // 
            // plLogo
            // 
            this.plLogo.Location = new System.Drawing.Point(0, 40);
            this.plLogo.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.plLogo.Size = new System.Drawing.Size(197, 152);
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picLogo.BackgroundImage")));
            this.picLogo.Location = new System.Drawing.Point(20, 10);
            this.picLogo.Size = new System.Drawing.Size(157, 132);
            // 
            // lblTitle
            // 
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.lblTitle.Size = new System.Drawing.Size(197, 38);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "I/O：";
            // 
            // txtIO
            // 
            this.txtIO.Location = new System.Drawing.Point(55, 28);
            this.txtIO.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtIO.Name = "txtIO";
            this.txtIO.Size = new System.Drawing.Size(130, 25);
            this.txtIO.TabIndex = 1;
            // 
            // Card1Uc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Card1Uc";
            this.Size = new System.Drawing.Size(205, 265);
            this.OnRemoveCard += new UserControlSamples.UI.UserControls.BaseCardUc.RemoveCardHandler(this.SafetyDoorCardUc_OnRemoveCard);
            this.plContainer.ResumeLayout(false);
            this.plContainer.PerformLayout();
            this.plLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtIO;
        private System.Windows.Forms.Label label1;
    }
}
