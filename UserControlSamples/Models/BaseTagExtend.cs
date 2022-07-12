using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    public class BaseTagExtend
    {
        /// <summary>
        /// 控件宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 控件原来宽度
        /// </summary>
        public int OldWidth { get; set; }

        /// <summary>
        /// 控件新宽度
        /// </summary>
        public int NewWidth { get; set; }

        /// <summary>
        /// 数据模式
        /// 1:显示
        /// 2:编辑
        /// </summary>
        public int DataMode { get; set; }

        /// <summary>
        /// 数据来源
        /// 0:新增数据
        /// 1:从数据库加载
        /// </summary>
        public int DataSource { get; set; }

        public string Tag { get; set; }

        /// <summary>
        /// 是否继续
        /// </summary>
        public bool Continute { get; set; }
    }
}
