using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Djb : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Djb()
        {
            InitializeComponent();
        }

        private void Frm_Djb_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            if (cmbJclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and exam_type = '{0}'", cmbJclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }

            if (checkBoxX1.Checked)
            {
                sb.Append(" and new_flag=1 ");
            }


            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            DataTable dt = ins.GetDjbInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到病人总数为(<font color='blue'><b>{0}</b></font>)", dt.Rows.Count.ToString());
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到病人总数为(<font color='blue'><b>{0}</b></font>)", "0");
            }

        }
        //导出excel
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
                        wk.Worksheets[0].Name = sj + "病理登记薄";
                        DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                        AddExcelSHeet(wk, dt);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "病理登记薄数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Frm_djbdy ins = new Frm_djbdy();
            ins.Owner = this;
            ins.Show();
        }
        /// <summary>
        /// 读取excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readExcel(object sender, EventArgs e)
        {
            Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(@"D:\PIS_Source\PIS_Source\ICD-10疾病编码表.xls");
            Aspose.Cells.Cells cellsIns;
            string big_type;
            string icd_code;
            string icd_name;
            string icd_py;
            DBHelper.BLL.icd10dict ins = new DBHelper.BLL.icd10dict();
            for (int i = 0; i < wk.Worksheets.Count; i++)
            {
                cellsIns = wk.Worksheets[i].Cells;
                big_type = wk.Worksheets[i].Name;
                if (cellsIns.MaxDataRow > 0)
                {
                    for (int j = 2; j < cellsIns.MaxDataRow; j++)
                    {
                        icd_code = cellsIns[j, 0].StringValue.Trim();
                        icd_name = cellsIns[j, 1].StringValue.Trim();
                        icd_py = PinYinConverter.GetFirst(icd_name);
                        ins.Inserticd10dict(big_type, icd_code, icd_name, icd_py);
                    }
                }
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow Row = col[i] as GridRow;
                        //病理号
                        string study_no = Row.Cells["study_no"].Value.ToString();
                        //姓名
                        string pat_name = Row.Cells["patient_name"].Value.ToString();
                        //如果状态为标本接收状态则打印登记条码
                        FastReportLib.PrintBarCode.PrintBarcode(study_no, pat_name, Frm_Tjcx.BarcodePrinter, 1);
                    }
                }
            }
        }




    }
}
