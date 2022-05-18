using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;

namespace PIS_Sys.blzd
{
    public partial class Frm_BgHj : DevComponents.DotNetBar.Office2007Form
    {
        public static string BLH = "";
        public static string sqd = "";
        public static Boolean Refresh_Flag = false;
        public Frm_BgHj()
        {
            InitializeComponent();
        }

        private void Frm_Qcxx_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
            GridPanel panel1 = superGridControl2.PrimaryGrid;
            superGridControl2.PrimaryGrid.RowHeaderIndexOffset = 1;
            panel1.MinRowHeight = 30;
            panel1.AutoGenerateColumns = false;
            superGridControl2.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            }
            RefreshData();
        }

        public void RefreshData()
        {
            //展示当前病理号下的报告痕迹
            DBHelper.BLL.exam_report insRep = new DBHelper.BLL.exam_report();
            DataTable dtdtms = insRep.GetReportHjInfo(BLH);
            if (dtdtms != null && dtdtms.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dtdtms;
            }
            else
            {
                superGridControl2.PrimaryGrid.VirtualRowCount = 0;
                superGridControl2.PrimaryGrid.DataSource = null;
            }
        }

        private void Frm_Qcxx_Activated(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
