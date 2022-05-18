using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_JsPjInfo : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_JsPjInfo()
        {
            InitializeComponent();
        }

        private void Frm_JsPjInfo_Load(object sender, EventArgs e)
        {
            //评级原因
            DBHelper.BLL.exam_filmmaking insStop = new DBHelper.BLL.exam_filmmaking();
            DataTable dtStop = insStop.GetQpPj_Data();
            comboBoxEx3.Items.Add("");
            if (dtStop != null && dtStop.Rows.Count > 0)
            {
                for (int i = 0; i < dtStop.Rows.Count; i++)
                {
                    comboBoxEx3.Items.Add(dtStop.Rows[i]["pj_info"].ToString());
                }
            }
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
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
            //
            this.dateTimePicker1.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            this.dateTimePicker2.Value = DateTime.Now;
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl2.PrimaryGrid.RowHeaderIndexOffset = 1;
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
            //绑定医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsJSExam_User(PIS_Statistics.Frm_Tjcx.CurDept_Code);
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
            DataSet ds1 = doctor_dict_ins.GetDsJSExam_User(PIS_Statistics.Frm_Tjcx.CurDept_Code);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                comboBoxEx2.DataSource = ds1.Tables[0];
                comboBoxEx2.DisplayMember = "user_name";
                comboBoxEx2.ValueMember = "user_code";
            }
            else
            {
                comboBoxEx2.DataSource = null;
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
                        wk.Worksheets[0].Name = sj + "切片信息列表";
                        AddExcelSHeet(wk);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "切片信息列表数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void AddExcelSHeet1(Aspose.Cells.Workbook wk)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                int i = 0;
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int j = 0; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                    {
                        wk.Worksheets[0].Cells[i, j].PutValue(superGridControl1.PrimaryGrid.Columns[j].HeaderText);
                    }
                    for (int j = 0; j < col.Count; j++)
                    {
                        GridRow Row = col[j] as GridRow;
                        i = i + 1;
                        for (int k = 0; k < superGridControl1.PrimaryGrid.Columns.Count; k++)
                        {
                            wk.Worksheets[0].Cells[i, k].PutValue(Row.Cells[k].Value);
                        }
                    }
                    wk.Worksheets[0].AutoFitColumns();
                    wk.Worksheets[0].AutoFitRows();
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
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
                        string sj = string.Format("{0}到{1}", dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));
                        wk.Worksheets[0].Name = sj + "免疫组化切片信息列表";
                        AddExcelSHeet1(wk);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "免疫组化切片信息列表数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误：" + ex.ToString());
            }
        }
        //查询免疫组化切片列表
        private void buttonX4_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" and zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'", this.dateTimePicker1.Value.ToString("yyyy-MM-dd"), dateTimePicker2.Value.ToString("yyyy-MM-dd"));

            if (!comboBox1.Text.Equals(""))
            {
                sb.AppendFormat(" and myzh_yl = '{0}'", comboBox1.Text.Trim());
            }
            if (!comboBoxEx2.Text.Trim().Equals(""))
            {
                sb.AppendFormat("  and zx_doc_name='{0}'", comboBoxEx2.Text.Trim());
            }

            if (!textBoxX1.Text.Trim().Equals(""))
            {
                sb.AppendFormat("  and myzh_bz like '%{0}%'", textBoxX1.Text.Trim());
            }


            DataTable dt = new DataTable();

            string sqlstr = "select study_no as 病理号,lk_no as  蜡块号,work_source as 套餐名,bj_name as 标记物,sq_datetime as 申请时间,zx_doc_name as 执行医师,zx_datetime as 执行时间,myzh_yl as level,myzh_bz as 评级备注, sq_doctor_name as 评级医师  from exam_tjyz where  yz_flag='已执行' " + sb.ToString();

            DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
            dt = ins.GetDataTable(sqlstr);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                tj_func1(dt);
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
        //查询切片列表
        private void buttonX2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(" and make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));

            if (!Cmcgjb.Text.Equals(""))
            {
                sb.AppendFormat(" and level = '{0}'", Cmcgjb.Text.Trim());
            }
            if (!comboBoxEx1.Text.Trim().Equals(""))
            {
                sb.AppendFormat("  and make_doc_name='{0}'", comboBoxEx1.Text.Trim());
            }

            if (!comboBoxEx3.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and level_memo='{0}'", comboBoxEx3.Text.Trim());
            }
            DataTable dt = new DataTable();

            string sqlstr = "select study_no as 病理号,draw_barcode as 切片号,work_source as 来源,memo_note as 备注,bm_doc_name as 包埋技师,bm_datetime as 包埋时间,make_doc_name as 制片技师,make_datetime as 制片时间,level,level_memo as 评级备注,level_doctor_name as 评级医师 from exam_filmmaking  where zp_flag>=0 " + sb.ToString();

            DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
            dt = ins.GetDataTable(sqlstr);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dt;
                tj_func(dt);
                //激活列表
                superGridControl2.Select();
                superGridControl2.Focus();
            }
            else
            {
                superGridControl2.PrimaryGrid.VirtualRowCount = 0;
                superGridControl2.PrimaryGrid.DataSource = null;
            }
        }
        //计数
        public void tj_func(DataTable dt)
        {
            int rows = dt.Rows.Count;
            float jia = 0;
            int yi = 0;
            int bing = 0;
            int qt = 0;
            for (int i = 0; i < rows; i++)
            {
                //级别
                string level = dt.Rows[i]["level"].ToString();
                switch (level)
                {
                    case "甲级":
                        jia += 1;
                        break;
                    case "乙级":
                        yi += 1;
                        break;
                    case "丙级":
                        bing += 1;
                        break;
                    default:
                        qt += 1;
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl2.PrimaryGrid.Footer == null)
                    {
                        superGridControl2.PrimaryGrid.Footer = new GridFooter();
                    }
                    float yxl = 0;
                    yxl = (jia * 100 / rows);
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    superGridControl2.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:甲<font color='blue'><b>{1}</b></font>,乙<font color='blue'><b>{2}</b></font>,丙<font color='blue'><b>{3}</b></font>,其他<font color='blue'><b>{4}</b></font>，HE染色切片优良率：<font color='blue'><b>{5}%</b></font>", rows, jia, yi, bing, qt, yxlstr);
                }
            }
        }

        //计数
        public void tj_func1(DataTable dt)
        {
            int rows = dt.Rows.Count;
            float jia = 0;
            int yi = 0;
            int bing = 0;
            int qt = 0;
            for (int i = 0; i < rows; i++)
            {
                //级别
                string level = dt.Rows[i]["level"].ToString();
                switch (level)
                {
                    case "甲级":
                        jia += 1;
                        break;
                    case "乙级":
                        yi += 1;
                        break;
                    case "丙级":
                        bing += 1;
                        break;
                    default:
                        qt += 1;
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    float yxl = 0;
                    yxl = (jia * 100 / rows);
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:甲<font color='blue'><b>{1}</b></font>,乙<font color='blue'><b>{2}</b></font>,丙<font color='blue'><b>{3}</b></font>,其他<font color='blue'><b>{4}</b></font>，免疫组化切片优良率：<font color='blue'><b>{5}%</b></font>", rows, jia, yi, bing, qt, yxlstr);
                }
            }
        }
    }
}
