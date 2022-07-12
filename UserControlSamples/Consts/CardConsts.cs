using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControlSamples.Models;

namespace UserControlSamples.Consts
{
    public class CardConsts
    {
        public const string Name = "自定义卡片";
        public const string RemoveCardEvent = "删除卡片事件";
        public const string CardPrimaryKeyProperty = "卡片主键信息";
        public const string ExtraProperty = "卡片附加信息";

        public const int DefaultPaddingTop = 5;
        public const int DefaultPaddingLeft = 20;
        public const CardEnum DefaultCardEnum = CardEnum.UnKnown;
    }
}
