using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class FrmLcgtList : DevComponents.DotNetBar.Office2007Form
    {
        public FrmLcgtList()
        {
            InitializeComponent();
        }

        private void FrmLcgtList_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
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
            buttonX1.PerformClick();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" and im_time>='{0} 00:00:00' and im_time<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            //加载已经存在的不合格
            DataSet _DataSet = new DataSet();
            DBHelper.BLL.exam_lcys_im ins = new DBHelper.BLL.exam_lcys_im();
            _DataSet = ins.GetlcgtList(sb.ToString());
            sb.Clear();
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = _DataSet.Tables[0];
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到总数为(<font color='blue'><b>{0}</b></font>)", _DataSet.Tables[0].Rows.Count.ToString());
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到总数为(<font color='blue'><b>{0}</b></font>)", "0");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
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
                        wk.Worksheets[0].Name = sj + "临床医师沟通信息";
                        DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                        AddExcelSHeet(wk, dt);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "临床医师沟通信息导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
