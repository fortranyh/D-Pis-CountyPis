using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_HeQp : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_HeQp()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Frm_HeQp_Load(object sender, EventArgs e)
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
            sb.AppendFormat(" and make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
            dt = ins.GetQpInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                tj_func(dt);
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
            int jia = 0;
            int yi = 0;
            int bing = 0;
            int qt = 0;
            //int he_count = 0;
            //int he_y = 0;
            //int he_l = 0;
            //int he_z = 0;
            //int he_c = 0;
            for (int i = 0; i < rows; i++)
            {
                //级别
                string level = dt.Rows[i]["level"].ToString();
                switch (level)
                {
                    case "甲级":
                        jia += 1;
                        break;
                    case "乙级":
                        yi += 1;
                        break;
                    case "丙级":
                        bing += 1;
                        break;
                    default:
                        qt += 1;
                        break;
                }
                //he标志
                //string he_flag = dt.Rows[i]["he_flag"].ToString();
                //if (he_flag.Equals("1"))
                //{
                //    he_count += 1;
                //}
                ////he评级
                //string he_level = dt.Rows[i]["he_level"].ToString();
                //switch (he_level)
                //{
                //    case "优":
                //        he_y += 1;
                //        break;
                //    case "良":
                //        he_l += 1;
                //        break;
                //    case "中":
                //        he_z += 1;
                //        break;
                //    case "差":
                //        he_c += 1;
                //        break;
                //}
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    float yxl = 0;
                    yxl = (jia * 100 / rows);
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    this.labelX1.Text = String.Format("统计值共查询到切片数:<font color='blue'><b>{0}</b></font> 条记录:甲<font color='blue'><b>{1}</b></font>,乙<font color='blue'><b>{2}</b></font>,丙<font color='blue'><b>{3}</b></font>,其他<font color='blue'><b>{4}</b></font>,HE染色切片优良率：<font color='blue'><b>{5}%</b></font>", rows, jia, yi, bing, qt, yxlstr);
                }
            }
        }
    }
}
