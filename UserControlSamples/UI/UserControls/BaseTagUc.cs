using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;

namespace UserControlSamples.UI.UserControls
{
    public partial class BaseTagUc : UserControl
    {
        public BaseTagUc()
        {
            InitializeComponent();

            Key = new ProjectSetKey();
            Extra = new BaseTagExtend();
        }

        public delegate void TagWidthChangedHandler(BaseTagUc obj);

        [Category(TagConsts.Name), Description(TagConsts.TagWidthChangedEvent)]
        public event TagWidthChangedHandler OnTagWidthChanged;


        public delegate void CloseTagHandler(BaseTagUc obj);

        [Category(TagConsts.Name), Description(TagConsts.CloseTagEvent)]
        public event CloseTagHandler OnCloseTag;

        [Category(TagConsts.Name), Description(TagConsts.TagProperty)]
        public ProjectSetKey Key { get; private set; }

        private bool _hiddenClose = TagConsts.DefaultHiddenClose;

        protected void TagChanged(string tag)
        {
            this.Extra.OldWidth = Width;
            Extra.Tag = tag;
            //var calcWidth = GetStringLength(tag) + picClose.Width + 8;
            //if (calcWidth < this.Width)
            //{
            //    this.Width = calcWidth;
            //}
            //else
            //{
            //    this.Width = calcWidth + 16;
            //}
            this.Extra.Width = Width;
            this.Extra.NewWidth = Width;
            OnFireTagWidthChanged();
        }

        [Category(TagConsts.Name), Description(TagConsts.HiddenCloseProperty), DefaultValue(TagConsts.DefaultHiddenClose)]
        public bool HiddenClose
        {
            get { return _hiddenClose; }
            set
            {
                _hiddenClose = value;
                picClose.Visible = !_hiddenClose;
            }
        }

        [Category(TagConsts.Name), Description(TagConsts.ExtraProperty)]
        public BaseTagExtend Extra { get; set; }

        public BaseTagUc(ProjectSetKey key, string tag = null) : this()
        {
            Key = key;
            if (!string.IsNullOrEmpty(tag))
            {
                Extra.Tag = tag;
                Extra.DataSource = 1;
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            OnFireClose();
        }

        protected static int GetStringLength(string tag)
        {
            Font f = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Pixel);
            Size sif = TextRenderer.MeasureText(tag, f, new Size(0, 0), TextFormatFlags.NoPadding);
            return sif.Width + 16;
        }


        private void OnFireTagWidthChanged()
        {
            if (OnTagWidthChanged != null)
            {
                OnTagWidthChanged(this);
            }
        }

        private void OnFireClose()
        {
            if (OnCloseTag != null)
            {
                OnCloseTag(this);
            }
        }

        public virtual string AddCmdString()
        {
            return GetAddCmdString(Extra.Tag);
        }


        public virtual string ModifyCmdString()
        {
            return GetModifyCmdString(Extra.Tag);
        }

        public virtual string DeleteCmdString()
        {
            return GetDeleteCmdString();
        }

        protected string GetLoadCmdString()
        {
            return $"SELECT {DataBaseConsts.TypeColumn},{DataBaseConsts.SnColumn},{DataBaseConsts.NameColumn},{DataBaseConsts.ValueColumn} FROM {DataBaseConsts.TableName} WHERE {DataBaseConsts.TypeColumn}='{Key.Type}' AND {DataBaseConsts.SnColumn}={Key.Sn})";
        }

        protected string GetAddCmdString(string name)
        {
            return $"INSERT INTO {DataBaseConsts.TableName}(project_type,unit_sn,unit_name) values ('{Key.Type}',{Key.Sn},'{name}');";
        }

        protected string GetModifyCmdString(string name)
        {
            return $"UPDATE {DataBaseConsts.TableName} SET {DataBaseConsts.NameColumn}='{name}' WHERE {DataBaseConsts.TypeColumn}='{Key.Type}' AND {DataBaseConsts.SnColumn}={Key.Sn};";
        }

        protected string GetDeleteCmdString()
        {
            return $"DELETE FROM {DataBaseConsts.TableName} WHERE {DataBaseConsts.TypeColumn}='{Key.Type}' AND {DataBaseConsts.SnColumn}={Key.Sn};";
        }

        protected void SaveData(string name)
        {
            var sSql = "";
            if (Extra.DataSource == 0)
            {
                sSql = GetAddCmdString(name);
            }
            else if (Extra.DataSource == 1)
            {
                sSql = GetModifyCmdString(name);
            }
            if (sSql != "")
            {
                Execute(sSql);
            }
        }

        protected void Clear()
        {
            var sSql = DeleteCmdString();
            Execute(sSql);
        }

        protected virtual void Execute(string sSql)
        {

        }
    }
}
