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
        /// <summary>
        /// 类型
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sn { get; set; }

        public string Key { get { return this.ToString(); } }

        public BaseCard() { }

        public BaseCard(string name, int sn)
        {
            this.PartName = name;
            this.Sn = sn;
        }

        public override string ToString()
        {
            return $"{PartName}#{Sn}";
        }

        public override int GetHashCode()
        {
            return PartName.GetHashCode() + Sn * 102;
        }

        public override bool Equals(object obj)
        {
            var baseCard = obj as BaseCard;
            if (baseCard == null) return false;
            return baseCard.ToString() == this.ToString();
        }
    }

    /// <summary>
    /// 卡类型
    /// </summary>
    public enum CardEnum : byte
    {
        Card1 = 0,
        Card2,
    }
}
