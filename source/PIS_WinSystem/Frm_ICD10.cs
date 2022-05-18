using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_ICD10 : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ICD10()
        {
            InitializeComponent();
        }
        public string icd_code = "";
        private void Frm_ICD10_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.icd10dict ins = new DBHelper.BLL.icd10dict();
            DataSet ds = ins.GetDsICD10TypeDict();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                this.comboBoxEx1.DataSource = ds.Tables[0];
                comboBoxEx1.DisplayMember = "big_type_name";
                comboBoxEx1.ValueMember = "big_type_name";
            }
            else
            {
                this.comboBoxEx1.DataSource = null;
            }

            textBoxX1.Select();
            textBoxX1.Focus();
        }

        private void textBoxX1_TextChanged(object sender, EventArgs e)
        {
            string py = textBoxX1.Text.Trim();
            DBHelper.BLL.icd10dict ins = new DBHelper.BLL.icd10dict();
            DataSet ds = ins.GetDsICD10Py(py);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = ds.Tables[0];
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
        }

        private void textBoxX1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
                if (this.superGridControl1.PrimaryGrid.SelectedRowCount == 0)
                {
                    if (this.superGridControl1.PrimaryGrid.Rows.Count > 0)
                    {
                        GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.Rows[0];
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
                            GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.Rows[iIndex - 1];
                            this.superGridControl1.PrimaryGrid.SetSelectedRows(iIndex - 1, 1, true);
                            this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                        }
                    }
                    else
                    {
                        if (iIndex <= this.superGridControl1.PrimaryGrid.Rows.Count - 2)
                        {
                            this.superGridControl1.PrimaryGrid.ClearSelectedRows();
                            this.superGridControl1.PrimaryGrid.ClearSelectedCells();
                            GridContainer row = (GridContainer)this.superGridControl1.PrimaryGrid.Rows[iIndex + 1];
                            this.superGridControl1.PrimaryGrid.SetSelectedRows(iIndex + 1, 1, true);
                            this.superGridControl1.PrimaryGrid.SetActiveRow(row);
                        }
                    }
                }

            }
            if (e.KeyCode == Keys.Enter)
            {
                if (this.superGridControl1.PrimaryGrid.Rows.Count > 0)
                {
                    int index = superGridControl1.ActiveRow.Index;

                    icd_code = ((GridRow)superGridControl1.PrimaryGrid.Rows[index]).Cells["icd_code"].Value.ToString();
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
            icd_code = ((GridRow)superGridControl1.PrimaryGrid.Rows[index]).Cells["icd_code"].Value.ToString();
            DialogResult = DialogResult.OK;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string py = textBoxX1.Text.Trim();
            DBHelper.BLL.icd10dict ins = new DBHelper.BLL.icd10dict();
            DataSet ds = ins.GetDsICD10Type(this.comboBoxEx1.Text);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = ds.Tables[0];
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
        }
    }
}
