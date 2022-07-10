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
    public partial class FrmAdd : Form
    {
        public delegate void AddTagHandler(string tag);
        public event AddTagHandler OnAddTag;
        public FrmAdd()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            OnFireAddTag(txtName.Text);
            Close();
        }

        private void OnFireAddTag(string tag)
        {
            if (OnAddTag != null)
            {
                OnAddTag(tag);
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)13) return;
            btnOk_Click(null, null);
        }
    }
}
