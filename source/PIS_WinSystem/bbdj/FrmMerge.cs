using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.bbdj
{
    public partial class FrmMerge : DevComponents.DotNetBar.Office2007Form
    {
        public string Pat_id;
        public string Exam_no;
        public string source_exam_no;
        public string source_study_no;
        public string Ice_flag;
        public FrmMerge()
        {
            InitializeComponent();
        }

        private void FrmMerge_Load(object sender, EventArgs e)
        {
            try
            {
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                superGridControl1.PrimaryGrid.DataSource = ins.GetDtSameExam(Exam_no, Pat_id);
            }
            catch
            {

            }
        }

        private void superGridControl1_RowDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridRowDoubleClickEventArgs e)
        {
            if (e.GridRow.GridIndex == -1)
            {
                return;
            }
            DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
            source_exam_no = dt.Rows[e.GridRow.GridIndex]["exam_no"].ToString();
            source_study_no = dt.Rows[e.GridRow.GridIndex]["study_no"].ToString();

            if (Ice_flag.Equals("是"))
            {
                if (dt.Rows[e.GridRow.GridIndex]["ice_flag"].ToString().Equals("0"))
                {
                    UpdateIceFlag(source_exam_no, "1");
                }
            }
            DialogResult = DialogResult.OK;
        }
        private void UpdateIceFlag(string examno, string ice_str)
        {
            DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
            ins.UpdateIce_Flag(examno, ice_str);
        }
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.Rows.Count > 0)
            {
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                StringBuilder sb = new StringBuilder();
                if (col.Count == 1)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow row = (GridRow)col[i];
                        source_exam_no = row.Cells["exam_no"].Value.ToString();
                        source_study_no = row.Cells["study_no"].Value.ToString();
                        if (Ice_flag.Equals("是"))
                        {
                            if (row.Cells["ice_flag"].Value.ToString().Equals("0"))
                            {
                                UpdateIceFlag(source_exam_no, "1");
                            }
                        }
                    }
                }
                else
                {
                    Frm_TJInfo("请选定一个检查！", "当前点击合并按钮，表明要合并；但现在您还没有选择一个要合并的记录！");
                    return;
                }
            }
            DialogResult = DialogResult.OK;
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
        private void buttonX2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }




    }
}
