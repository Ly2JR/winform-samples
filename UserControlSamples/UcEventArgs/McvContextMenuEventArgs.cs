using System;

namespace UserControlSamples.UcEventArgs
{
    public class McvContextMenuEventArgs : EventArgs
    {
        public McvContextMenuEventArgs()
        {

        }
        public string Text { get; set; }
        public int RowIndex { get; set; }

        public int ColIndex { get; set; }
    }
}
