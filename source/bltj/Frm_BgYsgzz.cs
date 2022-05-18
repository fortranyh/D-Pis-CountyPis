using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_BgYsgzz : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BgYsgzz()
        {
            InitializeComponent();
        }
        string Bg_ReportLimitTime = "0";
        private void Frm_BgYsgzz_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
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



            //报告超时是否计算周末 
            if (ConfigurationManager.AppSettings["ReportLimitTime"] != null)
            {
                Bg_ReportLimitTime = ConfigurationManager.AppSettings["ReportLimitTime"];
            }
            //
            dtStart.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            dtEnd.Value = DateTime.Now;
        }
        /// <summary>
        /// 根据年月日计算星期几(Label2.Text=CaculateWeekDay(2004,12,9);)
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns></returns>
        public static string CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1) m = 13;
            if (m == 2) m = 14;
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
            string weekstr = "";
            switch (week)
            {
                case 1: weekstr = "星期一"; break;
                case 2: weekstr = "星期二"; break;
                case 3: weekstr = "星期三"; break;
                case 4: weekstr = "星期四"; break;
                case 5: weekstr = "星期五"; break;
                case 6: weekstr = "星期六"; break;
                case 7: weekstr = "星期日"; break;
            }

            return weekstr;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX1.Enabled = false;
            if (dtStart.Value > dtEnd.Value)
            {
                MessageBox.Show("时间范围错误！", "起始时间必须小于终止时间！");
                buttonX1.Enabled = true;
                return;
            }
            //
            XxReport();
            buttonX1.Enabled = true;
        }
        private void XxReport()
        {
            superGridControl1.PrimaryGrid.Rows.Clear();

            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet dsMas = doctor_dict_ins.GetDsBgys_User(PIS_Statistics.Frm_Tjcx.CurDept_Code);
            string strName = "";
            int sanInt = 0;
            int siInt = 0;
            int wuInt = 0;
            int liuInt = 0;
            if (dsMas != null && dsMas.Tables[0].Rows.Count > 0)
            {
                DataTable dtMas = dsMas.Tables[0];
                int IntAddTime = 0;
                for (int i = 0; i < dtMas.Rows.Count; i++)
                {
                    strName = dtMas.Rows[i]["user_name"].ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("  and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59' and user_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), strName);
                    DataTable dt = new DataTable();
                    DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
                    dt = ins.GetReportTjZk(sb.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            IntAddTime = 0;

                            //获取当前时间是星期几
                            //string received_datetime = dt.Rows[k]["received_datetime"].ToString();
                            //DateTime receiDate = Convert.ToDateTime(received_datetime);
                            //string xq_date = CaculateWeekDay(receiDate.Year, receiDate.Month, receiDate.Day);
                            //if (xq_date.Equals("星期五") || xq_date.Equals("星期六") || xq_date.Equals("星期四"))
                            //{
                            //    IntAddTime = 2880;
                            //}
                            //else if (xq_date.Equals("星期三") || xq_date.Equals("星期日"))
                            //{
                            //    IntAddTime = 1440;
                            //}

                            int zk_time = Convert.ToInt32(dt.Rows[k]["zk_time"]);
                            if (zk_time - IntAddTime <= 4320)
                            {
                                sanInt += 1;
                            }

                            if (zk_time - IntAddTime > 4320 && zk_time - IntAddTime <= 5760)
                            {
                                siInt += 1;
                            }

                            if (zk_time - IntAddTime > 5760 && zk_time - IntAddTime <= 7200)
                            {
                                wuInt += 1;
                            }

                            if (zk_time - IntAddTime > 7200)
                            {
                                liuInt += 1;
                            }


                        }
                        sb.Clear();
                        //添加一行
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell1 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell2 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell3 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell4 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell5 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell7 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        gridCell1.Value = strName;
                        gridCell2.Value = sanInt.ToString();
                        gridCell3.Value = siInt.ToString();
                        gridCell4.Value = wuInt.ToString();
                        gridCell5.Value = liuInt.ToString();
                        gridCell7.Value = (sanInt + siInt + wuInt + liuInt).ToString();
                        gridCell7.CellStyles.Default.TextColor = System.Drawing.Color.Red;
                        DevComponents.DotNetBar.SuperGrid.GridRow gridRow1 = new DevComponents.DotNetBar.SuperGrid.GridRow();
                        gridRow1.Cells.Add(gridCell1);
                        gridRow1.Cells.Add(gridCell2);
                        gridRow1.Cells.Add(gridCell3);
                        gridRow1.Cells.Add(gridCell4);
                        gridRow1.Cells.Add(gridCell5);
                        gridRow1.Cells.Add(gridCell7);
                        superGridControl1.PrimaryGrid.Rows.Add(gridRow1);
                        sanInt = 0;
                        siInt = 0;
                        wuInt = 0;
                        liuInt = 0;
                    }
                }
                if (superGridControl1.PrimaryGrid.Rows.Count > 0)
                {
                    strName = "合计：";
                    sanInt = 0;
                    siInt = 0;
                    wuInt = 0;
                    liuInt = 0;
                    for (int j = 0; j < superGridControl1.PrimaryGrid.Rows.Count; j++)
                    {
                        sanInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[1].Value);
                        siInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[2].Value);
                        wuInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[3].Value);
                        liuInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[4].Value);
                    }
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell1 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell2 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell3 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell4 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell5 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell7 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    gridCell1.Value = strName;
                    gridCell1.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell2.Value = sanInt.ToString();
                    gridCell2.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell3.Value = siInt.ToString();
                    gridCell3.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell4.Value = wuInt.ToString();
                    gridCell4.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell5.Value = liuInt.ToString();
                    gridCell5.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell7.Value = (sanInt + siInt + wuInt + liuInt).ToString();
                    gridCell7.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    DevComponents.DotNetBar.SuperGrid.GridRow gridRow1 = new DevComponents.DotNetBar.SuperGrid.GridRow();
                    gridRow1.Cells.Add(gridCell1);
                    gridRow1.Cells.Add(gridCell2);
                    gridRow1.Cells.Add(gridCell3);
                    gridRow1.Cells.Add(gridCell4);
                    gridRow1.Cells.Add(gridCell5);
                    gridRow1.Cells.Add(gridCell7);
                    superGridControl1.PrimaryGrid.Rows.Add(gridRow1);
                }
            }
        }

    }
}
