using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Fzsn : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Fzsn()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Frm_Fzsn_Load(object sender, EventArgs e)
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
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(" zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'  ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
            //分类别查询 
            DataTable dtfz = insTJ.GetTjFzData(sb1.ToString());
            if (dtfz != null && dtfz.Rows.Count > 0)
            {
                for (int i = 0; i < dtfz.Rows.Count; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(" zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'  and taocan_type='分子病理' and bj_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), dtfz.Rows[i]["bj_name"].ToString());
                    DataTable dttj = insTJ.GetTjlbData(sb.ToString());
                    if (dttj != null && dttj.Rows.Count > 0)
                    {
                        tj_func(dttj);
                    }
                }
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
            int fz_hg = 0;
            int fz_bhg = 0;
            for (int i = 0; i < rows; i++)
            {
                //he评级
                string he_level = dt.Rows[i]["fz_hg"].ToString();
                switch (he_level)
                {
                    case "合格":
                        fz_hg += 1;
                        break;
                    case "不合格":
                        fz_bhg += 1;
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    float yxl = 0;
                    yxl = (fz_hg * 100 / rows);
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    this.labelX1.Text += "\r\n";
                    this.labelX1.Text += String.Format("共查询到{0}切片数:<font color='blue'><b>{1}</b></font> 条记录:合格数：<font color='blue'><b>{2}</b></font>,不合格数：<font color='blue'><b>{3}</b></font>,{4}项分子病理检测室内质控合格率：<font color='blue'><b>{5}%</b></font>", dt.Rows[i]["bj_name"].ToString(), rows, fz_hg, fz_bhg, dt.Rows[i]["bj_name"].ToString(), yxlstr);
                    this.labelX1.Text += "\r\n";
                }
            }
        }
    }
}
