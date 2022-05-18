using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Text;

namespace PIS_Sys.blzd
{
    public partial class Frm_qtJcbg : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_qtJcbg()
        {
            InitializeComponent();
        }
        public static string patientid = "";
        private void Frm_qtJcbg_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl3.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl4.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
        }

        private void Frm_qtJcbg_Activated(object sender, EventArgs e)
        {
            textBoxX1.Text = patientid;
            buttonX1.PerformClick();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (!textBoxX1.Text.Trim().Equals(""))
            {


            }
        }
        //检查信息
        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                this.richTextBoxEx1.Text = "";
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl1.PrimaryGrid.Rows.Clear();
                    superGridControl1.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                string exam_no = Row.Cells[0].Value.ToString();
                //获取检查报告信息 description,impression,memo,recommendation
                DataTable dtExamReport = null;
                if (dtExamReport != null)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("[description]=====================================");
                    sb.AppendLine(dtExamReport.Rows[0]["description"].ToString());
                    sb.AppendLine("[impression]=====================================");
                    sb.AppendLine(dtExamReport.Rows[0]["impression"].ToString());
                    sb.AppendLine("[memo]=====================================");
                    sb.AppendLine(dtExamReport.Rows[0]["memo"].ToString());
                    sb.AppendLine("[recommendation]=====================================");
                    sb.AppendLine(dtExamReport.Rows[0]["recommendation"].ToString());
                    this.richTextBoxEx1.Text = sb.ToString();
                    sb.Clear();
                    sb = null;
                }
            }
        }

        private void superGridControl3_RowClick(object sender, GridRowClickEventArgs e)
        {
            if (superGridControl3.PrimaryGrid.Rows.Count > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl3.PrimaryGrid.Rows.Clear();
                    superGridControl3.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl3.PrimaryGrid.Rows[index];
                //申请单号  
                string test_no = Row.Cells[0].Value.ToString();
                DataTable dtReportList = null;
                superGridControl2.PrimaryGrid.DataSource = dtReportList;
                //明细清空
                superGridControl4.PrimaryGrid.DataSource = null;
            }
        }

        private void superGridControl2_RowClick(object sender, GridRowClickEventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {

                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl2.PrimaryGrid.Rows.Clear();
                    superGridControl2.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl2.PrimaryGrid.Rows[index];
                //申请单号  
                string test_no = Row.Cells[0].Value.ToString();
                //编号
                string itme_no = Row.Cells[1].Value.ToString();
                DataTable dtReportList = null;
                superGridControl4.PrimaryGrid.DataSource = dtReportList;
            }
        }
    }
}
