using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;

namespace UserControlSamples.UI.UserControls
{
    public partial class TextTagUc : BaseTagUc
    {
        public TextTagUc()
        {
            InitializeComponent();
        }

        private int _maxTextLength = TextTagConsts.DefaultMaxTextLength;

        [Category(TextTagConsts.Name), Description(TextTagConsts.MaxTextLengthProperty), DefaultValue(TextTagConsts.DefaultMaxTextLength)]
        public int MaxTextLength
        {
            get { return _maxTextLength; }
            set
            {
                _maxTextLength = value;
                txtTag.MaxLength = _maxTextLength;
            }
        }

        public TextTagUc(ProjectSetKey key, string tag = null) : base(key, tag)
        {
            InitializeComponent();
            txtTag.SendToBack();
            if (!string.IsNullOrEmpty(tag))
            {
                this.txtTag.Text = tag;
                txtTag.ReadOnly = true;
                TagChanged(tag);
            }
            HiddenClose = string.IsNullOrEmpty(tag);
        }

        private void TextTagUc_OnCloseTag(BaseTagUc obj, BaseTag tag)
        {
            var dialog = MessageBox.Show($"确定删除标签[{tag.Tag}]?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;
            tag.Continute = true;
            Clear();
        }

        private void txtTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case TextTagConsts.InputEnterKey:
                    TagChanged(txtTag.Text);
                    SaveData(txtTag.Text);//直接保存到数据库
                    break;
                case TextTagConsts.InputEscKey:
                    break;
                default: return;
            }
            this.txtTag.ReadOnly = true;
            this.txtTag.SelectionStart = 0;
            this.txtTag.Select(this.txtTag.SelectionStart, 0);
            Extra.DataMode = 1;
            this.HiddenClose = false;
        }

        private void txtTag_DoubleClick(object sender, EventArgs e)
        {
            if (Extra.DataMode == 2) return;
            txtTag.ReadOnly = false;
            txtTag.SelectAll();
            txtTag.Focus();
            Extra.DataMode = 2;
            HiddenClose = true;
        }


        protected override void Execute(string sSql)
        {
            //Sqlite.Execute(sql)
        }
    }
}
