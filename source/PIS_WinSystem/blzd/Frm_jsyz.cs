using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_jsyz : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string SQD = "";
        public string BRXM = "";

        public Frm_jsyz()
        {
            InitializeComponent();
            //列表样式
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;

            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }

        }

        private void Frm_jsyz_Load(object sender, EventArgs e)
        {
            txt_blh.Text = BLH;
            txt_sqd.Text = SQD;
            txt_brxm.Text = BRXM;
            //医嘱类型  
            DBHelper.BLL.bq_type_dict insbq = new DBHelper.BLL.bq_type_dict();
            DataTable dtbq = insbq.GetDataJsyz();
            if (dtbq != null && dtbq.Rows.Count > 0)
            {
                comboBoxEx1.DataSource = dtbq;
                comboBoxEx1.DisplayMember = "bq_name";
                comboBoxEx1.ValueMember = "bq_code";
            }
            else
            {
                comboBoxEx1.DataSource = null;
            }

            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            RefreshData();
        }
        private void RefreshData()
        {
            //展示技术医嘱信息
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            DataTable dt = ins.GetData(BLH);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl2.PrimaryGrid.DataSource = null;
            }

        }
        public string barcode;
        public string parts;
        public int qcid;
        //选择蜡块号
        private void buttonX1_Click(object sender, EventArgs e)
        {
            Frm_chancelk ins = new Frm_chancelk();
            ins.Owner = this;
            ins.BLH = txt_blh.Text.Trim();
            ins.BringToFront();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                qcid = ins.id;
                parts = ins.parts;
                barcode = ins.barcode;
                txt_lkh.Text = barcode;
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
        //添加
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (!txt_lkh.Text.Trim().Equals(""))
            {
                //生成医嘱编号
                DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                string yzstr = sys_sequence_ins.GetYZID_Sequence();
                DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
                Boolean result = ins.InsertData(yzstr, BLH, barcode, comboBoxEx1.Text.Trim(), parts, integerInput1.Value, Program.User_Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "申请", "", textBoxX1.Text.Trim(), 1, qcid);
                if (result == true)
                {
                    qcid = 0;
                    parts = "";
                    barcode = "";
                    txt_lkh.Text = "";
                    Frm_TJInfo("提示", "添加技术医嘱成功！");
                    RefreshData();
                }
            }
            else
            {
                Frm_TJInfo("提示", "请先选择蜡块号！");

            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除技术医嘱", "确认", "确定要删除这条技术医嘱信息么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    if (superGridControl2.ActiveRow == null)
                    {
                        return;
                    }
                    int index = superGridControl2.ActiveRow.Index;
                    if (index == -1)
                    {
                        return;
                    }
                    //开始执行删除操作
                    int id = Convert.ToInt32(((GridRow)superGridControl2.PrimaryGrid.Rows[index]).Cells["id"].Value);
                    string yz_flag = ((GridRow)superGridControl2.PrimaryGrid.Rows[index]).Cells["yz_flag"].Value.ToString();
                    if (yz_flag.Equals("已执行"))
                    {
                        Frm_TJInfo("提示", "当前医嘱已经执行，诊断医师不能删除！\n请通知技师执行删除！");
                        return;
                    }
                    DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
                    Boolean result = ins.DelData(id);
                    if (result == true)
                    {
                        Frm_TJInfo("提示", "删除技术医嘱成功！");
                        RefreshData();
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "不存在技术医嘱！");
            }
        }

        private void Frm_jsyz_FormClosing(object sender, FormClosingEventArgs e)
        {

            //更新医嘱可用
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            ins.UpdateZCData(BLH);
        }




    }
}
