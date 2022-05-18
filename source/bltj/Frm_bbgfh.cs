using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_bbgfh : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bbgfh()
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

            StringBuilder sb = new StringBuilder();

            if (cmbJclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and b.exam_type = '{0}'", cmbJclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and b.exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }
            sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));


            DataTable dt = new DataTable();
            DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
            dt = ins.QuerySepcimensInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {

                tj_func(dt);

            }
            else
            {
                this.labelX1.Text = "时间区间内标本不存在！";
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
                string bbgfh_flag = dt.Rows[i]["bbgfh_flag"].ToString();
                if (bbgfh_flag.Equals("1"))
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
                    this.labelX1.Text = String.Format("统计值：共查询到 <font color='blue'><b>{0}</b></font> 条记录:规范化<font color='blue'><b>{1}</b></font>,其他<font color='blue'><b>{2}</b></font>,标本规范化固定率<font color='blue'><b>{3}% </b></font>", rows, gfh_int, (rows - gfh_int), yxlstr);
                }
            }
        }

        private void Frm_bbgfh_Load(object sender, EventArgs e)
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
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
    }
}
