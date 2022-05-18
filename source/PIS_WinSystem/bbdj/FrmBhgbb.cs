using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys.bbdj
{
    public partial class FrmBhgbb : DevComponents.DotNetBar.Office2007Form
    {
        public string dept_name;
        public string doctor_name;
        public string study_no;
        public string exam_no;
        public string special_name;
        public FrmBbdj frmIns;
        public string patient_id;
        public string input_id;
        public FrmBhgbb()
        {
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (this.richTextBoxEx1.Text.Trim() != "")
            {
                DBHelper.Model.exam_specimens_qualified ins = new DBHelper.Model.exam_specimens_qualified();
                ins.exam_no = exam_no;
                ins.specimens_name = this.textBoxX2.Text.Trim();
                ins.qualified_info = this.richTextBoxEx1.Text.Trim();
                ins.note_doctor = Program.User_Name;
                ins.doctor_code = Program.User_Code;
                ins.doctor_name = txt_sjys.Text.Trim();
                ins.dept_name = txt_sjks.Text.Trim();
                ins.study_no = txt_study_no.Text.Trim();
                ins.patient_id = textBoxX3.Text.Trim();
                ins.input_id = textBoxX4.Text.Trim();
                DBHelper.BLL.exam_specimens_qualified ProIns = new DBHelper.BLL.exam_specimens_qualified();
                string result_str = "";
                if (ProIns.Process_Specimens_qualified(ins, ref result_str))
                {
                    frmIns.Frm_TJInfo("成功", "不合格信息登记成功！");
                    RefreshData();
                    //是否启用接口服务
                    if (Program.Interface_SetInfo != null)
                    {
                        if (Program.Interface_SetInfo.enable_flag == 1)
                        {
                            //已经打印是否调用接口服务
                            if (Program.Interface_SetInfo.bhg_flag == 1)
                            {
                                ClientSCS.SynchronizedClient("bhg|" + exam_no + "|" + this.textBoxX2.Text.Trim() + "|" + this.richTextBoxEx1.Text.Trim());
                            }
                        }
                    }
                }
                else
                {
                    frmIns.Frm_TJInfo("失败", string.Format("错误信息为：\n{0}", result_str));
                }
            }
            else
            {
                frmIns.Frm_TJInfo("提示", "请输入不合格原因！");
                return;

            }
        }

        private void FrmBhgbb_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
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
            frmIns = this.Owner as FrmBbdj;

            this.textBoxX2.Text = special_name;
            txt_study_no.Text = study_no;
            txt_sjks.Text = dept_name;
            txt_sjys.Text = doctor_name;
            textBoxX3.Text = patient_id;
            textBoxX4.Text = input_id;
            this.textBoxX1.Text = Program.User_Name;
            RefreshData();

        }

        public void RefreshData()
        {
            //加载已经存在的不合格
            DataSet _DataSet = new DataSet();
            DBHelper.BLL.exam_specimens_qualified ins = new DBHelper.BLL.exam_specimens_qualified();
            _DataSet = ins.GetDsSpecimens_qualified(exam_no);
            if (_DataSet != null && _DataSet.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = _DataSet;
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
            }
        }
    }
}
