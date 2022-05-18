using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_tjQp : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_tjQp()
        {
            InitializeComponent();
        }

        public void RefreshData()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" zx_datetime>='{0} 00:00:00' and zx_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            if (this.comboBox1.SelectedIndex != 0)
            {
                sb.AppendFormat(" and work_source='{0}'", comboBox1.Text.Trim());
            }
            DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
            DataTable dttj = insTJ.GetTjlbData(sb.ToString());
            if (dttj != null && dttj.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dttj;
            }
            else
            {
                superGridControl2.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的特检切片信息！");
            }
        }
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
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            RefreshData();
        }


        private void Frm_tjQp_Load(object sender, EventArgs e)
        {
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.RowHeaderIndexOffset = 1;
            superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;
            panel2.AutoGenerateColumns = true;
            superGridControl2.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ReadOnly = true;
                panel2.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            this.comboBox1.SelectedIndex = 0;
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
    }
}
