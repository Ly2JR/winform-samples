using System.Collections.Generic;
using UserControlSamples.Consts;

namespace UserControlSamples.UserControls
{
    public partial class Card2Uc : BaseCardUc
    {
        public Card2Uc(int sn, IDictionary<string, string> items = null) : base(Card2Consts.DisplayName, sn, items)
        {
            InitializeComponent();

            GetData();
        }

        protected override void GetData()
        {
            if (base.Items == null) return;
            foreach (var item in Items)
            {
                switch (item.Key)
                {
                    case Card2Consts.IP_COLUMN:
                        txtIP.Text = item.Value;
                        break;
                    case Card2Consts.PORT_COLUMN:
                        txtPort.Text = item.Value;
                        break;
                }
            }
        }

        protected override void SetData()
        {
            ClearItem();
            AddItem(Card2Consts.IP_COLUMN, txtIP.Text);
            AddItem(Card2Consts.PORT_COLUMN, txtPort.Text);
        }
        public override string AddCmdString()
        {
            SetData();
            return base.AddCmdString();
        }
    }
}
