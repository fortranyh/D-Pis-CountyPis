using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_History : DevComponents.DotNetBar.Office2007Form
    {
        public string patient_id = "";
        public string patient_name = "";
        public string exam_no = "";

        public Frm_History()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
        }
        string ReportFolder;
        private void Frm_History_Load(object sender, EventArgs e)
        {
            //报告图片路径
            ReportFolder = Program.APPdirPath + @"\Pis_Report_Image";
            if (Directory.Exists(ReportFolder) == false)
            {
                Directory.CreateDirectory(ReportFolder);
            }
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //锁定前四列
            panel1.FrozenColumnCount = 4;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //执行查询
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetHisReportList(" exam_no!='" + exam_no + "' and patient_name='" + patient_name + "' and exam_status>=20");
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
            this.panelEx1.AutoScroll = true;
        }
        //展示jpeg报告
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
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                //查询报告状态
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                int status = insM.GetStudyExam_Status(study_no);

                if (status >= 55)
                {
                    //下载
                    Frm_Ftp_Down ins = new Frm_Ftp_Down();
                    ins.ReportFolder = ReportFolder;
                    ins.study_no = study_no;
                    ins.ShowDialog();
                    string PathImg = ReportFolder + @"\" + study_no;
                    this.pictureBox1.Image = null;
                    if (Directory.Exists(PathImg))
                    {
                        DirectoryInfo Dir = new DirectoryInfo(PathImg);
                        foreach (FileInfo FI in Dir.GetFiles())
                        {
                            Image img = GetImage(FI.FullName);
                            this.pictureBox1.Image = img;

                            this.pictureBox1.Location = new Point((panelEx1.Width - img.Width) / 2, 0);
                            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

                        }
                    }
                }
            }
        }

        public Image GetImage(string path)
        {
            FileStream fs = new System.IO.FileStream(path, FileMode.Open);
            Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            return result;
        }
        //可以滚动
        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            panelEx1.Select();
            panelEx1.Focus();
        }

    }
}
