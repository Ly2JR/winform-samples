using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using UserControlSamples.Consts;
using UserControlSamples.Models;
using UserControlSamples.UcEventArgs;
using UserControlSamples.Utility;

namespace UserControlSamples.UI.UserControls
{
    public partial class MergeDataGridView : DataGridView
    {
        public delegate void OnRowButtonHandler(McvRowButtonEventArgs e);

        [Category(MergeDataGridViewConsts.Name), Description(MergeDataGridViewConsts.RowButtonEvent)]
        public event OnRowButtonHandler OnRowButton;

        /// <summary>
        /// 行按钮间距
        /// </summary>
        private const int RowButtonPadding = 1;
        /// <summary>
        /// 合并表头背景颜色
        /// </summary>
        private Color _mergeHeaderBackColor = SystemColors.Control;
        /// <summary>
        /// 合并相同单元格
        /// </summary>
        private IList<string> _mergeCells = new List<string>();
        /// <summary>
        /// 行按钮图片
        /// </summary>
        private ImageList _rowButtonImageList;
        /// <summary>
        /// 行按钮缓存
        /// </summary>
        private readonly IDictionary<RowButonInfo, Button> _rowButtonCache = new Dictionary<RowButonInfo, Button>();
        /// <summary>
        /// 合并表头管理
        /// </summary>
        private readonly IDictionary<int, HeaderInfo> MergeHeaders = new Dictionary<int, HeaderInfo>();
        /// <summary>
        /// 行按钮管理
        /// </summary>
        private readonly IDictionary<int, List<RowButonInfo>> RowButtons = new Dictionary<int, List<RowButonInfo>>();
        /// <summary>
        /// 合并图片缓存
        /// </summary>
        private readonly IDictionary<string, Image> ImageCache = new Dictionary<string, Image>();

        [Category(MergeDataGridViewConsts.Name), Description(MergeDataGridViewConsts.KeyProperty)]
        public string Key { get; set; }

        [Category(MergeDataGridViewConsts.Name), Description(MergeDataGridViewConsts.MergeCellProperty)]
        public IList<string> MergeCells { get { return _mergeCells; } }

        [Category(MergeDataGridViewConsts.Name), Description(MergeDataGridViewConsts.MergeColumnBackColorProperty)]
        public Color MergeHeaderBackColor { get { return _mergeHeaderBackColor; } set { _mergeHeaderBackColor = value; } }


        public MergeDataGridView()
        {
            InitializeComponent();
            DoubleBuffered = true;  
        }

