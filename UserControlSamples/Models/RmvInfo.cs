using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class RmvInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 显示
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// 是否合并
        /// </summary>
        public bool Merge { get; set; }

        public RmvSpanInfo Span { get; set; }

        public List<RmvMultiButtonInfo> Buttons { get; set; }
    }
}
