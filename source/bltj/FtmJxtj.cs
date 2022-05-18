using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class FtmJxtj : DevComponents.DotNetBar.Office2007Form
    {
        public FtmJxtj()
        {
            InitializeComponent();
        }
        DataTable dttj = null;
        private void FtmJxtj_Load(object sender, EventArgs e)
        {
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
            //创建内存表
            dttj = new DataTable("mxtj");
            //医生姓名
            dttj.Columns.Add("ysxm", typeof(string));
            //组织学报告总数
            dttj.Columns.Add("blbg", typeof(string));
            //常规病理报告
            dttj.Columns.Add("havejx", typeof(string));
            //细胞学病理报告
            dttj.Columns.Add("nojx", typeof(string));
            superGridControl1.PrimaryGrid.DataSource = dttj;
            //
            dtStart.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            dtEnd.Value = DateTime.Now;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                // 
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
                    string wkName = "组织学镜下描述";
                    string localFilePath = sfd.FileName.ToString(); //获得文件路径 
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
                    MessageBox.Show("导出成功：" + localFilePath, "组织学镜下描述统计数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
        int blbg = 0;
        int havejx = 0;
        int nojx = 0;
        private void Caltj(string ysxm, Boolean flag)
        {
            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            DataRow dr = dttj.NewRow();
            //医生姓名
            dr["ysxm"] = ysxm;
            if (flag)
            {
                //组织病理总数
                dr["blbg"] = blbg.ToString();
                //含有镜下描述
                dr["havejx"] = havejx.ToString();
                //不包含镜下描述
                dr["nojx"] = nojx.ToString();
            }
            else
            {
                if (checkBoxX1.Checked)
                {
                    //组织病理总数
                    string sqlstr = string.Format("select count(*) from exam_report_view  where tmplet_index=0 and  exam_type='PL'  and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and zzreport_doc_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                    DataTable dt = ins.GetDt(sqlstr);
                    dr["blbg"] = dt.Rows[0][0].ToString();
                    blbg += Convert.ToInt32(dt.Rows[0][0]);
                    //含有镜下描述
                    sqlstr = string.Format("select count(*) from exam_report_view  where zdpz<>'' and  tmplet_index=0 and  exam_type='PL' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and zzreport_doc_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                    dt = ins.GetDt(sqlstr);
                    dr["havejx"] = dt.Rows[0][0].ToString();
                    havejx += Convert.ToInt32(dt.Rows[0][0]);
                    //不包含镜下描述
                    sqlstr = string.Format("select count(*) from exam_report_view  where zdpz='' and  tmplet_index=0 and  exam_type='PL'  and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and zzreport_doc_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                    dt = ins.GetDt(sqlstr);
                    dr["nojx"] = dt.Rows[0][0].ToString();
                    nojx += Convert.ToInt32(dt.Rows[0][0]);

                }
                else
                {
                    //组织病理总数
                    string sqlstr = string.Format("select count(*) from exam_report_view  where tmplet_index=0 and  exam_type='PL'  and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and cbreport_doc_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                    DataTable dt = ins.GetDt(sqlstr);
                    dr["blbg"] = dt.Rows[0][0].ToString();
                    blbg += Convert.ToInt32(dt.Rows[0][0]);
                    //含有镜下描述
                    sqlstr = string.Format("select count(*) from exam_report_view  where zdpz<>'' and  tmplet_index=0 and  exam_type='PL' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and cbreport_doc_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                    dt = ins.GetDt(sqlstr);
                    dr["havejx"] = dt.Rows[0][0].ToString();
                    havejx += Convert.ToInt32(dt.Rows[0][0]);
                    //不包含镜下描述
                    sqlstr = string.Format("select count(*) from exam_report_view  where zdpz='' and  tmplet_index=0 and  exam_type='PL'  and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and cbreport_doc_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                    dt = ins.GetDt(sqlstr);
                    dr["nojx"] = dt.Rows[0][0].ToString();
                    nojx += Convert.ToInt32(dt.Rows[0][0]);
                }

            }
            dttj.Rows.Add(dr);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            blbg = 0;
            havejx = 0;
            nojx = 0;
            dttj.Rows.Clear();
            superGridControl1.PrimaryGrid.InvalidateLayout();
            //查询部门全部人员
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet dsMas = doctor_dict_ins.GetDsBgys_User(PIS_Statistics.Frm_Tjcx.CurDept_Code);
            if (dsMas != null && dsMas.Tables[0].Rows.Count > 0)
            {
                DataTable dtMas = dsMas.Tables[0];
                for (int i = 0; i < dtMas.Rows.Count; i++)
                {
                    Caltj(dtMas.Rows[i]["user_name"].ToString(), false);
                }
                Caltj("总计：", true);
            }
            //刷新
            superGridControl1.PrimaryGrid.DataSource = dttj;
            Application.DoEvents();
            superGridControl1.PrimaryGrid.InvalidateLayout();
        }


    }
}
