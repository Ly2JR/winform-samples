using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class BaseGroupBox
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

        public BaseGroupBox() { }

        public BaseGroupBox(bool expand, int orginHeight)
        {
            this.Expand = expand;
            this.OrginHeight = orginHeight;
        }
    }
}
