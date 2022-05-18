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
    public partial class Frm_Sjckdj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Sjckdj()
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
        private void Frm_Sjckdj_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
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
            textBox3.Text = Program.User_Name;
            //试剂名称  
            DBHelper.BLL.shijiguanlics ins = new DBHelper.BLL.shijiguanlics();
            DataSet ds = ins.GetSjInfo();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                comboBox1.DataSource = ds.Tables[0];
                comboBox1.DisplayMember = "sjmc";
                comboBox1.ValueMember = "cursl";
            }
            comboBox1.SelectedValueChanged += new EventHandler(comboBox1_SelectedValueChanged);
            comboBox1.SelectedIndex = -1;
            RefreshData();
        }
        //查询
        private void buttonX1_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void RefreshData()
        {
            if (dateTimePicker1.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" cksj>='{0} 00:00:00' and cksj<='{1} 23:59:59'", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataTable dt = new DataTable();
            DBHelper.BLL.shijiguanlics ins = new DBHelper.BLL.shijiguanlics();
            dt = ins.GetInfoShijichuku(sb.ToString());
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1 && comboBox1.Items.Count > 0)
            {
                string str = comboBox1.SelectedValue.ToString();
                this.doubleInput2.Value = Convert.ToDouble(str);

            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                Frm_TJInfo("提示", "请先入库！");
                return;
            }
            if (doubleInput1.Value > doubleInput2.Value)
            {
                Frm_TJInfo("提示", "出库量不能大于库存！");
                return;
            }
            if (comboBox1.SelectedIndex != -1)
            {

                DBHelper.BLL.shijiguanlics ins = new DBHelper.BLL.shijiguanlics();
                if (ins.AddInfoShijichuku(comboBox1.Text.Trim(), doubleInput1.Value, this.dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.textBox3.Text.Trim(), this.textBox4.Text.Trim()) == 1)
                {
                    RefreshData();
                    //减少库存
                    string sjmcOld = comboBox1.Text.Trim();
                    if (ins.UpdateInfoShijiruku(sjmcOld, doubleInput2.Value - doubleInput1.Value) == 1)
                    {
                        comboBox1.SelectedValueChanged -= new EventHandler(comboBox1_SelectedValueChanged);

                        //试剂名称  
                        DataSet ds = ins.GetSjInfo();
                        if (ds != null && ds.Tables[0].Rows.Count > 0)
                        {
                            comboBox1.DataSource = ds.Tables[0];
                            comboBox1.DisplayMember = "sjmc";
                            comboBox1.ValueMember = "cursl";
                        }
                        comboBox1.SelectedValueChanged += new EventHandler(comboBox1_SelectedValueChanged);
                        comboBox1.SelectedIndex = -1;
                        comboBox1.Text = sjmcOld;
                        Frm_TJInfo("提示", "试剂出库录入成功！");
                    }

                }
            }
        }

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
                    string wkName = "试剂出库记录";

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
                    string sj = string.Format("{0}到{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    wk.Worksheets[0].Name = sj + wkName;
                    DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                    AddExcelSHeet(wk, dt);
                    wk.Save(localFilePath);
                    MessageBox.Show("导出成功：" + localFilePath, "统计数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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


    }
}
