using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.qcgl
{
    public partial class Frm_BqList : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BqList()
        {
            InitializeComponent();
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
            this.CenterToScreen();
            //列表样式
            superGridControl2.PrimaryGrid.ShowRowHeaders = true;
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
        }

        private void Frm_BqList_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void RefreshData()
        {
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            DataTable dt = ins.GetQcJsyzData();
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl2.PrimaryGrid.DataSource = null;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
                SelectedElementCollection col = superGridControl2.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow row = col[i] as GridRow;
                        int id = Convert.ToInt32(row.Cells["id"].Value);
                        //更新状态
                        ins.UpdateJsyzStatus(id, Program.User_Name);
                    }
                    RefreshData();
                }
            }
        }
        private void buttonX1_Click(object sender, EventArgs e)
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
            DBHelper.BLL.exam_jsyz ins = new DBHelper.BLL.exam_jsyz();
            DataTable dt = ins.GetQcYzxJsyzData(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "没有获取到信息！");
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

        private void SuperTabC_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            if (e.NewValue.Text == "已执行医嘱")
            {
                buttonX1.PerformClick();
            }
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

    }
}
