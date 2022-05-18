using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_QpQuery : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_QpQuery()
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
            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", ""));
            }
            else
            {

                if (checkBoxX6.Checked == true)
                {
                    sb.AppendFormat(" and qpgd_flag ={0} ", "0");
                }
                else
                {
                    sb.AppendFormat(" and make_datetime>='{0} 00:00:00' and make_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                if (checkBoxX1.Checked)
                {
                    sb.AppendFormat(" and level='{0}' ", "甲级");
                }
                else if (checkBoxX2.Checked)
                {
                    sb.AppendFormat(" and level='{0}' ", "乙级");
                }
                else if (checkBoxX3.Checked)
                {
                    sb.AppendFormat(" and level='{0}' ", "丙级");
                }
            }
            if (checkBoxX4.Checked == true)
            {
                sb.AppendFormat(" and work_source <> '{0}'", "冰冻");
            }
            if (checkBoxX5.Checked == true)
            {
                sb.AppendFormat(" and qpgd_flag ={0} ", "1");
            }
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
            dt = ins.GetQpInfo(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                tj_func(dt);
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的玻片信息！");
            }

        }


        //计数
        public void tj_func(DataTable dt)
        {

            int rows = dt.Rows.Count;
            int jia = 0;
            int yi = 0;
            int bing = 0;
            int qt = 0;
            //int he_count = 0;
            //int he_y = 0;
            //int he_l = 0;
            //int he_z = 0;
            //int he_c = 0;
            for (int i = 0; i < rows; i++)
            {
                //级别
                string level = dt.Rows[i]["level"].ToString();
                switch (level)
                {
                    case "甲级":
                        jia += 1;
                        break;
                    case "乙级":
                        yi += 1;
                        break;
                    case "丙级":
                        bing += 1;
                        break;
                    default:
                        qt += 1;
                        break;
                }
                ////he标志
                //string he_flag = dt.Rows[i]["he_flag"].ToString();
                //if (he_flag.Equals("1"))
                //{
                //    he_count += 1;
                //}
                ////he评级
                //string he_level = dt.Rows[i]["he_level"].ToString();
                //switch (he_level)
                //{
                //    case "优":
                //        he_y += 1;
                //        break;
                //    case "良":
                //        he_l += 1;
                //        break;
                //    case "中":
                //        he_z += 1;
                //        break;
                //    case "差":
                //        he_c += 1;
                //        break;
                //}
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    float yxl = 0;
                    yxl = (jia * 100 / rows);
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:甲<font color='blue'><b>{1}</b></font>,乙<font color='blue'><b>{2}</b></font>,丙<font color='blue'><b>{3}</b></font>,其他<font color='blue'><b>{4}</b></font>，HE染色切片优良率：<font color='blue'><b>{5}%</b></font>", rows, jia, yi, bing, qt, yxlstr);
                }
            }
        }



        Dictionary<string, int> DictionaryValues = new Dictionary<string, int>();
        public delegate void MenuEventHandler(string ChildText);
        public event MenuEventHandler MenuItemClick;
        private void CreatePopUpJcbwMenu()
        {
            DBHelper.BLL.exam_parts_dict Exam_part_Ins = new DBHelper.BLL.exam_parts_dict();
            DataTable dt = Exam_part_Ins.GetDsExam_parts_dict();
            if (dt != null && dt.Rows.Count > 0)
            {
                int j = 0;
                int k = 0;
                int m = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //生成菜单项 id,part_name,parent_code,order_no
                    //是父菜单时
                    if (dt.Rows[i]["parent_code"].ToString().Equals("0"))
                    {
                        k = this.contextMenuStrip2.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                        string tmp = dt.Rows[i]["id"].ToString();
                        if (DictionaryValues.ContainsKey(tmp) == false)
                        {
                            DictionaryValues.Add(tmp, k);
                        }
                        this.contextMenuStrip2.Items[k].Text = dt.Rows[i]["part_name"].ToString();
                        //添加父菜单的事件
                        (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).Click += new EventHandler(ActiveEvent);
                    }
                    else
                    {
                        //获取父菜单的索引
                        if (DictionaryValues.TryGetValue(dt.Rows[i]["parent_code"].ToString(), out j))
                        {
                            //添加子菜单到父菜单中
                            m = (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                            (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                            //记录菜单编码和菜单编号
                            DictionaryValues.Add(dt.Rows[i]["id"].ToString(), m);
                            //添加子菜单的事件
                            (this.contextMenuStrip2.Items[j] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                        }
                        else
                        {
                            //如果当前子菜单的父菜单不存在，则创建父菜单后再创建子菜单
                            k = this.contextMenuStrip2.Items.Add(new System.Windows.Forms.ToolStripMenuItem());
                            string tmp = dt.Rows[i]["parent_code"].ToString();
                            if (DictionaryValues.ContainsKey(tmp) == false)
                            {
                                DictionaryValues.Add(tmp, k);
                            }
                            this.contextMenuStrip2.Items[k].Text = Exam_part_Ins.GetExam_parts_Name(tmp);
                            //添加子菜单到父菜单中
                            m = (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems.Add(new System.Windows.Forms.ToolStripMenuItem());
                            (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Text = dt.Rows[i]["part_name"].ToString();
                            //记录菜单编码和菜单编号
                            DictionaryValues.Add(dt.Rows[i]["part_code"].ToString(), m);
                            //添加子菜单的事件
                            (this.contextMenuStrip2.Items[k] as System.Windows.Forms.ToolStripMenuItem).DropDownItems[m].Click += new EventHandler(ActiveEvent);
                        }
                    }
                }
                dt.Clear();
                dt = null;
            }
        }
        // 右键菜单的激发菜单子项的单击事件
        private void ActiveEvent(Object sender, System.EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem MI = sender as System.Windows.Forms.ToolStripMenuItem;
            if (MenuItemClick != null && MI.DropDownItems.Count == 0)
            {
                MenuItemClick(MI.Text);
            }
        }


        private void Frm_QpQuery_Load(object sender, EventArgs e)
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
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;

        }

        private void superGridControl1_GetCellValue(object sender, GridGetCellValueEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("zp_flag") == true)
            {
                if (e.Value is null || e.Value.ToString().Equals("0"))
                {
                    e.Value = "未开始";
                    e.GridCell.CellStyles.Default.TextColor = Color.Black;
                }
                else if (e.Value.ToString().Equals("1"))
                {
                    e.Value = "已完成";
                    e.GridCell.CellStyles.Default.TextColor = Color.Blue;
                }
                else if (e.Value.ToString().Equals("2"))
                {
                    e.Value = "已核对";
                    e.GridCell.CellStyles.Default.TextColor = Color.Red;
                }
            }
            else if (e.GridCell.GridColumn.Name.Equals("print_flag") == true)
            {
                if (e.Value is null || e.Value.ToString().Equals("0"))
                {
                    e.Value = "未打印";
                    e.GridCell.CellStyles.Default.TextColor = Color.Black;
                }
                else if (e.Value.ToString().Equals("1"))
                {
                    e.Value = "已打印";
                    e.GridCell.CellStyles.Default.TextColor = Color.Red;
                }
            }
            else if (e.GridCell.GridColumn.Name.Equals("qpgd_flag") == true)
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


        private void superGridControl1_GetRowHeaderStyle(object sender, GridGetRowHeaderStyleEventArgs e)
        {
            try
            {
                if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                {
                    //是否是HE切片
                    string bbgfh_flag = ((GridRow)e.GridRow).Cells["he_flag"].Value.ToString();
                    if (bbgfh_flag.Equals("1"))
                    {
                        e.GridRow.RowHeaderText = "HE";
                        e.Style.TextColor = Color.Blue;
                    }
                }

            }
            catch
            {

            }
        }


        //上一个
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
        //下一个
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
        //归档
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
                        DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                        ins.UpdateQpGd(id, Lklocation, Program.User_Name);
                    }
                    buttonX1.PerformClick();
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
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
                        DBHelper.BLL.exam_filmmaking ins = new DBHelper.BLL.exam_filmmaking();
                        ins.CancelQpGd(id);
                    }
                    buttonX1.PerformClick();
                }
            }
        }


    }
}
