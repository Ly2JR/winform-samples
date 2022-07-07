namespace UserControlSamples.UserControls
{
    partial class CardManagerUc
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CardManagerUc));
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsBtnBatchSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnExpandDown = new System.Windows.Forms.ToolStripButton();
            this.tsBtnExpandUp = new System.Windows.Forms.ToolStripButton();
            this.plContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1044, 44);
            this.panel1.TabIndex = 5;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBtnAdd,
            this.tsBtnBatchSave,
            this.toolStripSeparator1,
            this.tsBtnClear,
            this.toolStripSeparator2,
            this.tsBtnExpandDown,
            this.tsBtnExpandUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1044, 31);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsBtnAdd
            // 
            this.tsBtnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnAdd.Image")));
            this.tsBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnAdd.Name = "tsBtnAdd";
            this.tsBtnAdd.Size = new System.Drawing.Size(67, 28);
            this.tsBtnAdd.Text = "添加";
            this.tsBtnAdd.Click += new System.EventHandler(this.tsBtnAdd_Click);
            // 
            // tsBtnBatchSave
            // 
            this.tsBtnBatchSave.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnBatchSave.Image")));
            this.tsBtnBatchSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnBatchSave.Name = "tsBtnBatchSave";
            this.tsBtnBatchSave.Size = new System.Drawing.Size(67, 28);
            this.tsBtnBatchSave.Text = "保存";
            this.tsBtnBatchSave.Click += new System.EventHandler(this.tsBtnBatchSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnClear
            // 
            this.tsBtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnClear.Image")));
            this.tsBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnClear.Name = "tsBtnClear";
            this.tsBtnClear.Size = new System.Drawing.Size(67, 28);
            this.tsBtnClear.Text = "清空";
            this.tsBtnClear.Click += new System.EventHandler(this.tsBtnClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsBtnExpandDown
            // 
            this.tsBtnExpandDown.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExpandDown.Image")));
            this.tsBtnExpandDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExpandDown.Name = "tsBtnExpandDown";
            this.tsBtnExpandDown.Size = new System.Drawing.Size(67, 28);
            this.tsBtnExpandDown.Text = "展开";
            this.tsBtnExpandDown.Click += new System.EventHandler(this.tsBtnExpandDown_Click);
            // 
            // tsBtnExpandUp
            // 
            this.tsBtnExpandUp.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnExpandUp.Image")));
            this.tsBtnExpandUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnExpandUp.Name = "tsBtnExpandUp";
            this.tsBtnExpandUp.Size = new System.Drawing.Size(67, 28);
            this.tsBtnExpandUp.Text = "折叠";
            this.tsBtnExpandUp.Click += new System.EventHandler(this.tsBtnExpandUp_Click);
            // 
            // plContainer
            // 
            this.plContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContainer.Location = new System.Drawing.Point(0, 44);
            this.plContainer.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.plContainer.Name = "plContainer";
            this.plContainer.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.plContainer.Size = new System.Drawing.Size(1044, 2);
            this.plContainer.TabIndex = 7;
            // 
            // CardManagerUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plContainer);
            this.Controls.Add(this.panel1);
            this.Name = "CardManagerUc";
            this.Size = new System.Drawing.Size(1044, 46);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsBtnAdd;
        private System.Windows.Forms.ToolStripButton tsBtnBatchSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsBtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsBtnExpandDown;
        private System.Windows.Forms.ToolStripButton tsBtnExpandUp;
        protected System.Windows.Forms.Panel plContainer;
    }
}
