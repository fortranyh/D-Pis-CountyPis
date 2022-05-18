using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;

namespace PIS_Sys.dagl
{
    public partial class Frm_hj_report : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_hj_report()
        {
            InitializeComponent();
            //窗体最大化
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
        }
        public string study_no = "";
        private void Frm_hj_report_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //锁定前四列
            panel1.FrozenColumnCount = 4;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //查询
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetHistoryReportList(study_no);
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
    }
}
