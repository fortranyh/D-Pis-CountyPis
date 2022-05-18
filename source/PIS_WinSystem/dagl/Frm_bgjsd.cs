using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace PIS_Sys.dagl
{
    public partial class Frm_bgjsd : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bgjsd()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            this.reportViewer1.setToolBarVisible(true);
        }

        private void Frm_bgjsd_Load(object sender, EventArgs e)
        {
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            //绑定医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsAllExam_User(Program.Dept_Code);
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
            //病人来源 
            DBHelper.BLL.patient_source patient_source_ins = new DBHelper.BLL.patient_source();
            List<string> lst_source = patient_source_ins.GetPatient_source();
            for (int i = 0; i < lst_source.Count; i++)
            {
                cmb_patSource.Items.Add(lst_source[i]);
            }
            lst_source.Clear();

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string tj = "";

            tj = string.Format(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);


            string docName = comboBoxEx1.Text;
            if (!docName.Equals("全部") && !docName.Equals(""))
            {
                tj += string.Format(" and cbreport_doc_name='{0}' ", docName);
            }
            string patient_source = cmb_patSource.Text;
            if (!patient_source.Equals(""))
            {
                tj += string.Format(" and patient_source='{0}' ", patient_source);
            }
            DBHelper.BLL.exam_report Ins = new DBHelper.BLL.exam_report();
            DataSet ds = Ins.GetBgJsd(dtStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"), tj);
            if (ds != null)
            {
                this.reportViewer1.bgjsdListPreview(ds, dtEnd.Value.ToString("yyyy-MM-dd"), PIS_Sys.Properties.Settings.Default.ReportPrinter, patient_source);
            }
        }
    }
}
