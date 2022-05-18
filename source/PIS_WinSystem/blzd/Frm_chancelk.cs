using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_chancelk : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string SQD = "";
        public string barcode = "";
        public string parts = "";
        public int id = 0;
        public Frm_chancelk()
        {
            InitializeComponent();
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
        }

        private void superGridControl1_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            if (e.GridRow.GridIndex == -1)
            {
                return;
            }

            DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
            id = Convert.ToInt32(dt.Rows[e.GridRow.GridIndex]["id"].ToString());
            barcode = Convert.ToString(dt.Rows[e.GridRow.GridIndex]["barcode"].ToString());
            parts = Convert.ToString(dt.Rows[e.GridRow.GridIndex]["parts"].ToString());

            DialogResult = DialogResult.OK;
        }

        private void Frm_chancelk_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            //是否存在取材信息
            DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
            int sl = ins.GetMeterialsCount(BLH);
            if (sl == 0)
            {
                //不存在则插入一条 Insert_draw_meterials
                DBHelper.BLL.exam_specimens insSpec = new DBHelper.BLL.exam_specimens();
                DataTable dt = insSpec.GetQCSpecimensInfo(SQD);
                if (dt != null && dt.Rows.Count > 0)
                {
                    ins.Insert_draw_meterials(BLH, Convert.ToInt32(dt.Rows[0]["id"].ToString()), dt.Rows[0]["parts"].ToString(), Program.User_Name);
                }
            }
            RefreshData();
        }
        private void RefreshData()
        {
            //展示当前病理号下的所有取材信息
            DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
            DataTable dt = ins.GetDtHdMeterials(BLH);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }

        }
    }
}
