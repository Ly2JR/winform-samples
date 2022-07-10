namespace UserControlSamples.UI.UserControls
{
    partial class BaseCardUc
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseCardUc));
            this.panel1 = new System.Windows.Forms.Panel();
            this.plContainer = new System.Windows.Forms.Panel();
            this.lblSp2 = new System.Windows.Forms.Label();
            this.plLogo = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.lblSp1 = new System.Windows.Forms.Label();
            this.plTitle = new System.Windows.Forms.Panel();
            this.picClose = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.plLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.plTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.plContainer);
            this.panel1.Controls.Add(this.lblSp2);
            this.panel1.Controls.Add(this.plLogo);
            this.panel1.Controls.Add(this.lblSp1);
            this.panel1.Controls.Add(this.plTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(173, 227);
            this.panel1.TabIndex = 0;
            // 
            // plContainer
            // 
            this.plContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContainer.Location = new System.Drawing.Point(0, 163);
            this.plContainer.Name = "plContainer";
            this.plContainer.Size = new System.Drawing.Size(171, 62);
            this.plContainer.TabIndex = 5;
            // 
            // lblSp2
            // 
            this.lblSp2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSp2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSp2.Location = new System.Drawing.Point(0, 161);
            this.lblSp2.Name = "lblSp2";
            this.lblSp2.Size = new System.Drawing.Size(171, 2);
            this.lblSp2.TabIndex = 4;
            // 
            // plLogo
            // 
            this.plLogo.Controls.Add(this.picLogo);
            this.plLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.plLogo.Location = new System.Drawing.Point(0, 47);
            this.plLogo.Name = "plLogo";
            this.plLogo.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.plLogo.Size = new System.Drawing.Size(171, 114);
            this.plLogo.TabIndex = 3;
            // 
            // picLogo
            // 
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Location = new System.Drawing.Point(25, 15);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(121, 84);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 1;
            this.picLogo.TabStop = false;
            // 
            // lblSp1
            // 
            this.lblSp1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSp1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSp1.Location = new System.Drawing.Point(0, 45);
            this.lblSp1.Margin = new System.Windows.Forms.Padding(0);
            this.lblSp1.Name = "lblSp1";
            this.lblSp1.Size = new System.Drawing.Size(171, 2);
            this.lblSp1.TabIndex = 2;
            // 
            // plTitle
            // 
            this.plTitle.Controls.Add(this.picClose);
            this.plTitle.Controls.Add(this.lblTitle);
            this.plTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.plTitle.Location = new System.Drawing.Point(0, 0);
            this.plTitle.Name = "plTitle";
            this.plTitle.Size = new System.Drawing.Size(171, 45);
            this.plTitle.TabIndex = 1;
            // 
            // picClose
            // 
            this.picClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picClose.Image = ((System.Drawing.Image)(resources.GetObject("picClose.Image")));
            this.picClose.Location = new System.Drawing.Point(144, 6);
            this.picClose.Name = "picClose";
            this.picClose.Size = new System.Drawing.Size(24, 24);
            this.picClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picClose.TabIndex = 18;
            this.picClose.TabStop = false;
            this.picClose.Click += new System.EventHandler(this.picClose_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 45);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "标题";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDelete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 36);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDelete.Image")));
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(124, 32);
            this.tsmiDelete.Text = "删除";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // BaseCardUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "BaseCardUc";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Size = new System.Drawing.Size(179, 233);
            this.panel1.ResumeLayout(false);
            this.plLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.plTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picClose)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        protected System.Windows.Forms.Panel plContainer;
        private System.Windows.Forms.Label lblSp2;
        protected System.Windows.Forms.Panel plLogo;
        protected internal System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblSp1;
        private System.Windows.Forms.Panel plTitle;
        private System.Windows.Forms.PictureBox picClose;
        protected internal System.Windows.Forms.Label lblTitle;
    }
}
