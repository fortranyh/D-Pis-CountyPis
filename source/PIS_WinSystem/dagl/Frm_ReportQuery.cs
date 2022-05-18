using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.dagl
{
    public partial class Frm_ReportQuery : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ReportQuery()
        {
            InitializeComponent();
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
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            StringBuilder sb = new StringBuilder();

            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", "").ToUpper());
            }
            else if (!textBox1.Text.Trim().Equals(""))
            {
                if (comboBox5.SelectedIndex == 0)
                {
                    sb.AppendFormat(" and input_id='{0}'", textBox1.Text.Trim().Replace("'", ""));
                }
                else if (comboBox5.SelectedIndex == 1)
                {
                    sb.AppendFormat(" and patient_id='{0}'", textBox1.Text.Trim().Replace("'", ""));
                }
                else if (comboBox5.SelectedIndex == 2)
                {
                    sb.AppendFormat(" and hospital_card='{0}'", textBox1.Text.Trim().Replace("'", ""));
                }
            }
            else
            {
                if (RadioButton1.Checked)
                {
                    sb.AppendFormat(" and received_datetime>='{0} 00:00:00' and received_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton2.Checked)
                {
                    sb.AppendFormat(" and cbreport_datetime>='{0} 00:00:00' and cbreport_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }
                else if (RadioButton3.Checked)
                {
                    sb.AppendFormat("and report_print_datetime>='{0} 00:00:00' and report_print_datetime<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                }

                if (!TxtName.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and patient_name like '%{0}%'", TxtName.Text.Trim().Replace("'", ""));
                }

                if (CmbBrly.SelectedIndex != 0)
                {
                    sb.AppendFormat(" and patient_source = '{0}'", CmbBrly.Text.Trim());
                }
                if (!txtms.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and rysj like '%{0}%'", txtms.Text.Trim().Replace("'", ""));
                }
                if (!Txtzd.Text.Trim().Equals(""))
                {
                    string[] values = Txtzd.Text.Trim().Replace("'", "").Split('@');
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (!values[i].Equals(""))
                        {
                            sb.Append(" and zdyj like '%");
                            sb.Append(values[i]);
                            sb.Append("%'");
                        }
                    }
                }
                if (!txtjy.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and zdpz like '%{0}%'", txtjy.Text.Trim().Replace("'", ""));
                }
                if (comboBox1.SelectedIndex != 0)
                {
                    sb.AppendFormat(" and sfyx='{0}'", comboBox1.Text.Trim());
                }
                if (comboBox2.SelectedIndex != 0)
                {
                    if (comboBox2.SelectedIndex == 1)
                    {
                        sb.AppendFormat(" and ice_flag={0}", 1);
                    }
                    else if (comboBox2.SelectedIndex == 2)
                    {
                        sb.AppendFormat(" and ks_flag={0}", 1);
                    }
                }
                if (comboBox3.SelectedIndex != 0)
                {
                    if (comboBox3.SelectedIndex == 1)
                    {
                        sb.AppendFormat(" and wj_liud={0}", 1);
                    }
                    else if (comboBox3.SelectedIndex == 2)
                    {
                        sb.AppendFormat(" and wj_liud={0}", 0);
                    }
                }
                if (comboBox4.SelectedIndex != 0)
                {
                    if (comboBox4.SelectedIndex == 1)
                    {
                        sb.AppendFormat(" and lcfh='{0}'", "符合");
                    }
                    else if (comboBox4.SelectedIndex == 2)
                    {
                        sb.AppendFormat(" and lcfh='{0}'", "不符合");
                    }
                }
                if (!comboBoxEx1.Text.Trim().Equals(""))
                {
                    sb.AppendFormat("  and cbreport_doc_name='{0}' ", comboBoxEx1.Text.Trim());
                }
                if (!comboBoxEx3.Text.Trim().Equals(""))
                {
                    sb.AppendFormat("  and zzreport_doc_name='{0}' ", comboBoxEx3.Text.Trim());
                }
                if (!comboBoxEx4.Text.Trim().Equals(""))
                {
                    sb.AppendFormat("  and shreport_doc_name='{0}' ", comboBoxEx4.Text.Trim());
                }
                if (!comboBoxEx2.Text.Trim().Equals(""))
                {
                    sb.AppendFormat("  and qucai_doctor_name='{0}' ", comboBoxEx2.Text.Trim());
                }
                if (!cmb_gpys.Text.Trim().Equals(""))
                {
                    sb.AppendFormat("  and report_gb_doc='{0}' ", cmb_gpys.Text.Trim());
                }
                if (!txt_part.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and parts like '%{0}%'", txt_part.Text.Trim().Replace("'", ""));
                }

                if (!txt_reqdoc.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and req_physician like '%{0}%'", txt_reqdoc.Text.Trim().Replace("'", ""));
                }

                if (!txt_reqdept.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and req_dept like '%{0}%'", txt_reqdept.Text.Trim().Replace("'", ""));
                }

                if (cmbBggsNr.SelectedIndex != 0)
                {
                    sb.AppendFormat(" and bgnr_gs_flag = '{0}'", cmbBggsNr.Text);
                }

                if (cmbBbqc.SelectedIndex != 0)
                {
                    sb.AppendFormat(" and bbqc_info = '{0}'", cmbBbqc.Text);
                }
                if (!comboBoxEx5.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and delay_reason like '%{0}%'", comboBoxEx5.Text.Trim());
                }
                if (!cmb_sjdw.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and submit_unit like '%{0}%'", cmb_sjdw.Text.Trim());
                }
            }

            if (Cmbjclx.SelectedIndex != 0)
            {
                sb.AppendFormat(" and modality = '{0}'", Cmbjclx.SelectedValue);
            }
            else
            {

                sb.AppendFormat(" and exam_type in ({0}) ", ConfigurationManager.AppSettings["w_big_type_db"]);

            }
            DataTable dt = new DataTable();
            DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
            dt = ins.GetReportList(sb.ToString());
            if (dt != null && dt.Rows.Count > 0)
            {
                if (checkBoxX1.Checked == true)
                {
                    DBHelper.BLL.exam_type TypeIns = new DBHelper.BLL.exam_type();
                    DataTable DtType = TypeIns.GetReportLimit(Program.workstation_type_db);
                    if (Cmbjclx.SelectedIndex == 0)
                    {
                        if (DtType != null && DtType.Rows.Count > 0)
                        {
                            DataTable dtt = new DataTable();
                            dtt = dt.Clone();//拷贝框架
                            for (int k = 0; k < DtType.Rows.Count; k++)
                            {
                                DataRow[] dr = dt.Select("modality='" + DtType.Rows[k]["modality"].ToString() + "' and sj>" + DtType.Rows[k]["report_limit"].ToString());
                                for (int i = 0; i < dr.Length; i++)
                                {
                                    dtt.ImportRow((DataRow)dr[i]);
                                }
                            }
                            superGridControl1.PrimaryGrid.DataSource = dtt;
                            if (dtt != null && dtt.Rows.Count > 0)
                            {
                                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", dtt.Rows.Count);
                            }
                            else
                            {
                                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                                superGridControl1.PrimaryGrid.DataSource = null;
                                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
                            }
                        }
                    }
                    else
                    {
                        DataTable dtt = new DataTable();
                        dtt = dt.Clone();//拷贝框架
                        string report_limit = "0";
                        for (int k = 0; k < DtType.Rows.Count; k++)
                        {
                            if (DtType.Rows[k]["modality"].ToString().Equals(Cmbjclx.SelectedValue))
                            {
                                report_limit = DtType.Rows[k]["report_limit"].ToString();
                                break;
                            }
                        }
                        DataRow[] dr = dt.Select("modality='" + Cmbjclx.SelectedValue + "' and sj>" + report_limit);
                        for (int i = 0; i < dr.Length; i++)
                        {
                            dtt.ImportRow((DataRow)dr[i]);
                        }
                        superGridControl1.PrimaryGrid.DataSource = dtt;
                        if (dtt != null && dtt.Rows.Count > 0)
                        {
                            superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", dtt.Rows.Count);
                        }
                        else
                        {
                            superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                            superGridControl1.PrimaryGrid.DataSource = null;
                            superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
                        }
                    }
                }
                else
                {
                    superGridControl1.PrimaryGrid.DataSource = dt;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", dt.Rows.Count);
                    }
                    else
                    {
                        superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                        superGridControl1.PrimaryGrid.DataSource = null;
                        superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
                    }
                }

                //激活列表
                superGridControl1.Select();
                superGridControl1.Focus();
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
                Frm_TJInfo("提示", "不存在满足当前条件的报告信息！");
                btn_hj.Enabled = false;
            }

        }

        private void Frm_ReportQuery_Load(object sender, EventArgs e)
        {
            cmbBbqc.SelectedIndex = 0;
            cmbBggsNr.SelectedIndex = 0;
            //送检单位
            DBHelper.BLL.doctor_dict sjdw_ins = new DBHelper.BLL.doctor_dict();
            DataTable dtdw = sjdw_ins.GetSjdwList();
            if (dtdw != null && dtdw.Rows.Count > 0)
            {
                this.cmb_sjdw.DataSource = dtdw;
                cmb_sjdw.DisplayMember = "name";
                cmb_sjdw.ValueMember = "id";
            }
            cmb_sjdw.SelectedIndex = -1;
            //绑定延时原因
            DBHelper.BLL.exam_delay_report insdelay = new DBHelper.BLL.exam_delay_report();
            DataTable dtyy = insdelay.GetYsyyData();
            if (dtyy != null && dtyy.Rows.Count > 0)
            {
                comboBoxEx5.Items.Add("");
                for (int i = 0; i < dtyy.Rows.Count; i++)
                {
                    comboBoxEx5.Items.Add(dtyy.Rows[i]["delay_ys"].ToString());
                }
            }
            comboBoxEx5.SelectedIndex = 0;
            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode=ColumnAutoSizeMode.DisplayedCells;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl1.PrimaryGrid;
            panel1.MinRowHeight = 30;
            //锁定前四列
            panel1.FrozenColumnCount = 4;
            panel1.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].ReadOnly = true;
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel1.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //绑定初步医生
            DBHelper.BLL.doctor_dict doctor_dict_ins = new DBHelper.BLL.doctor_dict();
            DataSet ds = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                comboBoxEx1.DataSource = ds.Tables[0];
                comboBoxEx1.DisplayMember = "user_name";
                comboBoxEx1.ValueMember = "user_code";

            }
            else
            {
                comboBoxEx1.DataSource = null;
            }
            //规培医师 
            cmb_gpys.Items.Add("");
            DataSet dsgb = doctor_dict_ins.GetDsGB_User(Program.Dept_Code);
            if (dsgb != null && dsgb.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsgb.Tables[0].Rows.Count; i++)
                {
                    cmb_gpys.Items.Add(dsgb.Tables[0].Rows[i]["user_name"].ToString());
                }
            }
            cmb_gpys.SelectedIndex = 0;
            //主治
            DataSet dszz = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (dszz != null && dszz.Tables[0].Rows.Count > 0)
            {
                comboBoxEx3.DataSource = dszz.Tables[0];
                comboBoxEx3.DisplayMember = "user_name";
                comboBoxEx3.ValueMember = "user_code";

            }
            else
            {
                comboBoxEx3.DataSource = null;
            }
            //审核
            DataSet dssh = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (dssh != null && dssh.Tables[0].Rows.Count > 0)
            {
                comboBoxEx4.DataSource = dssh.Tables[0];
                comboBoxEx4.DisplayMember = "user_name";
                comboBoxEx4.ValueMember = "user_code";

            }
            else
            {
                comboBoxEx4.DataSource = null;
            }

            //取材医生
            DataSet ds1 = doctor_dict_ins.GetDsExam_User(Program.Dept_Code);
            if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
            {
                comboBoxEx2.DataSource = ds1.Tables[0];
                comboBoxEx2.DisplayMember = "user_name";
                comboBoxEx2.ValueMember = "user_code";
                //comboBoxEx2.Text = Program.User_Name;
            }
            else
            {
                comboBoxEx2.DataSource = null;
            }

            //病人来源 
            DBHelper.BLL.patient_source patient_source_ins = new DBHelper.BLL.patient_source();
            DataTable dts = patient_source_ins.GetTjPatient_source();
            if (dts != null && dts.Rows.Count > 0)
            {
                this.CmbBrly.DataSource = dts;
                CmbBrly.DisplayMember = "source_name";
                CmbBrly.ValueMember = "id";
            }
            //检查类型
            DBHelper.BLL.exam_type exam_type_ins = new DBHelper.BLL.exam_type();
            DataTable dt = exam_type_ins.GetTjAllDTExam_Type(Program.workstation_type_db);
            if (dt != null && dt.Rows.Count > 0)
            {
                this.Cmbjclx.DataSource = dt;
                Cmbjclx.DisplayMember = "modality_cn";
                Cmbjclx.ValueMember = "modality";
            }
            //
            dtStart.Value = DateTime.Now;
            dtEnd.Value = DateTime.Now;
            //
            comboBox1.SelectedIndex = 0;
            //刷新列表
            //buttonX1.PerformClick();
            comboBox5.SelectedIndex = 0;
        }

        //报告预览
        private void btn_save_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                //检查类型
                string modality = Row.Cells["modality"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                if (exam_status >= 55)
                {
                    Frm_ReportViewer ins = new Frm_ReportViewer();
                    ins.modality = modality;
                    ins.study_no = study_no;
                    ins.exam_no = exam_no;
                    ins.BringToFront();
                    ins.ShowDialog();
                }
                else
                {
                    Frm_TJInfo("提示", "报告未打印，不能预览！");
                }
            }
        }

        //多媒体资料浏览
        private void btn_cap_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                //检查类型
                string modality = Row.Cells["modality"].Value.ToString();
                Frm_dmt ins = new Frm_dmt();
                ins.study_no = study_no;
                ins.modality = modality;
                ins.BringToFront();
                ins.ShowDialog();
            }
        }
        //报告修改
        private void btn_modify_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];

                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();

                //查询报告状态
                DBHelper.BLL.exam_master insM = new DBHelper.BLL.exam_master();
                int status = insM.GetStudyExam_Status(study_no);
                if (status >= 55)
                {
                    //只能是自己或更高级别的人进行修改
                    DBHelper.BLL.exam_report InsR = new DBHelper.BLL.exam_report();
                    DataTable dt = InsR.GetReportYsInfo(study_no);
                    if (dt != null && dt.Rows.Count == 1)
                    {
                        DBHelper.BLL.sys_user InsU = new DBHelper.BLL.sys_user();
                        int CurUserRoleCode = InsU.GetUserRoleCode(dt.Rows[0]["cbreport_doc_code"].ToString());
                        if (Program.User_Code.Equals(dt.Rows[0]["cbreport_doc_code"].ToString()) || Program.User_Code.Equals(dt.Rows[0]["zzreport_doc_code"].ToString()) || Program.User_Code.Equals(dt.Rows[0]["shreport_doc_code"].ToString()) || CurUserRoleCode < Program.user_role_code)
                        {
                            eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                            TaskDialog.EnableGlass = false;
                            if (TaskDialog.Show("修改已经打印过的报告", "确认", "确定要修改这个报告么？", Curbutton) == eTaskDialogResult.Ok)
                            {
                                //更新检查状态
                                insM.UpdateExam_Status(study_no, "50");
                                //原先报告内容写入历史报告表
                                if (InsR.SaveExam_history_report(study_no, Program.User_Code, Program.User_Name))
                                {
                                    //刷新列表
                                    buttonX1.PerformClick();
                                    Frm_TJInfo("提示", "操作成功，报告当前已经可编辑！");
                                }
                            }
                        }
                        else
                        {
                            Frm_TJInfo("提示", "权限不够，您没有权限修改此报告！");
                        }
                    }
                }
                else
                {
                    Frm_TJInfo("提示", "当前报告未打印，不需要强制执行修改！");
                }
            }
        }

        private void superGridControl1_RowDoubleClick(object sender, GridRowDoubleClickEventArgs e)
        {
            btn_save_Click(null, null);
        }

        //归档底单打印
        private void btn_print_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
            {
                SelectedElementCollection col = superGridControl1.PrimaryGrid.GetSelectedRows();
                if (col.Count > 0)
                {
                    for (int i = 0; i < col.Count; i++)
                    {
                        GridRow Row = col[i] as GridRow;
                        //病理号
                        string study_no = Row.Cells["study_no"].Value.ToString();
                        //肉眼所见
                        string rysj = Row.Cells["rysj"].Value.ToString();
                        //病理诊断
                        string blzd = Row.Cells["zdyj"].Value.ToString();
                        //报告日期
                        string bgrq = "";
                        if (Row.Cells["cbreport_datetime"].Value.ToString().Length > 10)
                        {
                            bgrq = Row.Cells["cbreport_datetime"].Value.ToString().Substring(0, 10);
                        }
                        //报告医生
                        string bgys = Row.Cells["cbreport_doc_name"].Value.ToString();
                        if (FastReportLib.PrintGddd.DirectPrintGddd(study_no, rysj, blzd, bgys, bgrq, PIS_Sys.Properties.Settings.Default.ReportPrinter, ChkPrint.Checked))
                        {
                            Frm_TJInfo("提示", study_no + "归档底单打印完毕！");
                        }
                    }
                }
            }
        }
        //报告痕迹
        private void btn_hj_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];

                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                Frm_hj_report ins = new Frm_hj_report();
                ins.study_no = study_no;
                ins.BringToFront();
                ins.ShowDialog();
            }
        }
        //是否存在报告痕迹
        private void superGridControl1_RowClick(object sender, GridRowClickEventArgs e)
        {
            if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
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

                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //病理号
                string study_no = Row.Cells["study_no"].Value.ToString();
                DBHelper.BLL.exam_report ins = new DBHelper.BLL.exam_report();
                if (ins.GetExam_history_report_Count(study_no) > 0)
                {
                    btn_hj.Enabled = true;
                }
                else
                {
                    btn_hj.Enabled = false;
                }
            }
        }
        //报告接收单
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            Frm_bgjsd ins = new Frm_bgjsd();
            ins.Owner = this;
            ins.Show();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (superGridControl1.PrimaryGrid.VirtualRowCount > 0)
                {
                    //string localFilePath, fileNameExt, newFileName, FilePath; 
                    SaveFileDialog sfd = new SaveFileDialog();
                    //设置文件类型 
                    sfd.Filter = "Excel文件（*.xls）|*.xls";
                    //设置默认文件类型显示顺序 
                    sfd.FilterIndex = 1;

                    //保存对话框是否记忆上次打开的目录 
                    sfd.RestoreDirectory = true;

                    //点了保存按钮进入 
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string localFilePath = sfd.FileName.ToString(); //获得文件路径 
                        //存在先删除
                        if (File.Exists(localFilePath) == true)
                        {
                            File.Delete(localFilePath);
                        }
                        //创建空的
                        FileInfo fi = new FileInfo(localFilePath);
                        FileStream fs = fi.Create();
                        fs.Close();
                        fs.Dispose();
                        //
                        Aspose.Cells.Workbook wk = new Aspose.Cells.Workbook(localFilePath);
                        string sj = string.Format("{0}到{1}", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                        wk.Worksheets[0].Name = sj + "报告列表";
                        DataTable dt = superGridControl1.PrimaryGrid.DataSource as DataTable;
                        AddExcelSHeet(wk, dt);
                        wk.Save(localFilePath);
                        MessageBox.Show("导出成功：" + localFilePath, "报告列表数据导出", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出错误：" + ex.ToString());
            }
        }
        private void AddExcelSHeet(Aspose.Cells.Workbook wk, DataTable dt)
        {
            DataRow[] werow = dt.Select();
            if (werow.Length > 0)
            {
                int i = 0;
                for (int j = 0; j < superGridControl1.PrimaryGrid.Columns.Count; j++)
                {
                    wk.Worksheets[0].Cells[i, j].PutValue(superGridControl1.PrimaryGrid.Columns[j].HeaderText);
                }
                foreach (DataRow rows in werow)
                {
                    i = i + 1;
                    for (int j = 0; j < rows.ItemArray.Length; j++)
                    {
                        wk.Worksheets[0].Cells[i, j].PutValue(rows[j].ToString());
                    }
                }
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Focus();
        }


    }
}
