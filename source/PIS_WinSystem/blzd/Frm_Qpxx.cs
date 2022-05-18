using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_Qpxx : DevComponents.DotNetBar.Office2007Form
    {
        public static string CurBLH = "";
        public Frm_Qpxx()
        {
            InitializeComponent();
            superGridControl4.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl4.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            //列表样式
            GridPanel panel2 = superGridControl4.PrimaryGrid;
            panel2.MinRowHeight = 30;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                if (i != 0)
                {
                    panel2.Columns[i].ReadOnly = true;
                }
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
        }

        private void RefreshDataZP(string BLH)
        {
            if (!BLH.Equals(""))
            {
                DBHelper.BLL.exam_master insMas = new DBHelper.BLL.exam_master();
                int icount = insMas.Getwj_liud(CurBLH);
                if (icount == 0)
                {
                    checkBoxX4.Checked = false;
                }
                else
                {
                    checkBoxX4.Checked = true;
                }
                //
                if (superGridControl4.PrimaryGrid.Header == null)
                {
                    superGridControl4.PrimaryGrid.Header = new GridHeader();
                }
                superGridControl4.PrimaryGrid.Header.Text = "";
                if (superGridControl4.PrimaryGrid.Footer == null)
                {
                    superGridControl4.PrimaryGrid.Footer = new GridFooter();
                }
                superGridControl4.PrimaryGrid.Footer.Text = "";

                superGridControl4.PrimaryGrid.Rows.Clear();
                //刷新
                superGridControl4.PrimaryGrid.InvalidateLayout();

                //展示当前病理号下的所有制片信息
                DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                DataTable dt = ins.GetDtFilmMakingPj(BLH);
                if (dt != null && dt.Rows.Count > 0)
                {
                    superGridControl4.PrimaryGrid.DataSource = dt;
                    tj_ZP_func(dt, BLH);
                }
                else
                {
                    superGridControl4.PrimaryGrid.DataSource = null;
                }
            }

        }

        //切片计数
        public void tj_ZP_func(DataTable dt, string BLH)
        {

            DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
            int status = insM.GetStudyExam_Status(BLH);

            //查询当前病理号是否已经包埋确认
            Boolean Refresh_bm_flag = false;
            if (status >= 27)
            {
                Refresh_bm_flag = true;

            }
            else
            {
                Refresh_bm_flag = false;
            }
            //查询当前病理号是否已经切片核对
            Boolean Refresh_zp_flag = false;
            if (status >= 30)
            {
                Refresh_zp_flag = true;
            }
            else
            {
                Refresh_zp_flag = false;
                return;
            }
            int bpzs = 0;
            int rows = dt.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                //玻片总数
                bpzs += Convert.ToInt32(dt.Rows[i]["film_num"]);
                //最后一行开始赋值
                if (i == rows - 1)
                {

                    //制片核对医生信息
                    if (Refresh_zp_flag)
                    {
                        if (superGridControl4.PrimaryGrid.Header == null)
                        {
                            superGridControl4.PrimaryGrid.Header = new GridHeader();
                        }

                        string zpdocinfo = "";
                        string doctor_zp_name = "";
                        string doctor_zp_date = "";

                        if (insM.GetFilmInfo(ref doctor_zp_name, ref doctor_zp_date, BLH))
                        {
                            zpdocinfo = string.Format("玻片核对：{0}  玻片核对时间：{1}", doctor_zp_name, doctor_zp_date);
                        }
                        superGridControl4.PrimaryGrid.Header.Text = String.Format("<font color='Green'><b>玻片已经核对！</b></font> 。<font color='blue'><b>{0}</b></font>", zpdocinfo);
                    }

                    //包埋确认医生信息
                    string bmdocinfo = "";
                    if (Refresh_bm_flag)
                    {
                        string doctor_bm_name = "";
                        string doctor_bm_date = "";

                        if (insM.GetBaoMaiInfo(ref doctor_bm_name, ref doctor_bm_date, BLH))
                        {
                            bmdocinfo = string.Format("包埋确认：{0}  包埋确认时间：{1}", doctor_bm_name, doctor_bm_date);
                        }
                    }
                    if (superGridControl4.PrimaryGrid.Footer == null)
                    {
                        superGridControl4.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl4.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:玻片总数(<font color='blue'><b>{1}</b></font>)。<font color='blue'><b>{2}</b></font>", rows, bpzs, bmdocinfo);
                }
            }
        }

        private void Frm_Qpxx_Load(object sender, EventArgs e)
        {
            //评级原因
            DBHelper.BLL.exam_filmmaking insStop = new DBHelper.BLL.exam_filmmaking();
            DataTable dtStop = insStop.GetQpPj_Data();
            comboBoxEx1.Items.Add("");
            if (dtStop != null && dtStop.Rows.Count > 0)
            {
                for (int i = 0; i < dtStop.Rows.Count; i++)
                {
                    comboBoxEx1.Items.Add(dtStop.Rows[i]["pj_info"].ToString());
                }
            }
            RefreshDataZP(CurBLH);
        }

        private void superGridControl4_GetCellValue(object sender, GridGetCellValueEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("zp_flag") == true)
            {
                if (e.Value.ToString().Equals("0"))
                {
                    e.Value = "未开始";
                    e.GridCell.CellStyles.Default.TextColor = Color.Black;
                }
                else if (e.Value.ToString().Equals("1"))
                {
                    e.Value = "已完成";
                    e.GridCell.CellStyles.Default.TextColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("2"))
                {
                    e.Value = "已核对";
                    e.GridCell.CellStyles.Default.TextColor = Color.Red;
                }
            }
            else if (e.GridCell.GridColumn.Name.Equals("print_flag") == true)
            {
                if (e.Value.ToString().Equals("0"))
                {
                    e.Value = "未打印";
                    e.GridCell.CellStyles.Default.TextColor = Color.Black;
                }
                else if (e.Value.ToString().Equals("1"))
                {
                    e.Value = "已打印";
                    e.GridCell.CellStyles.Default.TextColor = Color.Red;
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string level = "";
            if (checkBoxX1.Checked == true)
            {
                level = checkBoxX1.Text;
            }
            else if (checkBoxX2.Checked == true)
            {
                level = checkBoxX2.Text;

            }
            else if (checkBoxX3.Checked == true)
            {

                level = checkBoxX3.Text;
            }

            SelectedElementCollection col = superGridControl4.PrimaryGrid.GetSelectedRows();
            if (col.Count > 0)
            {
                for (int i = 0; i < col.Count; i++)
                {
                    //只更新已经选中的记录  ins.Cells["taocan_type"].Value.ToString()
                    GridRow ins = col[i] as GridRow;
                    //更新级别
                    DBHelper.BLL.exam_filmmaking insM = new DBHelper.BLL.exam_filmmaking();
                    insM.UpdateFilmmakingLevel(Convert.ToInt32(ins.Cells["id"].Value), level, comboBoxEx1.Text.Trim());
                }
                //如果有更新则刷新列表
                RefreshDataZP(CurBLH);
            }
            else
            {
                Frm_TJInfo("提示", "请先选中要评级的切片！");

            }
        }

        //提示窗体
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

        private void checkBoxX4_CheckedChanged(object sender, EventArgs e)
        {
            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            if (checkBoxX4.Checked == true)
            {
                ins.Updatewj_liud(CurBLH, 1);
            }
            else
            {
                ins.Updatewj_liud(CurBLH, 0);
            }
            Frm_TJInfo("提示", "设置成功！");
        }

        private void Frm_Qpxx_Activated(object sender, EventArgs e)
        {
            RefreshDataZP(CurBLH);
        }
    }
}
