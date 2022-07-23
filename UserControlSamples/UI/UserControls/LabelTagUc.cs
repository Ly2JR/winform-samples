using System.Windows.Forms;
using UserControlSamples.Models;

namespace UserControlSamples.UI.UserControls
{
    public partial class LabelTagUc : BaseTagUc
    {
        public LabelTagUc()
        {
            InitializeComponent();
        }

        public LabelTagUc(ProjectSetKey key, string tag = null) : base(key, tag)
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(tag))
            {
                this.lblTag.SendToBack();
                this.lblTag.Text = tag;
                TagChanged(tag);
            }
        }

        private void LabelTagUc_OnCloseTag(BaseTagUc obj)
        {
            var dialog = MessageBox.Show($"确定删除标签[{obj.Extra.Tag}]?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;
            obj.Extra.Continute = true;
            Clear();
        }
    }
}
