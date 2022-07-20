using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserControlSamples.Extensions;
using UserControlSamples.Models;

namespace UserControlSamples.UI
{
    public partial class FrmManager : Form
    {
        public FrmManager()
        {
            InitializeComponent();
        }

        private readonly IList<RmvInfo> DefaultColumns = new List<RmvInfo>()
        {
            new RmvInfo(){FieldName="month",Order=1,Width=150,Text="月份",Visible=true},
            new RmvInfo(){FieldName="city",Order=0,Width=150,Text="城市",Visible=true,Merge=true},
            new RmvInfo(){FieldName="man",Order=2,Width=150,Text="男",Visible=true,Span=new RmvSpanInfo(){ SpanColumn=2,SpanHeader="人数"}},
            new RmvInfo(){FieldName="women",Order=3,Width=150,Text="女",Visible=true,},
        };

        FormWindowState tempWindowState;
        private void FrmManager_Shown(object sender, EventArgs e)
        {
            //从数据库加载控件
            //cardManagerUc1.LoadCard();
            //cardManagerUc2.LoadCard();
            //tagManagerUc1.LoadTag();
            InitRmv();
        }

        private void InitRmv()
        {
            rowMergeView1.DefaultStyle();
            rowMergeView1.InitColumns(DefaultColumns);
            var dt = new DataTable();
            dt.Columns.AddRange(new [] { new DataColumn("city", typeof(string)), new DataColumn("month", typeof(string)), new DataColumn("man", typeof(string)), new DataColumn("women", typeof(string)), });
            var newRow = dt.NewRow();
            newRow["month"] = "一月";
            newRow["city"] = "南京";
            newRow["man"] = "600";
            newRow["women"] = "400";
            dt.Rows.Add(newRow);
            newRow = dt.NewRow();
            newRow["month"] = "二月";
            newRow["city"] = "南京";
            newRow["man"] = "610";
            newRow["women"] = "420";
            dt.Rows.Add(newRow);
            newRow = dt.NewRow();
            newRow["month"] = "二月";
            newRow["city"] = "上海";
            newRow["man"] = "810";
            newRow["women"] = "710";
            dt.Rows.Add(newRow);
            rowMergeView1.DataSource = dt;

        }

        private void FrmManager_SizeChanged(object sender, EventArgs e)
        {
            if (tempWindowState != FormWindowState.Maximized && this.WindowState == FormWindowState.Maximized)
            {
                tempWindowState = FormWindowState.Maximized;
                cardManagerUc1.RefreshLayout();
                tagManagerUc1.RefreshLayout();
            }
            else if (tempWindowState == FormWindowState.Maximized && this.WindowState == FormWindowState.Normal)
            {
                tempWindowState = FormWindowState.Normal;
                cardManagerUc1.RefreshLayout();
                tagManagerUc1.RefreshLayout();
            }
        }
    }
}
