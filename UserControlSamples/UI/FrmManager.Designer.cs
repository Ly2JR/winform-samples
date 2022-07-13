namespace UserControlSamples.UI
{
    partial class FrmManager
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
            this.cardManagerUc1 = new UserControlSamples.UI.UserControls.CardManagerUc();
            this.tagManagerUc1 = new UserControlSamples.UI.UserControls.TagManagerUc();
            this.SuspendLayout();
            // 
            // cardManagerUc1
            // 
            this.cardManagerUc1.CurrentCardEnum = UserControlSamples.Models.CardEnum.Card1;
            this.cardManagerUc1.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardManagerUc1.Location = new System.Drawing.Point(0, 0);
            this.cardManagerUc1.Name = "cardManagerUc1";
            this.cardManagerUc1.Padding = new System.Windows.Forms.Padding(4);
            this.cardManagerUc1.Size = new System.Drawing.Size(800, 76);
            this.cardManagerUc1.TabIndex = 0;
            // 
            // tagManagerUc1
            // 
            this.tagManagerUc1.CurrentTagEnum = UserControlSamples.Models.TagEnum.TextTag;
            this.tagManagerUc1.CurrentTagSourceEnum = UserControlSamples.Models.TagSourceEnum.FromText;
            this.tagManagerUc1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tagManagerUc1.Location = new System.Drawing.Point(0, 76);
            this.tagManagerUc1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagManagerUc1.Name = "tagManagerUc1";
            this.tagManagerUc1.Size = new System.Drawing.Size(800, 60);
            this.tagManagerUc1.TabIndex = 1;
            // 
            // FrmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tagManagerUc1);
            this.Controls.Add(this.cardManagerUc1);
            this.DoubleBuffered = true;
            this.Name = "FrmManager";
            this.Text = "FrmManager";
            this.Shown += new System.EventHandler(this.FrmManager_Shown);
            this.SizeChanged += new System.EventHandler(this.FrmManager_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.CardManagerUc cardManagerUc1;
        private UserControls.TagManagerUc tagManagerUc1;
    }
}