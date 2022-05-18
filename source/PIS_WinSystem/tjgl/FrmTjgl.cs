using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.tjgl
{
    public partial class FrmTjgl : DevComponents.DotNetBar.Office2007Form
    {
        public FrmTjgl()
        {
            InitializeComponent();
            //列表样式
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            GridPanel panel3 = superGridControl3.PrimaryGrid;
            panel3.MinRowHeight = 30;
            for (int i = 0; i < panel3.Columns.Count; i++)
            {
                panel3.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel3.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel3.Columns[i].CellStyles.Default.Font = this.Font;
            }
        }
        private void FrmTjgl_Activated(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            //开启焦点并刷新数据
            if (timer1.Enabled == false)
            {
                timer1.Enabled = true;
            }

        }

        string knly_str = "科内留言:<font color='red'><b>{0}</b></font>/<font color='blue'><b>{1}</b></font>";
        private void buttonX5_Click(object sender, EventArgs e)
        {
            //根据工号查询
            DBHelper.BLL.dept_message ins = new DBHelper.BLL.dept_message();
            int total = ins.GetMessageTotalCount(Program.User_Code);
            int wd_count = ins.GetMessageCount(Program.User_Code);
            lbl_knly.Text = string.Format(knly_str, wd_count, total);
            //刷新特检医嘱
            RefreshData();
        }
        private void RefreshData()
        {
            DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
            DataTable dt = ins.GetTjyzData();
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }

        }
        private void FrmTjgl_Load(object sender, EventArgs e)
        {

        }
        //
        string modality = "";
        string lk_no = "";
        string study_no = "";
        string work_source = "";


        //展示特检医嘱信息
        public void ShowTjyzInfo()
        {
            DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
            DataTable dttj = insTJ.GetData(study_no, work_source, lk_no);
            if (dttj != null && dttj.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dttj;
            }
            else
            {
                superGridControl2.PrimaryGrid.DataSource = null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            buttonX5_Click(null, null);
        }
        //更新分析结果
        private void superGridControl2_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            try
            {
                //更新当前行备注
                GridCell cell = e.GridCell;
                if (cell == null)
                {
                    return;
                }
                GridRow row = cell.GridRow;
                if (row == null)
                {
                    return;
                }
                if (row.Index == -1)
                {
                    return;
                }
                int id = Convert.ToInt32(row.Cells["id"].Value);

                if (cell.GridColumn.Name.Equals("fx_result") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新特检结果
                        DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                        ins.UpdateResultData(id, e.NewValue.ToString());
                    }
                }

            }
            catch
            {

            }
            e.GridCell.InvalidateLayout();
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
        //医嘱执行
        private void btn_zp_SDOK_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                //获取检查前缀
                DBHelper.BLL.exam_type InsType = new DBHelper.BLL.exam_type();
                for (int i = 0; i < superGridControl2.PrimaryGrid.Rows.Count; i++)
                {
                    if (((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["chk"].Value.ToString().Equals("1"))
                    {
                        int id = Convert.ToInt32(((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["id"].Value);
                        if (((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["barcode"].Value.ToString().Equals(""))
                        {
                            //获取当前条码号
                            string prechar = InsType.GetPrechar(modality);
                            string barcode = ins.GetBarcode(txt_zp_blh.Text.Trim(), prechar);
                            //更新条码
                            ins.UpdatetjyzBarcode(id, "T" + barcode);
                            ((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["barcode"].Value = "T" + barcode;
                            //更新状态
                            ins.UpdatetjyzStatus(id, Program.User_Name);
                        }
                    }
                }
                //刷新数据
                RefreshData();
                ShowTjyzInfo();
            }
        }


        private void lbl_knly_Click(object sender, EventArgs e)
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

        private void FrmTjgl_Shown(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl2.PrimaryGrid.Rows.Count; i++)
            {
                ((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["chk"].Value = 1;
            }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl2.PrimaryGrid.Rows.Count; i++)
            {
                if (((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["chk"].Value.ToString().Equals("1"))
                {
                    ((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["chk"].Value = 0;
                }
                else
                {
                    ((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["chk"].Value = 1;
                }
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl2.PrimaryGrid.Rows.Count; i++)
            {
                ((GridRow)superGridControl2.PrimaryGrid.Rows[i]).Cells["chk"].Value = 0;
            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl3.PrimaryGrid.Rows.Count; i++)
            {
                ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value = 1;
            }
        }

        private void buttonX11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl3.PrimaryGrid.Rows.Count; i++)
            {
                if (((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value.ToString().Equals("1"))
                {
                    ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value = 0;
                }
                else
                {
                    ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value = 1;
                }
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl3.PrimaryGrid.Rows.Count; i++)
            {
                ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value = 0;
            }
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text == "已执行医嘱")
            {
                buttonX2.PerformClick();
            }
            else
            {
                //刷新特检医嘱
                RefreshData();
                //
                if (!txt_zp_blh.Text.Trim().Equals(""))
                {
                    ShowTjyzInfo();
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (textBoxX1.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            }
            else
            {
                sb.AppendFormat(" and study_no>='{0}' ", textBoxX1.Text.Trim());
            }
            DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
            DataTable dt = ins.GetTjYzxData(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl3.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl3.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "没有获取到信息！");
            }
        }
        //打印条码
        private void buttonX8_Click(object sender, EventArgs e)
        {
            if (superGridControl3.PrimaryGrid.Rows.Count > 0)
            {
                //开始打印
                for (int i = 0; i < superGridControl3.PrimaryGrid.Rows.Count; i++)
                {
                    if (((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value.ToString().Equals("1") || ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["chk"].Value.ToString().Equals("True"))
                    {
                        string qph = ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["barcode"].Value.ToString();
                        string bjw = ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["bj_name"].Value.ToString();
                        string ckh = ((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["lk_no"].Value.ToString(); ;
                        int dys = Convert.ToInt16(((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["group_num"].Value.ToString());
                        if (dys < 0 || dys > 30)
                        {
                            dys = PIS_Sys.Properties.Settings.Default.tjBarcodePrintNum;
                        }
                        string hospi1 = PIS_Sys.Properties.Settings.Default.Bp_Ksmc;
                        FastReportLib.PrintBarCode.PrintQPBarcode(ckh, qph, bjw, hospi1, PIS_Sys.Properties.Settings.Default.tjBarcodePrinter, dys);
                        int id = Convert.ToInt32(((GridRow)superGridControl3.PrimaryGrid.Rows[i]).Cells["id"].Value.ToString());
                        //更新打印标记
                        DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                        ins.UpdatePrintFlag(id);
                    }
                }
                buttonX2.PerformClick();
            }
        }

        private void superGridControl3_GetCellValue(object sender, GridGetCellValueEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("dy_flag") == true)
            {
                if (e.Value.ToString().Equals("未打印"))
                {
                    e.GridCell.CellStyles.Default.TextColor = Color.Black;
                }
                else if (e.Value.ToString().Equals("已打印"))
                {
                    e.GridCell.CellStyles.Default.TextColor = Color.Red;
                }
            }
        }

        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
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
                DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                //蜡块号
                lk_no = dt.Rows[index]["lk_no"].ToString();
                //病理号
                study_no = dt.Rows[index]["study_no"].ToString();
                //医嘱类型
                work_source = dt.Rows[index]["work_source"].ToString();
                //展示基本信息
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                DataTable dtM = insM.GetData(study_no);
                if (dtM != null && dtM.Rows.Count == 1)
                {
                    txt_zp_blh.Text = dtM.Rows[0]["study_no"].ToString();
                    txt_zp_name.Text = dtM.Rows[0]["patient_name"].ToString();
                    txt_zp_sqd.Text = dtM.Rows[0]["exam_no"].ToString();
                    modality = dtM.Rows[0]["modality"].ToString();
                    ShowTjyzInfo();
                }
            }
        }
    }
}
