using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_myzh_report : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string exam_no = "";
        public string blzd = "";
        public string lczd = "";
        private string patient_name, sex, age, req_dept, patient_source, submit_unit;

        public Frm_myzh_report()
        {
            InitializeComponent();
        }

        private void Frm_report_Load(object sender, EventArgs e)
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


            //
            report_bl_datetime.Value = DateTime.Now;
            //
            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
            DataTable dtPat = insM.GetPatientInfo(BLH);
            if (dtPat != null && dtPat.Rows.Count == 1)
            {
                req_dept = dtPat.Rows[0]["req_dept"].ToString();
                patient_name = dtPat.Rows[0]["patient_name"].ToString();
                sex = dtPat.Rows[0]["sex"].ToString();
                age = dtPat.Rows[0]["age"].ToString();
                lbl_receive_time.Text = dtPat.Rows[0]["received_datetime"].ToString();
                textBox1.Text = dtPat.Rows[0]["input_id"].ToString();
                if (!dtPat.Rows[0]["bed_no"].ToString().Equals(""))
                {
                    textBox2.Text = dtPat.Rows[0]["bed_no"].ToString() + "床";
                }
                submit_unit = dtPat.Rows[0]["submit_unit"].ToString();
                patient_source = dtPat.Rows[0]["patient_source"].ToString();
            }

            //报告内容
            DBHelper.BLL.myzh_report ins = new DBHelper.BLL.myzh_report();
            DataTable dt = ins.GetData(BLH);
            if (dt != null && dt.Rows.Count == 1)
            {
                txt_zh_md.Text = dt.Rows[0]["zh_md"].ToString();
                txt_rs_func.Text = dt.Rows[0]["rs_func"].ToString();
                richTextBoxEx1.Text = dt.Rows[0]["content"].ToString();
                comboBoxEx1.Text = dt.Rows[0]["report_doc"].ToString();
                report_bl_datetime.Text = dt.Rows[0]["report_dt"].ToString();
            }
        }
        //保存
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim() != "")
            {
                DBHelper.BLL.myzh_report ins = new DBHelper.BLL.myzh_report();
                if (ins.SaveReport(BLH, txt_rs_func.Text.Trim(), txt_zh_md.Text.Trim(), richTextBoxEx1.Text.Trim(), comboBoxEx1.Text, report_bl_datetime.Value.ToString("yyyy-MM-dd HH:mm:ss")) == true)
                {
                    Frm_TJInfo("提示", "保存成功!");
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
        //预览
        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX2_Click(null, null);
            if (!richTextBoxEx1.Text.Trim().Equals(""))
            {
                FastReportLib.myzhReportParas insM = new FastReportLib.myzhReportParas();
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
                insM.rs_func = txt_rs_func.Text.Trim();
                insM.zh_md = txt_zh_md.Text.Trim();
                insM.lczd = lczd;
                insM.blzd = blzd;
                if (submit_unit.Equals(""))
                {
                    submit_unit = "本院";
                }
                insM.submit_unit = submit_unit;
                insM.bed_no = textBox2.Text.Trim();
                insM.input_id = textBox1.Text.Trim();
                Frm_BgPre insF = new Frm_BgPre();
                insF.InsMyzh = insM;
                insF.reportType = 4;
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
