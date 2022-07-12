using System.Collections.Generic;
using System.Windows.Forms;
using UserControlSamples.Consts;

namespace UserControlSamples.UI.UserControls
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
                    case Card2Consts.IPColumn:
                        txtIP.Text = item.Value;
                        break;
                    case Card2Consts.PortColumn:
                        txtPort.Text = item.Value;
                        break;
                }
            }
        }

        protected override void SetData()
        {
            Items.Clear();
            Items.Add(Card2Consts.IPColumn, txtIP.Text);
            Items.Add(Card2Consts.PortColumn, txtPort.Text);
        }
        public override string AddCmdString()
        {
            SetData();
            return base.AddCmdString();
        }

        public override string ModifyCmdString()
        {
            SetData();
            return base.ModifyCmdString();
        }

        protected override void Execute(string sSql)
        {
            
        }

        private void RobotCardUc_OnRemoveCard(BaseCardUc obj)
        {
            var dialog = MessageBox.Show($"确定删除{obj.Key}?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dialog != DialogResult.OK) return;
            obj.Extra.Continute = true;
            Clear();
        }
    }
}
