using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserControlSamples.Consts;

namespace UserControlSamples.UI.UserControls
{
    public partial class RowMergeView : DataGridView
    {
        public RowMergeView()
        {
            InitializeComponent();
        }

        public RowMergeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowCount != 0)
            {
                for (int i = 0; i < e.RowCount; i++)
                {
                    Rows[e.RowIndex + i].HeaderCell.Value = (e.RowIndex + i + 1).ToString();
                }
                for (int i = e.RowIndex + e.RowCount; i < this.Rows.Count; i++)
                {
                    Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            base.OnRowsAdded(e);
        }

        #region 重写事件

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == -1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                e.Handled = true;
            }
            else if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                DrawCell(e);
            }
            else
            {
                if (e.RowIndex == -1)
                {
                    //需要合并
                    if (SpanHeaders.ContainsKey(e.ColumnIndex))
                    {
                        var g = e.Graphics;
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                        var left = e.CellBounds.Left;
                        var top = e.CellBounds.Top + 2;
                        var right = e.CellBounds.Right;
                        var bottom = e.CellBounds.Bottom;

                        switch (SpanHeaders[e.ColumnIndex].Position)
                        {
                            case 1:
                                left += 2;
                                break;
                            case 2:
                                break;
                            case 3:
                                right -= 2;
                                break;
                        }
                        //画上半部分底色
                        g.FillRectangle(new SolidBrush(e.CellStyle.BackColor), left, top, right - left, (bottom - top) / 2);

                        //画中线
                        g.DrawLine(new Pen(GridColor, Columns[e.ColumnIndex].DividerWidth), left, (top + bottom) / 2, right, (top + bottom) / 2);

                        //写标题
                        var sf = new StringFormat();
                        sf.Alignment = StringAlignment.Center;
                        sf.LineAlignment = StringAlignment.Center;

                        var fontBrush = new SolidBrush(e.CellStyle.ForeColor);
                        //列标题
                        g.DrawString(e.Value + "", e.CellStyle.Font, fontBrush, new Rectangle(left, (top + bottom) / 2, right - left, (bottom - top) / 2), sf);

                        left = GetColumnDisplayRectangle(SpanHeaders[e.ColumnIndex].Left, true).Left - 2;
                        if (left < 0)
                        {
                            left = GetCellDisplayRectangle(-1, -1, true).Width;
                        }

                        right = GetColumnDisplayRectangle(SpanHeaders[e.ColumnIndex].Right, true).Right - 2;
                        if (right < 0)
                        {
                            right = Width;
                        }
                        //一级标题
                        g.DrawString(SpanHeaders[e.ColumnIndex].Text, e.CellStyle.Font, fontBrush, new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                        e.Handled = true;
                    }
                }
            }

            base.OnCellPainting(e);
        }

        #endregion

        #region 画单元格
        /// <summary>
        /// 画单元格
        /// </summary>
        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            var name = Columns[e.ColumnIndex].Name;
            if (!MergeColumnNames.Contains(name) && e.RowIndex != -1) return;

            var currentVal = e.Value == null ? "" : e.Value.ToString().Trim();
            if (string.IsNullOrEmpty(currentVal)) return;

            var upRows = 0;
            var downRows = 0;
            var cellWidth = e.CellBounds.Width;

            //获取下面的行
            for (var i = e.RowIndex; i < Rows.Count; i++)
            {
                if (!Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(currentVal)) break;
                downRows++;
                if (e.RowIndex != i)
                {
                    var currentWidth = Rows[i].Cells[e.ColumnIndex].Size.Width;
                    cellWidth = cellWidth < currentWidth ? cellWidth : currentWidth;
                }
            }
            //获取上面的行
            for (var i = e.RowIndex - 1; i >= 0; i--)
            {
                if (!Rows[i].Cells[e.ColumnIndex].Value.ToString().Equals(currentVal)) break;
                upRows++;
                if (e.RowIndex != i)
                {
                    var currentWidth = Rows[i].Cells[e.ColumnIndex].Size.Width;
                    cellWidth = cellWidth < currentWidth ? cellWidth : currentWidth;
                }
            }
            int count = downRows + upRows;
            if (count < 2)
            {
                return;
            }

            var gridBrush = new SolidBrush(GridColor);
            var backBrush = new SolidBrush(Color.White);
            var gridLinePen = new Pen(gridBrush) { DashStyle = System.Drawing.Drawing2D.DashStyle.Solid };

            //选择的时候
            //if (Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
            //{
            //    backBrush.Color = Color.White;
            //    //e.Graphics.FillRectangle(backBrush, e.CellBounds.X,upRows* e.CellBounds.Top, e.CellBounds.Width, e.CellBounds.Height*count);
            //}

            //填充背景
            e.Graphics.FillRectangle(backBrush, e.CellBounds);
            //画字符串
            PaintingFont(e, cellWidth, upRows, downRows, count);
            if (downRows == 1)
            {
                //下线
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                count = 0;
            }
            //右线
            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
            e.Handled = true;
        }

        private void PaintingFont(DataGridViewCellPaintingEventArgs e, int cellWidth, int upRows, int downRows, int count)
        {
            var currentVal = e.Value.ToString();
            var fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            var fontSize = e.Graphics.MeasureString(currentVal, e.CellStyle.Font);
            var fontHeight = fontSize.Height;
            var fontWidth = fontSize.Width;
            int cellHeight = e.CellBounds.Height;
            switch (e.CellStyle.Alignment)
            {
                case DataGridViewContentAlignment.BottomLeft:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y + cellHeight * downRows - fontHeight);
                    break;
                case DataGridViewContentAlignment.BottomCenter:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellWidth - fontWidth) / 2, e.CellBounds.Y + cellHeight * downRows - fontHeight);
                    break;
                case DataGridViewContentAlignment.BottomRight:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellWidth - fontWidth, e.CellBounds.Y + cellHeight * downRows - fontHeight);
                    break;
                case DataGridViewContentAlignment.MiddleLeft:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellHeight * upRows + (cellHeight * count - fontHeight) / 2);
                    break;
                case DataGridViewContentAlignment.MiddleCenter:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellWidth - fontWidth) / 2, e.CellBounds.Y - cellHeight * upRows + (cellHeight * count - fontHeight) / 2);
                    break;
                case DataGridViewContentAlignment.MiddleRight:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellWidth - fontWidth, e.CellBounds.Y - cellHeight * upRows + (cellHeight * count - fontHeight) / 2);
                    break;

                case DataGridViewContentAlignment.TopLeft:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X, e.CellBounds.Y - cellHeight * upRows);
                    break;
                case DataGridViewContentAlignment.TopCenter:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellWidth - fontWidth) / 2, e.CellBounds.Y - cellHeight * upRows);
                    break;
                case DataGridViewContentAlignment.TopRight:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + cellWidth - fontWidth, e.CellBounds.Y - cellHeight * upRows);
                    break;
                default:
                    e.Graphics.DrawString(currentVal, e.CellStyle.Font, fontBrush, e.CellBounds.X + (cellWidth - fontWidth) / 2, e.CellBounds.Y - cellHeight * upRows + (cellHeight * count - fontHeight) / 2);
                    break;
            }
        }
        #endregion

        #region 自定义

        private IList<string> _megeColumnNames = new List<string>();

        [MergableProperty(false)]
        [Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Localizable(true)]
        [Category(RowMergeViewConsts.Name), Description(RowMergeViewConsts.MergeColumnNamesProperty), Browsable(true)]
        public IList<string> MergeColumnNames { get { return _megeColumnNames; } }

        private Color _mergeColumnBackColor = SystemColors.Control;

        [Category(RowMergeViewConsts.Name), Description(RowMergeViewConsts.MergeColumnBackColorProperty), Browsable(true)]
        public Color MergeColumnBackColor { get { return _mergeColumnBackColor; } set { _mergeColumnBackColor = value; } }
        /// <summary>
        /// 表头
        /// </summary>
        private struct SpanInfo
        {
            /// <summary>
            /// 文本
            /// </summary>
            public string Text { get; set; }

            /// <summary>
            /// 位置
            /// 1:左
            /// 2：中
            /// 3：右
            /// </summary>
            public int Position { get; set; }

            public int Left { get; set; }

            public int Right { get; set; }

            public SpanInfo(string text, int position, int left, int right)
            {
                this.Text = text;
                this.Position = position;
                this.Left = left;
                this.Right = right;
            }
        }

        private IDictionary<int, SpanInfo> SpanHeaders = new Dictionary<int, SpanInfo>();

        /// <summary>
        /// 合并列
        /// </summary>
        /// <param name="colIndex">列索引</param>
        /// <param name="colCount">需要合并的列数</param>
        /// <param name="text">合并后的文本</param>
        public void AddSpanHeader(int colIndex, int colCount, string text)
        {
            //if (colCount < 2) return;
            //当前列的最后一列索引
            int right = colIndex + colCount - 1;
            //当前列
            SpanHeaders[colIndex] = new SpanInfo(text, 1, colIndex, right);
            //最右列
            SpanHeaders[right] = new SpanInfo(text, 3, colIndex, right);
            //中间列
            for (var i = colIndex + 1; i < right; i++)
            {
                SpanHeaders[i] = new SpanInfo(text, 2, colIndex, right);
            }
        }

        /// <summary>
        /// 清除列合并
        /// </summary>
        public void ClearSpan()
        {
            SpanHeaders.Clear();
        }

        public void ReDrawHead()
        {
            foreach (var col in SpanHeaders.Keys)
            {
                Invalidate(GetCellDisplayRectangle(col, -1, true));
            }
        }
        #endregion
    }
}
