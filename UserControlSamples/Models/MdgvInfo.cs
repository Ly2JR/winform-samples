using System.Collections.Generic;

namespace UserControlSamples.Models
{
    public class MdgvInfo
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 是否主键,防止单元格重复合并
        /// </summary>
        public bool IsPrimaryKey { get; set; }

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
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 单元格是否合并
        /// </summary>
        public bool MergeCell { get; set; }

        /// <summary>
        /// 1:普通文本
        /// 2:图片
        /// </summary>
        public int ColumnType { get; set; } = 1;

        /// <summary>
        /// 合并栏目
        /// </summary>
        public MdgvHeaderInfo MergeHeader { get; set; }

        /// <summary>
        /// 按钮栏目
        /// </summary>
        public List<MdgvRowButtonInfo> RowButtons { get; set; }
    }
}
