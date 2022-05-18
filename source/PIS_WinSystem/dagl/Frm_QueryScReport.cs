using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_QueryScReport : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_QueryScReport()
        {
            InitializeComponent();
        }
        //提示窗体
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
        private void Frm_QueryScReport_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            panel1.MinRowHeight = 30;
            //锁定前四列
            panel1.FrozenColumnCount = 4;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }

            //病人来源 
            DBHelper.BLL.patient_source patient_source_ins = new DBHelper.BLL.patient_source();
            DataTable dts = patient_source_ins.GetTjPatient_source();
            if (dts != null && dts.Rows.Count > 0)
            {
                this.CmbBrly.DataSource = dts;
                CmbBrly.DisplayMember = "source_name";
                CmbBrly.ValueMember = "id";
            }
            //检查类型
            DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
            DataTable dt = exam_type_ins.GetTjAllDTExam_Type(Program.workstation_type_db);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Cmbjclx.DataSource = dt;
                Cmbjclx.DisplayMember = "modality_cn";
                Cmbjclx.ValueMember = "modality";
            }
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
        //查询
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
            else
            {
                if (RadioButton1.Checked)
                {
                    sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton2.Checked)
                {
                    sb.AppendFormat(" and cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton3.Checked)
                {
                    sb.AppendFormat(" and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (this.radioButton4.Checked)
                {
                    sb.AppendFormat(" and create_datetime>='{0} 00:00:00' and create_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                if (!TxtName.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and patient_name like '%{0}%'", TxtName.Text.Trim().Replace("'", ""));
                }
                if (CmbBrly.SelectedIndex != 0)
                {
                    sb.AppendFormat(" and patient_source = '{0}'", CmbBrly.Text.Trim());
                }
                if (!txtms.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and rysj like '%{0}%'", txtms.Text.Trim().Replace("'", ""));
                }
                if (!Txtzd.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and zdyj like '%{0}%'", Txtzd.Text.Trim().Replace("'", ""));
                }
                if (!txtjy.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and zdpz like '%{0}%'", txtjy.Text.Trim().Replace("'", ""));
                }
                if (!txt_gjc.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and gjc like '%{0}%'", txt_gjc.Text.Trim().Replace("'", ""));
                }
                if (!txt_zdbm.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and zdbm like '%{0}%'", txt_zdbm.Text.Trim().Replace("'", ""));
                }
                if (!txt_memo_note.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and memo_note like '%{0}%'", txt_memo_note.Text.Trim().Replace("'", ""));
                }
            }

            if (Cmbjclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and modality = '{0}'", Cmbjclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }

            sb.AppendFormat("and doc_code='{0}'", Program.User_Code);

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetScReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的报告信息！");
            }

        }
        //报告预览
        private void btn_save_Click(object sender, EventArgs e)
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
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                //检查类型
                string modality = Row.Cells["modality"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                if (exam_status >= 55)
                {
                    Frm_ReportViewer ins = new Frm_ReportViewer();
                    ins.modality = modality;
                    ins.study_no = study_no;
                    ins.exam_no = exam_no;
                    ins.BringToFront();
                    ins.ShowDialog();
                }
                else
                {
                    Frm_TJInfo("提示", "报告未打印，不能预览！");
                }
            }
        }
        //多媒体资料浏览
        private void btn_cap_Click(object sender, EventArgs e)
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
                //检查类型
                string modality = Row.Cells["modality"].Value.ToString();
                Frm_dmt ins = new Frm_dmt();
                ins.study_no = study_no;
                ins.modality = modality;
                ins.BringToFront();
                ins.ShowDialog();
            }
        }

        private void superGridControl1_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            btn_save_Click(null, null);
        }
    }
}
