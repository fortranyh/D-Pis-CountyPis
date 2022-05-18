using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_zhtj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_zhtj()
        {
            InitializeComponent();
        }
        DataTable dttj = null;

        private void Frm_zhtj_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //锁定前1列
            panel1.FrozenColumnCount = 1;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            dtStart.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            dtEnd.Value = DateTime.Now;
            //创建内存表
            dttj = new DataTable("mxtj");
            //指标名称
            dttj.Columns.Add("zbmc", typeof(string));
            //总数
            dttj.Columns.Add("zs", typeof(string));
            //百分比
            dttj.Columns.Add("bfb", typeof(string));
            superGridControl1.PrimaryGrid.DataSource = dttj;
        }
        private static int DateDiff(DateTime dateStart, DateTime dateEnd)
        {
            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());
            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days;
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            dttj.Rows.Clear();
            superGridControl1.PrimaryGrid.InvalidateLayout();

            int days = 0;
            days = DateDiff(dtStart.Value, dtEnd.Value);
            if (days >= 0 && days <= 32)
            {
                Caltj_BbGfhGdl();
                Caltj_HE();
                Caltj_Myzh();
                Caltj_Szbd();
                Caltj_zz();
                Caltj_xb();
                Caltj_szsl();
                Caltj_lc();
            }
            else
            {
                MessageBox.Show("仅能查询最多一个月范围内的质控，请调整时间段！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //刷新
            superGridControl1.PrimaryGrid.DataSource = dttj;
            Application.DoEvents();
            superGridControl1.PrimaryGrid.InvalidateLayout();
        }
        //标本规范化固定率
        private void Caltj_BbGfhGdl()
        {
            int zs = 0;
            double hgs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
            dt = ins.QuerySepcimensInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                for (int i = 0; i < zs; i++)
                {
                    //是否
                    string bbgfh_flag = dt.Rows[i]["bbgfh_flag"].ToString();
                    if (bbgfh_flag.Equals("1"))
                    {
                        hgs += 1;
                    }
                }
            }
            DataRow dr = dttj.NewRow();
            //指标名称
            dr["zbmc"] = "标本规范化固定率";
            //总数
            dr["zs"] = zs.ToString();
            //规范百分比
            double yxl = 0;
            if (zs != 0)
            {
                yxl = (hgs * 100 / zs);
            }
            string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
            //不规范百分比
            if (zs != 0)
            {
                yxl = ((zs - hgs) * 100 / zs);
            }
            string byxlstr = Convert.ToDouble(yxl).ToString("0.00");
            dr["bfb"] = string.Format("规范数：{0} 不规范数：{1} 规范百分比：{2} 不规范百分比：{3}", hgs, (zs - hgs), yxlstr + "%", byxlstr + "%");
            dttj.Rows.Add(dr);
        }
        //HE切片优良率
        private void Caltj_HE()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
            dt = ins.GetQpInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double jia = 0;
                double yi = 0;
                double bing = 0;
                double qt = 0;
                for (int i = 0; i < zs; i++)
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
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "HE切片优良率";
                //总数
                dr["zs"] = zs.ToString();
                //规范百分比
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (jia / zs) * 100;
                }
                string jiastr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = (yi * 100 / zs);
                }
                string yistr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = (bing * 100 / zs);
                }
                string bingstr = Convert.ToDouble(yxl).ToString("0.00") + "%";

                dr["bfb"] = string.Format("甲：{0} 乙：{1} 丙：{2} 甲级率：{3} 乙级率：{4} 丙级率：{5} ", jia, yi, bing, jiastr, yistr, bingstr);
                dttj.Rows.Add(dr);
            }
        }
        //免疫组化切片优良率
        private void Caltj_Myzh()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'  and taocan_type='免疫组化' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
            DataTable dt = insTJ.GetTjlbData(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double jia = 0;
                double yi = 0;
                double bing = 0;
                double qt = 0;
                for (int i = 0; i < zs; i++)
                {
                    //级别
                    string level = dt.Rows[i]["myzh_yl"].ToString();
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
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "免疫组化染色切片优良率";
                //总数
                dr["zs"] = zs.ToString();
                //规范百分比
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (jia / zs) * 100;
                }
                string jiastr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = (yi * 100 / zs);
                }
                string yistr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = (bing * 100 / zs);
                }
                string bingstr = Convert.ToDouble(yxl).ToString("0.00") + "%";

                dr["bfb"] = string.Format("甲：{0} 乙：{1} 丙：{2} 甲级率：{3} 乙级率：{4} 丙级率：{5} ", jia, yi, bing, jiastr, yistr, bingstr);
                dttj.Rows.Add(dr);
            }
        }
        //术中快速病理诊断及时率
        private void Caltj_Szbd()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and report_datetime>='{0} 00:00:00' and report_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
            dt = ins.GetShReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double gfh_int = 0;
                for (int i = 0; i < zs; i++)
                {
                    //是否
                    string bbgfh_flag = dt.Rows[i]["sj"].ToString();
                    if (Convert.ToInt32(bbgfh_flag) <= 45)
                    {
                        gfh_int += 1;
                    }
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "术中快速病理诊断及时率";
                //总数
                dr["zs"] = zs.ToString();
                //
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (gfh_int * 100 / zs);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = ((zs - gfh_int) * 100 / zs);
                }
                string byxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                dr["bfb"] = string.Format("及时报告数：{0} 不及时报告数：{1} 及时率：{2} 不及时率：{3}", gfh_int, zs - gfh_int, yxlstr, byxlstr);
                dttj.Rows.Add(dr);
            }
        }
        //组织病理诊断及时率
        private void Caltj_zz()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'  and exam_status=55 ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat(" and exam_type = '{0}' ", "PL");
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double gfh_int = 0;
                for (int i = 0; i < zs; i++)
                {
                    //是否
                    string bbgfh_flag = dt.Rows[i]["sj"].ToString();
                    if (Convert.ToInt32(bbgfh_flag) < 7200)
                    {
                        gfh_int += 1;
                    }
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "组织病理诊断及时率";
                //总数
                dr["zs"] = zs.ToString();
                //
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (gfh_int * 100 / zs);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = ((zs - gfh_int) * 100 / zs);
                }
                string byxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                dr["bfb"] = string.Format("及时报告数：{0} 不及时报告数：{1} 及时率：{2} 不及时率：{3}", gfh_int, zs - gfh_int, yxlstr, byxlstr);
                dttj.Rows.Add(dr);
            }
        }
        //细胞病理诊断及时率
        private void Caltj_xb()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59' and exam_status=55 ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat(" and (exam_type = '{0}' or exam_type = '{1}') ", "TCT", "XBX");
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double gfh_int = 0;
                for (int i = 0; i < zs; i++)
                {
                    //是否
                    string bbgfh_flag = dt.Rows[i]["sj"].ToString();
                    if (Convert.ToInt32(bbgfh_flag) < 7200)
                    {
                        gfh_int += 1;
                    }
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "细胞病理诊断及时率";
                //总数
                dr["zs"] = zs.ToString();
                //
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (gfh_int * 100 / zs);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = ((zs - gfh_int) * 100 / zs);
                }
                string byxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                dr["bfb"] = string.Format("及时报告数：{0} 不及时报告数：{1} 及时率：{2} 不及时率：{3}", gfh_int, zs - gfh_int, yxlstr, byxlstr);
                dttj.Rows.Add(dr);
            }
        }
        //术中快速诊断和石蜡诊断符合率
        private void Caltj_szsl()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and report_datetime>='{0} 00:00:00' and report_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
            dt = ins.GetShReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double gfh_int = 0;
                for (int i = 0; i < zs; i++)
                {
                    //是否
                    string bbgfh_flag = dt.Rows[i]["slfh"].ToString();
                    if (bbgfh_flag.Equals("符合"))
                    {
                        gfh_int += 1;
                    }
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "术中快速诊断和石蜡诊断符合率";
                //总数
                dr["zs"] = zs.ToString();
                //
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (gfh_int * 100 / zs);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = ((zs - gfh_int) * 100 / zs);
                }
                string byxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                dr["bfb"] = string.Format("符合报告数：{0} 不符合报告数：{1} 符合率：{2} 不符合率：{3}", gfh_int, zs - gfh_int, yxlstr, byxlstr);
                dttj.Rows.Add(dr);
            }
        }
        //临床诊断符合率统计
        private void Caltj_lc()
        {
            int zs = 0;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportLcfhList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                zs = dt.Rows.Count;
                double gfh_int = 0;
                for (int i = 0; i < zs; i++)
                {
                    //是否
                    string bbgfh_flag = dt.Rows[i]["lcfh"].ToString();
                    if (bbgfh_flag.Equals("符合"))
                    {
                        gfh_int += 1;
                    }
                }
                DataRow dr = dttj.NewRow();
                //指标名称
                dr["zbmc"] = "临床诊断符合率统计";
                //总数
                dr["zs"] = zs.ToString();
                //
                double yxl = 0;
                if (zs != 0)
                {
                    yxl = (gfh_int * 100 / zs);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                if (zs != 0)
                {
                    yxl = ((zs - gfh_int) * 100 / zs);
                }
                string byxlstr = Convert.ToDouble(yxl).ToString("0.00") + "%";
                dr["bfb"] = string.Format("符合报告数：{0} 不符合报告数：{1} 符合率：{2} 不符合率：{3}", gfh_int, zs - gfh_int, yxlstr, byxlstr);
                dttj.Rows.Add(dr);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                //string localFilePath, fileNameExt, newFileName, FilePath; 
                SaveFileDialog sfd = new SaveFileDialog();
                //设置文件类型 
                sfd.Filter = "Excel文件（*.xls）|*.xls";
                //设置默认文件类型显示顺序 
                sfd.FilterIndex = 1;

                //保存对话框是否记忆上次打开的目录 
                sfd.RestoreDirectory = true;

                //点了保存按钮进入 
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string wkName = "科室月度质控统计";

                    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                    //string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                    //获取文件路径，不带文件名 
                    //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                    //存在先删除
                    if (File.Exists(localFilePath) == true)
                    {
                        File.Delete(localFilePath);
                    }
                    //创建空的
                    FileInfo fi = new FileInfo(localFilePath);
                    FileStream fs = fi.Create();
                    fs.Close();
                    fs.Dispose();
                    //
                    Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(localFilePath);
                    string sj = string.Format("{0}到{1}", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    wk.Worksheets[0].Name = sj + wkName;
                    DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                    AddExcelSHeet(wk, dt);
                    wk.Save(localFilePath);
                    MessageBox.Show("导出成功：" + localFilePath, "数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void AddExcelSHeet(Aspose.Cells.Workbook wk, DataTable dt)
        {
            DataRow[] werow = dt.Select();
            if (werow.Length > 0)
            {
                int i = 0;
                for (int j = 0; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                {
                    wk.Worksheets[0].Cells[i, j].PutValue(superGridControl1.PrimaryGrid.Columns[j].HeaderText);
                }
                foreach (DataRow rows in werow)
                {
                    i = i + 1;
                    for (int j = 0; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                    {
                        wk.Worksheets[0].Cells[i, j].PutValue(rows[j].ToString());
                    }
                }
            }
        }
    }
}
