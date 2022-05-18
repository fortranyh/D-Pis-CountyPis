using FastReportLib;
using System;

namespace PIS_Sys.blzd
{
    public partial class Frm_BgPre : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BgPre()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.CenterToScreen();
            this.reportViewer1.setToolBarVisible(true);
        }
        public ysReportParas ins;
        public myzhReportParas InsMyzh;
        public iceReportParas insIce;
        public string Printer_Name;
        public int Print_Copys;
        public int reportType = 2;
        private void Frm_BgPre_Load(object sender, EventArgs e)
        {
            if (reportType == 2)
            {
                this.reportViewer1.setToolBarVisible(false);
                this.reportViewer1.PreviewIceReport(insIce, Printer_Name, Print_Copys);
            }
            else if (reportType == 1)
            {
                this.reportViewer1.setToolBarVisible(false);
                this.reportViewer1.PreviewDelayReport(ins, Printer_Name, Print_Copys, 1);
            }
            else if (reportType == 0)
            {
                this.reportViewer1.setToolBarVisible(false);
                this.reportViewer1.PreviewDelayReport(ins, Printer_Name, Print_Copys, 0);
            }
            else if (reportType == 3)
            {
                this.reportViewer1.setToolBarVisible(true);
                FastReportLib.BLReportParas InsBLReport = new FastReportLib.BLReportParas();
                string PathImg = "";
                FrmBlzd insBlzd = (FrmBlzd)this.Owner;
                insBlzd.RefreshBCPreviewReport(ref InsBLReport, ref PathImg);
                this.reportViewer1.LoadBCBLPreviewer(Printer_Name, Print_Copys, InsBLReport, PathImg, ins.Txt_Content, ins.Txt_report_Doctor, ins.Txt_shreport_Doctor, ins.Txt_BgDate);
            }
            else if (reportType == 4)
            {
                this.reportViewer1.setToolBarVisible(true);
                this.reportViewer1.PreviewMyzhReport(InsMyzh, Printer_Name, Print_Copys, 4);
            }
        }
    }
}
