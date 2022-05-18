using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_SC_Report : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_SC_Report()
        {
            InitializeComponent();
        }
        public string study_no = "";
        public string gjc = "";
        public string zdbm = "";
        private void Frm_SC_Report_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.sc_report ins = new DBHelper.BLL.sc_report();
            DataTable dt = ins.getScDatetime(study_no);
            if (dt != null && dt.Rows.Count == 0)
            {
                labelX4.Visible = false;
                textBoxX1.Text = zdbm;
                textBoxX2.Text = gjc;
                this.textBoxX1.Select();
                this.textBoxX1.Focus();
            }
            else
            {
                textBoxX1.Enabled = false;
                textBoxX1.Text = dt.Rows[0]["zdbm"].ToString();
                textBoxX2.Enabled = false;
                textBoxX2.Text = dt.Rows[0]["gjc"].ToString();
                richTextBoxEx1.Enabled = false;
                richTextBoxEx1.Text = dt.Rows[0]["memo_note"].ToString();
                buttonX2.Enabled = false;
                labelX4.Visible = true;
                labelX4.Text = "此报告已经被收藏，收藏日期：" + dt.Rows[0]["create_datetime"].ToString();

            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!study_no.Equals(""))
            {
                try
                {
                    DBHelper.BLL.sc_report ins = new DBHelper.BLL.sc_report();
                    Boolean result = ins.InsertReportInfo(study_no, textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), richTextBoxEx1.Text.Trim(), Program.User_Code, Program.User_Name);
                    if (result)
                    {
                        DialogResult = DialogResult.OK;
                        return;
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
