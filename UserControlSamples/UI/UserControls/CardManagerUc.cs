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
    public partial class CardManagerUc : UserControl
    {
        public delegate void OnCardChangedHandler(CardManagerUc obj, BaseCardUc uc);
        public delegate void OnCardManagerButtonHandler(CardManagerUc obj, string buttonKey);

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.AddNewCardEvent)]
        public event OnCardChangedHandler OnAddNewCard;

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.ButtonEvent)]
        public event OnCardManagerButtonHandler OnCardManagerButtonClick;

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.RemoveCardEvent)]
        public event OnCardChangedHandler OnRemoveCard;

        private readonly IDictionary<ProjectSetKey, BaseCardUc> _cardDics;
        private int _maxCardCount = CardManagerConsts.DefaultMaxCardCount;

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

        [Category(CardManagerConsts.Name), Description(CardManagerConsts.CardCountProperty)]
        public int CardHeight { get; private set; }

        public BaseExpand Extra { get; set; }

        public void ClearAll()
        {
            _cardDics.Clear();
            plContainer.Controls.Clear();
        }

        public CardManagerUc()
        {
            InitializeComponent();
            _cardDics = new Dictionary<ProjectSetKey, BaseCardUc>();
            Extra = new BaseExpand()
            {
                OrginHeight = this.Height
            };
        }



        private CardEnum _currentCardEnum = CardConsts.DefaultCardEnum;

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

        private void tsbtnAdd_Click(object sender, EventArgs e)
        {
            CreateCard();
            Expand(this, CardManagerConsts.AddButtonKey);
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
                }
                if (flag)
                {
                    item.Left -= (item.Width + CardConsts.DefaultPaddingLeft);
                }
            }
            container.Controls.Remove(deleteUc);
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

        private void AppendContainer(BaseCardUc newCard)
        {
            newCard.Parent = plContainer;
            if (_cardDics.ContainsKey(newCard.CurrentCard.Key))
            {
                MessageBox.Show($"编号{newCard.CurrentCard.Key.Sn}重复", "提示");
                return;
            }
            newCard.OnRemoveCard += (obj, currentCard) =>
            {
                if (!currentCard.Continute) return;
                var ret = _cardDics.ContainsKey(currentCard.Key);
                if (!ret) return;
                var uc = _cardDics[currentCard.Key];
                ret = _cardDics.Remove(currentCard.Key);
                if (ret)
                {
                    ResizeContainer(plContainer, uc);
                }
                OnFireRemoveCard(uc);
            };
            var x = _cardDics.Count * (newCard.Width + CardConsts.DefaultPaddingLeft);
            newCard.Location = new Point(x, CardConsts.DefaultPaddingTopBottom);

            _cardDics.Add(newCard.CurrentCard.Key, newCard);
            plContainer.Controls.Add(newCard);
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
                    CardHeight = addNew.Height;
                    break;
                case CardEnum.Card2:
                    addNew = new Card2Uc(sn, items);
                    CardHeight = addNew.Height;
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
            if (obj.Extra.Expand)
            {
                obj.Height = obj.Extra.OrginHeight;
                obj.Extra.Expand = false;
            }
            else
            {
                if (obj.Extra.ExpandHeight == 0)
                {
                    obj.Extra.OrginHeight = obj.Height;
                    obj.Height = obj.Height + obj.CardHeight + CardConsts.DefaultPaddingTopBottom * 2;
                    obj.Extra.ExpandHeight = obj.Height;
                }
                else
                {
                    obj.Height = obj.Extra.ExpandHeight;
                }
                obj.Extra.Expand = true;
            }
        }
    }
}