using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_tj2 : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_tj2()
        {
            InitializeComponent();
        }
        DataTable dttj = null;
        private void Frm_tj2_Load(object sender, EventArgs e)
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
            //病理报告总数
            dttj.Columns.Add("blbg", typeof(string));
            //常规病理报告
            dttj.Columns.Add("cgblbg", typeof(string));
            //细胞学病理报告
            dttj.Columns.Add("xbxblbg", typeof(string));
            //TCT病理报告
            dttj.Columns.Add("tctblbg", typeof(string));
            //分子病理报告
            dttj.Columns.Add("fzblbg", typeof(string));
            //会诊病理报告
            dttj.Columns.Add("hzblbg", typeof(string));
            //冰冻病理报告
            dttj.Columns.Add("iceblbg", typeof(string));
            //标本接收数
            dttj.Columns.Add("bbs", typeof(string));
            //蜡块数
            dttj.Columns.Add("lks", typeof(string));
            //包埋数
            dttj.Columns.Add("bms", typeof(string));
            //制片数
            dttj.Columns.Add("zps", typeof(string));
            //免疫组化报告
            dttj.Columns.Add("myzhbg", typeof(string));
            //免疫组化切片
            dttj.Columns.Add("myzhqp", typeof(string));
            superGridControl1.PrimaryGrid.DataSource = dttj;
        }

        private void buttonX2_Click(object sender, EventArgs e)
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
                    string wkName = "医师工作量明细统计";

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
                    MessageBox.Show("导出成功：" + localFilePath, "数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        //查询
        private void buttonX1_Click(object sender, EventArgs e)
        {
            blbg = 0;
            cgblbg = 0;
            xbxblbg = 0;
            tctblbg = 0;
            fzblbg = 0;
            hzblbg = 0;
            iceblbg = 0;
            bbs = 0;
            lks = 0;
            bms = 0;
            zps = 0;
            myzhbg = 0;
            myzhqp = 0;
            dttj.Rows.Clear();
            superGridControl1.PrimaryGrid.InvalidateLayout();
            //查询部门全部人员
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            DataTable dt = ins.SysAllUsers();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Caltj(dt.Rows[i]["user_name"].ToString(), false);
                }
                Caltj("总计：", true);
            }
            //刷新
            superGridControl1.PrimaryGrid.DataSource = dttj;
            Application.DoEvents();
            superGridControl1.PrimaryGrid.InvalidateLayout();
        }
        int blbg = 0;
        int cgblbg = 0;
        int xbxblbg = 0;
        int tctblbg = 0;
        int fzblbg = 0;
        int hzblbg = 0;
        int iceblbg = 0;
        int bbs = 0;
        int lks = 0;
        int bms = 0;
        int zps = 0;
        int myzhbg = 0;
        int myzhqp = 0;
        private void Caltj(string ysxm, Boolean flag)
        {
            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            DataRow dr = dttj.NewRow();
            //医生姓名
            dr["ysxm"] = ysxm;
            if (flag)
            {
                dr["blbg"] = blbg.ToString();
                //常规病理报告

                dr["cgblbg"] = cgblbg.ToString();
                //细胞学病理报告

                dr["xbxblbg"] = xbxblbg.ToString();

                //TCT病理报告
                dr["tctblbg"] = tctblbg.ToString();

                //分子病理报告
                dr["fzblbg"] = fzblbg.ToString();

                //会诊病理报告
                dr["hzblbg"] = hzblbg.ToString();


                //冰冻病理报告

                dr["iceblbg"] = iceblbg.ToString();
                //标本接收数

                dr["bbs"] = bbs.ToString();
                //蜡块数

                dr["lks"] = lks.ToString();
                //包埋数

                dr["bms"] = bms.ToString();
                //制片数

                dr["zps"] = zps.ToString();
                //免疫组化报告

                dr["myzhbg"] = myzhbg.ToString();
                //免疫组化切片

                dr["myzhqp"] = myzhqp.ToString();
            }
            else
            {
                //病理报告总数
                string sqlstr = string.Format("select count(*) from exam_report_view  where  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and (cbreport_doc_name='{2}'  || shreport_doc_name='{2}' || zzreport_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                DataTable dt = ins.GetDt(sqlstr);
                dr["blbg"] = dt.Rows[0][0].ToString();
                blbg += Convert.ToInt32(dt.Rows[0][0]);
                //常规病理报告
                sqlstr = string.Format("select count(*) from exam_report_view  where  exam_type='PL' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and (cbreport_doc_name='{2}'  || shreport_doc_name='{2}' || zzreport_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["cgblbg"] = dt.Rows[0][0].ToString();
                cgblbg += Convert.ToInt32(dt.Rows[0][0]);
                //细胞学病理报告
                sqlstr = string.Format("select count(*) from exam_report_view  where  exam_type='XBX' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and (cbreport_doc_name='{2}'  || shreport_doc_name='{2}' || zzreport_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["xbxblbg"] = dt.Rows[0][0].ToString();
                xbxblbg += Convert.ToInt32(dt.Rows[0][0]);
                //TCT病理报告
                sqlstr = string.Format("select count(*) from exam_report_view  where  exam_type='TCT' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and (cbreport_doc_name='{2}'  || shreport_doc_name='{2}' || zzreport_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["tctblbg"] = dt.Rows[0][0].ToString();
                tctblbg += Convert.ToInt32(dt.Rows[0][0]);

                //分子病理报告
                sqlstr = string.Format("select count(*) from exam_report_view  where  exam_type='FZ' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and (cbreport_doc_name='{2}'  || shreport_doc_name='{2}' || zzreport_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["fzblbg"] = dt.Rows[0][0].ToString();
                fzblbg += Convert.ToInt32(dt.Rows[0][0]);

                //会诊病理报告
                sqlstr = string.Format("select count(*) from exam_report_view  where  exam_type='HZ' and  report_print_datetime is not null  and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59' and (cbreport_doc_name='{2}'  || shreport_doc_name='{2}' || zzreport_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["hzblbg"] = dt.Rows[0][0].ToString();
                hzblbg += Convert.ToInt32(dt.Rows[0][0]);



                //冰冻病理报告
                sqlstr = string.Format("select count(*) from exam_ice_report  where    report_datetime is not null  and report_datetime>='{0} 00:00:00' and report_datetime<='{1} 23:59:59' and (report_doc_name='{2}'  || sh_doc_name='{2}')", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["iceblbg"] = dt.Rows[0][0].ToString();
                iceblbg += Convert.ToInt32(dt.Rows[0][0]);
                //标本接收数
                sqlstr = string.Format("select count(*) from exam_master where exam_status>=20 and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59' and receive_doctor_name='{2}'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["bbs"] = dt.Rows[0][0].ToString();
                bbs += Convert.ToInt32(dt.Rows[0][0]);
                //蜡块数
                sqlstr = string.Format("select count(*) from exam_draw_meterials where  draw_datetime>='{0} 00:00:00' and draw_datetime<='{1} 23:59:59' and draw_doctor_name='{2}'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["lks"] = dt.Rows[0][0].ToString();
                lks += Convert.ToInt32(dt.Rows[0][0]);
                //包埋数
                sqlstr = string.Format("select count(*) from exam_draw_meterials where bm_datetime is not null and  bm_datetime>='{0} 00:00:00' and bm_datetime<='{1} 23:59:59' and bm_doc_name='{2}'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["bms"] = dt.Rows[0][0].ToString();
                bms += Convert.ToInt32(dt.Rows[0][0]);
                //制片数
                sqlstr = string.Format("select count(*) from exam_filmmaking where  make_datetime is not null and  make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59' and make_doc_name='{2}'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["zps"] = dt.Rows[0][0].ToString();
                zps += Convert.ToInt32(dt.Rows[0][0]);
                //免疫组化阅片数
                sqlstr = string.Format("select count(*) from exam_tjyz where  sq_datetime is not null and  sq_datetime>='{0} 00:00:00' and sq_datetime<='{1} 23:59:59' and sq_doctor_name='{2}' ", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["myzhbg"] = dt.Rows[0][0].ToString();
                myzhbg += Convert.ToInt32(dt.Rows[0][0]);
                //免疫组化切片
                sqlstr = string.Format("select count(*) from exam_tjyz where yz_flag='已执行' and   zx_datetime is not null and  zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59' and zx_doc_name='{2}'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), ysxm);
                dt = ins.GetDt(sqlstr);
                dr["myzhqp"] = dt.Rows[0][0].ToString();
                myzhqp += Convert.ToInt32(dt.Rows[0][0]);
            }
            dttj.Rows.Add(dr);
        }

    }
}
