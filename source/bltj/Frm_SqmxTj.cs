using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_SqmxTj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_SqmxTj()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txt_sqys.Text.Trim().Equals(""))
            {
                MessageBox.Show("申请医师姓名不能为空！", "提示");
                return;
            }
            int zzx = 0;
            int bj = 0;
            int tj = 0;
            int ptxbx = 0;
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat(" and req_physician = '{0}'", txt_sqys.Text.Trim().Replace("'", ""));
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            DataTable dt = ins.GetSqReportInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["exam_type"].ToString().Equals("PL"))
                    {
                        zzx += 1;
                    }
                    if (dt.Rows[i]["patient_source"].ToString().Equals("体检"))
                    {
                        tj += 1;
                    }
                    if (dt.Rows[i]["exam_type"].ToString().Equals("XBX"))
                    {
                        ptxbx += 1;
                    }
                    if (dt.Rows[i]["req_dept"].ToString().Equals("保健中心"))
                    {
                        bj += 1;
                    }
                }
                this.labelX1.Text = String.Format("统计值：共查询到 <font color='blue'><b>{0}</b></font> 条记录:组织学<font color='blue'><b>{1}</b></font>,保健中心<font color='blue'><b>{2}</b></font>,体检<font color='blue'><b>{3}</b></font>,细胞学<font color='blue'><b>{4}</b></font>", dt.Rows.Count, zzx, tj, tj, ptxbx);
            }
            else
            {
                this.labelX1.Text = "时间区间内医生开单信息不存在！";
            }
        }
    }
}
