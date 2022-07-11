using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserControlSamples.Models;

namespace UserControlSamples.Consts
{
    public class TagManagerConsts
    {
        public const string Name = "标签管理";

        public const string AddNewTagEvent = "添加新标签";
        public const string RemoveTagEvent = "删除标签";
        public const string ButtonEvent = "按钮点击";

        public const string AddButtonKey = "ADD";
        public const string ClearButtonKey = "CLEAR";

        public const string AllowedMaxTagCountProperty = "允许最大标签数量";
        public const string TagSrouceEnumProperty = "标签数据类型";
        public const string TagEnumProperty = "标签类型";
        public const string CountProperty = "标签数量";
        public const string ExtraProperty = "附加信息";

        public const int DefaultMaxTagCount = 20;
        public const TagEnum DefaultTagEnum = TagEnum.LabelTag;
        public const TagSourceEnum DefaultTagSourceEnum = TagSourceEnum.UnKnown;

        public const string LabelDisplayName = "Label标签";
        public const string TextDisplayName = "Text标签";
    } 
}
