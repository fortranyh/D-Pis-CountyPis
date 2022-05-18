namespace PIS_Sys.dagl
{
    partial class Frm_BbQuery
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BbQuery));
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cmbJclx = new System.Windows.Forms.ComboBox();
            this.btnLKwz = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.txt_ckbw = new System.Windows.Forms.TextBox();
            this.Txtblh = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.ChkStart = new System.Windows.Forms.CheckBox();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.DataPropertyName = "id";
            this.gridColumn1.HeaderText = "id";
            this.gridColumn1.Name = "id";
            this.gridColumn1.Visible = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn2.DataPropertyName = "study_no";
            this.gridColumn2.HeaderText = "病理号";
            this.gridColumn2.Name = "study_no";
            this.gridColumn2.ReadOnly = true;
            this.gridColumn2.Width = 80;
            // 
            // gridColumn3
            // 
            this.gridColumn3.DataPropertyName = "exam_no";
            this.gridColumn3.HeaderText = "exam_no";
            this.gridColumn3.Name = "exam_no";
            this.gridColumn3.Visible = false;
            // 
            // gridColumn4
            // 
            this.gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn4.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn4.DataPropertyName = "parts";
            this.gridColumn4.HeaderText = "标本采集部位";
            this.gridColumn4.Name = "parts";
            this.gridColumn4.ReadOnly = true;
            // 
            // gridColumn5
            // 
            this.gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn5.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn5.DataPropertyName = "pack_order";
            this.gridColumn5.HeaderText = "袋数";
            this.gridColumn5.Name = "pack_order";
            this.gridColumn5.ReadOnly = true;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn6.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn6.DataPropertyName = "specimens_class";
            this.gridColumn6.HeaderText = "标本类型";
            this.gridColumn6.Name = "specimens_class";
            this.gridColumn6.ReadOnly = true;
            this.gridColumn6.Width = 80;
            // 
            // gridColumn7
            // 
            this.gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn7.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn7.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn7.DataPropertyName = "lk_count";
            this.gridColumn7.HeaderText = "蜡块数目";
            this.gridColumn7.Name = "lk_count";
            this.gridColumn7.ReadOnly = true;
            this.gridColumn7.Width = 200;
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "ck_count";
            this.gridColumn8.HeaderText = "材块数目";
            this.gridColumn8.Name = "ck_count";
            // 
            // gridColumn9
            // 
            this.gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn9.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn9.DataPropertyName = "receive_datetime";
            this.gridColumn9.HeaderText = "标本接收时间";
            this.gridColumn9.Name = "received_datetime";
            this.gridColumn9.ReadOnly = true;
            this.gridColumn9.Width = 60;
            // 
            // gridColumn10
            // 
            this.gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn10.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn10.DataPropertyName = "qucai_datetime";
            this.gridColumn10.HeaderText = "取材时间";
            this.gridColumn10.Name = "qucai_datetime";
            this.gridColumn10.ReadOnly = true;
            this.gridColumn10.Width = 40;
            // 
            // gridColumn11
            // 
            this.gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn11.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn11.DataPropertyName = "receive_doctor_name";
            this.gridColumn11.HeaderText = "标本接收医师";
            this.gridColumn11.Name = "receive_doctor_name";
            this.gridColumn11.ReadOnly = true;
            this.gridColumn11.Width = 80;
            // 
            // gridColumn12
            // 
            this.gridColumn12.DataPropertyName = "record_doctor_name";
            this.gridColumn12.HeaderText = "描述记录人";
            this.gridColumn12.Name = "record_doctor_name";
            // 
            // gridColumn13
            // 
            this.gridColumn13.DataPropertyName = "see_memo";
            this.gridColumn13.HeaderText = "大体描述";
            this.gridColumn13.Name = "see_memo";
            // 
            // gridColumn14
            // 
            this.gridColumn14.DataPropertyName = "specimens_process";
            this.gridColumn14.HeaderText = "标本处理";
            this.gridColumn14.Name = "specimens_process";
            this.gridColumn14.Width = 120;
            // 
            // gridColumn15
            // 
            this.gridColumn15.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn15.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn15.DataPropertyName = "specimens_location";
            this.gridColumn15.HeaderText = "标本存放位置";
            this.gridColumn15.Name = "specimens_location";
            this.gridColumn15.ReadOnly = true;
            // 
            // gridColumn16
            // 
            this.gridColumn16.DataPropertyName = "memo_note";
            this.gridColumn16.HeaderText = "备注";
            this.gridColumn16.Name = "memo_note";
            // 
            // gridColumn17
            // 
            this.gridColumn17.DataPropertyName = "bbgfh_flag";
            this.gridColumn17.HeaderText = "标本规范化";
            this.gridColumn17.Name = "bbgfh_flag";
            this.gridColumn17.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBoxX1);
            this.splitContainer1.Panel1.Controls.Add(this.cmbJclx);
            this.splitContainer1.Panel1.Controls.Add(this.btnLKwz);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxX1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonX3);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.buttonX1);
            this.splitContainer1.Panel1.Controls.Add(this.txt_ckbw);
            this.splitContainer1.Panel1.Controls.Add(this.Txtblh);
            this.splitContainer1.Panel1.Controls.Add(this.Label3);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.Label5);
            this.splitContainer1.Panel1.Controls.Add(this.chkEnd);
            this.splitContainer1.Panel1.Controls.Add(this.ChkStart);
            this.splitContainer1.Panel1.Controls.Add(this.dtStart);
            this.splitContainer1.Panel1.Controls.Add(this.dtEnd);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.superGridControl1);
            this.splitContainer1.Size = new System.Drawing.Size(924, 633);
            this.splitContainer1.SplitterDistance = 146;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 2;
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.CheckSignSize = new System.Drawing.Size(18, 18);
            this.checkBoxX1.Location = new System.Drawing.Point(22, 325);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(100, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 67;
            this.checkBoxX1.Text = "不规范固定";
            // 
            // cmbJclx
            // 
            this.cmbJclx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbJclx.FormattingEnabled = true;
            this.cmbJclx.Location = new System.Drawing.Point(22, 237);
            this.cmbJclx.Name = "cmbJclx";
            this.cmbJclx.Size = new System.Drawing.Size(115, 22);
            this.cmbJclx.TabIndex = 66;
            // 
            // btnLKwz
            // 
            this.btnLKwz.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLKwz.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnLKwz.Location = new System.Drawing.Point(12, 494);
            this.btnLKwz.Name = "btnLKwz";
            this.btnLKwz.Size = new System.Drawing.Size(124, 37);
            this.btnLKwz.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLKwz.TabIndex = 65;
            this.btnLKwz.Text = "更新材块存放位置";
            this.btnLKwz.Click += new System.EventHandler(this.btnLKwz_Click);
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(15, 468);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(121, 23);
            this.textBoxX1.TabIndex = 64;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(16, 416);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(121, 48);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 63;
            this.buttonX3.Text = "固定标本不规范";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(91, 14);
            this.label7.TabIndex = 61;
            this.label7.Text = "标本接收日期";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = global::PIS_Sys.Properties.Resources._20141119050404427_easyicon_net_32;
            this.buttonX1.Location = new System.Drawing.Point(15, 357);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(122, 51);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 60;
            this.buttonX1.Text = "查询";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // txt_ckbw
            // 
            this.txt_ckbw.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ckbw.Location = new System.Drawing.Point(23, 181);
            this.txt_ckbw.Name = "txt_ckbw";
            this.txt_ckbw.Size = new System.Drawing.Size(114, 26);
            this.txt_ckbw.TabIndex = 56;
            // 
            // Txtblh
            // 
            this.Txtblh.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Txtblh.Location = new System.Drawing.Point(23, 287);
            this.Txtblh.Name = "Txtblh";
            this.Txtblh.Size = new System.Drawing.Size(113, 26);
            this.Txtblh.TabIndex = 56;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(9, 159);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(63, 14);
            this.Label3.TabIndex = 50;
            this.Label3.Text = "材块部位";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 215);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 49;
            this.label1.Text = "检查类别";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(10, 267);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(49, 14);
            this.Label5.TabIndex = 49;
            this.Label5.Text = "病理号";
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Checked = true;
            this.chkEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnd.Enabled = false;
            this.chkEnd.Location = new System.Drawing.Point(7, 98);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(40, 18);
            this.chkEnd.TabIndex = 42;
            this.chkEnd.Text = "到";
            this.chkEnd.UseVisualStyleBackColor = true;
            // 
            // ChkStart
            // 
            this.ChkStart.AutoSize = true;
            this.ChkStart.Checked = true;
            this.ChkStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkStart.Enabled = false;
            this.ChkStart.Location = new System.Drawing.Point(7, 37);
            this.ChkStart.Name = "ChkStart";
            this.ChkStart.Size = new System.Drawing.Size(40, 18);
            this.ChkStart.TabIndex = 43;
            this.ChkStart.Text = "从";
            this.ChkStart.UseVisualStyleBackColor = true;
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy-MM-dd";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(25, 62);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(112, 23);
            this.dtStart.TabIndex = 40;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(25, 125);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(112, 23);
            this.dtEnd.TabIndex = 41;
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
            this.superGridControl1.PrimaryGrid.MinRowHeight = 30;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.PrimaryGrid.ShowRowHeaders = false;
            this.superGridControl1.PrimaryGrid.VirtualMode = true;
            this.superGridControl1.PrimaryGrid.VirtualRowHeight = 30;
            this.superGridControl1.Size = new System.Drawing.Size(772, 633);
            this.superGridControl1.TabIndex = 65;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.GetRowHeaderStyle += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderStyleEventArgs>(this.superGridControl1_GetRowHeaderStyle);
            // 
            // Frm_BbQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(924, 633);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_BbQuery";
            this.Text = "标本信息查询";
            this.Load += new System.EventHandler(this.Frm_BbQuery_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        internal System.Windows.Forms.Label label7;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        internal System.Windows.Forms.TextBox txt_ckbw;
        internal System.Windows.Forms.TextBox Txtblh;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.CheckBox chkEnd;
        internal System.Windows.Forms.CheckBox ChkStart;
        internal System.Windows.Forms.DateTimePicker dtStart;
        internal System.Windows.Forms.DateTimePicker dtEnd;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX btnLKwz;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbJclx;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
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
    }
}