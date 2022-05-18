using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_BgZlKz : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BgZlKz()
        {
            InitializeComponent();
        }
        private void Frm_BgZlKz_Load(object sender, EventArgs e)
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
            //
            dtStart.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            dtEnd.Value = DateTime.Now;

            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;
            //锁定前四列
            panel2.FrozenColumnCount = 4;
            panel2.AutoGenerateColumns = true;
            superGridControl2.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ReadOnly = true;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }

            //高度自动适应
            superGridControl3.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl3.PrimaryGrid.ShowRowGridIndex = true;
            //superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl3.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel3 = superGridControl3.PrimaryGrid;
            panel3.MinRowHeight = 30;
            //锁定前1列
            panel3.FrozenColumnCount = 1;
            panel3.AutoGenerateColumns = true;
            superGridControl3.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel3.Columns.Count; i++)
            {
                panel3.Columns[i].ReadOnly = true;
                panel3.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel3.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel3.Columns[i].CellStyles.Default.Font = this.Font;
            }


            //绑定医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsExam_User(PIS_Statistics.Frm_Tjcx.CurDept_Code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                comboBoxEx1.DataSource = ds.Tables[0];
                comboBoxEx1.DisplayMember = "user_name";
                comboBoxEx1.ValueMember = "user_code";
            }
            else
            {
                comboBoxEx1.DataSource = null;
            }
            //检查类型
            DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
            DataTable dt = exam_type_ins.GetTjAllDTExam_Type(ConfigurationManager.AppSettings["w_big_type_db"]);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Cmbjclx.DataSource = dt;
                Cmbjclx.DisplayMember = "modality_cn";
                Cmbjclx.ValueMember = "modality";
            }
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
            //总报告发送率
            TotalReport();
            //活检报告发送率
            HjReport();
            //详细明细
            XxReport();
            //延时原因
            YsReport();
            buttonX1.Enabled = true;
        }
        private void YsReport()
        {
            superGridControl3.PrimaryGrid.Rows.Clear();
            int wuInt = 0;
            int liuInt = 0;
            int qiInt = 0;
            string strName = "";
            DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
            DataTable dtMas = insMas.GetDt("select delay_ys,id from exam_ysbg_dict ");
            if (dtMas != null && dtMas.Rows.Count > 0)
            {
                int IntAddTime = 0;
                for (int i = 0; i < dtMas.Rows.Count; i++)
                {
                    strName = dtMas.Rows[i]["delay_ys"].ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59' and delay_reason = '{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), strName);
                    DataTable dt = new DataTable();
                    DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
                    dt = ins.GetReportTjZk(sb.ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {

                            int zk_time = Convert.ToInt32(dt.Rows[k]["zk_time"]);

                            if (zk_time - IntAddTime > 5760 && zk_time - IntAddTime <= 7200)
                            {
                                wuInt += 1;
                            }

                            if (zk_time - IntAddTime > 7200 && zk_time - IntAddTime <= 8640)
                            {
                                liuInt += 1;
                            }

                            if (zk_time - IntAddTime > 8640)
                            {
                                qiInt += 1;
                            }
                        }
                        sb.Clear();
                        //添加一行
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell1 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell4 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell5 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell6 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell7 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        gridCell1.Value = strName;
                        gridCell4.Value = wuInt.ToString();
                        gridCell5.Value = liuInt.ToString();
                        gridCell6.Value = qiInt.ToString();
                        gridCell7.Value = (wuInt + liuInt + qiInt).ToString();
                        gridCell7.CellStyles.Default.TextColor = System.Drawing.Color.Red;
                        DevComponents.DotNetBar.SuperGrid.GridRow gridRow1 = new DevComponents.DotNetBar.SuperGrid.GridRow();
                        gridRow1.Cells.Add(gridCell1);
                        gridRow1.Cells.Add(gridCell4);
                        gridRow1.Cells.Add(gridCell5);
                        gridRow1.Cells.Add(gridCell6);
                        gridRow1.Cells.Add(gridCell7);
                        superGridControl3.PrimaryGrid.Rows.Add(gridRow1);
                        wuInt = 0;
                        liuInt = 0;
                        qiInt = 0;
                    }
                }
                if (superGridControl3.PrimaryGrid.Rows.Count > 0)
                {
                    strName = "合计：";
                    wuInt = 0;
                    liuInt = 0;
                    qiInt = 0;
                    for (int j = 0; j < superGridControl3.PrimaryGrid.Rows.Count; j++)
                    {
                        wuInt += Convert.ToInt32(((GridRow)superGridControl3.PrimaryGrid.Rows[j]).Cells[1].Value);
                        liuInt += Convert.ToInt32(((GridRow)superGridControl3.PrimaryGrid.Rows[j]).Cells[2].Value);
                        qiInt += Convert.ToInt32(((GridRow)superGridControl3.PrimaryGrid.Rows[j]).Cells[3].Value);
                    }
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell1 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell4 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell5 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell6 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell7 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    gridCell1.Value = strName;
                    gridCell1.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell4.Value = wuInt.ToString();
                    gridCell4.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell5.Value = liuInt.ToString();
                    gridCell5.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell6.Value = qiInt.ToString();
                    gridCell6.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell7.Value = (wuInt + liuInt + qiInt).ToString();
                    gridCell7.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    DevComponents.DotNetBar.SuperGrid.GridRow gridRow1 = new DevComponents.DotNetBar.SuperGrid.GridRow();
                    gridRow1.Cells.Add(gridCell1);
                    gridRow1.Cells.Add(gridCell4);
                    gridRow1.Cells.Add(gridCell5);
                    gridRow1.Cells.Add(gridCell6);
                    gridRow1.Cells.Add(gridCell7);
                    superGridControl3.PrimaryGrid.Rows.Add(gridRow1);
                }
            }
        }




        private void TotalReport()
        {
            labelX4.Text = "0.00";
            labelX5.Text = "0.00";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportTjZk(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                int IntAddTime = 0;
                float wtnInt = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IntAddTime = 0;
                    int zk_time = Convert.ToInt32(dt.Rows[i]["zk_time"]);
                    if (!dt.Rows[i]["exam_type"].ToString().Equals("XBX"))
                    {
                        if (zk_time < 7200)
                        {
                            wtnInt += 1;
                        }
                    }
                    else
                    {
                        //获取当前时间是星期几
                        //string received_datetime = dt.Rows[i]["received_datetime"].ToString();
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
                        if (zk_time - IntAddTime < 4320)
                        {
                            wtnInt += 1;
                        }
                    }
                }
                labelX4.Text = String.Format("{0:F}", (wtnInt / dt.Rows.Count) * 100);
                labelX5.Text = String.Format("{0:F}", ((dt.Rows.Count - wtnInt) / dt.Rows.Count) * 100);
                dt.Clear();
                dt = null;
            }
        }
        private void HjReport()
        {
            labelX7.Text = "0.00";
            labelX6.Text = "0.00";
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            sb.AppendFormat(" and exam_type = '{0}'", "PL");
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportTjZk(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                float wtnInt = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int zk_time = Convert.ToInt32(dt.Rows[i]["zk_time"]);
                    if (zk_time <= 7200)
                    {
                        wtnInt += 1;
                    }
                }
                labelX7.Text = String.Format("{0:F}", (wtnInt / dt.Rows.Count) * 100);
                labelX6.Text = String.Format("{0:F}", ((dt.Rows.Count - wtnInt) / dt.Rows.Count) * 100);
                dt.Clear();
                dt = null;
            }
        }

        private void XxReport()
        {
            superGridControl1.PrimaryGrid.Rows.Clear();

            string sqlStr = "select modality,modality_cn from exam_type_dict where enable_flag=1  order by big_type ";
            DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
            DataTable dtMas = insMas.GetDt(sqlStr);
            string strName = "";
            int sanInt = 0;
            int siInt = 0;
            int wuInt = 0;
            int liuInt = 0;
            int qiInt = 0;
            if (dtMas != null && dtMas.Rows.Count > 0)
            {
                int IntAddTime = 0;
                for (int i = 0; i < dtMas.Rows.Count; i++)
                {
                    strName = dtMas.Rows[i]["modality_cn"].ToString();
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59' and modality='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), dtMas.Rows[i]["modality"].ToString());
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

                            if (zk_time - IntAddTime > 7200 && zk_time - IntAddTime <= 8640)
                            {
                                liuInt += 1;
                            }

                            if (zk_time - IntAddTime > 8640)
                            {
                                qiInt += 1;
                            }
                        }
                        sb.Clear();
                        //添加一行
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell1 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell2 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell3 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell4 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell5 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell6 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        DevComponents.DotNetBar.SuperGrid.GridCell gridCell7 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                        gridCell1.Value = strName;
                        gridCell2.Value = sanInt.ToString();
                        gridCell3.Value = siInt.ToString();
                        gridCell4.Value = wuInt.ToString();
                        gridCell5.Value = liuInt.ToString();
                        gridCell6.Value = qiInt.ToString();
                        gridCell7.Value = (sanInt + siInt + wuInt + liuInt + qiInt).ToString();
                        gridCell7.CellStyles.Default.TextColor = System.Drawing.Color.Red;
                        DevComponents.DotNetBar.SuperGrid.GridRow gridRow1 = new DevComponents.DotNetBar.SuperGrid.GridRow();
                        gridRow1.Cells.Add(gridCell1);
                        gridRow1.Cells.Add(gridCell2);
                        gridRow1.Cells.Add(gridCell3);
                        gridRow1.Cells.Add(gridCell4);
                        gridRow1.Cells.Add(gridCell5);
                        gridRow1.Cells.Add(gridCell6);
                        gridRow1.Cells.Add(gridCell7);
                        superGridControl1.PrimaryGrid.Rows.Add(gridRow1);
                        sanInt = 0;
                        siInt = 0;
                        wuInt = 0;
                        liuInt = 0;
                        qiInt = 0;
                    }
                }
                if (superGridControl1.PrimaryGrid.Rows.Count > 0)
                {
                    strName = "合计：";
                    sanInt = 0;
                    siInt = 0;
                    wuInt = 0;
                    liuInt = 0;
                    qiInt = 0;
                    for (int j = 0; j < superGridControl1.PrimaryGrid.Rows.Count; j++)
                    {
                        sanInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[1].Value);
                        siInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[2].Value);
                        wuInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[3].Value);
                        liuInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[4].Value);
                        qiInt += Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[j]).Cells[5].Value);
                    }
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell1 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell2 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell3 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell4 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell5 = new DevComponents.DotNetBar.SuperGrid.GridCell();
                    DevComponents.DotNetBar.SuperGrid.GridCell gridCell6 = new DevComponents.DotNetBar.SuperGrid.GridCell();
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
                    gridCell6.Value = qiInt.ToString();
                    gridCell6.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    gridCell7.Value = (sanInt + siInt + wuInt + liuInt + qiInt).ToString();
                    gridCell7.CellStyles.Default.TextColor = System.Drawing.Color.Blue;
                    DevComponents.DotNetBar.SuperGrid.GridRow gridRow1 = new DevComponents.DotNetBar.SuperGrid.GridRow();
                    gridRow1.Cells.Add(gridCell1);
                    gridRow1.Cells.Add(gridCell2);
                    gridRow1.Cells.Add(gridCell3);
                    gridRow1.Cells.Add(gridCell4);
                    gridRow1.Cells.Add(gridCell5);
                    gridRow1.Cells.Add(gridCell6);
                    gridRow1.Cells.Add(gridCell7);
                    superGridControl1.PrimaryGrid.Rows.Add(gridRow1);
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {
                if (superGridControl2.PrimaryGrid.VirtualRowCount > 0)
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
                        string localFilePath = sfd.FileName.ToString(); //获得文件路径 
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
                        wk.Worksheets[0].Name = sj + "不及时报告列表";
                        AddExcelSHeet(wk);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "不及时报告列表数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误：" + ex.ToString());
            }
        }
        private void AddExcelSHeet(Aspose.Cells.Workbook wk)
        {
            if (superGridControl2.PrimaryGrid.VirtualRowCount > 0)
            {
                int i = 0;
                SelectedElementCollection col = superGridControl2.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int j = 0; j < superGridControl2.PrimaryGrid.Columns.Count; j++)
                    {
                        wk.Worksheets[0].Cells[i, j].PutValue(superGridControl2.PrimaryGrid.Columns[j].HeaderText);
                    }
                    for (int j = 0; j < col.Count; j++)
                    {
                        GridRow Row = col[j] as GridRow;
                        i = i + 1;
                        for (int k = 0; k < superGridControl2.PrimaryGrid.Columns.Count; k++)
                        {
                            wk.Worksheets[0].Cells[i, k].PutValue(Row.Cells[k].Value);
                        }
                    }
                    wk.Worksheets[0].AutoFitColumns();
                    wk.Worksheets[0].AutoFitRows();
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            if (Cmbjclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and modality = '{0}'", Cmbjclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }

            if (!comboBoxEx1.Text.Trim().Equals(""))
            {
                sb.AppendFormat("  and cbreport_doc_name='{0}'", comboBoxEx1.Text.Trim());
            }
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                DataTable dtt = new DataTable();
                dtt = dt.Clone();//拷贝框架
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int IntAddTime = 0;
                    //获取当前时间是星期几
                    //string received_datetime = dt.Rows[i]["received_datetime"].ToString();
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
                    int zk_time = Convert.ToInt32(dt.Rows[i]["sj"]);

                    if (dt.Rows[i]["exam_type"].ToString().Equals("XBX"))
                    {
                        if (zk_time - IntAddTime > 4320)
                        {
                            dtt.ImportRow(dt.Rows[i]);
                        }
                    }
                    else
                    {
                        if (zk_time - IntAddTime > 7200)
                        {
                            dtt.ImportRow(dt.Rows[i]);
                        }
                    }
                    superGridControl2.PrimaryGrid.DataSource = dtt;
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        superGridControl2.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", dtt.Rows.Count);
                    }
                    else
                    {
                        superGridControl2.PrimaryGrid.VirtualRowCount = 0;
                        superGridControl2.PrimaryGrid.DataSource = null;
                        superGridControl2.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
                    }
                }
            }
            else
            {
                superGridControl2.PrimaryGrid.VirtualRowCount = 0;
                superGridControl2.PrimaryGrid.DataSource = null;
                superGridControl2.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
            }
        }

    }
}
