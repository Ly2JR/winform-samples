namespace UserControlSamples.UI
{
    partial class FrmCardManager
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
            UserControlSamples.Models.BaseExpand baseExpand3 = new UserControlSamples.Models.BaseExpand();
            UserControlSamples.Models.BaseExpand baseExpand4 = new UserControlSamples.Models.BaseExpand();
            this.cardManagerUc2 = new UserControlSamples.UI.UserControls.CardManagerUc();
            this.cardManagerUc1 = new UserControlSamples.UI.UserControls.CardManagerUc();
            this.tagManagerUc1 = new UserControlSamples.UI.UserControls.TagManagerUc();
            this.SuspendLayout();
            // 
            // cardManagerUc2
            // 
            this.cardManagerUc2.CurrentCardEnum = UserControlSamples.Models.CardEnum.Card2;
            this.cardManagerUc2.Dock = System.Windows.Forms.DockStyle.Top;
            baseExpand3.Expand = false;
            baseExpand3.ExpandHeight = 0;
            baseExpand3.OrginHeight = 74;
            this.cardManagerUc2.Extra = baseExpand3;
            this.cardManagerUc2.Location = new System.Drawing.Point(0, 74);
            this.cardManagerUc2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cardManagerUc2.Name = "cardManagerUc2";
            this.cardManagerUc2.Padding = new System.Windows.Forms.Padding(4);
            this.cardManagerUc2.Size = new System.Drawing.Size(800, 74);
            this.cardManagerUc2.TabIndex = 1;
            // 
            // cardManagerUc1
            // 
            this.cardManagerUc1.CurrentCardEnum = UserControlSamples.Models.CardEnum.Card1;
            this.cardManagerUc1.Dock = System.Windows.Forms.DockStyle.Top;
            baseExpand4.Expand = false;
            baseExpand4.ExpandHeight = 0;
            baseExpand4.OrginHeight = 74;
            this.cardManagerUc1.Extra = baseExpand4;
            this.cardManagerUc1.Location = new System.Drawing.Point(0, 0);
            this.cardManagerUc1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cardManagerUc1.Name = "cardManagerUc1";
            this.cardManagerUc1.Padding = new System.Windows.Forms.Padding(4);
            this.cardManagerUc1.Size = new System.Drawing.Size(800, 74);
            this.cardManagerUc1.TabIndex = 0;
            // 
            // tagManagerUc1
            // 
            this.tagManagerUc1.CurrentTableEnum = UserControlSamples.Models.TagEnum.LabelTag;
            this.tagManagerUc1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tagManagerUc1.Extra = null;
            this.tagManagerUc1.Location = new System.Drawing.Point(0, 148);
            this.tagManagerUc1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tagManagerUc1.Name = "tagManagerUc1";
            this.tagManagerUc1.Size = new System.Drawing.Size(800, 145);
            this.tagManagerUc1.TabIndex = 2;
            // 
            // FrmCardManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tagManagerUc1);
            this.Controls.Add(this.cardManagerUc2);
            this.Controls.Add(this.cardManagerUc1);
            this.Name = "FrmCardManager";
            this.Text = "FrmCardManager";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControls.CardManagerUc cardManagerUc1;
        private UserControls.CardManagerUc cardManagerUc2;
        private UserControls.TagManagerUc tagManagerUc1;
    }
}