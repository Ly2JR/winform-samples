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

namespace UserControlSamples.UserControls
{
    public partial class BaseCardUc : UserControl
    {
        public delegate void RemoveCardDelegate(BaseCard currentCard);

        [CategoryAttribute("自定义卡片"), DescriptionAttribute("删除卡片事件")]
        public event RemoveCardDelegate RemoveCardHandler;


        [CategoryAttribute("自定义卡片"), DescriptionAttribute("卡片信息"), ReadOnly(true)]
        public BaseCard CurrentCard { get; private set; }

        protected IDictionary<string, string> Items { get; set; }

        public string Key { get { return CurrentCard.ToString(); } }


        /// <summary>
        /// 数据类型:
        /// 0:新增数据
        /// 1:修改数据
        /// </summary>
        protected int DataType { get; private set; } = 0;

        /// <summary>
        /// 类型
        /// </summary>
        protected string PARTNAME
        {
            get { return CurrentCard.PartName; }
            set
            {
                CurrentCard.PartName = value;

            }
        }

        public BaseCardUc()
        {
            InitializeComponent();
            CurrentCard = new BaseCard();
            Items = new Dictionary<string, string>();
        }

        public BaseCardUc(string partName, int sn, IDictionary<string, string> items = null) : this()
        {
            CurrentCard.PartName = partName;
            CurrentCard.Sn = sn;
            if (items != null)
            {
                this.Items = items;
                DataType = 1;
            }
            this.lblTitle.Text = Key;
        }

        /// <summary>
        /// 清空
        /// </summary>
        protected void Clear()
        {
            ClearItem();
            var sSql = DeleteCmdString();
            Execute(sSql);
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <returns></returns>
        public virtual int LoadData()
        {
            return 0;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public virtual void Save()
        {
            if (Items.Count > 0)
            {
                var sSql = AddCmdString();
                Execute(sSql);
            }
        }

        protected virtual void GetData()
        {

        }

        protected virtual void SetData()
        {

        }

        protected void AddItem(string name, string value)
        {
            Items.Add(name, value);
        }

        protected void ClearItem()
        {
            Items.Clear();
        }

        public virtual string GetCmdString()
        {
            if (DataType == 0) return AddCmdString();
            if (DataType == 1) return ModifyCmdString();
            return "";
        }
        public virtual string AddCmdString()
        {
            var sb = new StringBuilder();
            foreach (var item in Items)
            {
                sb.Append(GetAddCmdString(item.Key, item.Value));
                sb.AppendLine("\r");
            }
            return sb.ToString();
        }


        public virtual string ModifyCmdString()
        {
            var sb = new StringBuilder();
            foreach (var item in Items)
            {
                sb.Append(GetModifyCmdString(item.Key, item.Value));
                sb.AppendLine("\r");
            }
            return sb.ToString();
        }

        public virtual string DeleteCmdString()
        {
            return GetDeleteCmdString();
        }


        protected string GetLoadCmdString()
        {
            return $"SELECT {DataBaseConsts.TYPE_COLUMN},{DataBaseConsts.SN_COLUMN},{DataBaseConsts.NAME_COLUMN},{DataBaseConsts.VALUE_COLUMN} FROM {DataBaseConsts.TABLE_NAME} WHERE {DataBaseConsts.TYPE_COLUMN}='{CurrentCard.PartName}' AND {DataBaseConsts.SN_COLUMN}={CurrentCard.Sn})";
        }

        protected string GetAddCmdString(string name, string value)
        {
            return $"INSERT INTO {DataBaseConsts.TABLE_NAME}({DataBaseConsts.TYPE_COLUMN},{DataBaseConsts.SN_COLUMN},{DataBaseConsts.NAME_COLUMN},{DataBaseConsts.VALUE_COLUMN}) values ('{CurrentCard.PartName}',{CurrentCard.Sn},'{name}','{value}');";
        }

        protected string GetModifyCmdString(string name, string value)
        {
            return $"UPDATE {DataBaseConsts.TABLE_NAME} SET {DataBaseConsts.VALUE_COLUMN}='{value}' WHERE {DataBaseConsts.TYPE_COLUMN}='{CurrentCard.PartName}' AND {DataBaseConsts.SN_COLUMN}={CurrentCard.Sn} AND {DataBaseConsts.NAME_COLUMN}='{name}';";
        }

        protected string GetDeleteCmdString()
        {
            return $"DELETE FROM {DataBaseConsts.TABLE_NAME} WHERE {DataBaseConsts.TYPE_COLUMN}='{CurrentCard.PartName}' AND {DataBaseConsts.SN_COLUMN}={CurrentCard.Sn};";
        }

        protected void SaveData(string name, string value)
        {
            var sSql = GetAddCmdString(name, value);
            Execute(sSql);
        }

        protected void Execute(string sSql)
        {
            
        }

        private void Delete()
        {
            var dialog = MessageBox.Show($"确定删除{CurrentCard}?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;
            Clear();
            if (RemoveCardHandler != null)
            {
                RemoveCardHandler(CurrentCard);
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Delete();
        }
    }
}
