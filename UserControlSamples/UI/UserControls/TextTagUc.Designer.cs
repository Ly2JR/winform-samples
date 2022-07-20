namespace UserControlSamples.UI.UserControls
{
    partial class TextTagUc
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
            this.txtTag = new System.Windows.Forms.TextBox();
            this.plContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.SuspendLayout();
            // 
            // plContainer
            // 
            this.plContainer.Controls.Add(this.txtTag);
            this.plContainer.Controls.SetChildIndex(this.picClose, 0);
            this.plContainer.Controls.SetChildIndex(this.txtTag, 0);
            // 
            // txtTag
            // 
            this.txtTag.Cursor = System.Windows.Forms.Cursors.Hand;
            this.txtTag.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTag.Location = new System.Drawing.Point(0, 0);
            this.txtTag.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.txtTag.MaxLength = 12;
            this.txtTag.Name = "txtTag";
            this.txtTag.Size = new System.Drawing.Size(107, 29);
            this.txtTag.TabIndex = 0;
            this.txtTag.DoubleClick += new System.EventHandler(this.txtTag_DoubleClick);
            this.txtTag.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTag_KeyPress);
            // 
            // TextTagUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.Name = "TextTagUc";
            this.OnCloseTag += new UserControlSamples.UI.UserControls.BaseTagUc.CloseTagHandler(this.TextTagUc_OnCloseTag);
            this.plContainer.ResumeLayout(false);
            this.plContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtTag;
    }
}
