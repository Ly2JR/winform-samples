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
using UserControlSamples.UserControls;

namespace UserControlSamples.Frms
{
    public partial class FrmCard : Form
    {
        private IDictionary<GroupBox, BaseGroupBox> _groupBoxStatus = new Dictionary<GroupBox, BaseGroupBox>();
        public FrmCard()
        {
            InitializeComponent();
        }

        private void LoadGroupBox()
        {
            foreach (var ctrl in this.Controls)
            {
                var groupBox = ctrl as GroupBox;
                if (groupBox != null)
                {
                    _groupBoxStatus.Add(groupBox, new BaseGroupBox(false, groupBox.Height));
                }
            }
        }

        private void FrmProjectSet_Shown(object sender, EventArgs e)
        {
          
        }

        private void OnExpandGroupBoxClick(CardManagerUc obj, string buttonKey)
        {
            if (obj.CardCount == 0) return;
            var parent = obj.Parent as GroupBox;
            if (parent != null)
            {
                if (_groupBoxStatus.ContainsKey(parent))
                {
                    var bExpand = _groupBoxStatus[parent].Expand;
                    switch (buttonKey)
                    {
                        case CardManagerButtonConsts.ADD_BUTTON_KEY:
                            if (!bExpand)
                            {
                                _groupBoxStatus[parent].OrginHeight = parent.Height;
                                parent.Height = parent.Height + obj.CardHeight + CardConsts.DefaultPaddingTopBottom * 2;
                                _groupBoxStatus[parent].Expand = true;
                            }
                            break;
                        case CardManagerButtonConsts.EXPAND_DOWN_BUTTON_KEY:
                            if (!bExpand)
                            {
                                parent.Height = parent.Height + obj.CardHeight + CardConsts.DefaultPaddingTopBottom * 2;
                                _groupBoxStatus[parent].Expand = true;
                            }
                            break;
                        case CardManagerButtonConsts.EXPAND_UP_BUTTON_KEY:
                            if (bExpand)
                            {
                                parent.Height = _groupBoxStatus[parent].OrginHeight;
                                _groupBoxStatus[parent].Expand = false;
                            }
                            break;
                        case CardManagerButtonConsts.EXPAND_SWITCH_BUTTON_KEY:
                            if (bExpand)
                            {
                                parent.Height = _groupBoxStatus[parent].OrginHeight;
                                _groupBoxStatus[parent].Expand = false;
                            }
                            else
                            {
                                parent.Height = parent.Height + obj.CardHeight + CardConsts.DefaultPaddingTopBottom * 2;
                                _groupBoxStatus[parent].Expand = true;
                            }
                            break;
                    }
                }
            }
        }

        private void FrmCard_Shown(object sender, EventArgs e)
        {
            LoadGroupBox();

            //dynamicCardManagerUc1.LoadCard();
            //dynamicCardManagerUc2.LoadCard();
            //dynamicCardManagerUc3.LoadCard();
        }
    }
}
