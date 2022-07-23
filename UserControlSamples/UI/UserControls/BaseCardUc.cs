using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;

namespace UserControlSamples.UI.UserControls
{
    public partial class BaseCardUc : UserControl
    {
        public delegate void RemoveCardHandler(BaseCardUc obj);

        [Category(CardConsts.Name), Description(CardConsts.RemoveCardEvent)]
        public event RemoveCardHandler OnRemoveCard;

        [Category(CardConsts.Name), Description(CardConsts.CardPrimaryKeyProperty)]
        public ProjectSetKey Key { get; private set; }

        [Category(CardConsts.Name), Description(CardConsts.CardPrimaryKeyProperty)]

        public BaseCardExtend Extra { get; set; }

        protected IDictionary<string, string> Items { get; set; }

        public BaseCardUc()
        {
            InitializeComponent();
            Key = new ProjectSetKey();
            Items = new Dictionary<string, string>();
            Extra = new BaseCardExtend();
        }

        public BaseCardUc(string type, int sn, IDictionary<string, string> items = null) : this()
        {
            Key.Type = type;
            Key.Sn = sn;
            if (items != null)
            {
                this.Items = items;
                Extra.DataSource = 1;
            }
            this.lblTitle.Text = Key.ToString();
        }

        /// <summary>
        /// 清空
        /// </summary>
        protected void Clear()
        {
            Items.Clear();
            var sSql = DeleteCmdString();
            Execute(sSql);
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

        public virtual string GetCmdString()
        {
            if (Extra.DataSource == 0) return AddCmdString();
            if (Extra.DataSource == 1) return ModifyCmdString();
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
            return $"SELECT {DataBaseConsts.TypeColumn},{DataBaseConsts.SnColumn},{DataBaseConsts.NameColumn},{DataBaseConsts.ValueColumn} FROM {DataBaseConsts.TableName} WHERE {DataBaseConsts.TypeColumn}='{Key.Type}' AND {DataBaseConsts.SnColumn}={Key.Sn})";
        }

        protected string GetAddCmdString(string name, string value)
        {
            return $"INSERT INTO {DataBaseConsts.TableName}(project_type,unit_sn,unit_name,unit_value) values ('{Key.Type}',{Key.Sn},'{name}','{value}');";
        }

        protected string GetModifyCmdString(string name, string value)
        {
            return $"UPDATE {DataBaseConsts.TableName} SET {DataBaseConsts.ValueColumn}='{value}' WHERE {DataBaseConsts.TypeColumn}='{Key.Type}' AND {DataBaseConsts.SnColumn}={Key.Sn} AND {DataBaseConsts.NameColumn}='{name}';";
        }

        protected string GetDeleteCmdString()
        {
            return $"DELETE FROM {DataBaseConsts.TableName} WHERE {DataBaseConsts.TypeColumn}='{Key.Type}' AND {DataBaseConsts.SnColumn}={Key.Sn};";
        }

        protected void SaveData(string name, string value)
        {
            var sSql = GetAddCmdString(name, value);
            Execute(sSql);
        }

        protected virtual void Execute(string sSql)
        {

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picClose_Click(object sender, EventArgs e)
        {
            OnFireRemoveCard();
        }

        private void OnFireRemoveCard()
        {
            if (OnRemoveCard != null)
            {
                OnRemoveCard(this);
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            OnFireRemoveCard();
        }
    }
}
