using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_bcbg : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string exam_no = "";
        public Frm_bcbg()
        {
            InitializeComponent();
        }
        private string patient_name, sex, age, req_dept, patient_source;

        private void Frm_bcbg_Load(object sender, EventArgs e)
        {
            //绑定医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                comboBoxEx1.DataSource = ds.Tables[0];
                comboBoxEx1.DisplayMember = "user_name";
                comboBoxEx1.ValueMember = "user_code";
                comboBoxEx1.Text = Program.User_Name;
            }
            else
            {
                comboBoxEx1.DataSource = null;
            }

            DataSet ds1 = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {

                comboBoxEx2.DataSource = ds1.Tables[0];
                comboBoxEx2.DisplayMember = "user_name";
                comboBoxEx2.ValueMember = "user_code";
                comboBoxEx2.Text = Program.User_Name;
            }
            else
            {
                comboBoxEx2.DataSource = null;
            }
            //
            report_bl_datetime.Value = DateTime.Now;
            //
            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
            DataTable dtPat = insM.GetPatientInfo(BLH);
            if (dtPat != null && dtPat.Rows.Count == 1)
            {
                req_dept = dtPat.Rows[0]["req_dept"].ToString();
                patient_source = dtPat.Rows[0]["patient_source"].ToString();
                patient_name = dtPat.Rows[0]["patient_name"].ToString();
                sex = dtPat.Rows[0]["sex"].ToString();
                age = dtPat.Rows[0]["age"].ToString();
                lbl_receive_time.Text = dtPat.Rows[0]["received_datetime"].ToString();
            }
            //报告内容
            DBHelper.BLL.exam_bc_report ins = new DBHelper.BLL.exam_bc_report();
            DataTable dt = ins.GetData(BLH);
            if (dt != null && dt.Rows.Count == 1)
            {
                buttonX3.Enabled = true;
                if (dt.Rows[0]["sh_flag"].ToString().Equals("1"))
                {
                    richTextBoxEx1.Text = dt.Rows[0]["content"].ToString();
                    comboBoxEx1.Text = dt.Rows[0]["report_doc_name"].ToString();
                    comboBoxEx2.Text = dt.Rows[0]["sh_doc_name"].ToString();
                    report_bl_datetime.Text = dt.Rows[0]["report_datetime"].ToString();
                    buttonX3.TextColor = Color.Blue;
                    buttonX1.Enabled = true;
                    buttonX5.Enabled = true;
                    buttonX4.Enabled = true;
                }
                else
                {
                    richTextBoxEx1.Text = dt.Rows[0]["content"].ToString();
                    comboBoxEx1.Text = dt.Rows[0]["report_doc_name"].ToString();
                    comboBoxEx2.Text = dt.Rows[0]["sh_doc_name"].ToString();
                    report_bl_datetime.Text = dt.Rows[0]["report_datetime"].ToString();
                    buttonX1.Enabled = false;
                    buttonX5.Enabled = false;
                    buttonX4.Enabled = false;
                }
            }
            else
            {
                buttonX3.Enabled = false;
                buttonX1.Enabled = false;
                buttonX5.Enabled = false;
                buttonX4.Enabled = false;
            }

        }
        //保存
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim() != "")
            {
                DBHelper.BLL.exam_bc_report ins = new DBHelper.BLL.exam_bc_report();
                DBHelper.Model.exam_bc_report insM = new DBHelper.Model.exam_bc_report();
                insM.content = richTextBoxEx1.Text.Trim();
                insM.study_no = BLH;
                insM.report_datetime = report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                insM.report_doc_code = comboBoxEx1.SelectedValue.ToString();
                insM.report_doc_name = comboBoxEx1.Text;
                if (ins.SaveReport(insM) == true)
                {
                    Frm_TJInfo("提示", "保存成功!");
                    buttonX3.Enabled = true;
                }
            }
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
        //审核
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim() != "")
            {

                DBHelper.BLL.exam_bc_report ins = new DBHelper.BLL.exam_bc_report();
                DataTable dt = ins.GetData(BLH);
                if (dt != null && dt.Rows.Count == 1)
                {
                    if (dt.Rows[0]["sh_flag"].ToString().Equals("1"))
                    {
                        if (ins.ShReport(BLH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, comboBoxEx2.SelectedValue.ToString(), comboBoxEx2.Text.Trim()))
                        {

                            Frm_TJInfo("提示", "审核成功!");
                            buttonX3.TextColor = Color.Blue;
                            buttonX1.Enabled = true;
                            buttonX5.Enabled = true;
                            buttonX4.Enabled = true;
                        }

                    }
                    else
                    {
                        if (ins.ShReport(BLH, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 1, comboBoxEx2.SelectedValue.ToString(), comboBoxEx2.Text.Trim()))
                        {

                            Frm_TJInfo("提示", "审核成功!");
                            buttonX3.TextColor = Color.Blue;
                            buttonX1.Enabled = true;
                            buttonX5.Enabled = true;
                            buttonX4.Enabled = true;
                        }
                    }
                }

            }
        }
        //打印
        private void buttonX5_Click(object sender, EventArgs e)
        {
            buttonX2_Click(null, null);
            if (!richTextBoxEx1.Text.Trim().Equals(""))
            {
                FastReportLib.ysReportParas insM = new FastReportLib.ysReportParas();
                insM.study_no = BLH;
                insM.ReportParaHospital = Program.HospitalName;
                if (lbl_receive_time.Text.Trim().Length >= 10)
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim().Substring(0, 10);
                }
                else
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim();
                }
                insM.Txt_Name = patient_name;
                insM.exam_no = exam_no;
                insM.Txt_Sex = sex;
                insM.Txt_Age = age;
                insM.req_dept = req_dept;
                insM.Txt_Content = richTextBoxEx1.Text.Trim();
                insM.Txt_BgDate = report_bl_datetime.Text;
                insM.Txt_report_Doctor = comboBoxEx1.Text;
                insM.patient_source = patient_source;
                if (FastReportLib.DelayReport.PrintDelayReport(insM, PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum, 1))
                {
                    Frm_TJInfo("提示", "报告打印成功！");
                }
            }
            else
            {
                Frm_TJInfo("提示", "报告内容不能为空！");
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.richTextBoxEx1.SelectedText);
            richTextBoxEx1.SelectedText = String.Empty;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(richTextBoxEx1.SelectedText);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            richTextBoxEx1.SelectedText = Clipboard.GetText();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX2_Click(null, null);
            buttonX3.PerformClick();
            if (!richTextBoxEx1.Text.Trim().Equals(""))
            {
                FastReportLib.ysReportParas insM = new FastReportLib.ysReportParas();
                insM.study_no = BLH;
                insM.ReportParaHospital = Program.HospitalName;
                if (lbl_receive_time.Text.Trim().Length >= 10)
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim().Substring(0, 10);
                }
                else
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim();
                }
                insM.Txt_Name = patient_name;
                insM.exam_no = exam_no;
                insM.Txt_Sex = sex;
                insM.Txt_Age = age;
                insM.req_dept = req_dept;
                insM.Txt_Content = richTextBoxEx1.Text.Trim();
                insM.Txt_BgDate = report_bl_datetime.Text;
                insM.Txt_report_Doctor = comboBoxEx1.Text;
                insM.patient_source = patient_source;
                Frm_BgPre insF = new Frm_BgPre();
                insF.ins = insM;
                insF.reportType = 1;
                insF.Printer_Name = PIS_Sys.Properties.Settings.Default.ReportPrinter;
                insF.Print_Copys = PIS_Sys.Properties.Settings.Default.ReportPrintNum;
                insF.ShowDialog();
            }
            else
            {
                Frm_TJInfo("提示", "报告内容不能为空！");
            }

        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            buttonX2_Click(null, null);
            buttonX3.PerformClick();
            if (!richTextBoxEx1.Text.Trim().Equals(""))
            {
                FastReportLib.ysReportParas insM = new FastReportLib.ysReportParas();
                insM.study_no = BLH;
                insM.ReportParaHospital = Program.HospitalName;
                if (lbl_receive_time.Text.Trim().Length >= 10)
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim().Substring(0, 10);
                }
                else
                {
                    insM.Txt_Jsrq = lbl_receive_time.Text.Trim();
                }
                insM.Txt_Name = patient_name;
                insM.exam_no = exam_no;
                insM.Txt_Sex = sex;
                insM.Txt_Age = age;
                insM.req_dept = req_dept;
                insM.Txt_Content = richTextBoxEx1.Text.Trim();
                insM.Txt_BgDate = report_bl_datetime.Text;
                insM.Txt_report_Doctor = comboBoxEx1.Text;
                insM.Txt_shreport_Doctor = comboBoxEx2.Text;
                insM.patient_source = patient_source;
                Frm_BgPre insF = new Frm_BgPre();
                insF.Owner = this.Owner;
                insF.ins = insM;
                insF.reportType = 3;
                insF.Printer_Name = PIS_Sys.Properties.Settings.Default.ReportPrinter;
                insF.Print_Copys = PIS_Sys.Properties.Settings.Default.ReportPrintNum;
                insF.ShowDialog();
            }
            else
            {
                Frm_TJInfo("提示", "报告内容不能为空！");
            }
        }

    }
}
