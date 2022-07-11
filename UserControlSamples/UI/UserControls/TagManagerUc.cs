﻿using System;
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
        public event OnTagManagerButtonHandler OnTagManagerButton;

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.RemoveTagEvent)]
        public event OnTagChangedHandler OnRemoveTag;

        public TagManagerUc()
        {
            InitializeComponent();
            _tagDics = new Dictionary<ProjectSetKey, BaseTagUc>();
        }

        private IDictionary<ProjectSetKey, BaseTagUc> _tagDics;

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.CountProperty)]
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


        private TagSourceEnum _currentTagSource = TagManagerConsts.DefaultTagSourceEnum;
        private TagEnum _currentTagType = TagManagerConsts.DefaultTagEnum;

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.TagSrouceEnumProperty), DefaultValue(TagManagerConsts.DefaultTagSourceEnum)]
        public TagSourceEnum CurrentTagSourceEnum
        {
            get { return _currentTagSource; }
            set
            {
                _currentTagSource = value;
                groupBox1.Text = DisplayName();
            }
        }

        [Category(TagManagerConsts.Name), Description(TagManagerConsts.TagEnumProperty), DefaultValue(TagManagerConsts.DefaultTagEnum)]
        public TagEnum CurrentTagEnum
        {
            get { return _currentTagType; }
            set
            {
                _currentTagType = value;
            }
        }

        private string DisplayName()
        {
            switch (CurrentTagSourceEnum)
            {
                case TagSourceEnum.FromLabel: return TagManagerConsts.LabelDisplayName;
                case TagSourceEnum.FromText:return TagManagerConsts.TextDisplayName;
            }
            return "";
        }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            AddTag();
            OnFireManagerButton(TagManagerConsts.AddButtonKey);
        }

        private void AddTag()
        {
            switch (CurrentTagEnum)
            {
                case TagEnum.LabelTag:
                    var add = new FrmAdd();
                    add.OnAddTag += CreateCard;
                    add.ShowDialog();
                    break;
                case TagEnum.TextTag:
                    CreateCard();
                    break;
            }
        }

        /// <summary>
        /// 添加新的卡片
        /// </summary>
        /// <param name="tag">卡内容</param>
        private void CreateCard(string tag = null)
        {
            var count = _tagDics.Count;
            if (count == _maxTagCount)
            {
                MessageBox.Show($"超过最大添加数量:{_maxTagCount}个", "提示");
                return;
            }
            if (CurrentTagEnum == TagEnum.TextTag)
            {
                if (HasEditTag()) return;
            }
            var sn = GetMaxId(_tagDics);
            var addNew = GetTagUc(sn, tag);
            if (addNew == null) return;
            AppendContainer(addNew);
        }

        private bool HasEditTag()
        {
            return _tagDics.Values.Cast<TextTagUc>().Any(o => o.Extra.DataMode == 2);
        }

        private BaseTagUc GetTagUc(int sn, string tag = null)
        {
            BaseTagUc addNew = null;
            var key = new ProjectSetKey(DisplayName(), sn);
            switch (CurrentTagEnum)
            {
                case TagEnum.LabelTag:
                    addNew = new LabelTagUc(key, tag);
                    break;
                case TagEnum.TextTag:
                    addNew = new TextTagUc(key, tag);
                    break;
            }
            return addNew;
        }

        private int GetMaxId(IDictionary<ProjectSetKey, BaseTagUc> items)
        {
            if (items.Count == 0) return 1;
            return items.Keys.Max(o => o.Sn) + 1;
        }

        private void AppendContainer(BaseTagUc newTag)
        {
            newTag.Parent = plContainer;
            if (_tagDics.ContainsKey(newTag.CurrentTag.Key))
            {
                MessageBox.Show($"编号{newTag.CurrentTag.Key.Sn}重复", "提示");
                return;
            }
            newTag.OnCloseTag += (obj, currentTag) =>
            {
                if (!currentTag.Continute) return;
                var ret = _tagDics.ContainsKey(currentTag.Key);
                if (!ret) return;
                var uc = _tagDics[currentTag.Key];
                ret = _tagDics.Remove(currentTag.Key);
                if (ret)
                {
                    ResizeContainer(plContainer, uc);
                }
                OnFireRemoveTag(uc);
            };
            newTag.OnTagWidthChanged += (obj) => {
                if (Count <= 1) return;
                if (obj.Extra.NewWidth == obj.Extra.OldWidth) return;
                ResizeContainer(plContainer, obj, false);
            };
            var x = GetNextTagLocationX(newTag);
            newTag.Location = new Point(x, TagConsts.DefaultPaddingTopBottom);

            _tagDics.Add(newTag.CurrentTag.Key, newTag);
            plContainer.Controls.Add(newTag);
            OnFireAddNewCard(newTag);
            if (CurrentTagEnum == TagEnum.TextTag)
            {
                newTag.Focus();
            };
        }

        private int GetNextTagLocationX(BaseTagUc newTag)
        {
            return _tagDics.Count * TagConsts.DefaultPaddingLeft + _tagDics.Values.Sum(o => o.Extra.Width);
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

        private void OnFireManagerButton(string buttonKey)
        {
            if (OnTagManagerButton != null)
            {
                OnTagManagerButton(this, buttonKey);
            }
        }

        private void ResizeContainer(Panel container, BaseTagUc deleteUc, bool needDelete = true)
        {
            var flag = false;
            foreach (BaseTagUc item in container.Controls)
            {
                if (item.CurrentTag == deleteUc.CurrentTag)
                {
                    flag = true;
                    continue;
                }
                if (flag)
                {
                    if (needDelete)
                    {
                        item.Left -= deleteUc.Width + TagConsts.DefaultPaddingLeft;
                    }
                    else
                    {
                        item.Left += (deleteUc.Extra.NewWidth - deleteUc.Extra.OldWidth);
                    }
                }
            }
            if (needDelete)
            {
                container.Controls.Remove(deleteUc);
            }
        }

        private void BatchDeleteTran()
        {
            var sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION;\r");
            foreach (var uc in _tagDics.Values)
            {
                var deleteCmd = uc.DeleteCmdString();
                sb.Append(deleteCmd);
            }
            sb.Append("COMMIT;");
            var sSql = sb.ToString();
            //Sqlite.Execute(sSql);
        }

        private void tlBtnClear_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show($"确定清空所有数据吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;
            tlBtnClear.Enabled = false;
            try
            {
                BatchDeleteTran();
                ClearAll();
                MessageBox.Show("清空完成", DisplayName());
            }
            finally
            {
                tlBtnClear.Enabled = true;
            }
            OnFireManagerButton(TagManagerConsts.ClearButtonKey);
        }

        public DataTable GetTagData()
        {
            var type = DisplayName();
            var sSql = $"SELECT * FROM  {DataBaseConsts.TableName} WHERE {DataBaseConsts.TypeColumn}='{type}' ORDER BY {DataBaseConsts.SnColumn} ASC";
            return null;
            //return Sqlite.Execute(sSql)
        }

        public void ClearAll()
        {
            _tagDics.Clear();
            plContainer.Controls.Clear();
        }

        public void LoadTag()
        {
            ClearAll();

            var cards = GetTagData();
            if (cards != null && cards.Rows.Count > 0)
            {
                IDictionary<ProjectSetKey, string> newTags = new Dictionary<ProjectSetKey, string>();
                foreach (DataRow item in cards.Rows)
                {
                    var type = Convert.ToString(item[DataBaseConsts.TypeColumn]);
                    var sn = Convert.ToInt16(item[DataBaseConsts.SnColumn]);
                    var name = Convert.ToString(item[DataBaseConsts.NameColumn]);
                    //var value = Convert.ToString(item[DataBaseConsts.ValueColumn]);
                    var key = new ProjectSetKey(type, sn);
                    if (!newTags.ContainsKey(key))
                    {
                        newTags.Add(key, name);
                    }
                }

                foreach (var newTag in newTags)
                {
                    var addNewTag = GetTagUc(newTag.Key.Sn, newTag.Value);
                    AppendContainer(addNewTag);
                }
            }
        }
    }
}
