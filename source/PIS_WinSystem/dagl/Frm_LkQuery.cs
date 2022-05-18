using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_LkQuery : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_LkQuery()
        {
            InitializeComponent();
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
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }

            StringBuilder sb = new StringBuilder();

            if (!txt_ckbw.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and parts like '%{0}%'", txt_ckbw.Text.Trim().Replace("'", ""));
            }

            if (checkBoxX1.Checked == true)
            {
                sb.AppendFormat(" and work_source <> '{0}'", "冰冻");
            }

            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", "").ToUpper());
            }
            else
            {
                if (checkBoxX3.Checked == true)
                {
                    sb.AppendFormat(" and gd_flag ={0} ", "0");
                }
                else
                {
                    sb.AppendFormat(" and draw_datetime>='{0} 00:00:00' and draw_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
            }
            if (checkBoxX2.Checked == true)
            {
                sb.AppendFormat(" and gd_flag ={0} ", "1");
            }

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
            dt = ins.GetLkInfo(sb.ToString());
            if (superGridControl1.PrimaryGrid.Footer == null)
            {
                superGridControl1.PrimaryGrid.Footer = new GridFooter();
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                if (dt != null && dt.Rows.Count > 0)
                {
                    superGridControl1.PrimaryGrid.DataSource = dt;
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", dt.Rows.Count);
                }
                else
                {
                    superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                    superGridControl1.PrimaryGrid.DataSource = null;
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
                }
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的蜡块信息！");
            }


        }


        private void Frm_LkQuery_Load(object sender, EventArgs e)
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
                //panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //动态生成检查部位右键菜单 contextMenuStrip2
            //this.MenuItemClick += new MenuEventHandler(bbxx_MenuItemClick);

            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
        //移交单打印
        private void buttonX3_Click(object sender, EventArgs e)
        {
            qcgl.Frm_QcList InsQcgl = new qcgl.Frm_QcList();
            InsQcgl.Owner = this;
            InsQcgl.BringToFront();
            InsQcgl.ShowDialog();
        }
        //执行归档
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow Row = col[i] as GridRow;
                        //
                        int id = Convert.ToInt32(Row.Cells["id"].Value);
                        //归档位置
                        string Lklocation = textBoxX1.Text.Trim();
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.UpdateLkGd(id, Lklocation, Program.User_Name);
                    }
                    buttonX1.PerformClick();
                }
            }
        }
        //查询
        private void Txtblh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(System.Windows.Forms.Keys.Enter))
            {
                string str_tj = Txtblh.Text.Trim().Replace("'", "");
                if (!str_tj.Equals(""))
                {
                    buttonX1.PerformClick();
                }

            }
        }
        //查询下一个
        private void buttonX4_Click(object sender, EventArgs e)
        {
            string str_tj = Txtblh.Text.Trim().Replace("'", "");
            if (!str_tj.Equals(""))
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(str_tj) == true)
                {
                    Txtblh.Text = (Convert.ToInt32(str_tj) + 1).ToString();
                }
                else
                {
                    string first = str_tj.Substring(0, 1);
                    string second = str_tj.Substring(1, str_tj.Length - 1);
                    if (Microsoft.VisualBasic.Information.IsNumeric(second) == true)
                    {
                        str_tj = (Convert.ToInt32(second) + 1).ToString();
                    }
                    else
                    {
                        Frm_TJInfo("提示", "输入有误，请确认输入。");
                        Txtblh.Focus();
                        return;
                    }
                    Txtblh.Text = first + str_tj;
                }
                buttonX1.PerformClick();
            }
            else
            {
                Frm_TJInfo("提示", "请输入要查询的病理号！");
                Txtblh.Focus();
            }
        }
        //查询上一个
        private void buttonX5_Click(object sender, EventArgs e)
        {
            string str_tj = Txtblh.Text.Trim().Replace("'", "");
            if (!str_tj.Equals(""))
            {
                if (Microsoft.VisualBasic.Information.IsNumeric(str_tj) == true)
                {
                    Txtblh.Text = (Convert.ToInt32(str_tj) - 1).ToString();
                }
                else
                {
                    string first = str_tj.Substring(0, 1);
                    string second = str_tj.Substring(1, str_tj.Length - 1);
                    if (Microsoft.VisualBasic.Information.IsNumeric(second) == true)
                    {
                        str_tj = (Convert.ToInt32(second) - 1).ToString();
                    }
                    else
                    {
                        Frm_TJInfo("提示", "输入有误，请确认输入。");
                        Txtblh.Focus();
                        return;
                    }
                    Txtblh.Text = first + str_tj;
                }
                buttonX1.PerformClick();
            }
            else
            {
                Frm_TJInfo("提示", "请输入要查询的病理号！");
                Txtblh.Focus();
            }
        }



        private void superGridControl1_GetCellValue(object sender, GridGetCellValueEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("gd_flag") == true)
            {
                if (e.Value is null || e.Value.ToString().Equals("0"))
                {
                    e.Value = "未归档";
                    e.GridCell.CellStyles.Default.TextColor = Color.Black;
                }
                else if (e.Value.ToString().Equals("1"))
                {
                    e.Value = "已归档";
                    e.GridCell.CellStyles.Default.TextColor = Color.Blue;
                }
            }
        }
        //取消归档
        private void buttonX6_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow Row = col[i] as GridRow;
                        //
                        int id = Convert.ToInt32(Row.Cells["id"].Value);
                        DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                        ins.CancelLkGd(id);
                    }
                    buttonX1.PerformClick();
                }
            }
        }

    }
}
