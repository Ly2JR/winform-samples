using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControlSamples.Frms;

namespace UserControlSamples
{
    public class Facade
    {
        public void ShowDialog()
        {
            var card = new FrmCard();
            card.ShowDialog();
        }
    }
}
