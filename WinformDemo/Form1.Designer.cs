namespace WinformDemo
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cardManagerUc1 = new UserControlSamples.UI.UserControls.CardManagerUc();
            this.tagManagerUc1 = new UserControlSamples.UI.UserControls.TagManagerUc();
            this.rowMergeView1 = new UserControlSamples.UI.UserControls.RowMergeView(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 450);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cardManagerUc1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 421);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "卡片";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tagManagerUc1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(792, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "标签";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rowMergeView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(792, 421);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "DataGridView";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cardManagerUc1
            // 
            this.cardManagerUc1.CurrentCardEnum = UserControlSamples.Models.CardEnum.Card1;
            this.cardManagerUc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardManagerUc1.Location = new System.Drawing.Point(3, 3);
            this.cardManagerUc1.Name = "cardManagerUc1";
            this.cardManagerUc1.Padding = new System.Windows.Forms.Padding(4);
            this.cardManagerUc1.Size = new System.Drawing.Size(786, 415);
            this.cardManagerUc1.TabIndex = 0;
            // 
            // tagManagerUc1
            // 
            this.tagManagerUc1.CurrentTagEnum = UserControlSamples.Models.TagEnum.TextTag;
            this.tagManagerUc1.CurrentTagSourceEnum = UserControlSamples.Models.TagSourceEnum.FromText;
            this.tagManagerUc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagManagerUc1.Location = new System.Drawing.Point(3, 3);
            this.tagManagerUc1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagManagerUc1.Name = "tagManagerUc1";
            this.tagManagerUc1.Size = new System.Drawing.Size(786, 415);
            this.tagManagerUc1.TabIndex = 0;
            // 
            // rowMergeView1
            // 
            this.rowMergeView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.rowMergeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rowMergeView1.Location = new System.Drawing.Point(3, 3);
            this.rowMergeView1.MergeColumnBackColor = System.Drawing.SystemColors.Control;
            this.rowMergeView1.Name = "rowMergeView1";
            this.rowMergeView1.RowHeadersWidth = 51;
            this.rowMergeView1.RowTemplate.Height = 27;
            this.rowMergeView1.Size = new System.Drawing.Size(786, 415);
            this.rowMergeView1.TabIndex = 0;
            this.rowMergeView1.OnRowButton += new UserControlSamples.UI.UserControls.RowMergeView.OnRowButtonHandler(this.rowMergeView1_OnMultiButton);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.rowMergeView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private UserControlSamples.UI.UserControls.CardManagerUc cardManagerUc1;
        private System.Windows.Forms.TabPage tabPage2;
        private UserControlSamples.UI.UserControls.TagManagerUc tagManagerUc1;
        private System.Windows.Forms.TabPage tabPage3;
        private UserControlSamples.UI.UserControls.RowMergeView rowMergeView1;
    }
}

