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
    public partial class CardManagerUc : UserControl
    {
        public delegate void OnCardChangedDelegate(BaseCardUc uc);
        public delegate void OnCardManagerButtonHandler(CardManagerUc obj, string buttonKey);

        [Category("卡片管理"), Description("添加新卡片")]
        public event OnCardChangedDelegate AddNewCard;

        [Category("卡片管理"), Description("卡片管理按钮点击")]
        public event OnCardManagerButtonHandler OnCardManagerButtonClick;

        [Category("卡片管理"), Description("删除卡片")]
        public event OnCardChangedDelegate RemoveCard;

        private readonly IDictionary<BaseCard, BaseCardUc> _cardDics;
        private int _maxCardCount = CardConsts.DefaultMaxCardCount;

        [Category("卡片管理"), Description("允许最大卡片数量"), DefaultValue(CardConsts.DefaultMaxCardCount)]
        public int MaxCardCount
        {
            get { return _maxCardCount; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException("不允许小于等于0");
                _maxCardCount = value;
            }
        }

        [Category("卡片管理"), Description("卡片展开状态")]
        public bool Expand { get; private set; }

        [Category("卡片管理"), Description("卡片数量")]
        public int CardCount { get { return _cardDics.Count; } }

        [Category("卡片管理"), Description("卡片高度")]
        public int CardHeight { get; private set; }

        /// <summary>
        /// 根据Key获取卡片控件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public BaseCardUc this[string key]
        {
            get
            {
                var matchCard = _cardDics.Keys.FirstOrDefault(o => o.ToString() == key);
                if (matchCard == null) return null;
                return _cardDics[matchCard];
            }
        }

        public BaseCardUc this[int index]
        {
            get
            {
                var matchCard = _cardDics.Keys.ElementAt(index);
                return _cardDics[matchCard];
            }
        }

        public void ClearAll()
        {
            BatchDeleteTran();
            _cardDics.Clear();
            plContainer.Controls.Clear();
        }


        public CardManagerUc()
        {
            InitializeComponent();
            _cardDics = new Dictionary<BaseCard, BaseCardUc>();
        }


        [Category("卡片管理"), Description("卡片类型")]
        public CardEnum CurrentCardEnum { get; set; }

        private void tsBtnAdd_Click(object sender, EventArgs e)
        {
            CreateCard();
            OnFireButtonClick(CardManagerButtonConsts.ADD_BUTTON_KEY);
        }

        private int GetMaxId(IDictionary<BaseCard, BaseCardUc> items)
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


        private string CardDisplayName()
        {
            switch (CurrentCardEnum)
            {
                case CardEnum.Card1: return Card1Consts.DisplayName;
                case CardEnum.Card2: return Card2Consts.DisplayName;
            }
            return "";
        }

        public DataTable GetCardData()
        {
            var type = CardDisplayName();
            var sSql = $"SELECT * FROM  {DataBaseConsts.TABLE_NAME} WHERE {DataBaseConsts.TYPE_COLUMN}='{type}' ORDER BY {DataBaseConsts.SN_COLUMN} ASC";
            return null;
        }
        public void LoadCard()
        {
            var cards = GetCardData();
            if (cards != null && cards.Rows.Count > 0)
            {
                IDictionary<BaseCard, IDictionary<string, string>> newCards = new Dictionary<BaseCard, IDictionary<string, string>>();
                foreach (DataRow item in cards.Rows)
                {
                    var type = Convert.ToString(item[DataBaseConsts.TYPE_COLUMN]);
                    var sn = Convert.ToInt16(item[DataBaseConsts.SN_COLUMN]);
                    var name = Convert.ToString(item[DataBaseConsts.NAME_COLUMN]);
                    var value = Convert.ToString(item[DataBaseConsts.VALUE_COLUMN]);
                    var card = new BaseCard(type, sn);
                    IDictionary<string, string> cardItems;
                    if (!newCards.ContainsKey(card))
                    {
                        cardItems = new Dictionary<string, string>();
                        cardItems.Add(name, value);
                        newCards.Add(card, cardItems);
                    }
                    else
                    {
                        cardItems = newCards[card];
                        cardItems.Add(name, value);
                    }
                }

                foreach (var newCard in newCards)
                {
                    var addNewCard = GetCardUc(newCard.Key.Sn, newCard.Value);
                    AppendContainer(addNewCard);
                }
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
            if (_cardDics.ContainsKey(newCard.CurrentCard))
            {
                MessageBox.Show($"编号{newCard.CurrentCard.Sn}重复", "提示");
                return;
            }
            newCard.RemoveCardHandler += (currentCard) =>
            {
                var ret = _cardDics.ContainsKey(currentCard);
                if (!ret) return;
                var uc = _cardDics[currentCard];
                ret = _cardDics.Remove(currentCard);
                if (ret)
                {
                    ResizeContainer(plContainer, uc);
                }
                if (RemoveCard != null)
                {
                    RemoveCard(uc);
                }
            };
            var x = _cardDics.Count * (newCard.Width + CardConsts.DefaultPaddingLeft);
            newCard.Location = new Point(x, CardConsts.DefaultPaddingTopBottom);

            _cardDics.Add(newCard.CurrentCard, newCard);
            plContainer.Controls.Add(newCard);

            if (AddNewCard != null)
            {
                AddNewCard(newCard);
            }
        }

        private void tsBtnClear_Click(object sender, EventArgs e)
        {
            var dialog = MessageBox.Show($"确定清空所有{CardDisplayName()}数据吗?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;
            tsBtnClear.Enabled = false;
            try
            {
                ClearAll();
                MessageBox.Show("清空完成", CardDisplayName());
            }
            finally
            {
                tsBtnClear.Enabled = true;
            }
        }

        /// <summary>
        /// Sqlite
        /// </summary>

        private void BatchSaveTran()
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
            MessageBox.Show(sSql);
        }

        /// <summary>
        /// Sqlite
        /// </summary>
        private void BatchDeleteTran()
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
            MessageBox.Show(sSql);
        }

        private void tsBtnBatchSave_Click(object sender, EventArgs e)
        {
            tsBtnBatchSave.Enabled = false;
            try
            {
                BatchSaveTran();
                MessageBox.Show("批量保存完成", CardDisplayName());
            }
            finally
            {
                tsBtnBatchSave.Enabled = true;
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

        private void tsBtnExpandDown_Click(object sender, EventArgs e)
        {
            OnFireButtonClick(CardManagerButtonConsts.EXPAND_DOWN_BUTTON_KEY);
        }

        private void tsBtnExpandUp_Click(object sender, EventArgs e)
        {
            OnFireButtonClick(CardManagerButtonConsts.EXPAND_UP_BUTTON_KEY);
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
            OnFireButtonClick(CardManagerButtonConsts.EXPAND_SWITCH_BUTTON_KEY);
        }
    }
}
