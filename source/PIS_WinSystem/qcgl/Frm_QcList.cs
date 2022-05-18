using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.qcgl
{
    public partial class Frm_QcList : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_QcList()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            this.reportViewer1.setToolBarVisible(true);
        }
        //执行查询
        private void buttonX1_Click(object sender, EventArgs e)
        {
            DBHelper.BLL.exam_draw_meterials Ins = new DBHelper.BLL.exam_draw_meterials();
            DataSet ds1 = Ins.GetDsStudyno(dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            DataSet ds2 = Ins.GetDsDrawList(dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            this.reportViewer1.QpListPreview(ds1, ds2, dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), PIS_Sys.Properties.Settings.Default.qcListPrinter);
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!this.reportViewer1.PrintQpListReport())
            {
                Frm_TJInfo("提示", "请先查询然后再打印！");
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

        //蜡块移交表
        private void buttonX3_Click(object sender, EventArgs e)
        {
            DBHelper.BLL.exam_draw_meterials Ins = new DBHelper.BLL.exam_draw_meterials();
            DataSet ds = Ins.GetDsYjb(dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            this.reportViewer1.LkYjbListPreview(ds, dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"), PIS_Sys.Properties.Settings.Default.qcListPrinter);

        }

        private void Frm_QcList_Load(object sender, EventArgs e)
        {

        }



    }
}
