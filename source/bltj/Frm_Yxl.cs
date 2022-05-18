using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Yxl : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Yxl()
        {
            InitializeComponent();
            this.CenterToScreen();
        }


        //统计
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            string tj = string.Format(" cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            DataTable dt = ins.GetYxl(tj);
            int total = 0;
            double yangx = 0;
            int qt = 0;
            int yinx = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["sfyx"].ToString().Equals("是"))
                {
                    yangx += Convert.ToInt32(dt.Rows[i]["sl"]);

                }
                else if (dt.Rows[i]["sfyx"].ToString().Equals("否"))
                {
                    yinx += Convert.ToInt32(dt.Rows[i]["sl"]);
                }
                else
                {
                    qt += Convert.ToInt32(dt.Rows[i]["sl"]);
                }
                total += Convert.ToInt32(dt.Rows[i]["sl"]);
            }
            double yxl = 0;
            if (total != 0)
            {
                yxl = (yangx * 100 / total);
            }
            string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
            labelX1.Text = string.Format("统计值：报告总数：{0} 阳性报告：{1} 阴性报告：{2} 其他：{3} 阳性率：{4}% ", total, yangx, yinx, qt, yxlstr);


        }

        private void Frm_Yxl_Load(object sender, EventArgs e)
        {
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
    }
}
