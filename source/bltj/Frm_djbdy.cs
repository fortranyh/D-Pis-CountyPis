using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace PIS_Statistics
{
    public partial class Frm_djbdy : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_djbdy()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            this.reportViewer1.setToolBarVisible(true);
        }

        private void Frm_djbdy_Load(object sender, EventArgs e)
        {
            //检查类型
            DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
            DataTable dt = exam_type_ins.GetTjAllDTBigExam_Type(ConfigurationManager.AppSettings["w_big_type_db"]);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.cmbJclx.DataSource = dt;
                cmbJclx.DisplayMember = "big_type_name";
                cmbJclx.ValueMember = "big_type_code";
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" received_datetime>='{0}' and received_datetime<='{1}'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));


            if (cmbJclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and exam_type = '{0}'", cmbJclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }


            DBHelper.BLL.exam_report Ins = new DBHelper.BLL.exam_report();
            DataSet ds = Ins.GetDsDjbPrintList(sb.ToString());
            this.reportViewer1.DjbPrintListPreview(ds, dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

        }
    }
}
