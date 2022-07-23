using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;
using UserControlSamples.UI.UserControls;

namespace UserControlSamples.Extensions
{
    public static class MergeDataGridViewExtension
    {
        /// <summary>
        /// 默认样式
        /// </summary>
        /// <param name="rowMergerView"></param>
        public static void DefaultStyle(this MergeDataGridView rowMergerView)
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
                Alignment = DataGridViewContentAlignment.MiddleLeft,
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
            rowMergerView.ColumnHeadersHeight = MergeDataGridViewConsts.DefaultColumnHeight;
            rowMergerView.AllowUserToAddRows = false;
            rowMergerView.AllowUserToDeleteRows = false;
            rowMergerView.AllowUserToResizeColumns = false;
            rowMergerView.AllowUserToResizeRows = false;
            rowMergerView.BackgroundColor = SystemColors.Control;
            rowMergerView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            rowMergerView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
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
        public static void InitColumns(this MergeDataGridView rowMergerView, IList<MdgvInfo> cols, ImageList imageList = null)
        {
            rowMergerView.Columns.Clear();
            foreach (var col in cols.OrderBy(o => o.Order))
            {
                DataGridViewColumn colBase = null;
                if (col.ColumnType == 1)
                {
                    colBase = new DataGridViewTextBoxColumn
                    {
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        HeaderText = col.Text,
                        DataPropertyName = col.FieldName,
                        Name = col.FieldName,
                        SortMode = DataGridViewColumnSortMode.NotSortable,
                        Visible = col.Visible,
                    };
                }
                else if (col.ColumnType == 2)
                {
                    colBase = new DataGridViewImageColumn
                    {
                        AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                        HeaderText = col.Text,
                        DataPropertyName = col.FieldName,
                        Name = col.FieldName,
                        SortMode = DataGridViewColumnSortMode.NotSortable,
                        Visible = col.Visible,
                        ImageLayout = DataGridViewImageCellLayout.Zoom,
                    };
                }
                if (col.Width > 0)
                {
                    colBase.FillWeight = col.Width;
                }
                else
                {
                    colBase.Width = 0;
                }
                if (col.MergeCell)
                {
                    rowMergerView.MergeCells.Add(col.FieldName);
                }
                if (col.MergeHeader != null)
                {
                    rowMergerView.AddMergeHeader(col.Order, col.MergeHeader.SpanColumn, col.MergeHeader.SpanHeader);
                }
                if (col.RowButtons != null)
                {
                    colBase.ReadOnly = true;
                    foreach (var item in col.RowButtons)
                    {
                        rowMergerView.AddRowButton(col.Order, item);
                    }
                }
                if (col.IsPrimaryKey)
                {
                    rowMergerView.Key = col.FieldName;
                }
                rowMergerView.Columns.Add(colBase);
            }
            rowMergerView.SetImageList(imageList);
        }
    }
}
