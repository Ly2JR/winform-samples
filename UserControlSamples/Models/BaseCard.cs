using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserControlSamples.Models
{
    /// <summary>
    /// 卡信息
    /// </summary>
    public class BaseCard
    {
        public ProjectSetKey Key { get; set; }

        public BaseCard()
        {
            Key = new ProjectSetKey();
        }

        public BaseCard(string name, int sn) : this()
        {
            Key.Type = name;
            Key.Sn = sn;
        }

        public bool Continute { get; set; }

        /// <summary>
        /// 数据来源
        /// 0:新增
        /// 1:从数据库加载
        /// </summary>
        public int DataSource { get; set; }

        public override string ToString()
        {
            return Key.ToString();
        }
    }

    /// <summary>
    /// 卡类型
    /// </summary>
    public enum CardEnum : byte
    {
        Card1 = 0,
        Card2,
        UnKnown = 99
    }
}
