using System.Collections.Generic;
using UserControlSamples.Consts;

namespace UserControlSamples.UserControls
{
    public partial class Card1Uc : BaseCardUc
    {
        public Card1Uc(int sn, IDictionary<string, string> items = null) : base(Card1Consts.DisplayName, sn, items)
        {
            InitializeComponent();

            GetData();
        }


        protected override void GetData()
        {
            if (Items == null) return;
            foreach (var item in Items)
            {
                switch (item.Key)
                {
                    case Card1Consts.IO_COLUMN:
                        txtIO.Text = item.Value;
                        break;
                }
            }
        }

        protected override void SetData()
        {
            ClearItem();
            AddItem(Card1Consts.IO_COLUMN, txtIO.Text);
        }
        public override string AddCmdString()
        {
            SetData();
            return base.AddCmdString();
        }
    }
}
