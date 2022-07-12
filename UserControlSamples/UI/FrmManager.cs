using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserControlSamples.UI
{
    public partial class FrmManager : Form
    {
        public FrmManager()
        {
            InitializeComponent();
        }
        FormWindowState tempWindowState;
        private void FrmManager_Shown(object sender, EventArgs e)
        {
            //从数据库加载控件
            //cardManagerUc1.LoadCard();
            //cardManagerUc2.LoadCard();
            //tagManagerUc1.LoadTag();
        }

        private void FrmManager_SizeChanged(object sender, EventArgs e)
        {
            if (tempWindowState != FormWindowState.Maximized && this.WindowState == FormWindowState.Maximized)
            {
                tempWindowState = FormWindowState.Maximized;
                cardManagerUc1.RefreshLayout();
                //tagManagerUc1.RefreshLayout();
            }
            else if (tempWindowState == FormWindowState.Maximized && this.WindowState == FormWindowState.Normal)
            {
                tempWindowState = FormWindowState.Normal;
                cardManagerUc1.RefreshLayout();
                //tagManagerUc1.RefreshLayout();
            }
        }
    }
}
