using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_lcfh : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_lcfh()
        {
            InitializeComponent();
            this.CenterToScreen();
        }

        private void Frm_lcfh_Load(object sender, EventArgs e)
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
            sb.AppendFormat(" report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportLcfhList(sb.ToString());
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
            int bfh_int = 0;
            int qt = 0;
            for (int i = 0; i < rows; i++)
            {
                //临床符合
                string bbgfh_flag = dt.Rows[i]["lcfh"].ToString();
                if (bbgfh_flag.Equals("符合"))
                {
                    gfh_int += 1;
                }
                else if (bbgfh_flag.Equals("不符合"))
                {
                    bfh_int += 1;
                }
                else
                {
                    qt += 1;
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
                    this.labelX1.Text = String.Format("统计值：共查询到 <font color='blue'><b>{0}</b></font> 条记录:符合<font color='blue'><b>{1}</b></font>,不符合<font color='blue'><b>{2}</b></font>,其他<font color='blue'><b>{3}</b></font>,临床诊断符合率<font color='blue'><b>{4}% </b></font>", rows, gfh_int, bfh_int, qt, yxlstr);
                }
            }
        }
    }
}
