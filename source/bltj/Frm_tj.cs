using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Statistics
{
    public partial class Frm_tj : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_tj()
        {
            InitializeComponent();
        }

        private void Frm_tj_Load(object sender, EventArgs e)
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
            comboBox1.SelectedIndex = 0;
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {


            StringBuilder sb = new StringBuilder();
            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            DataTable dt = null;
            string tj = "";
            switch (comboBox1.SelectedIndex)
            {
                case 0:

                    tj = string.Format(" received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(receive_doctor_name,'') as ysxm,COUNT(*) as gzl,'组织学' as lb from exam_master where exam_type='PL' and receive_doctor_name  is not null and exam_status>='20' and  {0}  GROUP BY receive_doctor_name order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(receive_doctor_name,'') as ysxm,COUNT(*) as gzl,'细胞学' as lb from exam_master where exam_type='XBX'  and receive_doctor_name  is not null and exam_status>='20' and  {0}  GROUP BY receive_doctor_name order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(receive_doctor_name,'') as ysxm,COUNT(*) as gzl,'液基细胞' as lb from exam_master where exam_type='TCT' and receive_doctor_name  is not null and exam_status>='20' and  {0}  GROUP BY receive_doctor_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(receive_doctor_name,'') as ysxm,COUNT(*) as gzl,'外院会诊' as lb from exam_master where  exam_type='HZ' and receive_doctor_name  is not null and exam_status>='20' and  {0}  GROUP BY receive_doctor_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(receive_doctor_name,'') as ysxm,COUNT(*) as gzl,'FISH' as lb from exam_master where  exam_type='FZ' and receive_doctor_name  is not null and exam_status>='20' and  {0}  GROUP BY receive_doctor_name order by ysxm)", tj);

                    break;
                case 1:
                    tj = string.Format(" draw_datetime>='{0} 00:00:00' and draw_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("select COALESCE(draw_doctor_name,'') as ysxm,COUNT(*) as gzl,'大体取材' as lb  from exam_draw_meterials where draw_doctor_name  is not null and hd_flag=1 and  {0} GROUP BY draw_doctor_name order by ysxm ", tj);
                    break;
                case 2:
                    tj = string.Format(" draw_datetime>='{0} 00:00:00' and draw_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("select COALESCE(bm_doc_name,'') as ysxm,COUNT(*) as gzl,'蜡块包埋' as lb from exam_draw_meterials where draw_doctor_name  is not null and bm_qr=1 and  {0} GROUP BY bm_doc_name order by ysxm ", tj);
                    break;
                case 3:
                    tj = string.Format(" make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("select COALESCE(make_doc_name,'') as ysxm,COUNT(*) as gzl,'玻片制片' as lb from exam_filmmaking where make_doc_name is not null and zp_flag>=1  and  {0}  GROUP BY make_doc_name order by ysxm ", tj);
                    break;
                case 4:
                    tj = string.Format(" cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(cbreport_doc_name,'') as ysxm,COUNT(*) as gzl,'组织学' as lb  from exam_report_view  where exam_status>=55 and exam_type='PL' and cbreport_doc_name is not null and  {0}  GROUP BY cbreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(cbreport_doc_name,'') as ysxm,COUNT(*) as gzl,'细胞学' as  lb    from exam_report_view where  exam_status>=55 and exam_type='XBX'   and cbreport_doc_name is not null and  {0}  GROUP BY cbreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(cbreport_doc_name,'') as ysxm,COUNT(*) as gzl,'液基细胞' as lb  from exam_report_view where  exam_status>=55 and exam_type='TCT'   and cbreport_doc_name is not null and  {0}  GROUP BY cbreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(cbreport_doc_name,'') as ysxm,COUNT(*) as gzl,'外院会诊' as lb  from exam_report_view where  exam_status>=55 and exam_type='HZ'  and cbreport_doc_name is not null and  {0}  GROUP BY cbreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("   union all (select COALESCE(cbreport_doc_name,'') as ysxm,COUNT(*) as gzl,'FISH' as lb     from exam_report_view where  exam_status>=55 and  exam_type='FZ'  and cbreport_doc_name is not null and  {0}  GROUP BY cbreport_doc_name order by ysxm)", tj);
                    break;
                case 5:
                    tj = string.Format(" zzreport_datetime>='{0} 00:00:00' and zzreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(zzreport_doc_name,'') as ysxm,COUNT(*) as gzl,'组织学' as lb   from exam_report_view  where exam_status>=55 and exam_type='PL' and zzreport_doc_name is not null and  {0}  GROUP BY zzreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(zzreport_doc_name,'') as ysxm,COUNT(*) as gzl,'细胞学' as lb    from exam_report_view where  exam_status>=55 and exam_type='XBX'   and zzreport_doc_name is not null and  {0}  GROUP BY zzreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(zzreport_doc_name,'') as ysxm,COUNT(*) as gzl,'液基细胞' as lb  from exam_report_view where  exam_status>=55 and exam_type='TCT'   and zzreport_doc_name is not null and  {0}  GROUP BY zzreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(zzreport_doc_name,'') as ysxm,COUNT(*) as gzl,'外院会诊' as lb  from exam_report_view where  exam_status>=55 and exam_type='HZ'  and zzreport_doc_name is not null and  {0}  GROUP BY zzreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("   union all (select COALESCE(zzreport_doc_name,'') as ysxm,COUNT(*) as gzl,'FISH' as lb     from exam_report_view where  exam_status>=55 and  exam_type='FZ'  and zzreport_doc_name is not null and  {0}  GROUP BY zzreport_doc_name order by ysxm)", tj);

                    break;
                case 6:
                    tj = string.Format(" shreport_datetime>='{0} 00:00:00' and shreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(shreport_doc_name,'') as ysxm,COUNT(*) as gzl,'组织学' as lb  from exam_report_view  where exam_status>=55 and exam_type='PL' and shreport_doc_name is not null and  {0}  GROUP BY shreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(shreport_doc_name,'') as ysxm,COUNT(*) as gzl,'细胞学' as lb  from exam_report_view where  exam_status>=55 and exam_type='XBX'   and shreport_doc_name is not null and  {0}  GROUP BY shreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(shreport_doc_name,'') as ysxm,COUNT(*) as gzl,'液基细胞' as lb  from exam_report_view where  exam_status>=55 and exam_type='TCT'   and shreport_doc_name is not null and  {0}  GROUP BY shreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(shreport_doc_name,'') as ysxm,COUNT(*) as gzl,'外院会诊' as lb  from exam_report_view where  exam_status>=55 and exam_type='HZ'  and shreport_doc_name is not null and  {0}  GROUP BY shreport_doc_name order by ysxm)", tj);
                    sb.AppendFormat("   union all (select COALESCE(shreport_doc_name,'') as ysxm,COUNT(*) as gzl,'FISH' as lb  from exam_report_view where  exam_status>=55 and  exam_type='FZ'  and shreport_doc_name is not null and  {0}  GROUP BY shreport_doc_name order by ysxm)", tj);

                    break;
                case 7:
                    tj = string.Format(" gd_datetime>='{0} 00:00:00' and gd_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(gd_doctor,'') as ysxm,COUNT(*) as gzl,'蜡块归档' as lb from exam_draw_meterials where gd_doctor is not null and gd_flag=1  and  {0}  GROUP BY gd_doctor order by ysxm) ", tj);
                    tj = string.Format(" qpgd_datetime>='{0} 00:00:00' and qpgd_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat(" union all ( select COALESCE(qpgd_doctor,'') as ysxm,COUNT(*) as gzl,'玻片归档' as lb from exam_filmmaking where qpgd_doctor is not null and qpgd_flag=1  and  {0}  GROUP BY qpgd_doctor order by ysxm) ", tj);
                    break;
                case 8:

                    tj = string.Format(" received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(req_physician,'') as ysxm,COUNT(*) as gzl,'组织学' as lb from exam_master where exam_type='PL' and req_physician  is not null and exam_status>='20' and  {0}  GROUP BY req_physician order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(req_physician,'') as ysxm,COUNT(*) as gzl,'细胞学' as lb from exam_master where exam_type='XBX'  and req_physician  is not null and exam_status>='20' and  {0}  GROUP BY req_physician order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(req_physician,'') as ysxm,COUNT(*) as gzl,'液基细胞' as lb from exam_master where exam_type='TCT' and req_physician  is not null and exam_status>='20' and  {0}  GROUP BY req_physician order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(req_physician,'') as ysxm,COUNT(*) as gzl,'外院会诊' as lb from exam_master where  exam_type='HZ' and req_physician  is not null and exam_status>='20' and  {0}  GROUP BY req_physician order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(req_physician,'') as ysxm,COUNT(*) as gzl,'FISH' as lb from exam_master where  exam_type='FZ' and req_physician  is not null and exam_status>='20' and  {0}  GROUP BY req_physician order by ysxm)", tj);

                    break;
                case 9:

                    tj = string.Format(" received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(req_dept,'') as ysxm,COUNT(*) as gzl,'组织学' as lb from exam_master where exam_type='PL' and req_dept  is not null and exam_status>='20' and  {0}  GROUP BY req_dept order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(req_dept,'') as ysxm,COUNT(*) as gzl,'细胞学' as lb from exam_master where exam_type='XBX'  and req_dept  is not null and exam_status>='20' and  {0}  GROUP BY req_dept order by ysxm)", tj);
                    sb.AppendFormat(" union all (select COALESCE(req_dept,'') as ysxm,COUNT(*) as gzl,'液基细胞' as lb from exam_master where exam_type='TCT' and req_dept  is not null and exam_status>='20' and  {0}  GROUP BY req_dept order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(req_dept,'') as ysxm,COUNT(*) as gzl,'外院会诊' as lb from exam_master where  exam_type='HZ' and req_dept  is not null and exam_status>='20' and  {0}  GROUP BY req_dept order by ysxm)", tj);
                    sb.AppendFormat("  union all (select COALESCE(req_dept,'') as ysxm,COUNT(*) as gzl,'FISH' as lb from exam_master where  exam_type='FZ' and req_dept  is not null and exam_status>='20' and  {0}  GROUP BY req_dept order by ysxm)", tj);

                    break;
                case 10:
                    tj = string.Format(" sq_datetime>='{0} 00:00:00' and sq_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                    sb.AppendFormat("(select COALESCE(sq_doctor_name,'') as ysxm,COUNT(*) as gzl,'申请' as lb from exam_tjyz where sq_doctor_name is not null   and  {0}  GROUP BY sq_doctor_name order by ysxm) ", tj);
                    sb.AppendFormat(" union all ( select COALESCE(zx_doc_name,'') as ysxm,COUNT(*) as gzl,'执行' as lb from exam_tjyz where zx_doc_name is not null and yz_flag='已执行'  and  {0}  GROUP BY zx_doc_name order by ysxm) ", tj);
                    break;

            }
            string sqlstr = sb.ToString();
            sb.Clear();
            dt = ins.GetDt(sqlstr);
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
        }
        //导出
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
                    string wkName = "";
                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            wkName = "登记医师统计";
                            break;
                        case 1:
                            wkName = "取材医师统计";
                            break;
                        case 2:
                            wkName = "包埋医师工作量统计";
                            break;
                        case 3:
                            wkName = "制片医师统计";
                            break;
                        case 4:
                            wkName = "初步报告统计";
                            break;
                        case 5:
                            wkName = "主治报告统计";
                            break;
                        case 6:
                            wkName = "审核报告统计";
                            break;
                        case 7:
                            wkName = "档案归档医师工作量统计";
                            break;
                        case 8:
                            wkName = "申请医师统计";
                            break;
                        case 9:
                            wkName = "申请科室统计";
                            break;
                        case 10:
                            wkName = "免疫组化工作量统计";
                            break;
                    }
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
