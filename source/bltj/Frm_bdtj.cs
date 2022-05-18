using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_bdtj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bdtj()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            string tj = string.Format(" received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            DataTable dt = ins.GetBdtj(tj);
            int total = 0;
            double yangx = 0;
            int yinx = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["ice_flag"].ToString().Equals("1"))
                {
                    yangx += Convert.ToInt32(dt.Rows[i]["sl"]);

                }
                else if (dt.Rows[i]["ice_flag"].ToString().Equals("0"))
                {
                    yinx += Convert.ToInt32(dt.Rows[i]["sl"]);
                }

                total += Convert.ToInt32(dt.Rows[i]["sl"]);
            }
            double yxl = 0;
            if (total != 0)
            {
                yxl = (yangx * 100 / total);
            }
            string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
            labelX1.Text = string.Format("统计值：检查总数：{0} 冰冻检查：{1} 非冰冻检查：{2}  冰冻率：{3}% ", total, yangx, yinx, yxlstr);

        }

        private void Frm_bdtj_Load(object sender, EventArgs e)
        {
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
    }
}
