using System;

namespace UserControlSamples.UcEventArgs
{
    public class RmvRowButtonEventArgs : EventArgs
    {
        public RmvRowButtonEventArgs()
        {

        }

        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public string ButtonKey { get; set; }

        public string ButtonText { get; set; }
    }
}
