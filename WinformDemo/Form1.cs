using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using UserControlSamples.Extensions;
using UserControlSamples.Models;
using UserControlSamples.UcEventArgs;

namespace WinformDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly DataTable DefaultDataTable = new DataTable();

        private readonly IList<MdgvInfo> DefaultColumns = new List<MdgvInfo>()
        {
            new MdgvInfo(){FieldName="key",Order=0,Width=0,Text="主键",Visible=false,IsPrimaryKey=true},
            new MdgvInfo(){FieldName="city",Order=1,Width=150,Text="城市",MergeCell=true},
            new MdgvInfo(){FieldName="icon",Order=2,Width=150,Text="图标",ColumnType=2,MergeCell=true},
            new MdgvInfo(){FieldName="month",Order=3,Width=150,Text="月份"},
            new MdgvInfo(){FieldName="man",Order=4,Width=150,Text="男",MergeCell=true,
                MergeHeader=new MdgvHeaderInfo(){ SpanColumn=2,SpanHeader="人数"}},
            new MdgvInfo(){FieldName="women",Order=5,Width=150,Text="女",},
            new MdgvInfo(){FieldName="operation",Order=6,Width=160,Text="操作",
                RowButtons=new List<MdgvRowButtonInfo>(){
                    new MdgvRowButtonInfo()
                    {
                        Key="btnCopyLine",
                        Text="复制",
                        Order=0,
                    },
                    new MdgvRowButtonInfo()
                    {
                        Key="btnDelLine",
                        Text="删行",
                        Order=1,
                    },
                } },
        };

        private void InitRmv()
        {
            rowMergeView1.DefaultStyle();//设置样式
            rowMergeView1.InitColumns(DefaultColumns);//设置栏目

            /**
             * 城市主表:
             * |key|city| 
             * |:-|:-|
             * |1|南京|
             * |2|上海|
             * 
             * 人数表
             * |key|month|sex|num|
             * |:-|:-|:-|:-|
             * |1|一月|男|610|
             * |1|一月|女|400|
             * |1|二月|男|610|
             * |1|二月|女|420|
             * |2|二月|男|610|
             * |2|二月|女|710|
             * */
            DefaultDataTable.Columns.AddRange(
                new[] {
                new DataColumn("key", typeof(int)),
                new DataColumn("icon", typeof(object)),
                new DataColumn("city", typeof(string)),
                new DataColumn("month", typeof(string)),
                new DataColumn("man", typeof(string)), 
                new DataColumn("women", typeof(string)), });
            var newRow = DefaultDataTable.NewRow();
            newRow["key"] =1;
            newRow["month"] = "一月";
            newRow["icon"] = Properties.Resources.apple;
            newRow["city"] = "南京";
            newRow["man"] = "610";
            newRow["women"] = "400";
            DefaultDataTable.Rows.Add(newRow);
            newRow = DefaultDataTable.NewRow();
            newRow["key"] = 1;
            newRow["month"] = "二月";
            newRow["icon"] = Properties.Resources.apple;
            newRow["city"] = "南京";
            newRow["man"] = "610";
            newRow["women"] = "420";
            DefaultDataTable.Rows.Add(newRow);
            newRow = DefaultDataTable.NewRow();
            newRow["key"] = 2;
            newRow["month"] = "二月";
            newRow["icon"] = Properties.Resources.螃蟹;
            newRow["city"] = "上海";
            newRow["man"] = "610";
            newRow["women"] = "710";
            DefaultDataTable.Rows.Add(newRow);
            rowMergeView1.DataSource = DefaultDataTable;
          
        }

        private void rowMergeView1_OnMultiButton(McvRowButtonEventArgs e)
        {
            MessageBox.Show($"第{e.RowIndex}行,第{e.ColumnIndex}列,名称:{e.ButtonText},按钮:{e.ButtonKey}");
            switch(e.ButtonKey)
            {
                case "btnDelLine":
                    rowMergeView1.DeleteRowButton(e.RowIndex);
                    break;
                case "btnCopyLine":
                    CopyLine(e.RowIndex);
                    break;
            }           
        }

        private void CopyLine(int rowIndex)
        {
            var copyRow = DefaultDataTable.Rows[rowIndex].ItemArray.Clone();
            var index = DefaultDataTable.Rows.Count;
            var newRow = DefaultDataTable.NewRow();
            newRow.ItemArray = copyRow as object[];
            newRow["key"] =index+1 ;
            DefaultDataTable.Rows.InsertAt(newRow, index);
        }
        FormWindowState tempWindowState;
        private void Form1_SizeChanged(object sender, EventArgs e)
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitRmv();
        }
    }
}
