using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserControlSamples.Models;

namespace UserControlSamples.UI.UserControls
{
    public partial class BaseTagUc : UserControl
    {
        public BaseTagUc()
        {
            InitializeComponent();

            CurrentTag = new BaseTag();
        }

        public BaseTag CurrentTag { get; private set; }

        public BaseTagUc(ProjectSetKey key, string tag) : this()
        {
            CurrentTag.Key = key;
            CurrentTag.Tag = tag;
            lblTag.Text = tag;
        }
    }
}
