using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Myzh : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Myzh()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Frm_Myzh_Load(object sender, EventArgs e)
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
            sb.AppendFormat("  zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'  and taocan_type='免疫组化' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
            DataTable dttj = insTJ.GetTjlbData(sb.ToString());
            if (dttj != null && dttj.Rows.Count > 0)
            {
                tj_func(dttj);
            }
            else
            {
                this.labelX1.Text = "不存在满足当前条件的玻片信息！";
            }

        }
        //计数
        public void tj_func(DataTable dt)
        {

            int rows = dt.Rows.Count;
            int he_y = 0;
            int he_l = 0;
            int he_z = 0;
            int he_c = 0;
            for (int i = 0; i < rows; i++)
            {
                //he评级
                string he_level = dt.Rows[i]["myzh_yl"].ToString();
                switch (he_level)
                {
                    case "甲级":
                        he_y += 1;
                        break;
                    case "乙级":
                        he_l += 1;
                        break;
                    case "丙级":
                        he_z += 1;
                        break;
                    default:
                        he_c += 1;
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    float yxl = 0;

                    yxl = (he_y * 100 / rows);

                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    this.labelX1.Text = String.Format("统计值,共查询到切片数:<font color='blue'><b>{0}</b></font> 条记录:甲：<font color='blue'><b>{1}</b></font>,乙：<font color='blue'><b>{2}</b></font>,丙：<font color='blue'><b>{3}</b></font>,其他：<font color='blue'><b>{4}</b></font>,免疫组化切片优良率：<font color='blue'><b>{5}%</b></font>", rows, he_y, he_l, he_z, he_c, yxlstr);
                }
            }
        }
    }
}
