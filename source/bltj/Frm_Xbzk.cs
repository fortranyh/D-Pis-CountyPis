using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_Xbzk : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Xbzk()
        {
            InitializeComponent();
        }

        private void Frm_Xbzk_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
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
            //显示本年度信息
            RefreshData();
        }

        private void RefreshData()
        {
            if (dateTimePicker1.Value > dtEnd.Value)
            {
                MessageBox.Show("起始时间必须小于终止时间！", "时间范围错误！");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" date>='{0} 00:00:00' and date<='{1} 23:59:59'", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.Sjzk_info ins = new DBHelper.BLL.Sjzk_info();
            dt = ins.GetInfoXbzk(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                tj_func1(dt);
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
        }
        //查询
        private void buttonX4_Click(object sender, EventArgs e)
        {
            count = 0;
            RefreshData();
        }
        int count = 0;
        //调出添加窗体
        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals("") || textBox2.Text.Trim().Equals(""))
            {
                MessageBox.Show("起始终止涂片号都不能为空！", "提示");
                return;
            }
            if (textBox8.Text.Trim().Equals("") || comboBox1.Text.Trim().Equals(""))
            {
                MessageBox.Show("涂片号、阴阳性不能为空！", "提示");
                return;
            }
            int start = Convert.ToInt32(textBox1.Text.Trim().Substring(1, textBox1.Text.Trim().Length - 1));
            int end = Convert.ToInt32(textBox2.Text.Trim().Substring(1, textBox2.Text.Trim().Length - 1));
            if (end < start)
            {
                MessageBox.Show("涂片号终止号必须大于起始号！", "提示");
                return;
            }
            count = end - start + 1;
            DBHelper.BLL.Sjzk_info ins = new DBHelper.BLL.Sjzk_info();
            if (ins.AddInfoXbzk(textBox8.Text.Trim(), comboBox1.Text.Trim(), dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox5.Text.Trim(), textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), comboBox2.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim()) == 1)
            {
                DialogResult = DialogResult.OK;
                RefreshData();
            }





        }
        string id = "";
        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl1.PrimaryGrid.Rows.Clear();
                    superGridControl1.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //
                id = Row.Cells["id"].Value.ToString();
                textBox8.Text = Row.Cells["num"].Value.ToString();
                comboBox1.Text = Row.Cells["yyx"].Value.ToString();
                dateTimePicker2.Value = Convert.ToDateTime(Row.Cells["date"].Value.ToString());
                textBox5.Text = Row.Cells["bz"].Value.ToString();

            }
        }
        //计数
        public void tj_func(DataTable dt)
        {
            if (count != 0)
            {
                int rows = dt.Rows.Count;
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }
                float yxl = 0;
                if (rows != 0)
                {
                    yxl = (rows * 100 / count);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("细胞学病理质控诊断符合率<font color='blue'><b>{0}% </b></font>", yxlstr);
            }
        }

        //计数YH修改
        public void tj_func1(DataTable dt)
        {
            StringBuilder sb1 = new StringBuilder();
            sb1.AppendFormat(" date>='{0} 00:00:00' and date<='{1} 23:59:59' and byyyx=zkyyx", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DBHelper.BLL.Sjzk_info ins1 = new DBHelper.BLL.Sjzk_info();
            int aa = ins1.GetInfoXbzkint(sb1.ToString());
            if (aa != 0)
            {
                int rows = dt.Rows.Count;
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }
                float yxl = 0;
                if (rows != 0)
                {
                    yxl = (aa * 100 / rows);
                }
                string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("细胞学病理质控诊断符合率<font color='blue'><b>{0}% </b></font>", yxlstr);
            }
        }

        private void AddExcelSHeet(Aspose.Cells.Workbook wk, DataTable dt)
        {
            DataRow[] werow = dt.Select();
            if (werow.Length > 0)
            {
                int i = 0;
                for (int j = 1; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                {
                    wk.Worksheets[0].Cells[i, j - 1].PutValue(superGridControl1.PrimaryGrid.Columns[j].HeaderText);
                }
                foreach (DataRow rows in werow)
                {
                    i = i + 1;
                    for (int j = 1; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                    {
                        wk.Worksheets[0].Cells[i, j - 1].PutValue(rows[j].ToString());
                    }
                }
            }
        }

        private void buttonX1_Click_1(object sender, EventArgs e)
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
                    string wkName = "";
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
                    string sj = string.Format("{0}到{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    wk.Worksheets[0].Name = sj + wkName;
                    DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                    AddExcelSHeet(wk, dt);
                    wk.Save(localFilePath);
                    MessageBox.Show("导出成功：" + localFilePath, "统计数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
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
                //
                id = Row.Cells["id"].Value.ToString();
                DBHelper.BLL.Sjzk_info ins = new DBHelper.BLL.Sjzk_info();
                if (ins.UpdateInfo(textBox8.Text.Trim(), comboBox1.Text.Trim(), textBox5.Text.Trim(), textBoxX1.Text.Trim(), textBoxX2.Text.Trim(), comboBox2.Text.Trim(), textBox1.Text.Trim(), textBox2.Text.Trim(), id) == 1)
                {
                    MessageBox.Show("更新成功！", "提示");
                    RefreshData();
                }


            }
        }

        private void superGridControl1_SelectionChanged_1(object sender, GridEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl1.PrimaryGrid.Rows.Clear();
                    superGridControl1.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //
                id = Row.Cells["id"].Value.ToString();
                textBox1.Text = Row.Cells["qsh"].Value.ToString();
                textBox2.Text = Row.Cells["zzh"].Value.ToString();
                textBox5.Text = Row.Cells["bz"].Value.ToString();
                textBox8.Text = Row.Cells["num"].Value.ToString();
                comboBox1.Text = Row.Cells["byyyx"].Value.ToString();
                textBoxX1.Text = Row.Cells["byzd"].Value.ToString();
                comboBox2.Text = Row.Cells["zkyyx"].Value.ToString();
                textBoxX2.Text = Row.Cells["zkzd"].Value.ToString();
                dateTimePicker2.Value = Convert.ToDateTime(Row.Cells["date"].Value.ToString());

            }
        }
    }
}
