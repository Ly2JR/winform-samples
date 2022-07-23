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

namespace UserControlSamples.UI.UserControls
{
    public partial class RowMergeView : DataGridView
    {
        public delegate void OnRowButtonHandler(RmvRowButtonEventArgs e);

        [Category(RowMergeViewConsts.Name), Description(RowMergeViewConsts.RowButtonEvent)]
        public event OnRowButtonHandler OnRowButton;

        private ImageList _rowButtonImageList;

        /// <summary>
        /// 多表关联时,主表的主键
        /// </summary>
        public string Key { get; set; }

        private IDictionary<RowButonInfo, Button> _rowButtonManager = new Dictionary<RowButonInfo, Button>();

        public RowMergeView()
        {
            InitializeComponent();
            DoubleBuffered = true;
            //SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint|ControlStyles.Selectable, true);
            //UpdateStyles();   
        }

        public RowMergeView(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }


        #region 重写事件

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

                if (SpanRows.Contains(name) && e.RowIndex != -1) //合并单元格
                {
                    DrawCell(e);
                }
                else if (MultiButtonHeaders.ContainsKey(e.ColumnIndex) && !_rowButtonManager.Keys.Any(o => o.RowIndex == e.RowIndex && o.ColumnIndex == e.ColumnIndex)) //多按钮
                {
                    DrawButton(e);
                }
            }
            else
            {
                //合并栏目
                if (e.RowIndex == -1)
                {
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
                        g.DrawString(SpanHeaders[e.ColumnIndex].Text, e.CellStyle.Font, fontBrush, new Rectangle(left, top, right - left, (bottom - top) / 2), sf);
                        e.Handled = true;
                    }
                }
            }

            base.OnCellPainting(e);
        }

        #endregion

        #region 画单元格

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

        private readonly int ButtonPadding = 1;

        private void DrawButton(DataGridViewCellPaintingEventArgs e)
        {
            //if (!MultiButtonHeaders.ContainsKey(e.ColumnIndex)) return;
            var btns = MultiButtonHeaders[e.ColumnIndex];
            var ret = GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
            var btnWidth = (ret.Width - (btns.Count + 1) * ButtonPadding) / btns.Count;
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
                    Location = new Point(ret.Left + i * btnWidth + (i + 1) * ButtonPadding, ret.Top + ButtonPadding),
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
                _rowButtonManager.Add(tag, btn);
                this.Controls.Add(btn);
            }
        }

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
                ClearSpan();
                //ClearImage();
                ClearButton();
            }
            base.OnDataSourceChanged(e);
        }

        public void DeleteRowButton(int rowIndex)
        {
            for (var i = 0; i < _rowButtonManager.Count; i++)
            {
                var btn = _rowButtonManager.ElementAt(i);
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

        public void ClearButton(bool all = false)
        {
            for (var i = Controls.Count - 1; i > 0; i--)
            {
                if (Controls[i] is Button)
                {
                    Controls.RemoveAt(i);
                }
            }
            _rowButtonManager.Clear();
        }

        /// <summary>
        /// 设置行按钮状态
        /// </summary>
        /// <param name="enable">true:启用,false:禁用</param>
        public void SetButtonEnable(bool enable)
        {
            foreach (var btn in _rowButtonManager)
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
            if (!_rowButtonManager.ContainsKey(key)) return;
            _rowButtonManager[key].Enabled = enable;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            var tag = btn.Tag as RowButonInfo;
            if (tag == null) return;
            OnFireRowButton(new RmvRowButtonEventArgs()
            {
                RowIndex = tag.RowIndex,
                ColumnIndex = tag.ColumnIndex,
                ButtonText = tag.Text,
                ButtonKey = tag.Key
            });
        }

        private void OnFireRowButton(RmvRowButtonEventArgs e)
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
                var cell = Rows[e.RowIndex].Cells[e.ColumnIndex];
                var val = cell.Value as Bitmap;
                if (val == null) return;
                var w = e.CellBounds.Width - 4;
                var h = cellHeight * count - 4;
                var zoomVal = ZoomImage(val, h, w);
                //TempImageCache.Add(keyVal, zoomVal);
                e.Graphics.DrawImage(zoomVal, x, y);
            }
        }

        //public bool ImageCache = false;
        //private IDictionary<string, Image> TempImageCache = new Dictionary<string, Image>();

        //public void ClearImage()
        //{
        //    TempImageCache.Clear();
        //}
        #endregion

        private Bitmap ZoomImage(Bitmap bitmap, int destHeight, int destWidth)
        {
            try
            {
                Bitmap sourImage = new Bitmap(bitmap);
                int width = 0, height = 0;
                //按比例缩放           
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                var destBitmap = new Bitmap(destWidth, destHeight);
                using (var g = Graphics.FromImage(destBitmap))
                {
                    g.Clear(Color.Transparent);
                    //设置画布的描绘质量         
                    g.CompositingQuality = CompositingQuality.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(sourImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                }
                //设置压缩质量     
                var encoderParams = new EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                var encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }
        #region 自定义

        private IList<string> _spanRows = new List<string>();

        [Category(RowMergeViewConsts.Name), Description(RowMergeViewConsts.MergeColumnNamesProperty), Browsable(true)]
        public IList<string> SpanRows { get { return _spanRows; } }

        private Color _mergeColumnBackColor = SystemColors.Control;

        [Category(RowMergeViewConsts.Name), Description(RowMergeViewConsts.MergeColumnBackColorProperty), Browsable(true)]
        public Color MergeColumnBackColor { get { return _mergeColumnBackColor; } set { _mergeColumnBackColor = value; } }

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

        private IDictionary<int, List<RowButonInfo>> MultiButtonHeaders = new Dictionary<int, List<RowButonInfo>>();

        public void AddSpanHeader(int colIndex, int colCount, string text)
        {
            int right = colIndex + colCount - 1;

            SpanHeaders[colIndex] = new SpanInfo(text, 1, colIndex, right);

            SpanHeaders[right] = new SpanInfo(text, 3, colIndex, right);

            for (var i = colIndex + 1; i < right; i++)
            {
                SpanHeaders[i] = new SpanInfo(text, 2, colIndex, right);
            }
        }

        public void AddMultiButtonColumn(int colIndex, RowButonInfo row)
        {
            if (!MultiButtonHeaders.ContainsKey(colIndex))
            {
                MultiButtonHeaders.Add(colIndex, new List<RowButonInfo>() { row });
            }
            else
            {
                MultiButtonHeaders[colIndex].Add(row);
            }
        }

        public void ClearSpan()
        {
            SpanHeaders.Clear();
        }

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
        #endregion
    }
}
