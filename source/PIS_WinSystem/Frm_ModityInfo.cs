using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_ModityInfo : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ModityInfo()
        {
            InitializeComponent();
            //窗体最大化
            this.CenterToScreen();
        }

        private void Frm_ModityInfo_Load(object sender, EventArgs e)
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
            //加载报告模板 
            DBHelper.BLL.exam_report insTemp = new DBHelper.BLL.exam_report();
            DataTable dtTemp = insTemp.GetReportTemp(Program.workstation_type_db);
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                comboBox1.DataSource = dtTemp;
                comboBox1.DisplayMember = "temp_name";
                comboBox1.ValueMember = "temp_index";
            }
            //检查类型 comboBox2
            DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
            DataTable dt = exam_type_ins.GetAllDTExam_Type();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbExam_type.DataSource = dt;
                cmbExam_type.DisplayMember = "modality_cn";
                cmbExam_type.ValueMember = "modality";
            }


        }
        public void QueryData()
        {

            string BLH = Txtblh.Text.Trim();

            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetModityList(BLH);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = dt;
                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.DataSource = null;
                Frm_TJInfo("提示", "不存在满足当前条件的报告信息！");
            }


        }

        //选中指定exam_no的记录
        private void SelectedOldExam_NO(string exam_no)
        {
            for (int i = 0; i < superGridControl1.PrimaryGrid.Rows.Count; i++)
            {
                string cur_studyno = ((GridRow)superGridControl1.PrimaryGrid.Rows[i]).Cells["exam_no"].Value.ToString();
                if (cur_studyno.Equals(exam_no))
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
            QueryData();
        }
        string exam_no = "";
        string study_no = "";
        string modality = "";
        int Report_Index = 0;
        string Oldexam_no = "";
        private void superGridControl1_SelectionChanged(object sender, GridEventArgs e)
        {
            if (e.GridPanel.ActiveRow == null)
            {
                superGridControl1.PrimaryGrid.Rows.Clear();
                superGridControl1.PrimaryGrid.InvalidateLayout();
                return;
            }
            int index = e.GridPanel.ActiveRow.Index;
            if (index == -1)
            {
                return;
            }
            GridRow Row = (GridRow)superGridControl1.PrimaryGrid.Rows[index];
            //申请单号
            exam_no = Row.Cells["exam_no"].Value.ToString();
            Oldexam_no = exam_no;
            //病理号
            study_no = Row.Cells["study_no"].Value.ToString();
            Txtblh.Text = study_no;
            //
            modality = Row.Cells["modality"].Value.ToString();
            for (int j = 0; j < cmbExam_type.Items.Count; j++)
            {
                cmbExam_type.SelectedIndex = j;
                if (cmbExam_type.SelectedValue.ToString() == modality)
                {
                    break;
                }
            }
            textBox1.Text = study_no;
            //查询报告模板
            DBHelper.BLL.exam_report insReport = new DBHelper.BLL.exam_report();
            int Report_Count = insReport.GetReportCount(study_no);
            if (Report_Count == 1)
            {
                buttonX2.Enabled = true;
                Report_Index = insReport.GetReportTemletIndex(study_no);
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    comboBox1.SelectedIndex = i;
                    if (comboBox1.SelectedValue.ToString() == Report_Index.ToString())
                    {
                        break;
                    }
                }

            }
            else
            {
                buttonX2.Enabled = false;
            }
        }
        //修改检查类型
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (study_no != "")
            {
                //先获取下当前的类型
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                //
                string mad = cmbExam_type.SelectedValue.ToString();
                if (mad != modality)
                {
                    if (TaskDialog.Show("修改检查类型", "确认", "确定要修改检查类型麽？", Curbutton) == eTaskDialogResult.Ok)
                    {
                        //取大类
                        DBHelper.BLL.exam_type insType = new DBHelper.BLL.exam_type();
                        string big_type = insType.GetBigType(mad);
                        DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                        if ((ins.UpdateModalityType(mad, big_type, exam_no) == 1))
                        {
                            Frm_TJInfo("提示", "修改成功！");
                            QueryData();
                        }
                    }
                }
            }
        }
        //修改病理号
        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Trim() != "" && study_no != "" && study_no != this.textBox1.Text.Trim())
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("修改病理号", "确认", "确定要修改此病理号麽？", Curbutton) == eTaskDialogResult.Ok)
                {
                    //
                    DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                    if (ins.GetStudyNoCount(textBox1.Text.Trim()) == 0)
                    {
                        if (ins.UpdateStudyNo(this.textBox1.Text.Trim(), exam_no) == 1)
                        {
                            Frm_TJInfo("提示", "修改成功！");
                            buttonX1.PerformClick();
                        }
                        else
                        {
                            Frm_TJInfo("提示", "修改失败！");

                        }
                    }
                    else
                    {
                        Frm_TJInfo("提示", "病理号已经存在！");
                    }
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            //修改模板
            if (study_no != "")
            {
                DBHelper.BLL.exam_master ins = new DBHelper.BLL.exam_master();
                int status = ins.GetStudyExam_Status(study_no);
                if (status >= 30)
                {
                    //执行更新
                    if (comboBox1.SelectedValue.ToString() != Report_Index.ToString())
                    {
                        eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                        TaskDialog.EnableGlass = false;
                        if (TaskDialog.Show("修改模板", "确认", "确定要修改此检查的报告模板麽？", Curbutton) == eTaskDialogResult.Ok)
                        {
                            DBHelper.BLL.exam_report insRe = new DBHelper.BLL.exam_report();
                            if (insRe.UpdateReportIndex(study_no, Convert.ToInt16(comboBox1.SelectedValue), "30"))
                            {
                                Frm_TJInfo("提示", "修改成功！");
                            }
                        }
                    }
                }
            }
        }
        //选中先前那条数据
        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            if (!Oldexam_no.Equals(""))
            {
                SelectedOldExam_NO(Oldexam_no);
            }
        }
        //删除图像找回/采错图像修正
        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (Txtblh.Text.Trim().Equals(""))
            {
                Frm_TJInfo("提示", "请先选中有图像的检查！");
            }
            else
            {
                Frm_ZhImage ins = new Frm_ZhImage();
                ins.study_no = Txtblh.Text.Trim();
                ins.BringToFront();
                ins.ShowDialog();
            }
        }

        private void superGridControl1_MouseHover(object sender, EventArgs e)
        {
            superGridControl1.Focus();
        }


    }
}
