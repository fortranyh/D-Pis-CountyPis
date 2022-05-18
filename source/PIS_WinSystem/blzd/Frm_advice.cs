using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_advice : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_advice()
        {
            InitializeComponent();
        }
        public string study_no = "";
        public string Y_DocName = "";
        public string Y_DocCode = "";
        public DateTime Y_Time = DateTime.Now;
        public string Y_Zd = "";
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
        //保存
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "修改意见不能为空！");
                return;
            }
            DBHelper.BLL.report_xg_advice ins = new DBHelper.BLL.report_xg_advice();
            StringBuilder sb = new StringBuilder();
            sb.Append("建议修改诊断内容：");
            sb.AppendLine(richTextBoxEx1.Text.Trim());
            Boolean flag = ins.InsertAdvice(study_no, sb.ToString(), Program.User_Code, Program.User_Name);
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
        //修改
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx1.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "修改意见不能为空！");
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
                    DBHelper.BLL.report_xg_advice ins = new DBHelper.BLL.report_xg_advice();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("建议修改诊断内容：");
                    sb.AppendLine(richTextBoxEx1.Text.Trim().Replace("建议修改诊断内容：", ""));
                    Boolean flag = ins.modityAdvice(id, sb.ToString());
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
                    Frm_TJInfo("提示", "非本人修改意见不能更新！");
                }
            }
        }
        //删除
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

                    if (TaskDialog.Show("删除修改意见", "确认", "确定要删除选中的这条修改意见么？", Curbutton) == eTaskDialogResult.Ok)
                    {
                        DBHelper.BLL.report_xg_advice ins = new DBHelper.BLL.report_xg_advice();
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
                    Frm_TJInfo("提示", "非本人修改意见不能删除！");
                }
            }
        }
        public void loadData()
        {
            DBHelper.BLL.report_xg_advice ins = new DBHelper.BLL.report_xg_advice();
            //查询是否有原有诊断内容
            int Icount = ins.GetXgAdviceCount(study_no, Y_Time);
            if (Icount == 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("原始诊断内容：");
                sb.AppendLine(Y_Zd);
                Boolean flag = ins.InsertYAdvice(study_no, sb.ToString(), Y_DocCode, Y_DocName, Y_Time);
            }
            DataTable dt = ins.GetXgAdvice(study_no);
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
        private void Frm_advice_Load(object sender, EventArgs e)
        {
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
        int id = 0;
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

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            SelectedOldId(id);
        }

    }
}
