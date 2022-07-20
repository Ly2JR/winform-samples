using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;
using UserControlSamples.UI.UserControls;

namespace UserControlSamples.Extensions
{
    public static class RowMergeViewExtension
    {
        /// <summary>
        /// 默认样式
        /// </summary>
        /// <param name="rowMergerView"></param>
        public static void DefaultStyle(this RowMergeView rowMergerView)
        {
            var alternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.LightCyan,
            };
            rowMergerView.AlternatingRowsDefaultCellStyle = alternatingRowsDefaultCellStyle;

            var columnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(211, 223, 240),
                Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134),
                ForeColor = Color.Navy,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText,
            };
            rowMergerView.ColumnHeadersDefaultCellStyle = columnHeadersDefaultCellStyle;

            var rowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.LightCyan,
                Font = new Font("微软雅黑", 10F, FontStyle.Regular, GraphicsUnit.Point, 134),
                ForeColor = Color.Red,
                SelectionBackColor = SystemColors.Highlight,
                SelectionForeColor = SystemColors.HighlightText
            };
            rowMergerView.RowHeadersDefaultCellStyle = rowHeadersDefaultCellStyle;

            var rowsDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
            };
            rowMergerView.RowsDefaultCellStyle = rowsDefaultCellStyle;

            rowMergerView.AutoGenerateColumns = false;
            rowMergerView.TopLeftHeaderCell.Value = "行号";
            rowMergerView.ColumnHeadersHeight = RowMergeViewConsts.DefaultColumnHeight;
            rowMergerView.AllowUserToAddRows = false;
            rowMergerView.AllowUserToDeleteRows = false;
            rowMergerView.AllowUserToResizeColumns = false;
            rowMergerView.AllowUserToResizeRows = false;
            rowMergerView.BackgroundColor = Color.White;
            rowMergerView.RowTemplate.ReadOnly = true;
            rowMergerView.BorderStyle = BorderStyle.Fixed3D;
            rowMergerView.ReadOnly = true;
            rowMergerView.MultiSelect = false;
            rowMergerView.VirtualMode = true;
            rowMergerView.BorderStyle = BorderStyle.None;
            rowMergerView.EnableHeadersVisualStyles = false;
            rowMergerView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            rowMergerView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
        }

        /// <summary>
        /// 初始化表头
        /// </summary>
        /// <param name="rowMergerView"></param>
        /// <param name="cols"></param>
        public static void InitColumns(this RowMergeView rowMergerView, IList<RmvInfo> cols)
        {
            rowMergerView.Columns.Clear();
            foreach (var col in cols.OrderBy(o => o.Order))
            {
                var textCol = new DataGridViewTextBoxColumn
                {
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                    HeaderText = col.Text,
                    DataPropertyName = col.FieldName,
                    Name = col.FieldName,
                    SortMode = DataGridViewColumnSortMode.NotSortable,
                    Width = col.Width,
                    Visible = col.Visible
                };
                if (col.Merge)
                {
                    rowMergerView.MergeColumnNames.Add(col.FieldName);
                }
                if (col.Span != null)
                {
                    rowMergerView.AddSpanHeader(col.Order, col.Span.SpanColumn, col.Span.SpanHeader);
                }
                rowMergerView.Columns.Add(textCol);
            }
        }
    }
}
