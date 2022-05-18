using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys.bbdj
{
    public partial class Frm_dept : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_dept()
        {
            InitializeComponent();
        }
        public string Dept_Str = "";
        private void Frm_dept_Load(object sender, EventArgs e)
        {
            string py = textBoxX1.Text.Trim();
            DBHelper.BLL.dept_dict dept_dict_ins = new DBHelper.BLL.dept_dict();
            DataSet dsDept = dept_dict_ins.GetDsExam_Dept_Py(py);
            if (dsDept != null && dsDept.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dsDept.Tables[0];
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
            }

            textBoxX1.Select();
            textBoxX1.Focus();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {

            DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;

            if (dt != null)
            {
                if (string.IsNullOrEmpty(this.textBoxX1.Text.Trim()))
                    dt.DefaultView.RowFilter = string.Empty;
                else
                    dt.DefaultView.RowFilter = string.Format("dept_py like '%{0}%'", this.textBoxX1.Text.Replace("'", "''"));
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
                    Dept_Str = ((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["dept_name"].Value.ToString();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void superGridControl1_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            if (e.GridRow.GridIndex == -1)
            {
                return;
            }
            int index = superGridControl1.ActiveRow.Index;
            Dept_Str = ((GridRow)superGridControl1.PrimaryGrid.VirtualRows[index]).Cells["dept_name"].Value.ToString();
            DialogResult = DialogResult.OK;
        }
    }
}
