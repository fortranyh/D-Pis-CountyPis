using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using System.Collections;
using System.IO;

namespace PathologyClient
{
    public partial class Frm_tjyz : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string SQD = "";
        public string BRXM = "";
        public Frm_tjyz()
        {
            InitializeComponent();
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            //列表样式
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
        }

        private void Frm_tjyz_Load(object sender, EventArgs e)
        {
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            //
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
        private void BtnQuery_Click(object sender, EventArgs e)
        {
            if (dtStart.Value > dtEnd.Value)
            {
                Frm_TJInfo("时间范围错误！", "起始时间必须小于终止时间！");
                return;
            }
            superGridControl2.PrimaryGrid.DataSource = null;
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
                superGridControl2.PrimaryGrid.DataSource = null;
                Application.DoEvents();
                GridRow Row = (GridRow)superGridControl1.PrimaryGrid.VirtualRows[index];
                //申请单号
                string exam_no = Row.Cells["exam_no"].Value.ToString();
                string study_no = Row.Cells["study_no"].Value.ToString();
                int exam_status = Convert.ToInt32(Row.Cells["exam_status"].Value.ToString());
               
                if (exam_status >= 25)
                {
                    IDictionary<string, string> parameters = new Dictionary<string, string>();
                    //展示当前病理号下的所有取材信息 
                    parameters.Add("study_no", study_no);
                    string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetTjyzXML", parameters);
                    DataSet ds = new DataSet();
                    ds.ReadXml(new StringReader(xmldata));
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            superGridControl2.PrimaryGrid.DataSource = dt;
                        }
                        else
                        {
                            superGridControl2.PrimaryGrid.DataSource = null;
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
    }
}
