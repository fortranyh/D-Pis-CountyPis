using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace PathologyClient
{
    public partial class Frm_Qcxx : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Qcxx()
        {
            InitializeComponent();
            superGridControl4.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            GridPanel panel2 = superGridControl4.PrimaryGrid;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
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
        private void Frm_Qcxx_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl5.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl5.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl5.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel1 = superGridControl5.PrimaryGrid;
            panel1.MinRowHeight = 30;
            panel1.AutoGenerateColumns = true;
            superGridControl5.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel1.Columns.Count; i++)
            {
                panel1.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            }
            //高度自动适应
            superGridControl4.PrimaryGrid.DefaultRowHeight = 0;

            //高度自动适应
            superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
            superGridControl1.PrimaryGrid.AllowRowResize = true;
            superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            GridPanel panel2 = superGridControl1.PrimaryGrid;
            panel2.MinRowHeight = 30;
            //锁定
            panel2.FrozenColumnCount = 7;
            panel2.AutoGenerateColumns = true;
            superGridControl1.PrimaryGrid.ShowRowHeaders = true;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ReadOnly = true;
                panel2.Columns[i].CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
            //
            dtStart.Value = DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0));
            dtEnd.Value = DateTime.Now;
            //执行查询
            BtnQuery.PerformClick();
        }
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            superGridControl4.PrimaryGrid.DataSource = null;
            superGridControl5.PrimaryGrid.DataSource = null;
            buttonX2.Visible = false;
            Application.DoEvents();

            StringBuilder sb = new StringBuilder();
            if (!Txtblh.Text.Trim().Equals(""))
            {
                sb.AppendFormat(" and study_no = '{0}'", Txtblh.Text.Trim().Replace("'", "").ToUpper());
            }
            else
            {
                sb.AppendFormat("and req_date_time>='{0} 00:00:00' and req_date_time<='{1} 23:59:59'", dtStart.Value.ToString("yyyy-MM-dd"), dtEnd.Value.ToString("yyyy-MM-dd"));
                if (!TxtName.Text.Trim().Equals(""))
                {
                    sb.AppendFormat(" and patient_name like '%{0}%'", TxtName.Text.Trim().Replace("'", ""));
                }
            }
            sb.AppendFormat(" and submit_unit = '{0}' ", Program.HospitalName);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("tjStr", sb.ToString());
            sb = null;
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetExamMasterXML", parameters);
            parameters.Clear();
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                superGridControl1.PrimaryGrid.DataSource = ds.Tables[0];
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", ds.Tables[0].Rows.Count);
            }
            else
            {
                superGridControl1.PrimaryGrid.VirtualRowCount = 0;
                superGridControl1.PrimaryGrid.DataSource = null;
                superGridControl1.PrimaryGrid.Footer.Text = String.Format("共查询到 <font color='Blue'><b>{0}</b></font> 条记录", 0);
            }
            //激活列表
            superGridControl1.Select();
            superGridControl1.Focus();
        }

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
                superGridControl4.PrimaryGrid.DataSource = null;
                superGridControl5.PrimaryGrid.DataSource = null;
                Application.DoEvents();
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                string study_no = Row.Cells["study_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                if (exam_status >= 20)
                {
                    buttonX2.Visible = false;
                }
                else
                {
                    buttonX2.Visible = true;
                }
                if (exam_status >= 25)
                {
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    //展示当前病理号下的所有取材信息 
                    parameters.Add("study_no", study_no);
                    string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetDrawMeterialsXML", parameters);
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xmldata));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            superGridControl4.PrimaryGrid.DataSource = dt;
                        }
                        else
                        {
                            superGridControl4.PrimaryGrid.DataSource = null;
                        }
                    }
                    //大体描述
                    parameters.Clear();
                    parameters.Add("exam_no", exam_no);
                    xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetDtmsXML", parameters);
                    DataSet dsdt = new DataSet();
                    dsdt.ReadXml(new StringReader(xmldata));
                    if (dsdt != null && dsdt.Tables.Count > 0 && dsdt.Tables[0].Rows.Count > 0)
                    {
                        DataTable dtdtms = dsdt.Tables[0];
                        if (dtdtms != null && dtdtms.Rows.Count > 0)
                        {
                            superGridControl5.PrimaryGrid.DataSource = dtdtms;
                        }
                        else
                        {
                            superGridControl5.PrimaryGrid.DataSource = null;
                        }
                    }
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
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
                superGridControl4.PrimaryGrid.DataSource = null;
                superGridControl5.PrimaryGrid.DataSource = null;
                Application.DoEvents();
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                string study_no = Row.Cells["study_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                if (exam_status < 55)
                {
                    //打印纸质申请单
                    if (PathologyClient.Properties.Settings.Default.Print_Sqd_Flag && !PathologyClient.Properties.Settings.Default.CurPrinter.Equals(""))
                    {
                        EntityModel.Exam_BlSqd BlSqdIns = Program.GetSqdInfo(exam_no);
                        if (BlSqdIns != null)
                        {
                            Boolean flag = BLSqdPrint.PrintBlSQD(BlSqdIns, PathologyClient.Properties.Settings.Default.CurPrinter, 1);
                            if (flag)
                            {
                                Frm_TJInfo("提示", "打印申请单成功！");
                            }
                        }
                    }
                }
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
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
                superGridControl4.PrimaryGrid.DataSource = null;
                superGridControl5.PrimaryGrid.DataSource = null;
                Application.DoEvents();
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                string study_no = Row.Cells["study_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                if (exam_status >= 20)
                {
                    Frm_TJInfo("提示", "病理诊断中心已经接收此申请单；不能执行作废！");
                    return;
                }
                else
                {
                    eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                    TaskDialog.EnableGlass = false;
                    if (TaskDialog.Show("作废申请单", "确认", "确定要作废本申请单么？", Curbutton) == eTaskDialogResult.Ok)
                    {
                        IDictionary<string, string> parameters = new Dictionary<string, string>();
                        //组织post请求参数
                        parameters.Add("exam_no", exam_no);
                        parameters.Add("user_code", Program.User_Code);
                        string Retdata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "ZfExamMaster", parameters);
                        DataSet ds = new DataSet();
                        ds.ReadXml(new StringReader(Retdata));
                        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string result_str = ds.Tables[0].Rows[0]["INFO"].ToString();
                            if (ds.Tables[0].Rows[0]["RESULT_CODE"].ToString().Equals("1"))
                            {

                                Frm_TJInfo("作废成功", result_str);
                                BtnQuery.PerformClick();
                            }
                            else
                            {
                                Frm_TJInfo("作废失败", result_str);
                            }
                        }
                    }
                }
            }
        }

        private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
        {
            if (e.GridCell.GridColumn.Name.Equals("status_name") == true)
            {
                string strStatus = (string)e.GridCell.Value;
                if (Program.exam_status_name_dic.ContainsKey(strStatus))
                {
                    e.Style.TextColor = Program.exam_status_name_dic[strStatus];
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
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
                string patient_name = Row.Cells["pat_name"].Value.ToString();
                string study_no = Row.Cells["study_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
                IDictionary<string, string> parameters = new Dictionary<string, string>();
                if (exam_status >= 20)
                {
                    Form Frm_SjzIns = Application.OpenForms["Frm_Sjz"];
                    if (Frm_SjzIns == null)
                    {
                        Frm_SjzIns = new Frm_Sjz();
                        Frm_SjzIns.BringToFront();
                        Frm_SjzIns.WindowState = FormWindowState.Normal;
                        Frm_Sjz.exam_no = exam_no;
                        Frm_Sjz.patient_name = patient_name;
                        Frm_Sjz.study_no = study_no;
                        Frm_SjzIns.Owner = this;
                        Frm_SjzIns.ShowDialog();
                    }
                    else
                    {
                        Frm_Sjz.exam_no = exam_no;
                        Frm_Sjz.patient_name = patient_name;
                        Frm_Sjz.study_no = study_no;
                        Frm_SjzIns.BringToFront();
                        Frm_SjzIns.WindowState = FormWindowState.Normal;
                    }
                }
                else
                {
                    Frm_TJInfo("提示", "病理诊断中心还未接收标本；\n当前状态没有时间轴信息！");
                }
            }
        }



    }
}
