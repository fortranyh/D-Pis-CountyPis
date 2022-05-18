using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_YsReport : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_YsReport()
        {
            InitializeComponent();
        }
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();

            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", "").ToUpper());
            }
            else if (!txtms.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and content like '%{0}%'", txtms.Text.Trim().Replace("'", ""));
            }
            else
            {
                sb.AppendFormat(" and report_datetime>='{0} 00:00:00' and report_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            }


            DataTable dt = new DataTable();
            DBHelper.BLL.exam_delay_report ins = new DBHelper.BLL.exam_delay_report();
            dt = ins.GetShReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的报告信息！");
            }
        }

        private void Frm_YsReport_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                FastReportLib.ysReportParas insM = new FastReportLib.ysReportParas();
                insM.study_no = study_no;
                insM.ReportParaHospital = Program.HospitalName;
                DataTable dt = null;
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                dt = ins.GetDt("select received_datetime,patient_name,sex,req_dept,age,patient_source,exam_no from exam_master_view  where study_no='" + study_no + "'");
                if (dt != null && dt.Rows.Count == 1)
                {
                    insM.Txt_Jsrq = dt.Rows[0]["received_datetime"].ToString();
                    insM.Txt_Name = dt.Rows[0]["patient_name"].ToString();
                    insM.Txt_Sex = dt.Rows[0]["sex"].ToString();
                    insM.Txt_Age = dt.Rows[0]["age"].ToString();
                    insM.req_dept = dt.Rows[0]["req_dept"].ToString();
                    insM.exam_no = dt.Rows[0]["exam_no"].ToString();
                    insM.Txt_Content = Row.Cells["content"].Value.ToString();
                    insM.Txt_BgDate = Row.Cells["report_datetime"].Value.ToString();
                    insM.Txt_report_Doctor = Row.Cells["report_doc_name"].Value.ToString();
                    insM.patient_source = dt.Rows[0]["patient_source"].ToString();
                    if (FastReportLib.DelayReport.PrintDelayReport(insM, PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum))
                    {
                        Frm_TJInfo("提示", "报告打印成功！");
                    }
                }
            }
        }


    }
}
