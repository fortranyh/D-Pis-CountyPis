using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PIS_Sys.jszp
{
    public partial class Frm_jszp : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_jszp()
        {
            InitializeComponent();
            //修改日期格式
            Thread.CurrentThread.CurrentCulture = new CultureInfo("zh-CN");
            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
        }
        //自动核对
        public void Middle_DoSendMessage(string sender, string e)
        {
            if (sender.Equals("hd"))
            {
                if (dataGridViewX5.Rows.Count > 0)
                {
                    if (dataGridViewX5.SelectedRows.Count > 0)
                    {
                        DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                        DataTable dt = ins.SmHdQpinfo(e, textBoxX2.Text.Trim(), zp_dt.Value.ToString("yyyy-MM-dd"), checkBoxX2.Checked, checkBoxX3.Checked);
                        if (dt != null && dt.Rows.Count > 0)
                        {

                            string zp_flag = dt.Rows[0]["zp_flag"].ToString();
                            if (zp_flag.Equals("1"))
                            {
                                int id = Convert.ToInt32(dt.Rows[0]["id"].ToString());
                                if (ins.UpdateQpHdflag(id))
                                {
                                    Frm_TJInfo("扫描核对", e + "已经核对！");
                                }
                            }
                            buttonX5.PerformClick();
                        }

                    }
                }
            }
        }
        private string MYZH_IP = "";
        private int MYZH_PORT = 8888;
        private void Frm_jszp_Load(object sender, EventArgs e)
        {
            Middle.DoSendMessage += new Middle.SendMessage(Middle_DoSendMessage);
            string MYZHFlag = ConfigurationManager.AppSettings["MYZH"] ?? "0";
            if (MYZHFlag.Equals("1"))
            {
                MYZH_IP = ConfigurationManager.AppSettings["MYZH_IP"];
                MYZH_PORT = Convert.ToInt32(ConfigurationManager.AppSettings["MYZH_PORT"]);
            }
            else
            {
                buttonX21.Visible = false;
            }

            //高度自动适应
            superGridControl5.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl5.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel5 = superGridControl5.PrimaryGrid;
            panel5.MinRowHeight = 30;
            //锁定1列
            panel5.FrozenColumnCount = 1;
            panel5.AutoGenerateColumns = true;
            for (int i = 0; i < panel5.Columns.Count; i++)
            {
                panel5.Columns[i].ReadOnly = true;
                panel5.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel5.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel5.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //置datagridview的列宽自动适应,列不可排序
            for (int i = 0; i < dataGridViewX1.ColumnCount; i++)
            {
                dataGridViewX1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewX1.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(14.25));
            dataGridViewX1.DefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(15));
            //Frozen 属性为 True 时， 该列左侧的所有列被固定， 横向滚动时固定列不随滚动条滚动而左右移动。这对于重要列固定显示很有用。
            dataGridViewX1.Columns["meterial_no"].Frozen = true;
            //置datagridview的列宽自动适应,列不可排序
            for (int i = 0; i < dataGridViewX2.ColumnCount; i++)
            {
                dataGridViewX2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewX2.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(14.25));
            dataGridViewX2.DefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(15));
            //Frozen 属性为 True 时， 该列左侧的所有列被固定， 横向滚动时固定列不随滚动条滚动而左右移动。这对于重要列固定显示很有用。
            dataGridViewX2.Columns["draw_barcodezp"].Frozen = true;
            //置datagridview的列宽自动适应,列不可排序
            for (int i = 0; i < dataGridViewX5.ColumnCount; i++)
            {
                dataGridViewX5.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewX5.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(14.25));
            dataGridViewX5.DefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(15));
            //Frozen 属性为 True 时， 该列左侧的所有列被固定， 横向滚动时固定列不随滚动条滚动而左右移动。这对于重要列固定显示很有用。
            dataGridViewX5.Columns["zp_infoyzp"].Frozen = true;
            //置datagridview的列宽自动适应,列不可排序
            for (int i = 0; i < dataGridViewX4.ColumnCount; i++)
            {
                dataGridViewX4.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewX4.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(14.25));
            dataGridViewX4.DefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(15));
            //置datagridview的列宽自动适应,列不可排序
            for (int i = 0; i < dataGridViewX3.ColumnCount; i++)
            {
                dataGridViewX3.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridViewX3.ColumnHeadersDefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(14.25));
            dataGridViewX3.DefaultCellStyle.Font = new Font(this.Font.Name, Convert.ToSingle(15));

            //绑定取材医生
            DBHelper.BLL.sys_info user_ins = new DBHelper.BLL.sys_info();
            DataSet ds = user_ins.GetDsAllExam_User(Program.Dept_Code);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                cmb_bmqcys.DataSource = ds.Tables[0];
                cmb_bmqcys.DisplayMember = "user_name";
                cmb_bmqcys.ValueMember = "user_code";
            }
            else
            {
                cmb_bmqcys.DataSource = null;
            }

            textBoxX5.Text = Program.User_Name;


            //任务来源  
            DBHelper.BLL.bq_type_dict insbq = new DBHelper.BLL.bq_type_dict();
            DataTable dtrwly = insbq.GetData();
            if (dtrwly != null && dtrwly.Rows.Count > 0)
            {
                comboBoxEx3.DataSource = dtrwly;
                comboBoxEx3.DisplayMember = "bq_name";
                comboBoxEx3.ValueMember = "bq_code";
            }
            else
            {
                comboBoxEx3.DataSource = null;
            }
            //医嘱类型
            DataTable dtyzlx = insbq.GetAllData();
            if (dtyzlx != null && dtyzlx.Rows.Count > 0)
            {
                cmbYzlx.DataSource = dtyzlx;
                cmbYzlx.DisplayMember = "bq_name";
                cmbYzlx.ValueMember = "bq_code";
            }
            else
            {
                cmbYzlx.DataSource = null;
            }

        }
        //查询包埋列表
        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //清空原来的数据
                dataGridViewX1.BringToFront();
                dataGridViewX1.Rows.Clear();
                //
                DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                DataTable dt = ins.GetBminfo(textBoxX1.Text.Trim(), cmb_bmqcys.Text.Trim(), dt_qcsj.Value.ToString("yyyy-MM-dd"), checkBoxX10.Checked);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX1.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["meterial_no"], dt.Rows[i]["parts"], dt.Rows[i]["group_num"], dt.Rows[i]["draw_doctor_name"], dt.Rows[i]["draw_datetime"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["bm_qr"], dt.Rows[i]["print_flag"], dt.Rows[i]["memo_note"], dt.Rows[i]["barcode"], dt.Rows[i]["qp_barcode"], dt.Rows[i]["bm_xh"], dt.Rows[i]["tuoshui_datetime"] };
                        dataGridViewX1.Rows.Add(row);
                        if (dt.Rows[i]["bm_qr"].ToString().Equals("0"))
                        {
                            dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "待包埋";
                            dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Black;
                        }
                        else if (dt.Rows[i]["bm_qr"].ToString().Equals("1"))
                        {
                            dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "已包埋";
                            dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Blue;
                        }
                        if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                        {
                            dataGridViewX1.Rows[i].Cells["print_flag"].Value = "未打印";
                            dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Black;
                        }
                        else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                        {
                            dataGridViewX1.Rows[i].Cells["print_flag"].Value = "已打印";
                            dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Red;
                        }
                        else if (dt.Rows[i]["work_source"].ToString().Equals("脱钙"))
                        {
                            dataGridViewX1.Rows[i].Cells["work_source"].Style.ForeColor = Color.Blue;
                        }
                        else if (dt.Rows[i]["work_source"].ToString().Equals("冰冻") || dt.Rows[i]["work_source"].ToString().Equals("冰余"))
                        {
                            dataGridViewX1.Rows[i].Cells["work_source"].Style.ForeColor = Color.Red;
                        }
                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX1.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            buttonX15.Tag = "asc";
        }

        private void buttonX15_Click(object sender, EventArgs e)
        {
            if (buttonX15.Tag.ToString().Equals("asc"))
            {
                try
                {
                    //开始查询
                    this.Cursor = Cursors.WaitCursor;
                    //清空原来的数据
                    dataGridViewX1.BringToFront();
                    dataGridViewX1.Rows.Clear();
                    //
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    DataTable dt = ins.GetBminfo(textBoxX1.Text.Trim(), cmb_bmqcys.Text.Trim(), dt_qcsj.Value.ToString("yyyy-MM-dd"), checkBoxX10.Checked, false);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int icount = 0;
                        string First_NO = dt.Rows[0]["study_no"].ToString();
                        Color[] rowColor = new Color[] { dataGridViewX1.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["meterial_no"], dt.Rows[i]["parts"], dt.Rows[i]["group_num"], dt.Rows[i]["draw_doctor_name"], dt.Rows[i]["draw_datetime"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["bm_qr"], dt.Rows[i]["print_flag"], dt.Rows[i]["memo_note"], dt.Rows[i]["barcode"], dt.Rows[i]["qp_barcode"], dt.Rows[i]["bm_xh"], dt.Rows[i]["tuoshui_datetime"] };
                            dataGridViewX1.Rows.Add(row);
                            if (dt.Rows[i]["bm_qr"].ToString().Equals("0"))
                            {
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "待包埋";
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["bm_qr"].ToString().Equals("1"))
                            {
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "已包埋";
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Blue;
                            }
                            if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX1.Rows[i].Cells["print_flag"].Value = "未打印";
                                dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX1.Rows[i].Cells["print_flag"].Value = "已打印";
                                dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Red;
                            }
                            //隔行变色
                            if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                            {
                                icount += 1;
                                First_NO = dt.Rows[i]["study_no"].ToString();
                                dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                            else
                            {
                                dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                        }
                        lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                    }
                    else
                    {
                        lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", 0);
                    }
                    dataGridViewX1.Focus();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                buttonX15.Tag = "desc";
            }
            else
            {
                buttonX1.PerformClick();
            }
        }
        //显示行号
        private void dataGridViewX1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle Rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridViewX1.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridViewX1.RowHeadersDefaultCellStyle.Font, Rectangle, dataGridViewX1.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void Print_bp(string qp_barcode, string xh, string work_source)
        {
            //模式
            if (ConfigurationManager.AppSettings["sfmbp_enable"] != null)
            {
                if (ConfigurationManager.AppSettings["sfmbp_enable"].ToString().Equals("1"))
                {
                    string bp_path = ConfigurationManager.AppSettings["sfmbp_path"].ToString().Trim();
                    if (Directory.Exists(bp_path) == true)
                    {
                        string uid = Helper.GetUidStr();
                        string file_path = bp_path + @"/" + uid + ".txt";
                        try
                        {
                            System.IO.StreamWriter sw = new StreamWriter(file_path, false, Encoding.GetEncoding("Unicode"));
                            sw.WriteLine(string.Format("{0},{1}", qp_barcode, work_source));
                            sw.Close();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("请打开设置系统，对接口路径进行设置！", "玻片号打印接口", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }


        //包埋界面标签打印
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.Rows.Count > 0)
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX1.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {
                        //获取检查前缀
                        DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                        //获取当前条码号
                        DBHelper.BLL.exam_draw_meterials InsFi = new DBHelper.BLL.exam_draw_meterials();
                        //获取modality
                        DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            int id = Convert.ToInt32(row.Cells["id"].Value);
                            string study_no = row.Cells["study_no"].Value.ToString();
                            string qp_barcode = row.Cells["qp_barcode"].Value.ToString();
                            if (qp_barcode.Equals(""))
                            {
                                string modality = insM.GetModality(study_no);
                                string prechar = InsType.GetPrechar(modality);
                                qp_barcode = InsFi.GetQPBarcode(study_no, prechar);
                                if (qp_barcode != "")
                                {
                                    //更新条码
                                    if (InsFi.UpdateQpBarcode(id, qp_barcode))
                                    {
                                        row.Cells["qp_barcode"].Value = qp_barcode;
                                        row.Cells["bm_xh"].Value = qp_barcode.Substring(qp_barcode.Length - 2, 2);
                                    }
                                    //更新打印状态
                                    if (InsFi.UpdatePrintFlag(id))
                                    {
                                        row.Cells["print_flag"].Value = "已打印";
                                    }
                                }
                            }
                        }

                        //模式
                        if (ConfigurationManager.AppSettings["sfmbp_enable"] != null && ConfigurationManager.AppSettings["sfmbp_enable"].ToString().Equals("1"))
                        {
                            for (int j = 0; j < col.Count; j++)
                            {
                                DataGridViewRow row = col[j];
                                string barcode = row.Cells["study_no"].Value.ToString();
                                string lkh2 = row.Cells["barcode"].Value.ToString();
                                string barcode2 = row.Cells["qp_barcode"].Value.ToString().Replace(barcode, "");

                                string bz2 = row.Cells["memo_note"].Value.ToString();
                                if (bz2.Equals(""))
                                {
                                    bz2 = row.Cells["work_source"].Value.ToString();
                                }

                                Print_bp(lkh2, barcode2, bz2);
                            }
                        }
                        else
                        {
                            //双联
                            if (PIS_Sys.Properties.Settings.Default.zpBarcodePrintSL)
                            {
                                //开始打印
                                string lkh1 = "";
                                string lkh2 = "";
                                string barcode1 = "";
                                string barcode2 = "";
                                string bz1 = "";
                                string bz2 = "";
                                string hospi1 = "";
                                string hospi2 = "";
                                for (int j = 0; j < col.Count; j = j + 2)
                                {
                                    if (col.Count % 2 == 1)
                                    {
                                        if (j == col.Count - 1)
                                        {
                                            DataGridViewRow row = col[j];
                                            lkh2 = row.Cells["barcode"].Value.ToString();
                                            barcode2 = row.Cells["qp_barcode"].Value.ToString();
                                            bz2 = row.Cells["memo_note"].Value.ToString();
                                            if (bz2.Equals(""))
                                            {
                                                bz2 = row.Cells["work_source"].Value.ToString();
                                            }
                                            hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                            lkh1 = "";
                                            barcode1 = "";
                                            bz1 = "";
                                            hospi1 = "";
                                        }
                                        else
                                        {
                                            DataGridViewRow row = col[j];
                                            lkh2 = row.Cells["barcode"].Value.ToString();
                                            barcode2 = row.Cells["qp_barcode"].Value.ToString();
                                            bz2 = row.Cells["memo_note"].Value.ToString();
                                            if (bz2.Equals(""))
                                            {
                                                bz2 = row.Cells["work_source"].Value.ToString();
                                            }
                                            hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                            DataGridViewRow row1 = col[j + 1];
                                            lkh1 = row1.Cells["barcode"].Value.ToString();
                                            barcode1 = row1.Cells["qp_barcode"].Value.ToString();
                                            bz1 = row1.Cells["memo_note"].Value.ToString();
                                            if (bz1.Equals(""))
                                            {
                                                bz1 = row1.Cells["work_source"].Value.ToString();
                                            }
                                            hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                        }
                                    }
                                    else
                                    {
                                        DataGridViewRow row = col[j];
                                        lkh2 = row.Cells["barcode"].Value.ToString();
                                        barcode2 = row.Cells["qp_barcode"].Value.ToString();
                                        bz2 = row.Cells["memo_note"].Value.ToString();
                                        if (bz2.Equals(""))
                                        {
                                            bz2 = row.Cells["work_source"].Value.ToString();
                                        }
                                        hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                        DataGridViewRow row1 = col[j + 1];
                                        lkh1 = row1.Cells["barcode"].Value.ToString();
                                        barcode1 = row1.Cells["qp_barcode"].Value.ToString();
                                        bz1 = row1.Cells["memo_note"].Value.ToString();
                                        if (bz1.Equals(""))
                                        {
                                            bz1 = row1.Cells["work_source"].Value.ToString();
                                        }
                                        hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                    }
                                    //打印
                                    FastReportLib.PrintBarCode.PrintQPBarcode2(lkh1, barcode1, bz1, hospi1, lkh2, barcode2, bz2, hospi2, PIS_Sys.Properties.Settings.Default.zpBarcodePrinter, PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum);
                                }
                            }
                            else
                            {
                                //单联
                                for (int j = 0; j < col.Count; j++)
                                {
                                    DataGridViewRow row = col[j];
                                    string barcode = row.Cells["study_no"].Value.ToString();
                                    string lkh1 = row.Cells["barcode"].Value.ToString();
                                    string barcode1 = row.Cells["qp_barcode"].Value.ToString().Replace(barcode, "");
                                    string bz1 = row.Cells["memo_note"].Value.ToString();
                                    if (bz1.Equals(""))
                                    {
                                        bz1 = row.Cells["work_source"].Value.ToString();
                                    }
                                    string hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                    FastReportLib.PrintBarCode.PrintQPBarcode(lkh1, barcode1, bz1, hospi1, PIS_Sys.Properties.Settings.Default.zpBarcodePrinter, PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum);
                                }

                            }
                        }
                        //buttonX1.PerformClick();
                    }
                }
            }
        }
        //包埋确认
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.Rows.Count > 0)
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX1.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {
                        //获取检查前缀
                        DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                        //获取当前条码号
                        DBHelper.BLL.exam_draw_meterials InsFi = new DBHelper.BLL.exam_draw_meterials();
                        //获取modality
                        DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                        DBHelper.BLL.exam_filmmaking insF = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            int id = Convert.ToInt32(row.Cells["id"].Value);
                            string study_no = row.Cells["study_no"].Value.ToString();
                            string barcode = row.Cells["barcode"].Value.ToString();
                            string qp_barcode = row.Cells["qp_barcode"].Value.ToString();
                            string bz = row.Cells["memo_note"].Value.ToString();
                            string work_source = row.Cells["work_source"].Value.ToString();
                            int print_flag = 0;
                            if (row.Cells["print_flag"].Value.ToString().Equals("已打印"))
                            {
                                print_flag = 1;
                            }

                            if (qp_barcode.Equals(""))
                            {
                                string modality = insM.GetModality(study_no);
                                string prechar = InsType.GetPrechar(modality);
                                qp_barcode = InsFi.GetQPBarcode(study_no, prechar);
                                if (qp_barcode != "")
                                {
                                    //更新条码
                                    if (InsFi.UpdateQpBarcode(id, qp_barcode))
                                    {
                                        row.Cells["qp_barcode"].Value = qp_barcode;
                                        row.Cells["bm_xh"].Value = qp_barcode.Substring(qp_barcode.Length - 2, 2);
                                    }
                                }
                            }
                            //开始插入切片表  更新包埋状态 更新检查状态
                            string ResultStr = "";
                            if (insF.Process_filmmaking(study_no, id, qp_barcode, barcode, work_source, bz, Program.User_Code, Program.User_Name, print_flag, ref ResultStr))
                            {
                                InsFi.UpdateBmQr(id, Program.User_Name);
                                insM.UpdateBM_Flag(study_no, Program.User_Code, Program.User_Name);
                            }
                            else
                            {
                                Frm_TJInfo("错误！", ResultStr);
                            }
                        }
                        buttonX1.PerformClick();
                    }
                }
            }
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 1;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }

        private void Frm_jszp_Activated(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            //开启焦点并刷新数据
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //刷新列表
            buttonX1.PerformClick();
        }
        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.ToString().Equals("待包埋列表"))
            {
                buttonX1.PerformClick();
            }
            else if (e.NewValue.ToString().Equals("待切片列表"))
            {
                buttonX10.PerformClick();
            }
            else if (e.NewValue.ToString().Equals("医嘱列表"))
            {
                buttonX7.PerformClick();
            }
            else if (e.NewValue.ToString().Equals("已制片列表"))
            {
                buttonX5.PerformClick();
            }
        }
        //查询待切片列表
        private void buttonX10_Click(object sender, EventArgs e)
        {
            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //清空原来的数据
                dataGridViewX2.BringToFront();
                dataGridViewX2.Rows.Clear();
                //
                DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                DataTable dt = ins.GetWaitQpinfo(textBoxX3.Text.Trim(), bm_dt.Value.ToString("yyyy-MM-dd"));
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX2.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["draw_barcode"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["bm_datetime"], dt.Rows[i]["zp_flag"], dt.Rows[i]["film_num"], dt.Rows[i]["memo_note"], dt.Rows[i]["print_flag"], dt.Rows[i]["barcode"], dt.Rows[i]["dzpqp_xh"] };
                        dataGridViewX2.Rows.Add(row);
                        if (dt.Rows[i]["zp_flag"].ToString().Equals("0"))
                        {
                            dataGridViewX2.Rows[i].Cells["zp_flagzp"].Value = "待制片";
                            dataGridViewX2.Rows[i].Cells["zp_flagzp"].Style.ForeColor = Color.Black;
                        }
                        else if (dt.Rows[i]["zp_flag"].ToString().Equals("1"))
                        {
                            dataGridViewX2.Rows[i].Cells["zp_flagzp"].Value = "已制片";
                            dataGridViewX2.Rows[i].Cells["zp_flagzp"].Style.ForeColor = Color.Blue;
                        }
                        if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                        {
                            dataGridViewX2.Rows[i].Cells["print_flagzp"].Value = "未打印";
                            dataGridViewX2.Rows[i].Cells["print_flagzp"].Style.ForeColor = Color.Black;
                        }
                        else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                        {
                            dataGridViewX2.Rows[i].Cells["print_flagzp"].Value = "已打印";
                            dataGridViewX2.Rows[i].Cells["print_flagzp"].Style.ForeColor = Color.Red;
                        }
                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX2.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX2.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel1.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel1.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX2.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            buttonX16.Tag = "asc";
        }

        private void buttonX16_Click(object sender, EventArgs e)
        {
            if (buttonX16.Tag.ToString().Equals("asc"))
            {
                try
                {
                    //开始查询
                    this.Cursor = Cursors.WaitCursor;
                    //清空原来的数据
                    dataGridViewX2.BringToFront();
                    dataGridViewX2.Rows.Clear();
                    //
                    DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                    DataTable dt = ins.GetWaitQpinfo(textBoxX3.Text.Trim(), bm_dt.Value.ToString("yyyy-MM-dd"), false);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int icount = 0;
                        string First_NO = dt.Rows[0]["study_no"].ToString();
                        Color[] rowColor = new Color[] { dataGridViewX2.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["draw_barcode"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["bm_datetime"], dt.Rows[i]["zp_flag"], dt.Rows[i]["film_num"], dt.Rows[i]["memo_note"], dt.Rows[i]["print_flag"], dt.Rows[i]["barcode"] };
                            dataGridViewX2.Rows.Add(row);
                            if (dt.Rows[i]["zp_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX2.Rows[i].Cells["zp_flagzp"].Value = "待制片";
                                dataGridViewX2.Rows[i].Cells["zp_flagzp"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["zp_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX2.Rows[i].Cells["zp_flagzp"].Value = "已制片";
                                dataGridViewX2.Rows[i].Cells["zp_flagzp"].Style.ForeColor = Color.Blue;
                            }
                            if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX2.Rows[i].Cells["print_flagzp"].Value = "未打印";
                                dataGridViewX2.Rows[i].Cells["print_flagzp"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX2.Rows[i].Cells["print_flagzp"].Value = "已打印";
                                dataGridViewX2.Rows[i].Cells["print_flagzp"].Style.ForeColor = Color.Red;
                            }
                            //隔行变色
                            if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                            {
                                icount += 1;
                                First_NO = dt.Rows[i]["study_no"].ToString();
                                dataGridViewX2.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                            else
                            {
                                dataGridViewX2.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                        }
                        toolStripStatusLabel1.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = String.Format("共查询到 {0} 条记录", 0);
                    }
                    dataGridViewX2.Focus();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                buttonX16.Tag = "desc";
            }
            else
            {
                buttonX10.PerformClick();
            }
        }

        private void dataGridViewX2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle Rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridViewX2.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridViewX2.RowHeadersDefaultCellStyle.Font, Rectangle, dataGridViewX2.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        //切片合并
        private void buttonX9_Click(object sender, EventArgs e)
        {
            if (dataGridViewX2.Rows.Count > 0)
            {
                if (dataGridViewX2.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX2.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();
                    if (col.Count > 0)
                    {
                        //查询是否都是一个病理号下面的
                        Boolean First_flag = true;
                        string study_no = "";
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            if (First_flag)
                            {
                                First_flag = false;
                                study_no = row.Cells["study_nozp"].Value.ToString();
                                continue;
                            }
                            if (!study_no.Equals(row.Cells["study_nozp"].Value.ToString()))
                            {
                                Frm_TJInfo("提示", "合并的条目必须同属于一个病理号！\n当前选择有误不能进行合并！");
                                return;
                            }
                        }
                        First_flag = true;
                        //获取检查前缀
                        DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                        //获取modality
                        DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                        DBHelper.BLL.exam_filmmaking insF = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            int id = Convert.ToInt32(row.Cells["idzp"].Value);
                            if (First_flag)
                            {
                                study_no = row.Cells["study_nozp"].Value.ToString();
                                string qp_barcode = row.Cells["barcodezp"].Value.ToString();
                                if (qp_barcode.Equals(""))
                                {
                                    string modality = insM.GetModality(study_no);
                                    string prechar = InsType.GetPrechar(modality);
                                    qp_barcode = insF.GetBarcode(study_no, prechar);
                                    if (qp_barcode != "")
                                    {
                                        if (insF.UpdateQpBarcode(id, qp_barcode))
                                        {
                                            row.Cells["barcodezp"].Value = qp_barcode;
                                        }
                                    }
                                }
                                First_flag = false;
                                continue;
                            }
                            //合并切片
                            insF.Hb_film(id);
                        }
                        buttonX10.PerformClick();
                    }
                }
            }
        }
        //切片确认
        private void buttonX12_Click(object sender, EventArgs e)
        {
            if (dataGridViewX2.Rows.Count > 0)
            {
                if (dataGridViewX2.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX2.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {
                        //获取检查前缀
                        DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                        //获取modality
                        DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                        DBHelper.BLL.exam_filmmaking insF = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            int id = Convert.ToInt32(row.Cells["idzp"].Value);
                            string study_no = row.Cells["study_nozp"].Value.ToString();
                            string qp_barcode = row.Cells["barcodezp"].Value.ToString();
                            if (qp_barcode.Equals(""))
                            {
                                string modality = insM.GetModality(study_no);
                                string prechar = InsType.GetPrechar(modality);
                                qp_barcode = insF.GetBarcode(study_no, prechar);
                                if (qp_barcode != "")
                                {
                                    if (insF.UpdateQpBarcode(id, qp_barcode))
                                    {
                                        row.Cells["barcodezp"].Value = qp_barcode;
                                    }
                                }
                            }
                            //更新检查状态
                            if (insF.Qr_film(id, qp_barcode, Program.User_Code, Program.User_Name))
                            {
                                insM.UpdateZP_Flag(study_no, Program.User_Code, Program.User_Name);
                            }

                        }

                        buttonX10.PerformClick();
                    }
                }
            }
        }
        //已制片列表
        private void buttonX5_Click(object sender, EventArgs e)
        {
            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //清空原来的数据
                dataGridViewX5.BringToFront();
                dataGridViewX5.Rows.Clear();
                //
                DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();

                DataTable dt = ins.GetOverQpinfo(comboBox1.Text.Trim(), textBoxX2.Text.Trim(), zp_dt.Value.ToString("yyyy-MM-dd"), checkBoxX2.Checked, checkBoxX3.Checked);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX5.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["parts"], dt.Rows[i]["work_source"], dt.Rows[i]["draw_barcode"], dt.Rows[i]["zp_info"], dt.Rows[i]["barcode"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["make_doc_name"], dt.Rows[i]["make_datetime"], dt.Rows[i]["zp_flag"], dt.Rows[i]["print_flag"], dt.Rows[i]["film_num"], dt.Rows[i]["memo_note"], dt.Rows[i]["level"], dt.Rows[i]["level_memo"], dt.Rows[i]["qp_xh"] };
                        dataGridViewX5.Rows.Add(row);
                        if (dt.Rows[i]["zp_flag"].ToString().Equals("0"))
                        {
                            dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Value = "待制片";
                            dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Style.ForeColor = Color.Black;
                        }
                        else if (dt.Rows[i]["zp_flag"].ToString().Equals("1"))
                        {
                            dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Value = "待核对";
                            dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Style.ForeColor = Color.Blue;
                        }
                        else if (dt.Rows[i]["zp_flag"].ToString().Equals("2"))
                        {
                            dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Value = "已核对";
                            dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Style.ForeColor = Color.Red;
                        }

                        if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                        {
                            dataGridViewX5.Rows[i].Cells["print_flagyzp"].Value = "未打印";
                            dataGridViewX5.Rows[i].Cells["print_flagyzp"].Style.ForeColor = Color.Black;
                        }
                        else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                        {
                            dataGridViewX5.Rows[i].Cells["print_flagyzp"].Value = "已打印";
                            dataGridViewX5.Rows[i].Cells["print_flagyzp"].Style.ForeColor = Color.Red;
                        }
                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX5.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX5.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel2.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel2.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX5.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            buttonX13.Tag = "asc";
        }
        //标签打印
        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (dataGridViewX5.Rows.Count > 0)
            {
                if (dataGridViewX5.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX5.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();
                    if (col.Count > 0)
                    {
                        //更新打印状态
                        DBHelper.BLL.exam_filmmaking InsFi = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            string print_flag = row.Cells["print_flagyzp"].Value.ToString();
                            if (print_flag.Equals("未打印"))
                            {
                                int id = Convert.ToInt32(row.Cells["idyzp"].Value);
                                //更新打印状态
                                if (InsFi.UpdatePrintFlag(id))
                                {
                                    row.Cells["print_flagyzp"].Value = "已打印";
                                }
                            }
                        }
                        //模式
                        if (ConfigurationManager.AppSettings["sfmbp_enable"] != null && ConfigurationManager.AppSettings["sfmbp_enable"].ToString().Equals("1"))
                        {
                            for (int j = 0; j < col.Count; j++)
                            {
                                DataGridViewRow row = col[j];
                                string study_noyzp = row.Cells["study_noyzp"].Value.ToString();
                                string lkh2 = row.Cells["draw_barcodeyzp"].Value.ToString();
                                string barcode2 = row.Cells["barcodeyzp"].Value.ToString().Replace(study_noyzp, "");
                                string bz2 = row.Cells["zp_infoyzp"].Value.ToString();
                                if (bz2.Equals(""))
                                {
                                    if (row.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                    {
                                        bz2 = row.Cells["work_sourceyzp"].Value.ToString();
                                    }
                                    else
                                    {
                                        bz2 = row.Cells["memo_noteyzp"].Value.ToString();
                                    }
                                }
                                Print_bp(lkh2, barcode2, bz2);
                            }
                        }
                        else
                        {
                            //双联
                            if (PIS_Sys.Properties.Settings.Default.zpBarcodePrintSL)
                            {
                                //开始打印
                                string lkh1 = "";
                                string lkh2 = "";
                                string barcode1 = "";
                                string barcode2 = "";
                                string bz1 = "";
                                string bz2 = "";
                                string hospi1 = "";
                                string hospi2 = "";
                                for (int j = 0; j < col.Count; j = j + 2)
                                {
                                    if (col.Count % 2 == 1)
                                    {
                                        if (j == col.Count - 1)
                                        {
                                            DataGridViewRow row = col[j];
                                            lkh2 = row.Cells["draw_barcodeyzp"].Value.ToString();
                                            barcode2 = row.Cells["barcodeyzp"].Value.ToString();
                                            bz2 = row.Cells["zp_infoyzp"].Value.ToString();
                                            if (bz2.Equals(""))
                                            {
                                                if (row.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                                {
                                                    bz2 = row.Cells["work_sourceyzp"].Value.ToString();
                                                }
                                                else
                                                {
                                                    bz2 = row.Cells["memo_noteyzp"].Value.ToString();
                                                }
                                            }
                                            hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                            lkh1 = "";
                                            barcode1 = "";
                                            bz1 = "";
                                            hospi1 = "";
                                        }
                                        else
                                        {
                                            DataGridViewRow row = col[j];
                                            lkh2 = row.Cells["draw_barcodeyzp"].Value.ToString();
                                            barcode2 = row.Cells["barcodeyzp"].Value.ToString();
                                            bz2 = row.Cells["zp_infoyzp"].Value.ToString();
                                            if (bz2.Equals(""))
                                            {
                                                if (row.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                                {
                                                    bz2 = row.Cells["work_sourceyzp"].Value.ToString();
                                                }
                                                else
                                                {
                                                    bz2 = row.Cells["memo_noteyzp"].Value.ToString();
                                                }
                                            }
                                            hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                            DataGridViewRow row1 = col[j + 1];
                                            lkh1 = row1.Cells["draw_barcodeyzp"].Value.ToString();
                                            barcode1 = row1.Cells["barcodeyzp"].Value.ToString();
                                            bz1 = row1.Cells["zp_infoyzp"].Value.ToString();
                                            if (bz1.Equals(""))
                                            {
                                                if (row1.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                                {
                                                    bz1 = row1.Cells["work_sourceyzp"].Value.ToString();
                                                }
                                                else
                                                {
                                                    bz1 = row1.Cells["memo_noteyzp"].Value.ToString();
                                                }
                                            }
                                            hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                        }
                                    }
                                    else
                                    {
                                        DataGridViewRow row = col[j];
                                        lkh2 = row.Cells["draw_barcodeyzp"].Value.ToString();
                                        barcode2 = row.Cells["barcodeyzp"].Value.ToString();
                                        bz2 = row.Cells["zp_infoyzp"].Value.ToString();
                                        if (bz2.Equals(""))
                                        {
                                            if (row.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                            {
                                                bz2 = row.Cells["work_sourceyzp"].Value.ToString();
                                            }
                                            else
                                            {
                                                bz2 = row.Cells["memo_noteyzp"].Value.ToString();
                                            }
                                        }
                                        hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                        DataGridViewRow row1 = col[j + 1];
                                        lkh1 = row1.Cells["draw_barcodeyzp"].Value.ToString();
                                        barcode1 = row1.Cells["barcodeyzp"].Value.ToString();
                                        bz1 = row1.Cells["zp_infoyzp"].Value.ToString();
                                        if (bz1.Equals(""))
                                        {
                                            if (row1.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                            {
                                                bz1 = row1.Cells["work_sourceyzp"].Value.ToString();
                                            }
                                            else
                                            {
                                                bz1 = row1.Cells["memo_noteyzp"].Value.ToString();
                                            }
                                        }
                                        hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                    }
                                    //打印
                                    FastReportLib.PrintBarCode.PrintQPBarcode2(lkh1, barcode1, bz1, hospi1, lkh2, barcode2, bz2, hospi2, PIS_Sys.Properties.Settings.Default.zpBarcodePrinter, PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum);
                                }
                            }
                            else
                            {
                                //单联
                                for (int j = 0; j < col.Count; j++)
                                {
                                    string hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                    DataGridViewRow row = col[j];
                                    string study_noyzp = row.Cells["study_noyzp"].Value.ToString();
                                    string lkh1 = row.Cells["draw_barcodeyzp"].Value.ToString();
                                    string barcode1 = row.Cells["barcodeyzp"].Value.ToString().Replace(study_noyzp, "");
                                    string bz1 = row.Cells["zp_infoyzp"].Value.ToString();
                                    if (bz1.Equals(""))
                                    {
                                        if (row.Cells["memo_noteyzp"].Value.ToString().Equals(""))
                                        {
                                            bz1 = row.Cells["work_sourceyzp"].Value.ToString();
                                        }
                                        else
                                        {
                                            bz1 = row.Cells["memo_noteyzp"].Value.ToString();
                                        }
                                    }
                                    FastReportLib.PrintBarCode.PrintQPBarcode(lkh1, barcode1, bz1, hospi1, PIS_Sys.Properties.Settings.Default.zpBarcodePrinter, PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum);
                                }
                            }
                        }
                        //buttonX5.PerformClick();
                    }
                }
            }
        }
        //手动核对
        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (dataGridViewX5.Rows.Count > 0)
            {
                if (dataGridViewX5.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX5.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();
                    if (col.Count > 0)
                    {
                        DBHelper.BLL.exam_filmmaking insF = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];

                            string zp_flag = row.Cells["zp_flagyzp"].Value.ToString();
                            if (zp_flag.Equals("待核对"))
                            {
                                int id = Convert.ToInt32(row.Cells["idyzp"].Value);
                                if (insF.UpdateQpHdflag(id))
                                {
                                    row.Cells["zp_flagyzp"].Value = "已核对";
                                }
                            }
                        }
                        buttonX5.PerformClick();
                    }
                }
            }
        }

        private void buttonX13_Click(object sender, EventArgs e)
        {
            if (buttonX13.Tag.ToString().Equals("asc"))
            {
                try
                {
                    //开始查询
                    this.Cursor = Cursors.WaitCursor;
                    //清空原来的数据
                    dataGridViewX5.BringToFront();
                    dataGridViewX5.Rows.Clear();
                    //
                    DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                    DataTable dt = ins.GetOverQpinfo(comboBox1.Text.Trim(), textBoxX2.Text.Trim(), zp_dt.Value.ToString("yyyy-MM-dd"), checkBoxX2.Checked, checkBoxX3.Checked, false);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int icount = 0;
                        string First_NO = dt.Rows[0]["study_no"].ToString();
                        Color[] rowColor = new Color[] { dataGridViewX5.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["draw_barcode"], dt.Rows[i]["zp_info"], dt.Rows[i]["barcode"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["make_doc_name"], dt.Rows[i]["make_datetime"], dt.Rows[i]["zp_flag"], dt.Rows[i]["print_flag"], dt.Rows[i]["film_num"], dt.Rows[i]["memo_note"], dt.Rows[i]["level"], dt.Rows[i]["level_memo"] };
                            dataGridViewX5.Rows.Add(row);
                            if (dt.Rows[i]["zp_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Value = "待制片";
                                dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["zp_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Value = "待核对";
                                dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Style.ForeColor = Color.Blue;
                            }
                            else if (dt.Rows[i]["zp_flag"].ToString().Equals("2"))
                            {
                                dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Value = "已核对";
                                dataGridViewX5.Rows[i].Cells["zp_flagyzp"].Style.ForeColor = Color.Red;
                            }

                            if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX5.Rows[i].Cells["print_flagyzp"].Value = "未打印";
                                dataGridViewX5.Rows[i].Cells["print_flagyzp"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX5.Rows[i].Cells["print_flagyzp"].Value = "已打印";
                                dataGridViewX5.Rows[i].Cells["print_flagyzp"].Style.ForeColor = Color.Red;
                            }
                            //隔行变色
                            if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                            {
                                icount += 1;
                                First_NO = dt.Rows[i]["study_no"].ToString();
                                dataGridViewX5.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                            else
                            {
                                dataGridViewX5.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                        }
                        toolStripStatusLabel2.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                    }
                    else
                    {
                        toolStripStatusLabel2.Text = String.Format("共查询到 {0} 条记录", 0);
                    }
                    dataGridViewX5.Focus();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

                buttonX13.Tag = "desc";
            }
            else
            {
                buttonX5.PerformClick();
            }
        }

        private void dataGridViewX5_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle Rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridViewX5.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridViewX5.RowHeadersDefaultCellStyle.Font, Rectangle, dataGridViewX5.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        //添加切片
        private void buttonX11_Click(object sender, EventArgs e)
        {
            string rwly = comboBoxEx3.Text.ToString();
            if (rwly.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "请输入任务来源！");
                comboBoxEx3.Focus();
                return;
            }

            int draw_id = 0;
            if (textBoxX5.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "请输入制片人！");
                textBoxX5.Focus();
                return;
            }

            if (textBoxX7.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "请输入病理号！");
                textBoxX7.Focus();
                return;
            }
            //获取drawid
            DBHelper.BLL.exam_draw_meterials insDrawIns = new DBHelper.BLL.exam_draw_meterials();
            draw_id = insDrawIns.GetDrawId(textBoxX7.Text.Trim(), textBoxX6.Text.Trim());
            if (draw_id == -1)
            {
                Frm_TJInfo("错误", "不存在此病理号下的蜡块号！");
                return;
            }
            //获取检查前缀
            DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
            //获取modality
            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
            DBHelper.BLL.exam_filmmaking insF = new DBHelper.BLL.exam_filmmaking();
            //获取包埋医生
            string bm_doc_name = insF.GetBmDocName(draw_id);
            string study_no = textBoxX7.Text.Trim();
            string modality = insM.GetModality(study_no);
            string prechar = InsType.GetPrechar(modality);
            string qp_barcode = insF.GetBarcode(study_no, prechar);
            if (qp_barcode != "")
            {
                int result = insF.InsertQpInfo(study_no, draw_id, study_no + "-" + textBoxX6.Text.Trim(), qp_barcode, rwly, textBoxX8.Text.Trim(), 1, textBoxX5.Text.Trim(), dateTimePicker1.Value.ToString("yyyy-MM-dd"), bm_doc_name);
                if (result == 1)
                {
                    Frm_TJInfo("提示", "添加切片信息成功！");
                    buttonX5.PerformClick();
                }
            }
        }
        //删除切片
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (dataGridViewX5.Rows.Count > 0)
            {
                if (dataGridViewX5.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection col = dataGridViewX5.SelectedRows;
                    if (col.Count > 0)
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;
                        if (TaskDialog.Show("删除切片信息", "确认", "确定要删除选中的切片么？", Curbutton) != eTaskDialogResult.Ok)
                        {
                            return;
                        }
                        DBHelper.BLL.exam_filmmaking InsFi = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            string make_doc_name = row.Cells["make_doc_nameyzp"].Value.ToString();
                            if (make_doc_name.Equals(Program.User_Name))
                            {
                                int id = Convert.ToInt32(row.Cells["idyzp"].Value);
                                //删除切片信息
                                InsFi.DelQpInfo(id);
                            }
                            else
                            {
                                Frm_TJInfo("提示", "非本人制片不能删除！");
                            }
                        }
                        buttonX5.PerformClick();
                    }
                }
            }
        }

        private void dataGridViewX4_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle Rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridViewX4.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridViewX4.RowHeadersDefaultCellStyle.Font, Rectangle, dataGridViewX4.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }

        private void dataGridViewX3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            System.Drawing.Rectangle Rectangle = new System.Drawing.Rectangle(e.RowBounds.Location.X, e.RowBounds.Location.Y, dataGridViewX3.RowHeadersWidth - 4, e.RowBounds.Height);
            TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), dataGridViewX3.RowHeadersDefaultCellStyle.Font, Rectangle, dataGridViewX3.RowHeadersDefaultCellStyle.ForeColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Right);
        }
        //查询医嘱
        private void buttonX7_Click(object sender, EventArgs e)
        {

            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //
                DBHelper.BLL.exam_yz ins = new DBHelper.BLL.exam_yz();
                DataTable dt1 = ins.GetYzinfo(txt_yz_blh.Text.Trim(), cmbYzlx.Text.Trim());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    superGridControl5.PrimaryGrid.DataSource = dt1;
                }
                else
                {
                    superGridControl5.PrimaryGrid.DataSource = null;
                }

                //清空原来的数据
                dataGridViewX4.BringToFront();
                dataGridViewX4.Rows.Clear();
                //
                DataTable dt = ins.QueryYzinfo(txt_yz_blh.Text.Trim(), cmbYzlx.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX4.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["lk_no"], dt.Rows[i]["work_source"], dt.Rows[i]["bj_name"], dt.Rows[i]["group_num"], dt.Rows[i]["sq_doctor_name"], dt.Rows[i]["sq_datetime"], dt.Rows[i]["qc_id"], dt.Rows[i]["memo_note"], dt.Rows[i]["yzlx"] };
                        dataGridViewX4.Rows.Add(row);

                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX4.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void superGridControl5_RowClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs e)
        {

            if (superGridControl5.PrimaryGrid.Rows.Count > 0)
            {
                if (e.GridPanel.ActiveRow == null)
                {
                    superGridControl5.PrimaryGrid.Rows.Clear();
                    superGridControl5.PrimaryGrid.InvalidateLayout();
                    return;
                }
                int index = e.GridPanel.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }

                //清空原来的数据
                dataGridViewX4.BringToFront();
                dataGridViewX4.Rows.Clear();

                GridRow Row = (GridRow)superGridControl5.PrimaryGrid.Rows[index];
                //
                string study_no = Row.Cells["study_no"].Value.ToString();
                //
                string work_source = Row.Cells["work_source"].Value.ToString();
                //
                string lk_no = Row.Cells["lk_no"].Value.ToString();
                //
                string yzlx = Row.Cells["yzlx"].Value.ToString();
                DBHelper.BLL.exam_yz ins = new DBHelper.BLL.exam_yz();
                DataTable dt = ins.GetSpeYzinfo(study_no, lk_no, work_source, yzlx);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX4.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["lk_no"], dt.Rows[i]["work_source"], dt.Rows[i]["bj_name"], dt.Rows[i]["group_num"], dt.Rows[i]["sq_doctor_name"], dt.Rows[i]["sq_datetime"], dt.Rows[i]["qc_id"], dt.Rows[i]["memo_note"], dt.Rows[i]["yzlx"] };
                        dataGridViewX4.Rows.Add(row);

                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX4.Focus();
            }
            superGridControl5.Select();
            superGridControl5.Focus();
        }

        private void buttonX14_Click(object sender, EventArgs e)
        {
            if (dataGridViewX4.Rows.Count > 0)
            {
                if (dataGridViewX4.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX4.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {
                        DBHelper.BLL.exam_yz insYz = new DBHelper.BLL.exam_yz();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            string yzlx = row.Cells["yzlxyz"].Value.ToString();
                            int id = Convert.ToInt32(row.Cells["idyz"].Value.ToString());

                            //获取检查前缀
                            DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                            //获取modality
                            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                            DBHelper.BLL.exam_filmmaking insF = new DBHelper.BLL.exam_filmmaking();
                            string study_no = row.Cells["study_noyz"].Value.ToString();
                            int draw_id = Convert.ToInt32(row.Cells["qc_idyz"].Value.ToString());
                            string modality = insM.GetModality(study_no);
                            string prechar = InsType.GetPrechar(modality);
                            string qp_barcode = insF.GetBarcode(study_no, prechar);
                            string lk_no = row.Cells["lk_noyz"].Value.ToString();
                            int group_num = Convert.ToInt32(row.Cells["group_numyz"].Value.ToString());
                            string rwly = row.Cells["work_sourceyz"].Value.ToString();
                            string bjw = "";
                            if (yzlx.Equals("0"))
                            {
                                bjw = rwly;
                            }
                            else
                            {
                                bjw = row.Cells["bj_nameyz"].Value.ToString();
                            }
                            if (qp_barcode != "")
                            {
                                if (insF.InsertQpInfo(study_no, draw_id, lk_no, qp_barcode, rwly, bjw, group_num, Program.User_Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "") == 1)
                                {
                                    insYz.UpdateyzStatus(id, Program.User_Name, qp_barcode, yzlx);
                                }
                            }
                        }
                        buttonX7.PerformClick();
                    }
                }
            }
        }

        private void buttonX20_Click(object sender, EventArgs e)
        {
            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //清空原来的数据
                dataGridViewX3.BringToFront();
                dataGridViewX3.Rows.Clear();
                //
                DBHelper.BLL.exam_yz ins = new DBHelper.BLL.exam_yz();
                DataTable dt = ins.QueryYzxYzinfo(textBoxX4.Text.Trim(), dtStart.Value.ToString("yyyy-MM-dd"), this.dtEnd.Value.ToString("yyyy-MM-dd"));
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX3.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["lk_no"], dt.Rows[i]["work_source"], dt.Rows[i]["bj_name"], dt.Rows[i]["group_num"], dt.Rows[i]["barcode"], dt.Rows[i]["sq_doctor_name"], dt.Rows[i]["sq_datetime"], dt.Rows[i]["zx_doc_name"], dt.Rows[i]["zx_datetime"], dt.Rows[i]["memo_note"], dt.Rows[i]["yzlx"], dt.Rows[i]["qc_id"] };
                        dataGridViewX3.Rows.Add(row);

                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX3.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX3.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel4.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel4.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX3.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void buttonX19_Click(object sender, EventArgs e)
        {
            if (dataGridViewX3.Rows.Count > 0)
            {
                if (dataGridViewX3.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX3.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {
                        //更新打印状态
                        DBHelper.BLL.exam_filmmaking InsFi = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            string barcode = row.Cells["barcodeyzx"].Value.ToString();
                            //更加条码号更新打印状态
                            InsFi.UpdateBarcodePrintFlag(barcode);
                        }

                        //模式
                        if (ConfigurationManager.AppSettings["sfmbp_enable"] != null && ConfigurationManager.AppSettings["sfmbp_enable"].ToString().Equals("1"))
                        {
                            for (int j = 0; j < col.Count; j++)
                            {
                                DataGridViewRow row = col[j];
                                string study_noyzx = row.Cells["study_noyzx"].Value.ToString();
                                string lkh2 = row.Cells["lk_noyzx"].Value.ToString();
                                string barcode2 = row.Cells["barcodeyzx"].Value.ToString().Replace(study_noyzx, "");
                                string rwly = row.Cells["work_sourceyzx"].Value.ToString();
                                string yzlx = row.Cells["yzlxyzx"].Value.ToString();
                                string bz2 = "";
                                if (yzlx.Equals("0"))
                                {
                                    bz2 = rwly;
                                }
                                else
                                {
                                    bz2 = row.Cells["bj_nameyzx"].Value.ToString();
                                }
                                Print_bp(lkh2, barcode2, yzlx);
                            }
                        }
                        else
                        {
                            //双联
                            if (PIS_Sys.Properties.Settings.Default.zpBarcodePrintSL)
                            {
                                //开始打印
                                string lkh1 = "";
                                string lkh2 = "";
                                string barcode1 = "";
                                string barcode2 = "";
                                string bz1 = "";
                                string bz2 = "";
                                string hospi1 = "";
                                string hospi2 = "";
                                for (int j = 0; j < col.Count; j = j + 2)
                                {
                                    if (col.Count % 2 == 1)
                                    {
                                        if (j == col.Count - 1)
                                        {
                                            DataGridViewRow row = col[j];
                                            lkh2 = row.Cells["lk_noyzx"].Value.ToString();
                                            barcode2 = row.Cells["barcodeyzx"].Value.ToString();
                                            string rwly = row.Cells["work_sourceyzx"].Value.ToString();
                                            string yzlx = row.Cells["yzlxyzx"].Value.ToString();
                                            if (yzlx.Equals("0"))
                                            {
                                                bz2 = rwly;
                                            }
                                            else
                                            {
                                                bz2 = row.Cells["bj_nameyzx"].Value.ToString();
                                            }
                                            hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                            lkh1 = "";
                                            barcode1 = "";
                                            bz1 = "";
                                            hospi1 = "";
                                        }
                                        else
                                        {
                                            DataGridViewRow row = col[j];
                                            lkh2 = row.Cells["lk_noyzx"].Value.ToString();
                                            barcode2 = row.Cells["barcodeyzx"].Value.ToString();
                                            string rwly = row.Cells["work_sourceyzx"].Value.ToString();
                                            string yzlx = row.Cells["yzlxyzx"].Value.ToString();
                                            if (yzlx.Equals("0"))
                                            {
                                                bz2 = rwly;
                                            }
                                            else
                                            {
                                                bz2 = row.Cells["bj_nameyzx"].Value.ToString();
                                            }
                                            hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                            DataGridViewRow row1 = col[j + 1];
                                            lkh1 = row1.Cells["lk_noyzx"].Value.ToString();
                                            barcode1 = row1.Cells["barcodeyzx"].Value.ToString();
                                            rwly = row1.Cells["work_sourceyzx"].Value.ToString();
                                            yzlx = row1.Cells["yzlxyzx"].Value.ToString();
                                            if (yzlx.Equals("0"))
                                            {
                                                bz1 = rwly;
                                            }
                                            else
                                            {
                                                bz1 = row1.Cells["bj_nameyzx"].Value.ToString();
                                            }
                                            hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                        }
                                    }
                                    else
                                    {
                                        DataGridViewRow row = col[j];
                                        lkh2 = row.Cells["lk_noyzx"].Value.ToString();
                                        barcode2 = row.Cells["barcodeyzx"].Value.ToString();
                                        string rwly = row.Cells["work_sourceyzx"].Value.ToString();
                                        string yzlx = row.Cells["yzlxyzx"].Value.ToString();
                                        if (yzlx.Equals("0"))
                                        {
                                            bz2 = rwly;
                                        }
                                        else
                                        {
                                            bz2 = row.Cells["bj_nameyzx"].Value.ToString();
                                        }
                                        hospi2 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                        DataGridViewRow row1 = col[j + 1];
                                        lkh1 = row1.Cells["lk_noyzx"].Value.ToString();
                                        barcode1 = row1.Cells["barcodeyzx"].Value.ToString();
                                        rwly = row1.Cells["work_sourceyzx"].Value.ToString();
                                        yzlx = row1.Cells["yzlxyzx"].Value.ToString();
                                        if (yzlx.Equals("0"))
                                        {
                                            bz1 = rwly;
                                        }
                                        else
                                        {
                                            bz1 = row1.Cells["bj_nameyzx"].Value.ToString();
                                        }
                                        hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                    }
                                    //打印
                                    FastReportLib.PrintBarCode.PrintQPBarcode2(lkh1, barcode1, bz1, hospi1, lkh2, barcode2, bz2, hospi2, PIS_Sys.Properties.Settings.Default.zpBarcodePrinter, PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum);
                                }
                            }
                            else
                            {
                                //单联
                                for (int j = 0; j < col.Count; j++)
                                {
                                    string hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                                    DataGridViewRow row = col[j];
                                    string study_noyzx = row.Cells["study_noyzx"].Value.ToString();
                                    string lkh1 = row.Cells["lk_noyzx"].Value.ToString();
                                    string barcode1 = row.Cells["barcodeyzx"].Value.ToString().Replace(study_noyzx, "");
                                    string rwly = row.Cells["work_sourceyzx"].Value.ToString();
                                    string yzlx = row.Cells["yzlxyzx"].Value.ToString();
                                    string bz1 = "";
                                    if (yzlx.Equals("0"))
                                    {
                                        bz1 = rwly;
                                    }
                                    else
                                    {
                                        bz1 = row.Cells["bj_nameyzx"].Value.ToString();
                                    }
                                    FastReportLib.PrintBarCode.PrintQPBarcode(lkh1, barcode1, bz1, hospi1, PIS_Sys.Properties.Settings.Default.zpBarcodePrinter, PIS_Sys.Properties.Settings.Default.zpBarcodePrintNum);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void superTabControl2_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text == "待执行医嘱")
            {
                //刷新科内医嘱
                buttonX7.PerformClick();
            }
            else if (e.NewValue.Text == "已执行医嘱")
            {
                buttonX20.PerformClick();
            }
        }

        //展示科内留言
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            Form Frm_mesIns = Application.OpenForms["FrmMessage"];
            if (Frm_mesIns == null)
            {
                Frm_mesIns = new FrmMessage();
                Frm_mesIns.TopMost = true;
                Frm_mesIns.WindowState = FormWindowState.Normal;
                Frm_mesIns.Show();
                Frm_mesIns.Activate();
            }
            else
            {
                Frm_mesIns.BringToFront();
                Frm_mesIns.WindowState = FormWindowState.Normal;
                Frm_mesIns.Activate();
            }
        }
        //技术类查询
        private void buttonX17_Click(object sender, EventArgs e)
        {
            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //
                DBHelper.BLL.exam_yz ins = new DBHelper.BLL.exam_yz();
                DataTable dt1 = ins.GetjsYzinfo(txt_yz_blh.Text.Trim());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    superGridControl5.PrimaryGrid.DataSource = dt1;
                }
                else
                {
                    superGridControl5.PrimaryGrid.DataSource = null;
                }

                //清空原来的数据
                dataGridViewX4.BringToFront();
                dataGridViewX4.Rows.Clear();
                //
                DataTable dt = ins.QueryjsYzinfo(txt_yz_blh.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX4.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["lk_no"], dt.Rows[i]["work_source"], dt.Rows[i]["bj_name"], dt.Rows[i]["group_num"], dt.Rows[i]["sq_doctor_name"], dt.Rows[i]["sq_datetime"], dt.Rows[i]["qc_id"], dt.Rows[i]["memo_note"], dt.Rows[i]["yzlx"] };
                        dataGridViewX4.Rows.Add(row);

                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX4.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
        //特检类查询
        private void buttonX18_Click(object sender, EventArgs e)
        {
            try
            {
                //开始查询
                this.Cursor = Cursors.WaitCursor;
                //
                DBHelper.BLL.exam_yz ins = new DBHelper.BLL.exam_yz();
                DataTable dt1 = ins.GettjYzinfo(txt_yz_blh.Text.Trim());
                if (dt1 != null && dt1.Rows.Count > 0)
                {
                    superGridControl5.PrimaryGrid.DataSource = dt1;
                }
                else
                {
                    superGridControl5.PrimaryGrid.DataSource = null;
                }

                //清空原来的数据
                dataGridViewX4.BringToFront();
                dataGridViewX4.Rows.Clear();
                //
                DataTable dt = ins.QuerytjYzinfo(txt_yz_blh.Text.Trim());
                if (dt != null && dt.Rows.Count > 0)
                {
                    int icount = 0;
                    string First_NO = dt.Rows[0]["study_no"].ToString();
                    Color[] rowColor = new Color[] { dataGridViewX4.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["lk_no"], dt.Rows[i]["work_source"], dt.Rows[i]["bj_name"], dt.Rows[i]["group_num"], dt.Rows[i]["sq_doctor_name"], dt.Rows[i]["sq_datetime"], dt.Rows[i]["qc_id"], dt.Rows[i]["memo_note"], dt.Rows[i]["yzlx"] };
                        dataGridViewX4.Rows.Add(row);
                        //隔行变色
                        if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                        {
                            icount += 1;
                            First_NO = dt.Rows[i]["study_no"].ToString();
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                        else
                        {
                            dataGridViewX4.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                        }
                    }
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                }
                else
                {
                    toolStripStatusLabel3.Text = String.Format("共查询到 {0} 条记录", 0);
                }
                dataGridViewX4.Focus();
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        //全部发送免疫组化设备
        private void buttonX21_Click(object sender, EventArgs e)
        {
            if (dataGridViewX3.Rows.Count > 0)
            {
                if (dataGridViewX3.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX3.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {
                        //更新打印状态
                        DBHelper.BLL.exam_filmmaking InsFi = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            string yzlx = row.Cells["yzlxyzx"].Value.ToString();
                            if (yzlx.Equals("1"))
                            {
                                string id = row.Cells["idyzx"].Value.ToString();
                                string BLH = row.Cells["study_noyzx"].Value.ToString();
                                string lk_no = row.Cells["lk_noyzx"].Value.ToString();
                                string djsj = row.Cells["sq_datetimeyzx"].Value.ToString();
                                string bj_name = row.Cells["bj_nameyzx"].Value.ToString();
                                string barcode = row.Cells["barcodeyzx"].Value.ToString();
                                string XM; string XB; string rs_code; string rs_name;
                                DBHelper.BLL.tjbjw tjbjwins = new DBHelper.BLL.tjbjw();
                                DBHelper.BLL.exam_tjyz examtjyzins = new DBHelper.BLL.exam_tjyz();
                                DataSet dsbjw = tjbjwins.GetBjwInfo("select * from tj_bjw_dict where bjw_name='" + bj_name + "'");
                                DataSet dspat = examtjyzins.GetInfo("select patient_name,sex from exam_master_view where study_no='" + BLH + "'");
                                rs_code = dsbjw.Tables[0].Rows[0]["rs_code"].ToString();
                                rs_name = dsbjw.Tables[0].Rows[0]["rs_name"].ToString();
                                if (rs_name.Equals("") || rs_code.Equals(""))
                                {
                                    labelInfotj.Text = string.Format("请维护标记物{0}的设备的染色码和染色名称对应关系！当前其他所有发送已经停止", bj_name);
                                    return;
                                }
                                XM = dspat.Tables[0].Rows[0]["patient_name"].ToString().Substring(0, 1) + "^" + dspat.Tables[0].Rows[0]["patient_name"].ToString().Substring(1);
                                XB = dspat.Tables[0].Rows[0]["sex"].ToString();
                                if (XB.Equals("男"))
                                {
                                    XB = "M";
                                }
                                else if (XB.Equals("女"))
                                {
                                    XB = "F";
                                }
                                else
                                {
                                    XB = "U";
                                }
                                labelInfotj.Text = "";
                                string ret = Myzh_BenchMark_Hl7.Myzh_BenchMark.SendHl7Msg(id, BLH, lk_no, barcode, djsj, XM, XB, rs_code, rs_name, MYZH_IP, MYZH_PORT);
                                if (ret.Equals("OK"))
                                {
                                    labelInfotj.Text = "命令成功发送！";
                                }
                                else
                                {
                                    labelInfotj.Text = string.Format("标记物{0}命令成功失败；其他所有发送已经停止！", bj_name);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
            labelInfotj.Text = "";
        }

        private void btn_zp_SMOK_Click(object sender, EventArgs e)
        {
            checkBoxX3.Checked = true;
            FrmSmHd ins = new FrmSmHd();
            ins.TopMost = true;
            ins.Owner = this;
            ins.ShowDialog();
            checkBoxX3.Checked = false;
        }

        //导出excel
        private void buttonX22_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {

                    DataGridViewSelectedRowCollection LstCols = dataGridViewX1.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 96)
                    {
                        Frm_TJInfo("提示", "不能导出,最多选择96条记录！");
                        return;
                    }

                    if (col.Count > 0)
                    {
                        //获取检查前缀
                        DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                        //获取当前条码号
                        DBHelper.BLL.exam_draw_meterials InsFi = new DBHelper.BLL.exam_draw_meterials();
                        //获取modality
                        DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            int id = Convert.ToInt32(row.Cells["id"].Value);
                            string study_no = row.Cells["study_no"].Value.ToString();
                            string qp_barcode = row.Cells["qp_barcode"].Value.ToString();
                            if (qp_barcode.Equals(""))
                            {
                                string modality = insM.GetModality(study_no);
                                string prechar = InsType.GetPrechar(modality);
                                qp_barcode = InsFi.GetQPBarcode(study_no, prechar);
                                if (qp_barcode != "")
                                {
                                    //更新条码
                                    if (InsFi.UpdateQpBarcode(id, qp_barcode))
                                    {
                                        row.Cells["qp_barcode"].Value = qp_barcode;
                                    }
                                    //更新打印状态
                                    if (InsFi.UpdatePrintFlag(id))
                                    {
                                        row.Cells["print_flag"].Value = "已打印";
                                    }
                                }
                            }
                        }

                        if (System.IO.File.Exists(Program.APPdirPath + @"\qptm.xls"))
                        {
                            Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(Program.APPdirPath + @"\qptm.xls");
                            //先清空全部
                            for (int i = 0; i < 12; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    wk.Worksheets[0].Cells[i, j].PutValue("");
                                }
                            }


                            //开始写

                            for (int i = 0; i < 12; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    int index = i * 8 + j;
                                    if (col.Count > index)
                                    {
                                        DataGridViewRow row = col[index];
                                        wk.Worksheets[0].Cells[i, j].PutValue(row.Cells["barcode"].Value.ToString());
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            wk.Save(Program.APPdirPath + @"\qptm.xls");

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

                                //
                                File.Copy(Program.APPdirPath + @"\qptm.xls", localFilePath);
                                MessageBox.Show("导出成功：" + localFilePath, "蜡块号导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            Frm_TJInfo("提示", "qptm.xlsx模版不存在！");
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误：" + ex.ToString());
            }
        }
        //导出excel
        private void buttonX23_Click(object sender, EventArgs e)
        {
            if (dataGridViewX5.Rows.Count > 0)
            {
                if (dataGridViewX5.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX5.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();
                    if (col.Count > 96)
                    {
                        Frm_TJInfo("提示", "不能导出,最多选择96条记录！");
                        return;
                    }
                    if (col.Count > 0)
                    {
                        //更新打印状态
                        DBHelper.BLL.exam_filmmaking InsFi = new DBHelper.BLL.exam_filmmaking();
                        for (int i = 0; i < col.Count; i++)
                        {
                            DataGridViewRow row = col[i];
                            string print_flag = row.Cells["print_flagyzp"].Value.ToString();
                            if (print_flag.Equals("未打印"))
                            {
                                int id = Convert.ToInt32(row.Cells["idyzp"].Value);
                                //更新打印状态
                                if (InsFi.UpdatePrintFlag(id))
                                {
                                    row.Cells["print_flagyzp"].Value = "已打印";
                                }
                            }
                        }

                        if (System.IO.File.Exists(Program.APPdirPath + @"\qptm.xls"))
                        {
                            Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(Program.APPdirPath + @"\qptm.xls");
                            //先清空全部
                            for (int i = 0; i < 12; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    wk.Worksheets[0].Cells[i, j].PutValue("");
                                }
                            }


                            //开始写

                            for (int i = 0; i < 12; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    int index = i * 8 + j;
                                    if (col.Count > index)
                                    {
                                        DataGridViewRow row = col[index];
                                        wk.Worksheets[0].Cells[i, j].PutValue(row.Cells["draw_barcodeyzp"].Value.ToString());
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            wk.Save(Program.APPdirPath + @"\qptm.xls");

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

                                //
                                File.Copy(Program.APPdirPath + @"\qptm.xls", localFilePath);
                                MessageBox.Show("导出成功：" + localFilePath, "蜡块号导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            Frm_TJInfo("提示", "qptm.xlsx模版不存在！");
                            return;
                        }


                        buttonX5.PerformClick();
                    }
                }
            }
        }
        //脱钙
        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked == true)
            {
                try
                {
                    //开始查询
                    this.Cursor = Cursors.WaitCursor;
                    //清空原来的数据
                    dataGridViewX1.BringToFront();
                    dataGridViewX1.Rows.Clear();
                    //
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    DataTable dt = ins.GetTgBminfo();

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int icount = 0;
                        string First_NO = dt.Rows[0]["study_no"].ToString();
                        Color[] rowColor = new Color[] { dataGridViewX1.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["meterial_no"], dt.Rows[i]["parts"], dt.Rows[i]["group_num"], dt.Rows[i]["draw_doctor_name"], dt.Rows[i]["draw_datetime"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["bm_qr"], dt.Rows[i]["print_flag"], dt.Rows[i]["memo_note"], dt.Rows[i]["barcode"], dt.Rows[i]["qp_barcode"] };
                            dataGridViewX1.Rows.Add(row);
                            if (dt.Rows[i]["bm_qr"].ToString().Equals("0"))
                            {
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "待包埋";
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["bm_qr"].ToString().Equals("1"))
                            {
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "已包埋";
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Blue;
                            }
                            if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX1.Rows[i].Cells["print_flag"].Value = "未打印";
                                dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX1.Rows[i].Cells["print_flag"].Value = "已打印";
                                dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Red;
                            }
                            else if (dt.Rows[i]["work_source"].ToString().Equals("脱钙"))
                            {
                                dataGridViewX1.Rows[i].Cells["work_source"].Style.ForeColor = Color.Blue;
                            }
                            else if (dt.Rows[i]["work_source"].ToString().Equals("冰冻") || dt.Rows[i]["work_source"].ToString().Equals("冰余"))
                            {
                                dataGridViewX1.Rows[i].Cells["work_source"].Style.ForeColor = Color.Red;
                            }
                            //隔行变色
                            if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                            {
                                icount += 1;
                                First_NO = dt.Rows[i]["study_no"].ToString();
                                dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                            else
                            {
                                dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                        }
                        lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                    }
                    else
                    {
                        lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", 0);
                    }
                    dataGridViewX1.Focus();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }

            }
            else
            {
                try
                {
                    //开始查询
                    this.Cursor = Cursors.WaitCursor;
                    //清空原来的数据
                    dataGridViewX1.BringToFront();
                    dataGridViewX1.Rows.Clear();
                    //
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    DataTable dt = ins.GetFtgBminfo(textBoxX1.Text.Trim(), cmb_bmqcys.Text.Trim(), dt_qcsj.Value.ToString("yyyy-MM-dd"), checkBoxX10.Checked);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int icount = 0;
                        string First_NO = dt.Rows[0]["study_no"].ToString();
                        Color[] rowColor = new Color[] { dataGridViewX1.DefaultCellStyle.BackColor, Color.LightSteelBlue };
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Object[] row = new Object[] { dt.Rows[i]["id"], dt.Rows[i]["study_no"], dt.Rows[i]["work_source"], dt.Rows[i]["meterial_no"], dt.Rows[i]["parts"], dt.Rows[i]["group_num"], dt.Rows[i]["draw_doctor_name"], dt.Rows[i]["draw_datetime"], dt.Rows[i]["bm_doc_name"], dt.Rows[i]["bm_qr"], dt.Rows[i]["print_flag"], dt.Rows[i]["memo_note"], dt.Rows[i]["barcode"], dt.Rows[i]["qp_barcode"] };
                            dataGridViewX1.Rows.Add(row);
                            if (dt.Rows[i]["bm_qr"].ToString().Equals("0"))
                            {
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "待包埋";
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["bm_qr"].ToString().Equals("1"))
                            {
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Value = "已包埋";
                                dataGridViewX1.Rows[i].Cells["bm_qr"].Style.ForeColor = Color.Blue;
                            }
                            if (dt.Rows[i]["print_flag"].ToString().Equals("0"))
                            {
                                dataGridViewX1.Rows[i].Cells["print_flag"].Value = "未打印";
                                dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Black;
                            }
                            else if (dt.Rows[i]["print_flag"].ToString().Equals("1"))
                            {
                                dataGridViewX1.Rows[i].Cells["print_flag"].Value = "已打印";
                                dataGridViewX1.Rows[i].Cells["print_flag"].Style.ForeColor = Color.Red;
                            }
                            else if (dt.Rows[i]["work_source"].ToString().Equals("脱钙"))
                            {
                                dataGridViewX1.Rows[i].Cells["work_source"].Style.ForeColor = Color.Blue;
                            }
                            else if (dt.Rows[i]["work_source"].ToString().Equals("冰冻") || dt.Rows[i]["work_source"].ToString().Equals("冰余"))
                            {
                                dataGridViewX1.Rows[i].Cells["work_source"].Style.ForeColor = Color.Red;
                            }
                            //隔行变色
                            if (!dt.Rows[i]["study_no"].ToString().Equals(First_NO))
                            {
                                icount += 1;
                                First_NO = dt.Rows[i]["study_no"].ToString();
                                dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                            else
                            {
                                dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = rowColor[icount % 2];
                            }
                        }
                        lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", dt.Rows.Count);
                    }
                    else
                    {
                        lab_bmInfo.Text = String.Format("共查询到 {0} 条记录", 0);
                    }
                    dataGridViewX1.Focus();
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                buttonX15.Tag = "asc";
            }
        }
        //更新蜡块脱水上机时间
        private void buttonX9_Click_1(object sender, EventArgs e)
        {
            if (dataGridViewX1.Rows.Count > 0)
            {
                if (dataGridViewX1.SelectedRows.Count > 0)
                {
                    DataGridViewSelectedRowCollection LstCols = dataGridViewX1.SelectedRows;

                    IEnumerable<DataGridViewRow> Recols = LstCols.Cast<DataGridViewRow>().Reverse<DataGridViewRow>();

                    List<DataGridViewRow> col = Recols.ToList<DataGridViewRow>();

                    if (col.Count > 0)
                    {

                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;
                        if (TaskDialog.Show("病理信息管理系统", "确认", "要更新选中蜡块的上脱水机时间麽？", Curbutton) == eTaskDialogResult.Ok)
                        {

                            DBHelper.BLL.exam_draw_meterials InsFi = new DBHelper.BLL.exam_draw_meterials();

                            for (int i = 0; i < col.Count; i++)
                            {
                                DataGridViewRow row = col[i];
                                int id = Convert.ToInt32(row.Cells["id"].Value);
                                InsFi.UpdateLkTs_DateTime(id);
                            }

                            buttonX1.PerformClick();
                        }
                    }
                }
            }
        }

    }
}
