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
    public partial class TagManagerUc : UserControl
    {
        public delegate void OnTagChangedHandler(TagManagerUc obj, BaseTagUc uc);
        public delegate void OnTagManagerButtonHandler(TagManagerUc obj, string buttonKey);

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.AddNewTagEvent)]
        public event OnTagChangedHandler OnAddNewTag;

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.ButtonEvent)]
        public event OnTagManagerButtonHandler OnTagManagerButtonClick;

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.RemoveTagEvent)]
        public event OnTagChangedHandler OnRemoveTag;

        public TagManagerUc()
        {
            InitializeComponent();
            _tagDics = new Dictionary<ProjectSetKey, BaseTagUc>();
        }

        public BaseExpand Extra { get; set; }

        private IDictionary<ProjectSetKey, BaseTagUc> _tagDics;

        public int Count { get { return _tagDics.Count; } }

        private int _maxTagCount = TagManagerConsts.DefaultMaxTagCount;

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.AllowedMaxTagCountProperty), DefaultValue(TagManagerConsts.DefaultMaxTagCount)]
        public int MaxTagCount
        {
            get { return _maxTagCount; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("不允许小于等于0");
                _maxTagCount = value;
            }
        }


        private TagEnum _currentTable = TagManagerConsts.DefaultTagEnum;

        public TagEnum CurrentTableEnum
        {
            get { return _currentTable; }
            set
            {
                _currentTable = value;
                groupBox1.Text = DisplayName();
            }
        }

        private string DisplayName()
        {
            switch (CurrentTableEnum)
            {
                case TagEnum.LabelTag: return LabelTagConsts.DisplayName;
            }
            return "";
        }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            var add = new FrmAdd();
            add.OnAddTag += CreateCard;
            add.ShowDialog();
        }

        /// <summary>
        /// 添加新的卡片
        /// </summary>
        /// <param name="tag">卡内容</param>
        private void CreateCard(string tag)
        {
            var count = _tagDics.Count;
            if (count == _maxTagCount)
            {
                MessageBox.Show($"超过最大添加数量:{_maxTagCount}个", "提示");
                return;
            }
            var sn = GetMaxId(_tagDics);
            var key = new ProjectSetKey(DisplayName(), sn);
            var addNew = new BaseTagUc(key, tag);
            AppendContainer(addNew);
        }

        private int GetMaxId(IDictionary<ProjectSetKey, BaseTagUc> items)
        {
            if (items.Count == 0) return 1;
            return items.Count + 1;
        }

        private void AppendContainer(BaseTagUc newTag)
        {
            newTag.Parent = plContainer;
            if (_tagDics.ContainsKey(newTag.CurrentTag.Key))
            {
                MessageBox.Show($"编号{newTag.CurrentTag.Key.Sn}重复", "提示");
                return;
            }
            var x = _tagDics.Count * (newTag.Width + TagConsts.DefaultPaddingLeft);
            newTag.Location = new Point(x, TagConsts.DefaultPaddingTopBottom);

            _tagDics.Add(newTag.CurrentTag.Key, newTag);
            plContainer.Controls.Add(newTag);
            OnFireAddNewCard(newTag);
        }


        private void OnFireRemoveTag(BaseTagUc removeTag)
        {
            if (OnRemoveTag != null)
            {
                OnRemoveTag(this, removeTag);
            }
        }

        private void OnFireAddNewCard(BaseTagUc newTag)
        {
            if (OnAddNewTag != null)
            {
                OnAddNewTag(this, newTag);
            }
        }



        private void tlBtnClear_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show($"确定清空所有数据吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;

            _tagDics.Clear();
            plContainer.Controls.Clear();
        }
    }
}
