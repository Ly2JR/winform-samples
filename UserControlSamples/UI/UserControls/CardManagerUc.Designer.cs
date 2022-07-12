namespace UserControlSamples.UI.UserControls
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.plMenuContainer = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tlBtnBatchSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlBtnClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlsBtnExpandDown = new System.Windows.Forms.ToolStripButton();
            this.tlsBtnExpandUp = new System.Windows.Forms.ToolStripButton();
            this.lblSp = new System.Windows.Forms.Label();
            this.plContainer = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            this.plMenuContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.plContainer);
            this.groupBox1.Controls.Add(this.lblSp);
            this.groupBox1.Controls.Add(this.plMenuContainer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(4, 4);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(474, 286);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // plMenuContainer
            // 
            this.plMenuContainer.Controls.Add(this.toolStrip1);
            this.plMenuContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.plMenuContainer.Location = new System.Drawing.Point(3, 20);
            this.plMenuContainer.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plMenuContainer.Name = "plMenuContainer";
            this.plMenuContainer.Size = new System.Drawing.Size(468, 43);
            this.plMenuContainer.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnAdd,
            this.tlBtnBatchSave,
            this.toolStripSeparator1,
            this.tlBtnClear,
            this.toolStripSeparator2,
            this.tlsBtnExpandDown,
            this.tlsBtnExpandUp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(468, 43);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdd.Image")));
            this.tsbtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.Size = new System.Drawing.Size(72, 40);
            this.tsbtnAdd.Text = "添加";
            this.tsbtnAdd.Click += new System.EventHandler(this.tsbtnAdd_Click);
            // 
            // tlBtnBatchSave
            // 
            this.tlBtnBatchSave.Image = ((System.Drawing.Image)(resources.GetObject("tlBtnBatchSave.Image")));
            this.tlBtnBatchSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlBtnBatchSave.Name = "tlBtnBatchSave";
            this.tlBtnBatchSave.Size = new System.Drawing.Size(72, 40);
            this.tlBtnBatchSave.Text = "保存";
            this.tlBtnBatchSave.Click += new System.EventHandler(this.tlBtnBatchSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 43);
            // 
            // tlBtnClear
            // 
            this.tlBtnClear.Image = ((System.Drawing.Image)(resources.GetObject("tlBtnClear.Image")));
            this.tlBtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlBtnClear.Name = "tlBtnClear";
            this.tlBtnClear.Size = new System.Drawing.Size(72, 40);
            this.tlBtnClear.Text = "清空";
            this.tlBtnClear.Click += new System.EventHandler(this.tlBtnClear_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 43);
            // 
            // tlsBtnExpandDown
            // 
            this.tlsBtnExpandDown.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnExpandDown.Image")));
            this.tlsBtnExpandDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnExpandDown.Name = "tlsBtnExpandDown";
            this.tlsBtnExpandDown.Size = new System.Drawing.Size(72, 40);
            this.tlsBtnExpandDown.Text = "展开";
            this.tlsBtnExpandDown.Click += new System.EventHandler(this.tlsBtnExpandDown_Click);
            // 
            // tlsBtnExpandUp
            // 
            this.tlsBtnExpandUp.Image = ((System.Drawing.Image)(resources.GetObject("tlsBtnExpandUp.Image")));
            this.tlsBtnExpandUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlsBtnExpandUp.Name = "tlsBtnExpandUp";
            this.tlsBtnExpandUp.Size = new System.Drawing.Size(72, 40);
            this.tlsBtnExpandUp.Text = "折叠";
            this.tlsBtnExpandUp.Click += new System.EventHandler(this.tlsBtnExpandUp_Click);
            // 
            // lblSp
            // 
            this.lblSp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSp.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSp.Location = new System.Drawing.Point(3, 63);
            this.lblSp.Name = "lblSp";
            this.lblSp.Size = new System.Drawing.Size(468, 2);
            this.lblSp.TabIndex = 1;
            // 
            // plContainer
            // 
            this.plContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plContainer.Location = new System.Drawing.Point(3, 65);
            this.plContainer.Name = "plContainer";
            this.plContainer.Size = new System.Drawing.Size(468, 219);
            this.plContainer.TabIndex = 2;
            // 
            // CardManagerUc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "CardManagerUc";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(482, 294);
            this.groupBox1.ResumeLayout(false);
            this.plMenuContainer.ResumeLayout(false);
            this.plMenuContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel plMenuContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbtnAdd;
        private System.Windows.Forms.ToolStripButton tlBtnBatchSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tlBtnClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tlsBtnExpandDown;
        private System.Windows.Forms.ToolStripButton tlsBtnExpandUp;
        private System.Windows.Forms.Panel plContainer;
        private System.Windows.Forms.Label lblSp;
    }
}
