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
    public partial class Frm_fwfy : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_fwfy()
        {
            InitializeComponent();
        }

        private void Frm_fwfy_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
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

        private void RefreshData()
        {
            if (dateTimePicker1.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" clrq>='{0} 00:00:00' and clrq<='{1} 23:59:59'", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.hcgl ins = new DBHelper.BLL.hcgl();
            dt = ins.GetInfoFwfy(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
            }

        }
        private void buttonX3_Click(object sender, EventArgs e)
        {

            Frm_hcgl_fwfy ins = new Frm_hcgl_fwfy();
            ins.BringToFront();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                RefreshData();
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        string id = "";
        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl1.PrimaryGrid.VirtualRows.Clear();
                    superGridControl1.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //
                id = Row.Cells["id"].Value.ToString();
                Oldid = id;
                textBox1.Text = Row.Cells["ylwp"].Value.ToString();
                textBox3.Text = Row.Cells["zl"].Value.ToString();
                textBox5.Text = Row.Cells["sl"].Value.ToString();
                textBox6.Text = Row.Cells["clff"].Value.ToString();
                textBox7.Text = Row.Cells["zxz"].Value.ToString();
                textBox8.Text = Row.Cells["hsz"].Value.ToString();
                textBox4.Text = Row.Cells["bz"].Value.ToString();
                dtStart.Value = Convert.ToDateTime(Row.Cells["clrq"].Value.ToString());
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim().Equals("") || textBox7.Text.Trim().Equals("") || textBox8.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "物品名称、执行者、回收者不能为空！");
                return;
            }
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
                //
                id = Row.Cells["id"].Value.ToString();
                DBHelper.BLL.hcgl ins = new DBHelper.BLL.hcgl();
                if (ins.UpdateInfoFwfy(textBox1.Text.Trim(), textBox3.Text.Trim(), textBox5.Text.Trim(), dtStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim(), textBox4.Text.Trim(), id) == 1)
                {
                    Frm_TJInfo("提示", "更新成功！");
                    RefreshData();
                }


            }
        }

        //选中指定id的记录
        private void SelectedOldid(string id)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.VirtualRowCount; i++)
            {
                string cur_studyno = ((GridRow)superGridControl1.PrimaryGrid.VirtualRows[i]).Cells["id"].Value.ToString();
                if (cur_studyno.Equals(id))
                {
                    //先清空以前选中的所有行
                    this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                    this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                    //最前面的黑色三角号跟着移动
                    this.superGridControl1.PrimaryGrid.SetActiveRow((GridContainer)superGridControl1.PrimaryGrid.VirtualRows[i]);
                    //选中当前行
                    superGridControl1.PrimaryGrid.SetSelectedRows(i, 1, true);
                    superGridControl1.PrimaryGrid.SetActiveRow((GridRow)superGridControl1.PrimaryGrid.VirtualRows[i]);
                    break;
                }
            }
        }
        string Oldid = "";

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            if (!Oldid.Equals(""))
            {
                SelectedOldid(Oldid);
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

        //导出excel

        private void buttonX7_Click(object sender, EventArgs e)
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
                    string wkName = "医疗废物废液管理记录";

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
                    MessageBox.Show("导出成功：" + localFilePath, "统计数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

    }
}
