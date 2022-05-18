using Aspose.Cells;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace PathologyClient
{
    public partial class Frm_ReportQuery : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ReportQuery()
        {
            InitializeComponent();
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            pictureBox1.Image = null;
            buttonX2.Visible = false;
            Application.DoEvents();

            StringBuilder sb = new StringBuilder();
            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", "").ToUpper());
            }
            else if (!textBox1.Text.Trim().Equals(""))
            {
                if (comboBox5.SelectedIndex == 0)
                {
                    sb.AppendFormat(" and input_id='{0}'", textBox1.Text.Trim().Replace("'", ""));
                }
                else if (comboBox5.SelectedIndex == 1)
                {
                    sb.AppendFormat(" and patient_id='{0}'", textBox1.Text.Trim().Replace("'", ""));
                }
                else if (comboBox5.SelectedIndex == 2)
                {
                    sb.AppendFormat(" and hospital_card='{0}'", textBox1.Text.Trim().Replace("'", ""));
                }
            }
            else
            {
                if (radioButton4.Checked)
                {
                    sb.AppendFormat("and req_date_time>='{0} 00:00:00' and req_date_time<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton1.Checked)
                {
                    sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton2.Checked)
                {
                    sb.AppendFormat(" and cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton3.Checked)
                {
                    sb.AppendFormat("and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }


                if (!TxtName.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and patient_name like '%{0}%'", TxtName.Text.Trim().Replace("'", ""));
                }

                if (CmbBrly.SelectedIndex != 0)
                {
                    sb.AppendFormat(" and patient_source = '{0}'", CmbBrly.Text.Trim());
                }
                if (!txtms.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and rysj like '%{0}%'", txtms.Text.Trim().Replace("'", ""));
                }
                if (!Txtzd.Text.Trim().Equals(""))
                {
                    string[] values = Txtzd.Text.Trim().Replace("'", "").Split('@');
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (!values[i].Equals(""))
                        {
                            sb.Append(" and zdyj like '%");
                            sb.Append(values[i]);
                            sb.Append("%'");
                        }
                    }
                }
            }
            sb.AppendFormat(" and submit_unit = '{0}' and exam_status>=55 ", Program.HospitalName);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("tjStr", sb.ToString());
            sb = null;
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetExamMasterXML", parameters);
            parameters.Clear();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = ds.Tables[0];
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", ds.Tables[0].Rows.Count);
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
            }
            //激活列表
            superGridControl1.Select();
            superGridControl1.Focus();
        }

        private void Frm_ReportQuery_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.AllowRowResize = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //锁定
            panel1.FrozenColumnCount = 7;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            dtStart.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            dtEnd.Value = DateTime.Now;
            //
            CmbBrly.SelectedIndex = 0;
            //刷新列表
            comboBox5.SelectedIndex = 0;
            //执行查询
            buttonX1.PerformClick();
        }
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                pictureBox1.Image = null;
                buttonX2.Visible = false;
                Application.DoEvents();
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                if (exam_status >= 55)
                {
                    parameters.Add("exam_no", exam_no);
                    parameters.Add("img_type", "7");
                    string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetReportImgFlag", parameters);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(xmldata);
                    string flag = xmlDoc.DocumentElement.InnerText;
                    if (flag.Equals("0"))
                    {
                        Frm_TJInfo("提示", "整体报告不存在！");
                    }
                    else
                    {
                        //报告图片路径
                        string ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
                        if (Directory.Exists(ReportFolder) == false)
                        {
                            Directory.CreateDirectory(ReportFolder);
                        }
                        string filePathStr = string.Format(@"{0}\{1}.jpg", ReportFolder, exam_no);
                        filePathStr = PublicBaseLib.PostWebService.PostCallWebServiceForImgFile(Program.WebServerUrl, "GetReportImgBinary", parameters, filePathStr);
                        if (!filePathStr.Equals("") && File.Exists(@filePathStr))
                        {
                            Image img = GetImage(@filePathStr);
                            this.pictureBox1.Image = img;
                            this.pictureBox1.Location = new Point((panelEx1.Width - img.Width) / 2, 0);
                            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                        }
                        buttonX2.Visible = true;
                    }
                    parameters.Clear();
                    this.panelEx1.AutoScroll = true;
                }
            }
        }
        public Image GetImage(string path)
        {
            //加水印
            PublicBaseLib.WaterAddToPic.AddWaterText(@path, @path, string.Format("{0} {1}", Program.HospitalName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")), PublicBaseLib.WaterPositionMode.RightBottom, "#FF0000", 127);
            //
            FileStream fs = new System.IO.FileStream(path, FileMode.Open);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            File.Delete(@path);
            return result;
        }
        private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("status_name") == true)
            {
                string strStatus = (string)e.GridCell.Value;
                if (Program.exam_status_name_dic.ContainsKey(strStatus))
                {
                    e.Style.TextColor = Program.exam_status_name_dic[strStatus];
                }
            }
        }


        //打印
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                if (!PathologyClient.Properties.Settings.Default.CurPrinter.Equals(""))
                {
                    Print();
                }
                else
                {
                    Frm_TJInfo("提示", "请先点击系统设置，设置好报告打印机！");
                }
            }
            else
            {
                Frm_TJInfo("提示", "不能打印！");
            }
        }
        public void Print()
        {
            try
            {

                var printerSettings = new PrinterSettings
                {
                    PrinterName = PathologyClient.Properties.Settings.Default.CurPrinter
                };
                PrintDocument document = new PrintDocument
                {
                    PrinterSettings = printerSettings,
                    DocumentName = Thread.CurrentThread.Name,
                    PrintController = new StandardPrintController()
                };

                document.PrinterSettings.Collate = true;
                document.PrinterSettings.Copies = 1;
                document.PrintPage += OnPrintPage;
                document.Print();
                Frm_TJInfo("提示", "打印成功！");
            }
            catch
            {
            }
        }
        private void OnPrintPage(object sender, PrintPageEventArgs e)
        {

            try
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    Application.DoEvents();
                    e.Graphics.DrawImage(pictureBox1.Image, e.Graphics.VisibleClipBounds);
                    e.HasMorePages = false;
                    pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                    Application.DoEvents();
                }
            }
            catch
            {
            }
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panelEx1.Select();
            panelEx1.Focus();
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                string patient_name = Row.Cells["pat_name"].Value.ToString();
                string study_no = Row.Cells["study_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                if (exam_status >= 20)
                {
                    Form Frm_SjzIns = Application.OpenForms["Frm_Sjz"];
                    if (Frm_SjzIns == null)
                    {
                        Frm_SjzIns = new Frm_Sjz();
                        Frm_SjzIns.BringToFront();
                        Frm_SjzIns.WindowState = FormWindowState.Normal;
                        Frm_Sjz.exam_no = exam_no;
                        Frm_Sjz.patient_name = patient_name;
                        Frm_Sjz.study_no = study_no;
                        Frm_SjzIns.Owner = this;
                        Frm_SjzIns.ShowDialog();
                    }
                    else
                    {
                        Frm_Sjz.exam_no = exam_no;
                        Frm_Sjz.patient_name = patient_name;
                        Frm_Sjz.study_no = study_no;
                        Frm_SjzIns.BringToFront();
                        Frm_SjzIns.WindowState = FormWindowState.Normal;
                    }
                }
                else
                {
                    Frm_TJInfo("提示", "病理诊断中心还未接收标本；\n当前状态没有时间轴信息！");
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            try
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
                        wk.Worksheets[0].Name = sj + "报告列表";
                        AddExcelSHeet(wk);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "报告列表数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误：" + ex.ToString());
            }
        }
        /// <summary>
        /// 设置表页的列宽度自适应
        /// </summary>
        /// <param name="sheet">worksheet对象</param>
        void setColumnWithAuto(Worksheet sheet)
        {
            Cells cells = sheet.Cells;
            int columnCount = cells.MaxColumn;  //获取表页的最大列数
            int rowCount = cells.MaxRow;        //获取表页的最大行数

            for (int col = 0; col < columnCount; col++)
            {
                sheet.AutoFitColumn(col, 0, rowCount);
            }
            for (int col = 0; col < columnCount; col++)
            {
                cells.SetColumnWidthPixel(col, cells.GetColumnWidthPixel(col) + 30);
            }
        }
        private void AddExcelSHeet(Aspose.Cells.Workbook wk)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                int i = 0;
                for (int j = 0; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                {
                    wk.Worksheets[0].Cells[i, j].PutValue(superGridControl1.PrimaryGrid.Columns[j].HeaderText);
                }
                for (int index = 0; index < superGridControl1.PrimaryGrid.Rows.Count; index++)
                {
                    GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                    i = i + 1;
                    for (int j = 0; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                    {
                        if (Row.Cells[j].Value == null)
                        {
                            wk.Worksheets[0].Cells[i, j].PutValue("");
                        }
                        else
                        {
                            wk.Worksheets[0].Cells[i, j].PutValue(Row.Cells[j].Value.ToString());
                        }
                    }
                }
                setColumnWithAuto(wk.Worksheets[0]);
            }
        }
    }
}
