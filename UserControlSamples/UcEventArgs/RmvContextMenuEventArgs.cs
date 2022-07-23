using System;

namespace UserControlSamples.UcEventArgs
{
    public class RmvContextMenuEventArgs : EventArgs
    {
        public RmvContextMenuEventArgs()
        {

        }
        public string Text { get; set; }
        public int RowIndex { get; set; }

        public int ColIndex { get; set; }
    }
}
