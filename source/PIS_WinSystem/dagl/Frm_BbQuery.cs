using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_BbQuery : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BbQuery()
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
                sb.AppendFormat(" and a.parts like '%{0}%'", txt_ckbw.Text.Trim().Replace("'", ""));
            }

            if (cmbJclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and b.exam_type = '{0}'", cmbJclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and b.exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }

            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", ""));
            }
            else
            {
                sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
            }

            if (checkBoxX1.Checked == true)
            {
                sb.AppendFormat(" and bbgfh_flag = {0}", "0");
            }


            DataTable dt = new DataTable();
            DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
            dt = ins.QuerySepcimensInfo(sb.ToString());
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
                if (superGridControl1.PrimaryGrid.Footer == null)
                {
                    superGridControl1.PrimaryGrid.Footer = new GridFooter();
                }


                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font>个检查 :规范化<font color='blue'><b>{1}</b></font>,其他<font color='blue'><b>{2}</b></font>,标本规范化固定率<font color='blue'><b>{3}% </b></font>,总袋数<font color='blue'><b>{4} </b></font>袋。", 0, 0, 0, 0, 0);
                Frm_TJInfo("提示", "不存在满足当前条件的标本信息！");
            }

        }



        private void Frm_BbQuery_Load(object sender, EventArgs e)
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
            //检查类型
            DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
            DataTable dt = exam_type_ins.GetTjAllDTBigExam_Type(Program.workstation_type_db);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.cmbJclx.DataSource = dt;
                cmbJclx.DisplayMember = "big_type_name";
                cmbJclx.ValueMember = "big_type_code";
            }
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
        }
        //设置标本不规范化标记 
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
                        DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                        int result = ins.UpdateBbGfh(id, 0);
                    }
                    Frm_TJInfo("提示", "标本不规范化固定设置成功！");
                    buttonX1.PerformClick();
                }
            }
        }
        //
        private void superGridControl1_GetRowHeaderStyle(object sender, GridGetRowHeaderStyleEventArgs e)
        {
            try
            {
                if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                {
                    //是否规范化固定
                    string bbgfh_flag = ((GridRow)e.GridRow).Cells["bbgfh_flag"].Value.ToString();
                    if (bbgfh_flag.Equals("0"))
                    {
                        e.GridRow.RowHeaderText = "不";
                        e.Style.TextColor = Color.Blue;
                    }
                }

            }
            catch
            {

            }
        }
        //
        //计数
        public void tj_func(DataTable dt)
        {

            int rows = dt.Rows.Count;
            int gfh_int = 0;
            int packCount = 0;
            for (int i = 0; i < rows; i++)
            {
                //是否规范化
                string bbgfh_flag = dt.Rows[i]["bbgfh_flag"].ToString();
                if (bbgfh_flag.Equals("1"))
                {
                    gfh_int += 1;
                }
                //是否添加每行的袋数
                packCount += Convert.ToInt32(dt.Rows[i]["pack_order"].ToString());

                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    float yxl = 0;
                    if (rows != 0)
                    {
                        yxl = (gfh_int * 100 / rows);
                    }
                    string yxlstr = Convert.ToDouble(yxl).ToString("0.00");
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font>个检查 :规范化<font color='blue'><b>{1}</b></font>,其他<font color='blue'><b>{2}</b></font>,标本规范化固定率<font color='blue'><b>{3}% </b></font>,总袋数<font color='blue'><b>{4} </b></font>袋。", rows, gfh_int, (rows - gfh_int), yxlstr, packCount);
                }
            }
        }
        //更新材块存放位置
        private void btnLKwz_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                if (!textBoxX1.Text.Trim().Equals(""))
                {
                    SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                    if (col.Count > 0)
                    {
                        for (int i = 0; i < col.Count; i++)
                        {
                            GridRow Row = col[i] as GridRow;
                            //
                            string exam_no = Row.Cells["exam_no"].Value.ToString();
                            DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                            ins.Update_Specimens_Location(textBoxX1.Text.Trim(), exam_no);
                        }
                        Frm_TJInfo("提示", "存放位置更新成功！");
                        buttonX1.PerformClick();
                    }
                }
            }
        }




    }
}
