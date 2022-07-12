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
        public CardManagerExtend Extra { get; set; }

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
            Extra = new CardManagerExtend()
            {
                OldHeight = this.Height
            };
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
                    var removeWidth = deleteUc.Width + CardConsts.DefaultPaddingLeft;
                    var removeHeight = deleteUc.Height + CardConsts.DefaultPaddingTop;
                    item.Left -= removeWidth;

                    if (item.Extra.RowIndex > deleteUc.Extra.RowIndex)
                    {
                        if (item.Extra.ColIndex == 0)
                        {
                            if (item.Left < 0)//上移到最后一个
                            {
                                item.Extra.RowIndex = deleteUc.Extra.RowIndex;
                                item.Left = (Extra.Cols - 1) * removeWidth;
                            }
                            item.Top -= removeHeight;
                        }
                    }
                    item.Extra.ColIndex--;
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

        private Point NextCardLocation(Control parent, BaseCardUc newCard, int total)
        {
            var nextHeight = newCard.Height + CardConsts.DefaultPaddingTop;
            var nextWidth = newCard.Width + CardConsts.DefaultPaddingLeft;

            var maxCols = parent.Width / nextWidth;
            var maxRows = total / maxCols;

            var nextRow = maxRows + 1;
            var nextCol = total % maxCols;

            var nextX = nextCol * nextWidth;
            var nextY = maxRows * nextHeight;

            //放大
            if (maxRows >= 0 && nextCol == 0)
            {
                if (parent.Height < nextRow * nextHeight)
                {
                    Height += nextHeight;
                    if (Extra.OldHeight == 0)
                    {
                        Extra.OldHeight = Height;
                    }
                    Extra.Expand = true;
                }
            }
            ContainerSizeChanged = false;
            if (Extra.Cols > 0 && Extra.Cols != maxCols)
            {
                Height = Extra.OldHeight + nextRow * nextHeight;
                ContainerSizeChanged = true;
            }
            Extra.Rows = nextRow;
            Extra.Cols = maxCols;
            Extra.NewHeight = Height;
            newCard.Extra.RowIndex = nextRow;
            newCard.Extra.ColIndex = nextCol;
            return new Point(nextX, nextY);
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
            var newPoint = NextCardLocation(this, newCard, CardCount);
            newCard.Location = newPoint;
            newCard.Parent = plContainer;
            CardHeight = newCard.Height;
            CardWidth = newCard.Width;
            _cardDics.Add(newCard.Key, newCard);
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
            var index = 0;
            foreach (BaseCardUc item in plContainer.Controls)
            {
                var newPoint = NextCardLocation(this, item, index);
                item.Location = newPoint;
                index++;
            }
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