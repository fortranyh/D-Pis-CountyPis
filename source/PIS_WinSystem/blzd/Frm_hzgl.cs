using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_hzgl : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_hzgl()
        {
            InitializeComponent();
        }
        public string study_no = "";
        public string txt_zdyj = "";
        public string txt_bl_czys = "";
        private void Frm_hzgl_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.huizhen_info inshz = new DBHelper.BLL.huizhen_info();
            DataTable dthz = inshz.GetHzinfo(study_no);
            if (dthz != null && dthz.Rows.Count > 0)
            {
                textBoxX1.Text = dthz.Rows[0]["doc_name"].ToString();
                report_bl_datetime.Text = dthz.Rows[0]["create_dt"].ToString();
                richTextBoxEx3.Text = dthz.Rows[0]["chry"].ToString();
                richTextBoxEx2.Text = dthz.Rows[0]["zjyj"].ToString();
                richTextBoxEx3.ReadOnly = true;
                report_bl_datetime.Enabled = false;
                buttonX4.Enabled = false;
                buttonX5.Enabled = true;
            }
            else
            {
                textBoxX1.Text = Program.User_Name;
                buttonX5.Enabled = false;

            }
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            loadData();
        }
        public void loadData()
        {
            DBHelper.BLL.report_hz_advice ins = new DBHelper.BLL.report_hz_advice();
            DataTable dt = ins.GetHzAdvice(study_no);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
        }

        //选中指定id的记录
        private void SelectedOldId(int id)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                string cur_id = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["id"].Value.ToString();
                if (cur_id.Equals(id.ToString()))
                {
                    //先清空以前选中的所有行
                    this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                    this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                    //最前面的黑色三角号跟着移动
                    this.superGridControl1.PrimaryGrid.SetActiveRow((GridContainer)superGridControl1.PrimaryGrid.Rows[i]);
                    //选中当前行
                    superGridControl1.PrimaryGrid.SetSelectedRows(i, 1, true);
                    superGridControl1.PrimaryGrid.SetActiveRow((GridRow)superGridControl1.PrimaryGrid.Rows[i]);
                    break;
                }
            }
        }
        int id = 0;
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            SelectedOldId(id);
        }

        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //
                id = Convert.ToInt32(Row.Cells["id"].Value.ToString());
                //
                string doc_code = Row.Cells["doc_code"].Value.ToString();
                //
                if (doc_code.Equals(Program.User_Code))
                {
                    richTextBoxEx1.Text = Row.Cells["advice"].Value.ToString();
                }
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
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "会诊意见不能为空！");
                return;
            }
            DBHelper.BLL.report_hz_advice ins = new DBHelper.BLL.report_hz_advice();
            Boolean flag = ins.InsertAdvice(study_no, richTextBoxEx1.Text.Trim(), Program.User_Code, Program.User_Name);
            if (flag)
            {
                Frm_TJInfo("提示", "保存成功！");
                loadData();
            }
            else
            {
                Frm_TJInfo("提示", "保存失败！");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "会诊意见不能为空！");
                return;
            }
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //
                id = Convert.ToInt32(Row.Cells["id"].Value.ToString());
                //
                string doc_code = Row.Cells["doc_code"].Value.ToString();
                //
                if (doc_code.Equals(Program.User_Code))
                {
                    DBHelper.BLL.report_hz_advice ins = new DBHelper.BLL.report_hz_advice();
                    Boolean flag = ins.modityAdvice(id, richTextBoxEx1.Text.Trim());
                    if (flag)
                    {
                        Frm_TJInfo("提示", "修改成功！");
                        loadData();
                    }
                    else
                    {
                        Frm_TJInfo("提示", "修改失败！");
                    }
                }
                else
                {
                    Frm_TJInfo("提示", "非本人会诊意见不能更新！");
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                if (superGridControl1.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl1.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
                //
                id = Convert.ToInt32(Row.Cells["id"].Value.ToString());
                //
                string doc_code = Row.Cells["doc_code"].Value.ToString();
                //
                if (doc_code.Equals(Program.User_Code))
                {
                    eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                    TaskDialog.EnableGlass = false;

                    if (TaskDialog.Show("删除会诊意见", "确认", "确定要删除选中的这条会诊意见么？", Curbutton) == eTaskDialogResult.Ok)
                    {
                        DBHelper.BLL.report_hz_advice ins = new DBHelper.BLL.report_hz_advice();
                        Boolean flag = ins.DelAdvice(id);
                        if (flag)
                        {
                            Frm_TJInfo("提示", "删除成功！");
                            richTextBoxEx1.Text = "";
                            loadData();
                        }
                        else
                        {
                            Frm_TJInfo("提示", "删除失败！");
                        }
                    }
                }
                else
                {
                    Frm_TJInfo("提示", "非本人会诊意见不能删除！");
                }
            }
        }
        //保存专家意见
        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (!richTextBoxEx2.Text.Trim().Equals(""))
            {
                DBHelper.BLL.huizhen_info ins = new DBHelper.BLL.huizhen_info();
                if (ins.modityZjyj(study_no, richTextBoxEx2.Text.Trim()))
                {
                    Frm_TJInfo("提示", "保存专家意见成功！");
                }
            }
            else
            {
                Frm_TJInfo("提示", "执行此保存专家意见不能为空！");
            }
        }


        private void buttonX4_Click_1(object sender, EventArgs e)
        {
            if (richTextBoxEx3.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "参会人员不能为空！");
                return;
            }
            DBHelper.BLL.huizhen_info ins = new DBHelper.BLL.huizhen_info();
            if (ins.InsertHzInfo(study_no, richTextBoxEx3.Text.Trim(), richTextBoxEx2.Text.Trim(), Program.User_Code, Program.User_Name))
            {
                Frm_TJInfo("提示", "保存成功！");
                buttonX4.Enabled = false;
                buttonX5.Enabled = true;
                //把当前诊断写入科内会诊
                if (txt_bl_czys.Trim().Equals(Program.User_Name))
                {
                    DBHelper.BLL.report_hz_advice insKN = new DBHelper.BLL.report_hz_advice();
                    insKN.InsertAdvice(study_no, txt_zdyj, Program.User_Code, Program.User_Name);
                }
            }
            else
            {
                Frm_TJInfo("提示", "保存失败！");
            }
        }

    }
}
