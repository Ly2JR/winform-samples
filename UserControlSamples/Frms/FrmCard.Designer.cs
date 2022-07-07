namespace UserControlSamples.Frms
{
    partial class FrmCard
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cardManagerUc1 = new UserControlSamples.UserControls.CardManagerUc();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cardManagerUc2 = new UserControlSamples.UserControls.CardManagerUc();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cardManagerUc1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(800, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "卡片1管理";
            // 
            // cardManagerUc1
            // 
            this.cardManagerUc1.CurrentCardEnum = UserControlSamples.Models.CardEnum.Card1;
            this.cardManagerUc1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardManagerUc1.Location = new System.Drawing.Point(3, 21);
            this.cardManagerUc1.Name = "cardManagerUc1";
            this.cardManagerUc1.Size = new System.Drawing.Size(794, 47);
            this.cardManagerUc1.TabIndex = 0;
            this.cardManagerUc1.OnCardManagerButtonClick += new UserControlSamples.UserControls.CardManagerUc.OnCardManagerButtonHandler(this.OnExpandGroupBoxClick);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cardManagerUc2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 71);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(800, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "卡片2管理";
            // 
            // cardManagerUc2
            // 
            this.cardManagerUc2.CurrentCardEnum = UserControlSamples.Models.CardEnum.Card2;
            this.cardManagerUc2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardManagerUc2.Location = new System.Drawing.Point(3, 21);
            this.cardManagerUc2.Name = "cardManagerUc2";
            this.cardManagerUc2.Size = new System.Drawing.Size(794, 47);
            this.cardManagerUc2.TabIndex = 0;
            this.cardManagerUc2.OnCardManagerButtonClick += new UserControlSamples.UserControls.CardManagerUc.OnCardManagerButtonHandler(this.OnExpandGroupBoxClick);
            // 
            // FrmCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FrmCard";
            this.Text = "卡片管理";
            this.Shown += new System.EventHandler(this.FrmCard_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private UserControls.CardManagerUc cardManagerUc1;
        private System.Windows.Forms.GroupBox groupBox2;
        private UserControls.CardManagerUc cardManagerUc2;
    }
}