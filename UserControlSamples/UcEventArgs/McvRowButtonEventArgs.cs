using System;

namespace UserControlSamples.UcEventArgs
{
    public class McvRowButtonEventArgs : EventArgs
    {
        public McvRowButtonEventArgs()
        {

        }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public string ButtonKey { get; set; }

        public string ButtonText { get; set; }
    }
}