        public MergeDataGridView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            //禁用最左边列有个三角箭头
            if (e.RowIndex >= 0 && e.ColumnIndex == -1)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.ContentBackground);
                e.Handled = true;
            }
            else if (e.RowIndex > -1 && e.ColumnIndex > -1)
            {
                var name = Columns[e.ColumnIndex].Name;

                if (MergeCells.Contains(name) && e.RowIndex != -1) //合并单元格
                {
                    DrawCell(e);
                }
                else if (RowButtons.ContainsKey(e.ColumnIndex) && !_rowButtonCache.Keys.Any(o => o.RowIndex == e.RowIndex && o.ColumnIndex == e.ColumnIndex)) //多按钮
                {
                    DrawButton(e);
                }
            }
            else
            {
                //合并栏目
                if (e.RowIndex == -1)
                {
                    if (MergeHeaders.ContainsKey(e.ColumnIndex))
                    {
                        var g = e.Graphics;
                        e.Paint(e.CellBounds, DataGridViewPaintParts.Background | DataGridViewPaintParts.Border);

                        var left = e.CellBounds.Left;
                        var top = e.CellBounds.Top + 2;
                        var right = e.CellBounds.Right;
                        var bottom = e.CellBounds.Bottom;

                        switch (MergeHeaders[e.ColumnIndex].Position)
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

                        left = GetColumnDisplayRectangle(MergeHeaders[e.ColumnIndex].Left, true).Left - 2;
                        if (left < 0)
                        {
                            left = GetCellDisplayRectangle(-1, -1, true).Width;
                        }

                        right = GetColumnDisplayRectangle(MergeHeaders[e.ColumnIndex].Right, true).Right - 2;
                        if (right < 0)
                        {
                            right = Width;
                        }
                        g.DrawString(MergeHeaders[e.ColumnIndex].Text, e.CellStyle.Font, fontBrush, new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                        e.Handled = true;
                    }
                }
            }

            base.OnCellPainting(e);
        }

        private void DrawCell(DataGridViewCellPaintingEventArgs e)
        {
            var currentVal = Convert.ToString(e.Value).Trim();
            if (string.IsNullOrEmpty(currentVal)) return;
            var currentKey = "";
            if (!string.IsNullOrEmpty(Key))
            {
                currentKey = Rows[e.RowIndex].Cells[Key].Value.ToString();
            }

            var upRows = 0;
            var downRows = 0;
            var cellWidth = e.CellBounds.Width;

            //获取下面的行
            for (var i = e.RowIndex; i < Rows.Count; i++)
            {
                var nextKeyVal = "";
                if (!string.IsNullOrEmpty(Key))
                {
                    nextKeyVal = Rows[i].Cells[Key].Value.ToString();
                }
                var nextVal = Convert.ToString(Rows[i].Cells[e.ColumnIndex].Value);
                if (nextVal.Equals(currentVal) && nextKeyVal.Equals(currentKey))
                {
                    downRows++;
                    if (e.RowIndex != i)
                    {
                        var currentWidth = Rows[i].Cells[e.ColumnIndex].Size.Width;
                        cellWidth = cellWidth < currentWidth ? cellWidth : currentWidth;
                    }
                }
            }
            //获取上面的行
            for (var i = e.RowIndex - 1; i >= 0; i--)
            {
                var upKeyVal = "";
                if (!string.IsNullOrEmpty(Key))
                {
                    upKeyVal = Rows[i].Cells[Key].Value.ToString();
                }
                var upVal = Convert.ToString(Rows[i].Cells[e.ColumnIndex].Value);
                if (upVal.Equals(currentVal) && upKeyVal.Equals(currentKey))
                {
                    upRows++;
                    if (e.RowIndex != i)
                    {
                        var currentWidth = Rows[i].Cells[e.ColumnIndex].Size.Width;
                        cellWidth = cellWidth < currentWidth ? cellWidth : currentWidth;
                    }
                }
            }
            int count = downRows + upRows;
            if (count < 2)
            {
                return;
            }

            var gridBrush = new SolidBrush(GridColor);
            var backBrush = new SolidBrush(Color.White);
            var gridLinePen = new Pen(gridBrush) { DashStyle = DashStyle.Solid };

            //选择的时候
            //if (Rows[e.RowIndex].Cells[e.ColumnIndex].Selected)
            //{
            //    backBrush.Color = Color.White;
            //    //e.Graphics.FillRectangle(backBrush, e.CellBounds.X,upRows* e.CellBounds.Top, e.CellBounds.Width, e.CellBounds.Height*count);
            //}

            e.Graphics.FillRectangle(backBrush, e.CellBounds);
            Painting(e, cellWidth, upRows, downRows, count);
            if (downRows == 1)
            {
                e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                count = 0;
            }

            e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
            e.Handled = true;
        }

       

        private void DrawButton(DataGridViewCellPaintingEventArgs e)
        {
            //if (!MultiButtonHeaders.ContainsKey(e.ColumnIndex)) return;
            var btns = RowButtons[e.ColumnIndex];
            var ret = GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var btnWidth = (ret.Width - (btns.Count + 1) * RowButtonPadding) / btns.Count;
            for (var i = 0; i < btns.Count; i++)
            {
                var key = btns[i];
                key.RowIndex = e.RowIndex;
                key.ColumnIndex = e.ColumnIndex;
                //if (_buttonManager.ContainsKey(key)) break;
                var tag = key.Clone() as RowButonInfo;
                var btn = new Button()
                {
                    Size = new Size(btnWidth, ret.Height - 2),
                    Location = new Point(ret.Left + i * btnWidth + (i + 1) * RowButtonPadding, ret.Top + RowButtonPadding),
                    Name = $"{key}",
                    Text = btns[i].Text,
                    Tag = tag,
                    BackColor = Color.White,
                    ImageAlign = ContentAlignment.MiddleRight,
                    TextAlign = ContentAlignment.MiddleCenter,
                };
                //btn.FlatAppearance.MouseOverBackColor = _mergeColumnBackColor;
                if (!string.IsNullOrEmpty(btns[i].ImageKey) && _rowButtonImageList != null)
                {
                    btn.TextImageRelation = TextImageRelation.ImageBeforeText;
                    btn.ImageList = _rowButtonImageList;
                    btn.ImageKey = btns[i].ImageKey;
                }
                btn.Click += Btn_Click;
                _rowButtonCache.Add(tag, btn);
                this.Controls.Add(btn);
            }
        }

        /// <summary>
        /// 显示左行号
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowsAdded(DataGridViewRowsAddedEventArgs e)
        {
            if (e.RowCount == 0) return;
            for (var i =0; i < e.RowCount; i++)
            {
                var index = RowCount - e.RowCount + i;
                var indexValue = RowCount-e.RowCount + i+1;
                Rows[index].HeaderCell.Value = indexValue.ToString();
            }
            base.OnRowsAdded(e);
        }

        /// <summary>
        /// 显示左行号
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRowsRemoved(DataGridViewRowsRemovedEventArgs e)
        {
            if (RowCount > 0)
            {
                for (var i = e.RowIndex + e.RowCount-1; i < Rows.Count; i++)
                {
                    Rows[i].HeaderCell.Value = (i + 1).ToString();
                }
            }
            base.OnRowsRemoved(e);
        }

        protected override void OnDataSourceChanged(EventArgs e)
        {
            if (DataSource == null)
            {
                ClearMergeHeader();
                ClearButton();
            }
            base.OnDataSourceChanged(e);
        }

        /// <summary>
        /// 删除行按钮
        /// </summary>
        /// <param name="rowIndex"></param>
        public void DeleteRowButton(int rowIndex)
        {
            for (var i = 0; i < _rowButtonCache.Count; i++)
            {
                var btn = _rowButtonCache.ElementAt(i);
                if (btn.Key.RowIndex == rowIndex)
                {
                    this.Controls.Remove(btn.Value);
                }
                else if (btn.Key.RowIndex > rowIndex)
                {
                    btn.Key.RowIndex--;
                    btn.Value.Top -= btn.Value.Height + 2;
                }
            }
            Rows.RemoveAt(rowIndex);
        }

        /// <summary>
        /// 清空所有行按钮
        /// </summary>
        /// <param name="all"></param>
        public void ClearButton()
        {
            for (var i = Controls.Count - 1; i > 0; i--)
            {
                if (Controls[i] is Button)
                {
                    Controls.RemoveAt(i);
                }
            }
            _rowButtonCache.Clear();
        }

        /// <summary>
        /// 设置行按钮状态
        /// </summary>
        /// <param name="enable">true:启用,false:禁用</param>
        public void SetButtonEnable(bool enable)
        {
            foreach (var btn in _rowButtonCache)
            {
                btn.Value.Enabled = enable;
            }
        }

        /// <summary>
        /// 设置行按钮状态
        /// </summary>
        /// <param name="key"></param>
        /// <param name="enable">true:启用,false:禁用</param>
        public void SetButtonEnable(RowButonInfo key, bool enable)
        {
            if (!_rowButtonCache.ContainsKey(key)) return;
            _rowButtonCache[key].Enabled = enable;
        }

        /// <summary>
        /// 按钮按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            var tag = btn.Tag as RowButonInfo;
            if (tag == null) return;
            OnFireRowButton(new McvRowButtonEventArgs()
            {
                RowIndex = tag.RowIndex,
                ColumnIndex = tag.ColumnIndex,
                ButtonText = tag.Text,
                ButtonKey = tag.Key
            });
        }

        private void OnFireRowButton(McvRowButtonEventArgs e)
        {
            if (OnRowButton != null)
            {
                OnRowButton(e);
            }
        }

        private void Painting(DataGridViewCellPaintingEventArgs e, int cellWidth, int upRows, int downRows, int count)
        {
            var column = Columns[e.ColumnIndex];
            var fontBrush = new SolidBrush(e.CellStyle.ForeColor);
            int cellHeight = e.CellBounds.Height;
            if (column is DataGridViewTextBoxColumn)
            {
                var currentVal = e.Value.ToString();
                var fontSize = e.Graphics.MeasureString(currentVal, e.CellStyle.Font);
                var fontHeight = fontSize.Height;
                var fontWidth = fontSize.Width;
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
            else if (column is DataGridViewImageColumn)
            {
                var keyVal = $"{Rows[e.RowIndex].Cells[Key]}{column.DataPropertyName}";
                var x = e.CellBounds.X + 1;
                var y = e.CellBounds.Y - cellHeight * upRows + 1;
                if (ImageCache.ContainsKey(keyVal))
                {
                    var zoomImage = ImageCache[keyVal];
                    e.Graphics.DrawImage(zoomImage, x, y);
                }
                else
                {
                    var cell = Rows[e.RowIndex].Cells[e.ColumnIndex];
                    var val = cell.Value as Bitmap;
                    if (val == null) return;
                    var w = e.CellBounds.Width - 4;
                    var h = cellHeight * count - 4;
                    var zoomImage = ImageHelper.ZoomImage(val, h, w);
                    ImageCache.Add(keyVal, zoomImage);
                    e.Graphics.DrawImage(zoomImage, x, y);
                }
            }
        }

      

        public void ClearImage()
        {
            ImageCache.Clear();
        }



        /// <summary>
        /// 合并表头
        /// </summary>
        private struct HeaderInfo
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

            public HeaderInfo(string text, int position, int left, int right)
            {
                this.Text = text;
                this.Position = position;
                this.Left = left;
                this.Right = right;
            }
        }

        /// <summary>
        /// 添加合并表头
        /// </summary>
        /// <param name="colIndex">行索引</param>
        /// <param name="colCount">合并个数</param>
        /// <param name="text">显示内容</param>

        public void AddMergeHeader(int colIndex, int colCount, string text)
        {
            int right = colIndex + colCount - 1;

            MergeHeaders[colIndex] = new HeaderInfo(text, 1, colIndex, right);

            MergeHeaders[right] = new HeaderInfo(text, 3, colIndex, right);

            for (var i = colIndex + 1; i < right; i++)
            {
                MergeHeaders[i] = new HeaderInfo(text, 2, colIndex, right);
            }
        }

        /// <summary>
        /// 添加行按
        /// </summary>
        /// <param name="colIndex">列索引</param>
        /// <param name="row">按钮信息</param>
        public void AddRowButton(int colIndex, RowButonInfo row)
        {
            if (!RowButtons.ContainsKey(colIndex))
            {
                RowButtons.Add(colIndex, new List<RowButonInfo>() { row });
            }
            else
            {
                RowButtons[colIndex].Add(row);
            }
        }

        /// <summary>
        /// 清空合并表头
        /// </summary>
        public void ClearMergeHeader()
        {
            MergeHeaders.Clear();
        }

        /// <summary>
        /// 重绘
        /// </summary>
        /// <param name="colIndex"></param>
        /// <param name="rowIndex"></param>
        public void ReDraw(int colIndex, int rowIndex)
        {
            Invalidate(GetCellDisplayRectangle(colIndex, rowIndex, true));
        }

        /// <summary>
        /// 按钮图标
        /// </summary>
        /// <param name="imageList"></param>
        public void SetImageList(ImageList imageList)
        {
            this._rowButtonImageList = imageList;
        }
    }
}
