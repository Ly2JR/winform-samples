using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class BaseCardExtend
    {
        /// <summary>
        /// True:展开
        /// False:折叠
        /// </summary>
        public bool Expand { get; set; }

        /// <summary>
        /// 原高度
        /// </summary>
        public int OrginHeight { get; set; }

        /// <summary>
        /// 展开高度
        /// </summary>
        public int ExpandHeight { get; set; }

        public BaseCardExtend() { }

        public BaseCardExtend(bool expand, int orginHeight)
        {
            this.Expand = expand;
            this.OrginHeight = orginHeight;
        }
    }
}
