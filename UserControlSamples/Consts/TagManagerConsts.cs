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
        public const int DefaultMaxTagCount = 20;
        public const string AllowedMaxTagCountProperty = "允许最大标签数量";
        public const string GrooveAttributeDisplayName = "坡口";
        public const TagEnum DefaultTagEnum = TagEnum.UnKnown;
    } 
}
