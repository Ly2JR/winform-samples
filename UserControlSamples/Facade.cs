using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControlSamples.UI;

namespace UserControlSamples
{
    public class Facade
    {
        public void ShowDialog()
        {
            var card = new FrmCardManager();
            card.ShowDialog();
        }
    }
}
