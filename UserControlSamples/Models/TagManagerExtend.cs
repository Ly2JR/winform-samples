namespace UserControlSamples.Models
{
    public class TagManagerExtend
    {
        /// <summary>
        /// True:展开
        /// False:折叠
        /// </summary>
        public bool Expand { get; set; }

        public int Cols { get; set; }

        public int Rows { get; set; }

        /// <summary>
        /// 展开高度
        /// </summary>
        public int NewHeight { get; set; }

        public int OldHeight { get; set; }
    }
}
