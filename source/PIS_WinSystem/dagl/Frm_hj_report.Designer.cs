namespace PIS_Sys.dagl
{
    partial class Frm_hj_report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_hj_report));
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn18 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn19 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn20 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn21 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn22 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn23 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn24 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn25 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn26 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn27 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn28 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn29 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn30 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.DataPropertyName = "exam_no";
            this.gridColumn1.HeaderText = "exam_no";
            this.gridColumn1.Name = "exam_no";
            this.gridColumn1.Visible = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.DataPropertyName = "study_no";
            this.gridColumn2.HeaderText = "病理号";
            this.gridColumn2.Name = "study_no";
            this.gridColumn2.Width = 80;
            // 
            // gridColumn3
            // 
            this.gridColumn3.DataPropertyName = "modality_cn";
            this.gridColumn3.HeaderText = "检查类型";
            this.gridColumn3.Name = "modality_cn";
            this.gridColumn3.Width = 140;
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "status_name";
            this.gridColumn4.HeaderText = "当前状态";
            this.gridColumn4.Name = "status_name";
            // 
            // gridColumn5
            // 
            this.gridColumn5.DataPropertyName = "patient_name";
            this.gridColumn5.HeaderText = "病人姓名";
            this.gridColumn5.Name = "pat_name";
            // 
            // gridColumn6
            // 
            this.gridColumn6.DataPropertyName = "sex";
            this.gridColumn6.HeaderText = "性别";
            this.gridColumn6.Name = "pat_sex";
            this.gridColumn6.Width = 40;
            // 
            // gridColumn7
            // 
            this.gridColumn7.DataPropertyName = "age";
            this.gridColumn7.HeaderText = "年龄";
            this.gridColumn7.Name = "pat_age";
            this.gridColumn7.Width = 60;
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "patient_source";
            this.gridColumn8.HeaderText = "病人来源";
            this.gridColumn8.Name = "patient_source";
            // 
            // gridColumn9
            // 
            this.gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn9.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn9.DataPropertyName = "zdyj";
            this.gridColumn9.HeaderText = "诊断结果";
            this.gridColumn9.MinimumWidth = 200;
            this.gridColumn9.Name = "zdyj";
            this.gridColumn9.Width = 200;
            // 
            // gridColumn10
            // 
            this.gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn10.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn10.DataPropertyName = "zdpz";
            this.gridColumn10.HeaderText = "治疗建议";
            this.gridColumn10.MinimumWidth = 200;
            this.gridColumn10.Name = "zdpz";
            this.gridColumn10.Width = 200;
            // 
            // gridColumn11
            // 
            this.gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn11.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn11.DataPropertyName = "rysj";
            this.gridColumn11.HeaderText = "肉眼所见";
            this.gridColumn11.MinimumWidth = 200;
            this.gridColumn11.Name = "rysj";
            this.gridColumn11.Width = 200;
            // 
            // gridColumn12
            // 
            this.gridColumn12.DataPropertyName = "req_physician";
            this.gridColumn12.HeaderText = "送检医师";
            this.gridColumn12.Name = "req_doc_name";
            // 
            // gridColumn13
            // 
            this.gridColumn13.DataPropertyName = "req_dept";
            this.gridColumn13.HeaderText = "送检科室";
            this.gridColumn13.Name = "req_dept";
            // 
            // gridColumn14
            // 
            this.gridColumn14.DataPropertyName = "submit_unit";
            this.gridColumn14.HeaderText = "送检单位";
            this.gridColumn14.Name = "submit_unit";
            // 
            // gridColumn15
            // 
            this.gridColumn15.DataPropertyName = "modality";
            this.gridColumn15.HeaderText = "modality";
            this.gridColumn15.Name = "modality";
            this.gridColumn15.Visible = false;
            // 
            // gridColumn16
            // 
            this.gridColumn16.DataPropertyName = "curstatus";
            this.gridColumn16.HeaderText = "curstatus";
            this.gridColumn16.Name = "curstatus";
            this.gridColumn16.Visible = false;
            // 
            // gridColumn17
            // 
            this.gridColumn17.DataPropertyName = "cbreport_doc_name";
            this.gridColumn17.HeaderText = "报告医师";
            this.gridColumn17.Name = "cbreport_doc_name";
            // 
            // gridColumn18
            // 
            this.gridColumn18.DataPropertyName = "zzreport_doc_name";
            this.gridColumn18.HeaderText = "主诊医师";
            this.gridColumn18.Name = "zzreport_doc_name";
            // 
            // gridColumn19
            // 
            this.gridColumn19.DataPropertyName = "shreport_doc_name";
            this.gridColumn19.HeaderText = "审核医师";
            this.gridColumn19.Name = "shreport_doc_name";
            // 
            // gridColumn20
            // 
            this.gridColumn20.DataPropertyName = "received_datetime";
            this.gridColumn20.HeaderText = "标本接收时间";
            this.gridColumn20.Name = "received_datetime";
            // 
            // gridColumn21
            // 
            this.gridColumn21.DataPropertyName = "cbreport_datetime";
            this.gridColumn21.HeaderText = "报告时间";
            this.gridColumn21.Name = "cbreport_datetime";
            // 
            // gridColumn22
            // 
            this.gridColumn22.DataPropertyName = "zzreport_datetime";
            this.gridColumn22.HeaderText = "主诊时间";
            this.gridColumn22.Name = "zzreport_datetime";
            // 
            // gridColumn23
            // 
            this.gridColumn23.DataPropertyName = "shreport_datetime";
            this.gridColumn23.HeaderText = "审核时间";
            this.gridColumn23.Name = "shreport_datetime";
            // 
            // gridColumn24
            // 
            this.gridColumn24.DataPropertyName = "report_print_datetime";
            this.gridColumn24.HeaderText = "打印时间";
            this.gridColumn24.Name = "report_print_datetime";
            // 
            // gridColumn25
            // 
            this.gridColumn25.DataPropertyName = "exam_type";
            this.gridColumn25.Name = "exam_type";
            this.gridColumn25.Visible = false;
            // 
            // gridColumn26
            // 
            this.gridColumn26.DataPropertyName = "sj";
            this.gridColumn26.HeaderText = "分钟数";
            this.gridColumn26.Name = "sj";
            // 
            // gridColumn27
            // 
            this.gridColumn27.DataPropertyName = "sfyx";
            this.gridColumn27.HeaderText = "阳性报告";
            this.gridColumn27.Name = "sfyx";
            // 
            // gridColumn28
            // 
            this.gridColumn28.DataPropertyName = "lcfh";
            this.gridColumn28.HeaderText = "临床符合";
            this.gridColumn28.Name = "lcfh";
            // 
            // gridColumn29
            // 
            this.gridColumn29.DataPropertyName = "xgreport_doc_name";
            this.gridColumn29.HeaderText = "修改人";
            this.gridColumn29.Name = "xgreport_doc_name";
            // 
            // gridColumn30
            // 
            this.gridColumn30.DataPropertyName = "xgreport_print_datetime";
            this.gridColumn30.HeaderText = "修改时间";
            this.gridColumn30.Name = "xgreport_print_datetime";
            // 
            // superGridControl1
            // 
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn11);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn12);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn13);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn14);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn15);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn16);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn17);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn18);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn19);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn20);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn21);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn22);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn23);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn24);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn25);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn26);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn27);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn28);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn29);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn30);
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.superGridControl1.Size = new System.Drawing.Size(1020, 579);
            this.superGridControl1.TabIndex = 66;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // Frm_hj_report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1020, 579);
            this.Controls.Add(this.superGridControl1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_hj_report";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报告修改痕迹列表";
            this.Load += new System.EventHandler(this.Frm_hj_report_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn18;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn21;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn22;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn23;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn24;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn25;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn26;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn27;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn28;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn29;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn30;
    }
}