using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;

namespace UserControlSamples.UI.UserControls
{
    public partial class CardManagerUc : UserControl
    {
        public delegate void OnCardChangedHandler(CardManagerUc obj, BaseCardUc uc);
        public delegate void OnCardManagerButtonHandler(CardManagerUc obj, string buttonKey);

        private readonly IDictionary<ProjectSetKey, BaseCardUc> _cardDics;
        private int _maxCardCount = CardManagerConsts.DefaultMaxCardCount;
        private CardEnum _currentCardEnum = CardConsts.DefaultCardEnum;

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.AddNewCardEvent)]
        public event OnCardChangedHandler OnAddNewCard;

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.ButtonEvent)]
        public event OnCardManagerButtonHandler OnCardManagerButtonClick;

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.RemoveCardEvent)]
        public event OnCardChangedHandler OnRemoveCard;

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.AllowedMaxCardCountProperty), DefaultValue(CardManagerConsts.DefaultMaxCardCount)]
        public int MaxCardCount
        {
            get { return _maxCardCount; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("不允许小于等于0");
                _maxCardCount = value;
            }
        }

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.CardCountProperty)]
        public int CardCount { get { return _cardDics.Count; } }

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.CardHeightProperty)]
        public int CardHeight { get; private set; }

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.CardWidthProperty)]
        public int CardWidth { get; private set; }

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.ContainerSizeChanged)]
        public bool ContainerSizeChanged { get; private set; }

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.ContainerSizeChanged)]
        public CardManagerExtend Extra { get; private set; }

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.CardTypeProperty), DefaultValue(CardConsts.DefaultCardEnum)]
        public CardEnum CurrentCardEnum
        {
            get { return _currentCardEnum; }
            set
            {
                _currentCardEnum = value;
                groupBox1.Text = DisplayName();
            }
        }

        public void ClearAll()
        {
            _cardDics.Clear();
            plContainer.Controls.Clear();
        }

        public CardManagerUc()
        {
            InitializeComponent();
            _cardDics = new Dictionary<ProjectSetKey, BaseCardUc>();
            Extra = new CardManagerExtend();
        }

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            CreateCard();
            OnFireButtonClick(CardManagerConsts.AddButtonKey);
        }

        private int GetMaxId(IDictionary<ProjectSetKey, BaseCardUc> items)
        {
            if (items.Count == 0) return 1;
            var keys = items.Keys;
            return keys.Max(o => o.Sn) + 1;
        }

        private void ResizeContainer(Panel container, BaseCardUc deleteUc)
        {
            var flag = false;
            foreach (BaseCardUc item in container.Controls)
            {
                if (item.Key == deleteUc.Key)
                {
                    flag = true;
                    continue;
                }
                if (flag)
                {
                    item.Extra.ColIndex--;

                    if (item.Extra.ColIndex < 0)
                    {
                        item.Extra.ColIndex = Extra.Cols;
                        item.Extra.RowIndex--;
                        var width = GetCardWidthByRow(item.Extra.RowIndex);
                        item.Left = width - deleteUc.Width;
                        item.Top -= deleteUc.Height;
                    }
                    else
                    {
                        item.Left -= deleteUc.Width + CardConsts.DefaultPaddingLeft;
                    }
                }
            }
            container.Controls.Remove(deleteUc);
        }

        private int GetCardWidthByRow(int row)
        {
            var items = _cardDics.Values.Where(o => o.Extra.RowIndex == row);
            var count = items.Count();
            return items.Sum(o => o.Width + CardConsts.DefaultPaddingLeft);
        }

        private string DisplayName()
        {
            switch (CurrentCardEnum)
            {
                case CardEnum.Card1: return Card1Consts.DisplayName;
                case CardEnum.Card2: return Card2Consts.DisplayName;
            }
            return "";
        }

        public virtual DataTable GetCardData()
        {
            var type = DisplayName();
            var sSql = $"SELECT * FROM  {DataBaseConsts.TableName} WHERE {DataBaseConsts.TypeColumn}='{type}' ORDER BY {DataBaseConsts.SnColumn} ASC";
            return null;
            //return Sqlite.Execute(sSql);
        }
        public void LoadCard()
        {
            ClearAll();

            var cards = GetCardData();
            if (cards != null && cards.Rows.Count > 0)
            {
                IDictionary<ProjectSetKey, IDictionary<string, string>> newCards = new Dictionary<ProjectSetKey, IDictionary<string, string>>();
                foreach (DataRow item in cards.Rows)
                {
                    var type = Convert.ToString(item[DataBaseConsts.TypeColumn]);
                    var sn = Convert.ToInt16(item[DataBaseConsts.SnColumn]);
                    var name = Convert.ToString(item[DataBaseConsts.NameColumn]);
                    var value = Convert.ToString(item[DataBaseConsts.ValueColumn]);
                    var key = new ProjectSetKey(type, sn);
                    IDictionary<string, string> cardItems;
                    if (!newCards.ContainsKey(key))
                    {
                        cardItems = new Dictionary<string, string>();
                        cardItems.Add(name, value);
                        newCards.Add(key, cardItems);
                    }
                    else
                    {
                        cardItems = newCards[key];
                        cardItems.Add(name, value);
                    }
                }

                foreach (var newCard in newCards)
                {
                    var addNewCard = GetCardUc(newCard.Key.Sn, newCard.Value);
                    AppendContainer(addNewCard);
                }

                Expand(this, CardManagerConsts.ExpandDownButtonKey);
            }
        }

        /// <summary>
        /// 添加新的卡片
        /// </summary>
        /// <param name="items">卡内容</param>
        private void CreateCard()
        {
            var count = _cardDics.Count;
            if (count == _maxCardCount)
            {
                MessageBox.Show($"超过最大添加数量:{_maxCardCount}台", "提示");
                return;
            }
            var sn = GetMaxId(_cardDics);
            var addNew = GetCardUc(sn);
            if (addNew == null) return;
            AppendContainer(addNew);
        }

        private void AddCard(Control parent, BaseCardUc newCard, IDictionary<ProjectSetKey, BaseCardUc> refresh = null)
        {
            var defaultDics = _cardDics;
            if (refresh != null)
            {
                defaultDics = refresh;
            }
            var items = defaultDics.Values.Where(o => o.Extra.RowIndex == Extra.Rows);
            var existsWidth = items.Sum(o => o.Width + CardConsts.DefaultPaddingLeft);

            var nextX = existsWidth + CardConsts.DefaultPaddingLeft;
            var nextY = Extra.Rows * newCard.Height + CardConsts.DefaultPaddingTop;

            var colIndex = items.Count();

            if (parent.Width < nextX + newCard.Width)
            {
                Extra.Cols = colIndex;
                colIndex = 0;
                Extra.Rows++;

                items = defaultDics.Values.Where(o => o.Extra.RowIndex == Extra.Rows);

                existsWidth = items.Sum(o => o.Width + CardConsts.DefaultPaddingLeft);
                nextX = existsWidth + CardConsts.DefaultPaddingLeft;
                nextY = Extra.Rows * newCard.Height + CardConsts.DefaultPaddingTop;
            }
            if (parent.Height < (nextY + newCard.Height))
            {
                if (Extra.OldHeight == 0)
                {
                    Extra.OldHeight = Height;
                }
                Height += newCard.Height + CardConsts.DefaultPaddingTop;
            }

            newCard.Extra.RowIndex = Extra.Rows;
            newCard.Extra.ColIndex = colIndex;
            newCard.Location = new Point(nextX, nextY);
            newCard.Parent = parent;

            CardHeight = newCard.Height;
            CardWidth = newCard.Width;

            defaultDics.Add(newCard.Key, newCard);
            Extra.Expand = true;
            parent.Controls.Add(newCard);
        }


        private void AppendContainer(BaseCardUc newCard)
        {
            if (_cardDics.ContainsKey(newCard.Key))
            {
                MessageBox.Show($"编号{newCard.Key.Sn}重复", "提示");
                return;
            }
            newCard.OnRemoveCard += (obj) =>
            {
                if (!obj.Extra.Continute) return;
                var ret = _cardDics.ContainsKey(obj.Key);
                if (!ret) return;
                var uc = _cardDics[obj.Key];
                ret = _cardDics.Remove(obj.Key);
                if (ret)
                {
                    ResizeContainer(plContainer, uc);
                }
                OnFireRemoveCard(uc);
            };
            AddCard(plContainer, newCard);
            OnFireAddNewCard(newCard);
        }

        private void OnFireRemoveCard(BaseCardUc removeCard)
        {
            if (OnRemoveCard != null)
            {
                OnRemoveCard(this, removeCard);
            }
        }

        private void OnFireAddNewCard(BaseCardUc newCard)
        {
            if (OnAddNewCard != null)
            {
                OnAddNewCard(this, newCard);
            }
        }

        private void tlBtnClear_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show($"确定清空所有{DisplayName()}数据吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
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
        }


        public virtual void BatchSaveTran()
        {
            var sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION;\r");
            foreach (var uc in _cardDics.Values)
            {
                var addCmd = uc.GetCmdString();
                sb.Append(addCmd);
            }
            sb.Append("COMMIT;");
            var sSql = sb.ToString();
            //Sqlite.Execute(sSql);
        }

        public virtual void BatchDeleteTran()
        {
            var sb = new StringBuilder();
            sb.Append("BEGIN TRANSACTION;\r");
            foreach (var uc in _cardDics.Values)
            {
                var deleteCmd = uc.DeleteCmdString();
                sb.Append(deleteCmd);
            }
            sb.Append("COMMIT;");
            var sSql = sb.ToString();
            //Sqlite.Execute(sSql);
        }

        private void tlBtnBatchSave_Click(object sender, EventArgs e)
        {
            tlBtnBatchSave.Enabled = false;
            try
            {
                BatchSaveTran();
                MessageBox.Show("保存完成", DisplayName());
            }
            finally
            {
                tlBtnBatchSave.Enabled = true;
            }
        }

        private BaseCardUc GetCardUc(int sn, IDictionary<string, string> items = null)
        {
            BaseCardUc addNew = null;
            switch (CurrentCardEnum)
            {
                case CardEnum.Card1:
                    addNew = new Card1Uc(sn, items);
                    break;
                case CardEnum.Card2:
                    addNew = new Card2Uc(sn, items);
                    break;
            }
            return addNew;
        }

        private void tlsBtnExpandDown_Click(object sender, EventArgs e)
        {
            Expand(this, CardManagerConsts.ExpandDownButtonKey);
            OnFireButtonClick(CardManagerConsts.ExpandDownButtonKey);
        }

        private void tlsBtnExpandUp_Click(object sender, EventArgs e)
        {
            Expand(this, CardManagerConsts.ExpandUpButtonKey);
            OnFireButtonClick(CardManagerConsts.ExpandUpButtonKey);
        }

        private void OnFireButtonClick(string buttonKey)
        {
            if (OnCardManagerButtonClick != null)
            {
                OnCardManagerButtonClick(this, buttonKey);
            }
        }

        private void toolStrip1_DoubleClick(object sender, EventArgs e)
        {
            Expand(this, CardManagerConsts.ExpandSwitchButtonKey);
            OnFireButtonClick(CardManagerConsts.ExpandSwitchButtonKey);

        }

        public void RefreshLayout()
        {
            ContainerSizeChanged = true;
            if (!Extra.Expand) return;

            var refreshDic = new Dictionary<ProjectSetKey, BaseCardUc>();
            Extra.Rows = 0;
            Extra.Cols = 0;
            foreach (var uc in _cardDics.Values)
            {
                uc.Extra.ColIndex = 0;
                uc.Extra.RowIndex = 0;
            }
            plContainer.SuspendLayout();
            foreach (var item in _cardDics.Values)
            {
                AddCard(plContainer, item, refreshDic);
            }
            plContainer.ResumeLayout();
            Height = (Extra.Rows + 1) * (CardHeight + CardConsts.DefaultPaddingTop) + Extra.OldHeight;
            Extra.NewHeight = Height;
        }

        private void Expand(CardManagerUc obj, string buttonKey)
        {
            switch (buttonKey)
            {
                case CardManagerConsts.AddButtonKey:
                    if (obj.Extra.Expand) return;
                    break;
                case CardManagerConsts.ExpandDownButtonKey:
                    if (obj.Extra.Expand || obj.CardCount == 0) return;
                    break;
                case CardManagerConsts.ExpandUpButtonKey:
                    if (!obj.Extra.Expand || obj.CardCount == 0) return;
                    break;
            }
            var height = obj.Extra.Rows * (obj.CardHeight + CardConsts.DefaultPaddingTop);
            if (obj.Extra.Expand) //折叠
            {
                Height -= height;
                obj.Extra.Expand = false;
            }
            else //展开
            {
                Height += height;
                obj.Extra.Expand = true;
            }
            obj.plContainer.Visible = obj.Extra.Expand;
            if (ContainerSizeChanged)
            {
                RefreshLayout();
            }
        }
    }
}