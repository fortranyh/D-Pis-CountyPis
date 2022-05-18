using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.bbdj
{
    public partial class Frm_ExamItem : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ExamItem()
        {
            InitializeComponent();
        }

        private string result_str = "";
        private float costs = 0;
        private string exam_no = "";
        public float GetCosts()
        {
            return costs;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Frm_ExamItem_Load(object sender, EventArgs e)
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
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
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
            //列表样式
            superGridControl1.PrimaryGrid.SelectionGranularity = SelectionGranularity.Row;
            DBHelper.BLL.charge_dict item_dict_ins = new DBHelper.BLL.charge_dict();
            DataSet dsItem = item_dict_ins.GetDsCharge_dict();
            if (dsItem != null && dsItem.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dsItem.Tables[0];
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
            }
            this.superGridControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.superGridControl1_KeyDown);
            this.comboBox1.SelectedIndex = 0;
            textBoxX3.Select();
            textBoxX3.Focus();

        }

        private void superGridControl1_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            if (e.GridRow.GridIndex == -1)
            {
                return;
            }
            DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
            result_str = dt.Rows[e.GridRow.GridIndex]["charge_code"].ToString();
            textBoxX2.Text = result_str;
            costs = Convert.ToSingle(dt.Rows[e.GridRow.GridIndex]["costs"]);
            DialogResult = DialogResult.OK;
        }

        private void superGridControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (this.superGridControl1.PrimaryGrid.SelectedRowCount == 0)
                {
                    if (this.superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                    {
                        GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.VirtualRows[0];
                        this.superGridControl1.PrimaryGrid.SetSelectedRows(0, 1, true);
                        this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                    }
                }
                else
                {
                    int iIndex = this.superGridControl1.PrimaryGrid.ActiveRow.Index;
                    if (e.KeyCode == Keys.Up)
                    {
                        if (iIndex >= 1)
                        {
                            this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                            this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                            GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.VirtualRows[iIndex - 1];
                            this.superGridControl1.PrimaryGrid.SetSelectedRows(iIndex - 1, 1, true);
                            this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                        }
                    }
                    else
                    {
                        if (iIndex <= this.superGridControl1.PrimaryGrid.VirtualRowCount - 2)
                        {
                            this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                            this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                            GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.VirtualRows[iIndex + 1];
                            this.superGridControl1.PrimaryGrid.SetSelectedRows(iIndex + 1, 1, true);
                            this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                        }
                    }
                }

            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                {
                    int index = superGridControl1.ActiveRow.Index;
                    result_str = ((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["charge_code"].Value.ToString();
                    textBoxX2.Text = result_str;
                    costs = Convert.ToSingle(((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["costs"].Value);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;

            if (dt != null)
            {
                if (string.IsNullOrEmpty(this.textBoxX1.Text.Trim()))
                    dt.DefaultView.RowFilter = string.Empty;
                else
                    dt.DefaultView.RowFilter = string.Format("py_code like '%{0}%'", this.textBoxX1.Text.Replace("'", "''"));
            }
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (this.superGridControl1.PrimaryGrid.SelectedRowCount == 0)
                {
                    if (this.superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                    {
                        GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.VirtualRows[0];
                        this.superGridControl1.PrimaryGrid.SetSelectedRows(0, 1, true);
                        this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                    }
                }
                else
                {
                    int iIndex = this.superGridControl1.PrimaryGrid.ActiveRow.Index;
                    if (e.KeyCode == Keys.Up)
                    {
                        if (iIndex >= 1)
                        {
                            this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                            this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                            GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.VirtualRows[iIndex - 1];
                            this.superGridControl1.PrimaryGrid.SetSelectedRows(iIndex - 1, 1, true);
                            this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                        }
                    }
                    else
                    {
                        if (iIndex <= this.superGridControl1.PrimaryGrid.VirtualRowCount - 2)
                        {
                            this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                            this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                            GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.VirtualRows[iIndex + 1];
                            this.superGridControl1.PrimaryGrid.SetSelectedRows(iIndex + 1, 1, true);
                            this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                        }
                    }
                }

            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                {
                    int index = superGridControl1.ActiveRow.Index;
                    result_str = ((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["charge_code"].Value.ToString();
                    textBoxX2.Text = result_str;
                    costs = Convert.ToSingle(((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["costs"].Value);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        //计费
        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            if (!exam_no.Equals("") && !result_str.Equals(""))
            {
                DBHelper.BLL.exam_charge ins = new DBHelper.BLL.exam_charge();
                Boolean zxresult = ins.Process_exam_charge(result_str, exam_no, Program.User_Code, Program.User_Name, this.integerInput1.Value);
                if (zxresult)
                {
                    //刷新费用
                    refreshFy();
                }

            }
            else
            {
                Frm_TJInfo("提示", "请输入病人信息和选择计费项目！");
            }


        }
        //退费
        private void buttonX2_Click_1(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.VirtualRowCount > 0)
            {
                if (superGridControl2.ActiveRow == null)
                {
                    return;
                }
                int index = superGridControl2.ActiveRow.Index;
                if (index == -1)
                {
                    return;
                }
                GridRow Row = (GridRow)superGridControl2.PrimaryGrid.VirtualRows[index];
                //
                int id = Convert.ToInt32(Row.Cells["id"].Value);
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("执行退费", "确认", "确定要退除当前选定的计费么？", Curbutton) == eTaskDialogResult.Ok)
                {

                    DBHelper.BLL.exam_charge ins = new DBHelper.BLL.exam_charge();
                    int col = ins.DelChargesFromId(id);
                    if (col == 1)
                    {
                        //刷新费用
                        refreshFy();
                    }
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

        private void textBoxX3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!textBoxX3.Text.Trim().Equals(""))
                {
                    //获取病人信息
                    DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                    DataTable dt = ins.GetPatInfo(this.comboBox1.SelectedIndex, textBoxX3.Text.Trim(), ConfigurationManager.AppSettings["w_big_type_db"]);
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        textBoxX4.Text = dt.Rows[0]["patient_name"].ToString();
                        textBoxX5.Text = dt.Rows[0]["sex"].ToString();
                        textBoxX6.Text = dt.Rows[0]["patient_source"].ToString();
                        exam_no = dt.Rows[0]["exam_no"].ToString();
                    }
                    else
                    {
                        Frm_TJInfo("提示", "输入病人条件没有查询到病人。\n请确认后重新输入！");
                        return;
                    }
                    //刷新费用
                    refreshFy();
                    //
                    textBoxX1.Select();
                    textBoxX1.Focus();

                }
                else
                {
                    Frm_TJInfo("提示", "请输入病人条件！");
                }
            }
        }
        private void refreshFy()
        {
            //获取费用信息
            if (!exam_no.Equals(""))
            {
                DBHelper.BLL.exam_charge insc = new DBHelper.BLL.exam_charge();
                DataTable dtc = insc.GetFyInfo(exam_no);
                if (dtc != null && dtc.Rows.Count > 0)
                {
                    superGridControl2.PrimaryGrid.DataSource = dtc;
                    tj_func(dtc);
                }
                else
                {
                    superGridControl2.PrimaryGrid.VirtualRowCount = 0;
                    superGridControl2.PrimaryGrid.DataSource = null;
                    if (superGridControl2.PrimaryGrid.Footer == null)
                    {
                        superGridControl2.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl2.PrimaryGrid.Footer.Text = String.Format("当前病人已经收费总额为(<font color='blue'><b>{0}</b></font>)", "0");
                }

            }

        }
        //计数
        public void tj_func(DataTable dt)
        {
            float Curcosts = 0;
            int rows = dt.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                //总费用
                Curcosts += Convert.ToSingle(dt.Rows[i]["costs"]);

                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl2.PrimaryGrid.Footer == null)
                    {
                        superGridControl2.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl2.PrimaryGrid.Footer.Text = String.Format("当前病人已经收费总额为(<font color='blue'><b>{0}</b></font>)", Curcosts.ToString());
                }
            }
        }

        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {
            if (e.GridPanel.ActiveRow == null)
            {
                superGridControl1.PrimaryGrid.VirtualRows.Clear();
                superGridControl1.PrimaryGrid.InvalidateLayout();
                return;
            }
            int index = e.GridPanel.ActiveRow.Index;
            if (index == -1)
            {
                return;
            }
            GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
            result_str = ((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["charge_code"].Value.ToString();
            textBoxX2.Text = result_str;
        }



    }
}
