using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_BdReport : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BdReport()
        {
            InitializeComponent();
        }
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
        private void Frm_BdReport_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
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
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                FastReportLib.iceReportParas insM = new FastReportLib.iceReportParas();
                insM.study_no = study_no;
                insM.ReportParaHospital = Program.HospitalName;
                DataTable dt = null;
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                dt = ins.GetDt("select received_datetime,patient_name,sex,req_dept,age,input_id,bed_no,exam_no from exam_master_view  where study_no='" + study_no + "'");
                if (dt != null && dt.Rows.Count == 1)
                {
                    insM.Txt_Jsrq = dt.Rows[0]["received_datetime"].ToString();
                    insM.Txt_Name = dt.Rows[0]["patient_name"].ToString();
                    insM.Txt_Sex = dt.Rows[0]["sex"].ToString();
                    insM.Txt_Age = dt.Rows[0]["age"].ToString();
                    insM.exam_no = dt.Rows[0]["exam_no"].ToString();
                    insM.req_dept = dt.Rows[0]["req_dept"].ToString();
                    insM.Txt_Content = Row.Cells["content"].Value.ToString();
                    insM.Txt_BgDate = Row.Cells["report_datetime"].Value.ToString();
                    insM.Txt_report_Doctor = Row.Cells["report_doc_name"].Value.ToString();
                    insM.shys = Row.Cells["sh_doc_name"].Value.ToString();
                    insM.zyh = dt.Rows[0]["input_id"].ToString();
                    if (!dt.Rows[0]["bed_no"].ToString().Equals(""))
                    {
                        insM.ch = dt.Rows[0]["bed_no"].ToString() + "床";
                    }
                    if (FastReportLib.IceReport.PrintIceReport(insM, PIS_Sys.Properties.Settings.Default.ReportPrinter, PIS_Sys.Properties.Settings.Default.ReportPrintNum))
                    {
                        Frm_TJInfo("提示", "报告打印成功！");
                    }
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();


            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and a.study_no = '{0}'", Txtblh.Text.Trim().Replace("'", "").ToUpper());
            }
            else if (!txtms.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and content like '%{0}%'", txtms.Text.Trim().Replace("'", ""));
            }
            else
            {
                sb.AppendFormat(" and report_datetime>='{0} 00:00:00' and report_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            }

            if (checkBoxX2.Checked == true)
            {
                sb.AppendFormat(" and slfh='{0}' ", "不符合");
            }

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
            dt = ins.GetShReportList(sb.ToString());

            if (dt != null && dt.Rows.Count > 0)
            {

                if (checkBoxX1.Checked == true)
                {
                    DataTable dtt = new DataTable();
                    dtt = dt.Clone();//拷贝框架
                    DataRow[] dr = dt.Select("sj>45");
                    for (int i = 0; i < dr.Length; i++)
                    {
                        dtt.ImportRow((DataRow)dr[i]);
                    }
                    superGridControl1.PrimaryGrid.DataSource = dtt;
                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = dt;
                }
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的报告信息！");
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (this.checkBoxX8.Checked == false && this.checkBoxX6.Checked == false)
            {
                Frm_TJInfo("提示", "请先符合或者不符合");
                return;
            }
            //
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                //选中的记录
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                int id = Convert.ToInt32(Row.Cells["id"].Value);

                string level = "";
                if (this.checkBoxX8.Checked == true)
                {
                    level = "符合";
                }
                else if (this.checkBoxX6.Checked == true)
                {
                    level = "不符合";
                }
                DBHelper.BLL.exam_ice_report ins = new DBHelper.BLL.exam_ice_report();
                if (ins.Updateslfh(id, level) == 1)
                {
                    Frm_TJInfo("提示", "设置成功！");
                    buttonX1.PerformClick();
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            try
            {
                if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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
                        wk.Worksheets[0].Name = sj + "病理冰冻报告";
                        DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                        AddExcelSHeet(wk, dt);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "病理冰冻报告数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误：" + ex.ToString());
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
