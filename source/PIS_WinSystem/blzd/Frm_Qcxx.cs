using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;

namespace PIS_Sys.blzd
{
    public partial class Frm_Qcxx : DevComponents.DotNetBar.Office2007Form
    {
        public static string BLH = "";
        public static string sqd = "";
        public static Boolean Refresh_Flag = false;
        public Frm_Qcxx()
        {
            InitializeComponent();
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }

        }

        private void Frm_Qcxx_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
            GridPanel panel1 = superGridControl2.PrimaryGrid;
            panel1.MinRowHeight = 30;
            panel1.AutoGenerateColumns = true;
            superGridControl2.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            }
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            RefreshData(Refresh_Flag);
        }

        public void RefreshData(Boolean CurFlag = false)
        {
            //展示当前病理号下的所有取材信息
            DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
            DataTable dt = ins.GetDtHdMeterials(BLH);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                tj_func(dt, CurFlag);
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
            //大体描述
            DBHelper.BLL.exam_specimens insSpe = new DBHelper.BLL.exam_specimens();
            DataTable dtdtms = insSpe.GetDtmsInfo(sqd);
            if (dtdtms != null && dtdtms.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dtdtms;
            }
            else
            {
                superGridControl2.PrimaryGrid.DataSource = null;
            }
        }
        //计数
        public void tj_func(DataTable dt, Boolean CurFlag = false)
        {

            if (CurFlag)
            {
                string doctor_qc_name = "";
                string doctor_qc_date = "";
                string qcdocinfo = "";
                DBHelper.BLL.exam_specimens ins = new DBHelper.BLL.exam_specimens();
                string rec_doc = ins.GetSpecimensRecord_doctor_name(sqd);
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                if (insM.GetQuCaiInfo(ref doctor_qc_name, ref doctor_qc_date, BLH))
                {
                    qcdocinfo = string.Format("取材核对：{0}  取材核对时间：{1} 大体描述录入人：{2}", doctor_qc_name, doctor_qc_date, rec_doc);
                }
                if (superGridControl1.PrimaryGrid.Header == null)
                {
                    superGridControl1.PrimaryGrid.Header = new GridHeader();
                }
                superGridControl1.PrimaryGrid.Header.Text = String.Format("<font color='Green'><b>{0}</b></font>。<font color='Blue'><b>{1}</b></font>", "当前取材已经核对完毕！", qcdocinfo);
            }
            int dbb = 0;
            int xbb = 0;
            int bdbb = 0;
            int ckzs = 0;
            int rows = dt.Rows.Count;
            for (int i = 0; i < rows; i++)
            {
                //材块总数
                ckzs += Convert.ToInt32(dt.Rows[i]["group_num"]);
                //标本类型
                string specimens_class = dt.Rows[i]["specimens_class"].ToString();

                switch (specimens_class)
                {
                    case "大标本":
                        dbb += 1;
                        break;
                    case "小标本":
                        xbb += 1;
                        break;
                    case "冰冻标本":
                        bdbb += 1;
                        break;
                    default:
                        break;
                }
                //最后一行开始赋值
                if (i == rows - 1)
                {
                    if (superGridControl1.PrimaryGrid.Footer == null)
                    {
                        superGridControl1.PrimaryGrid.Footer = new GridFooter();
                    }
                    superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='blue'><b>{0}</b></font> 条记录:大标本(<font color='blue'><b>{1}</b></font>),小标本(<font color='blue'><b>{2}</b></font>),冰冻标本(<font color='blue'><b>{3}</b></font>).腊块总数(<font color='blue'><b>{4}</b></font>),材块总数(<font color='blue'><b>{5}</b></font>)", rows, dbb, xbb, bdbb, rows, ckzs);
                }
            }
        }

        private void superGridControl1_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            //更新当前行备注
            GridCell cell = e.GridCell;
            if (cell == null)
            {
                return;
            }
            GridRow row = cell.GridRow;
            if (row == null)
            {
                return;
            }
            if (row.Index == -1)
            {
                return;
            }
            int id = Convert.ToInt32(row.Cells["id"].Value);
            if (cell.GridColumn.Name.Equals("parts") == true)
            {
                if (e.NewValue != e.OldValue)
                {
                    //更新原先这条取材的材块数目
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    ins.UpdateParts(id, e.NewValue.ToString());
                }
            }
            //备注信息
            if (cell.GridColumn.Name.Equals("memo_note") == true)
            {
                if (e.NewValue != e.OldValue)
                {
                    //更新原先这条取材的备注信息
                    DBHelper.BLL.exam_draw_meterials ins = new DBHelper.BLL.exam_draw_meterials();
                    ins.UpdateMemo_note(id, e.NewValue.ToString());
                }
            }
            e.GridCell.InvalidateLayout();
        }

        private void Frm_Qcxx_Activated(object sender, EventArgs e)
        {
            RefreshData(Refresh_Flag);
        }
    }
}
