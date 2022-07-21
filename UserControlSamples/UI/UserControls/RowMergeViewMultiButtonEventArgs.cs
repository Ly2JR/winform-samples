using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.UI.UserControls
{
    public class RowMergeViewMultiButtonEventArgs : EventArgs
    {
        public RowMergeViewMultiButtonEventArgs()
        {

        }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public string ButtonKey { get; set; }

        public string ButtonText { get; set; }
    }
}
