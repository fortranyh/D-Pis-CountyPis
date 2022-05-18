using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Szsl : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Szsl()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Frm_Szsl_Load(object sender, EventArgs e)
        {
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and report_datetime>='{0} 00:00:00' and report_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
            dt = ins.GetShReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                tj_func(dt);
            }
            else
            {
                this.labelX1.Text = "不存在满足当前条件的报告信息！";
            }
        }
        //计数
        public void tj_func(DataTable dt)
        {

            int rows = dt.Rows.Count;
            int gfh_int = 0;
            for (int i = 0; i < rows; i++)
            {
                //是否
                string bbgfh_flag = dt.Rows[i]["slfh"].ToString();
                if (bbgfh_flag.Equals("符合"))
                {
                    gfh_int += 1;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    float yxl = 0;
                    if (rows != 0)
                    {
                        yxl = (gfh_int * 100 / rows);
                    }
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    this.labelX1.Text = String.Format("统计值：共查询到 <font color='blue'><b>{0}</b></font> 条记录。符合报告数：<font color='blue'><b>{1}</b></font>,其他<font color='blue'><b>{2}</b></font>,术中快速诊断和石蜡诊断符合率：<font color='blue'><b>{3}% </b></font>", rows, gfh_int, (rows - gfh_int), yxlstr);
                }
            }
        }
    }
}
