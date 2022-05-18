using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Szzd : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Szzd()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Frm_Szzd_Load(object sender, EventArgs e)
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
            double gfh_int = 0;
            for (int i = 0; i < rows; i++)
            {
                //是否
                string bbgfh_flag = dt.Rows[i]["sj"].ToString();
                if (Convert.ToInt32(bbgfh_flag) <= 45)
                {
                    gfh_int += 1;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    double yxl = 0;
                    if (rows != 0)
                    {
                        yxl = (gfh_int * 100 / rows);
                    }
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    this.labelX1.Text = String.Format("统计值：共查询到 <font color='blue'><b>{0}</b></font> 条记录。及时报告数：<font color='blue'><b>{1}</b></font>,其他<font color='blue'><b>{2}</b></font>,书中快速病理诊断及时率<font color='blue'><b>{3}% </b></font>", rows, gfh_int, (rows - gfh_int), yxlstr);
                }
            }
        }



    }
}
