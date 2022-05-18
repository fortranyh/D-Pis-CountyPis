using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_Zzys : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Zzys()
        {
            InitializeComponent();
        }
        public string study_no = "";
        public string Zyzy_Name = "";
        private void Frm_Zzys_Load(object sender, EventArgs e)
        {
            //绑定医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsExam_Dcotor(Program.Dept_Code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                cmb_zzys.DataSource = ds.Tables[0];
                cmb_zzys.DisplayMember = "doctor_name";
                cmb_zzys.ValueMember = "doctor_code";
            }
            else
            {
                cmb_zzys.DataSource = null;
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        //确定
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!study_no.Equals(""))
            {
                string zzys_code = cmb_zzys.SelectedValue.ToString();
                Zyzy_Name = cmb_zzys.Text.ToString();
                try
                {
                    if (!zzys_code.Equals(""))
                    {
                        DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
                        Boolean result = ins.SendReportToZzys(study_no, zzys_code, Zyzy_Name);
                        if (result)
                        {
                            DialogResult = DialogResult.OK;
                            return;
                        }
                    }
                }
                catch
                {

                }
            }

            DialogResult = DialogResult.Cancel;
        }

    }
}
