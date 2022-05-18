using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.qcgl
{
    public partial class FrmCkhd : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string sqd = "";
        public Boolean Refresh_Flag = false;
        public FrmCkhd()
        {
            InitializeComponent();
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
            }
        }

        private void FrmCkhd_Load(object sender, EventArgs e)
        {
            if (Refresh_Flag)
            {
                this.ribbonBar1.Enabled = false;
                this.superGridControl1.Enabled = false;
            }
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            RefreshData(Refresh_Flag);
        }
        private void RefreshData(Boolean CurFlag = false)
        {
            //展示当前病理号下的所有取材信息
            DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
            DataTable dt = ins.GetDtHdMeterials(BLH);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                tj_func(dt, CurFlag);
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }

        }
        //计数
        public void tj_func(DataTable dt, Boolean CurFlag = false)
        {

            if (CurFlag)
            {
                string doctor_qc_name = "";
                string doctor_qc_date = "";
                string qcdocinfo = "";
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                string rec_doc = ins.GetSpecimensRecord_doctor_name(sqd);
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                if (insM.GetQuCaiInfo(ref doctor_qc_name, ref doctor_qc_date, BLH))
                {
                    qcdocinfo = string.Format("取材核对：{0}  取材核对时间：{1}  大体描述录入人：{2}", doctor_qc_name, doctor_qc_date, rec_doc);
                }
                if (superGridControl1.PrimaryGrid.Header == null)
                {
                    superGridControl1.PrimaryGrid.Header = new GridHeader();
                }
                superGridControl1.PrimaryGrid.Header.Text = String.Format("<font color='Green'><b>{0}</b></font>。<font color='Blue'><b>{1}</b></font>", "当前取材已经核对完毕！", qcdocinfo);
            }
            int dbb = 0;
            int xbb = 0;
            int bdbb = 0;
            int ckzs = 0;
            int rows = dt.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                //材块总数
                ckzs += Convert.ToInt32(dt.Rows[i]["group_num"]);
                //标本类型
                string specimens_class = dt.Rows[i]["specimens_class"].ToString();

                switch (specimens_class)
                {
                    case "大标本":
                        dbb += 1;
                        break;
                    case "小标本":
                        xbb += 1;
                        break;
                    case "冰冻标本":
                        bdbb += 1;
                        break;
                    default:
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:大标本(<font color='blue'><b>{1}</b></font>),小标本(<font color='blue'><b>{2}</b></font>),冰冻标本(<font color='blue'><b>{3}</b></font>).腊块总数(<font color='blue'><b>{4}</b></font>),材块总数(<font color='blue'><b>{5}</b></font>)", rows, dbb, xbb, bdbb, rows, ckzs);
                }
            }
        }

        //全部选择
        private void btn_all_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                if (((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value.ToString().Equals("0"))
                {
                    ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value = 1;
                }
            }
        }
        //反向选择
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                if (((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value.ToString().Equals("1"))
                {
                    ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value = 0;
                }
                else
                {
                    ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value = 1;
                }
            }
        }
        //清空选择，进行部分选择
        private void btn_clear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {

                ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value = 0;

            }
        }
        //保存
        private void btn_save_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {

                string flag = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["sfys"].Value.ToString();
                string hd_flag = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value.ToString();
                int id = Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["id"].Value);
                if (hd_flag.Equals("1"))
                {
                    if (flag.Equals("0"))
                    {
                        //更新核对信息
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        int result = ins.UpdateHDDrawInfo(id);
                        if (result == 0)
                        {
                            Frm_TJInfo("提示", "保存失败");
                        }
                    }
                }
            }
            Frm_TJInfo("提示", "保存完毕！");
            RefreshData(Refresh_Flag);

        }
        //核对（改变当前病理为已取材状态）
        private void btn_ckhd_Click(object sender, EventArgs e)
        {
            eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
            TaskDialog.EnableGlass = false;

            if (TaskDialog.Show("核对取材信息", "确认", "确认取材信息已经全部核对完毕了么？", Curbutton) == eTaskDialogResult.Ok)
            {
                //再次验证全部已经进行了保存且确认为正常
                for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
                {
                    string flag = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["hd_flag"].Value.ToString();
                    int id = Convert.ToInt32(((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["id"].Value);
                    if (flag.Equals("1"))
                    {
                        //更新核对信息
                        DBHelper.BLL.exam_draw_meterials insM = new DBHelper.BLL.exam_draw_meterials();
                        int resultM = insM.UpdateHDDrawInfo(id);
                        if (resultM == 0)
                        {
                            Frm_TJInfo("提示", "保存失败");
                            return;
                        }
                    }
                }
                //执行核对
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                int result = ins.UpdateQC_Flag(BLH, Program.User_Code, Program.User_Name);
                if (result == 1)
                {
                    Frm_TJInfo("取材核对成功！", "\n检查状态更新为已取材状态！");

                    //是否启用接口服务
                    if (Program.Interface_SetInfo != null)
                    {
                        if (Program.Interface_SetInfo.enable_flag == 1)
                        {
                            //已经取材结束是否调用接口服务
                            if (Program.Interface_SetInfo.draw_flag == 1)
                            {
                                ClientSCS.SynchronizedClient("");
                            }
                        }
                    }

                    Refresh_Flag = true;
                    if (Refresh_Flag)
                    {
                        this.ribbonBar1.Enabled = false;
                        this.superGridControl1.Enabled = false;
                    }
                    //进行一次刷新
                    RefreshData(Refresh_Flag);
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
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
    }
}
