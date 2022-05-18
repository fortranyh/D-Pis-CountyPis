namespace PIS_Sys.blzd
{
    partial class Frm_hzgl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_hzgl));
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.report_bl_datetime = new System.Windows.Forms.DateTimePicker();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.richTextBoxEx3 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.richTextBoxEx2 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.richTextBoxEx1 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
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
            this.gridColumn2.DataPropertyName = "doc_name";
            this.gridColumn2.HeaderText = "医师";
            this.gridColumn2.Name = "doc_name";
            // 
            // gridColumn3
            // 
            this.gridColumn3.DataPropertyName = "create_dt";
            this.gridColumn3.HeaderText = "时间";
            this.gridColumn3.MinimumWidth = 160;
            this.gridColumn3.Name = "create_dt";
            this.gridColumn3.Width = 160;
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "doc_code";
            this.gridColumn4.HeaderText = "doc_code";
            this.gridColumn4.Name = "doc_code";
            this.gridColumn4.Visible = false;
            // 
            // gridColumn5
            // 
            this.gridColumn5.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn5.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn5.DataPropertyName = "advice";
            this.gridColumn5.HeaderText = "会诊诊断意见";
            this.gridColumn5.Name = "advice";
            this.gridColumn5.Width = 200;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(7, 8);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(106, 26);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "会诊发起人：";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(104, 8);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.ReadOnly = true;
            this.textBoxX1.Size = new System.Drawing.Size(114, 26);
            this.textBoxX1.TabIndex = 1;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(225, 8);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(95, 26);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "参与人员：";
            // 
            // report_bl_datetime
            // 
            this.report_bl_datetime.CustomFormat = "yyyy-MM-dd";
            this.report_bl_datetime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.report_bl_datetime.Location = new System.Drawing.Point(104, 44);
            this.report_bl_datetime.Name = "report_bl_datetime";
            this.report_bl_datetime.Size = new System.Drawing.Size(114, 26);
            this.report_bl_datetime.TabIndex = 60;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(21, 44);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(93, 26);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "发起时间：";
            // 
            // richTextBoxEx3
            // 
            // 
            // 
            // 
            this.richTextBoxEx3.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx3.Location = new System.Drawing.Point(306, 8);
            this.richTextBoxEx3.MaxLength = 100;
            this.richTextBoxEx3.Name = "richTextBoxEx3";
            this.richTextBoxEx3.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs24\\lang2052\\par\r\n}\r\n";
            this.richTextBoxEx3.Size = new System.Drawing.Size(427, 62);
            this.richTextBoxEx3.TabIndex = 61;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(21, 79);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(93, 26);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "专家意见：";
            // 
            // richTextBoxEx2
            // 
            // 
            // 
            // 
            this.richTextBoxEx2.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx2.Location = new System.Drawing.Point(104, 76);
            this.richTextBoxEx2.Name = "richTextBoxEx2";
            this.richTextBoxEx2.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs24\\lang2052\\par\r\n}\r\n";
            this.richTextBoxEx2.Size = new System.Drawing.Size(629, 88);
            this.richTextBoxEx2.TabIndex = 62;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(21, 437);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(93, 26);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "意见录入：";
            // 
            // superGridControl1
            // 
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superGridControl1.Location = new System.Drawing.Point(104, 170);
            this.superGridControl1.Name = "superGridControl1";
            // 
            // 
            // 
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.UseAlternateRowStyle = true;
            this.superGridControl1.Size = new System.Drawing.Size(675, 265);
            this.superGridControl1.TabIndex = 63;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.RowClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridRowClickEventArgs>(this.superGridControl1_RowClick);
            // 
            // richTextBoxEx1
            // 
            // 
            // 
            // 
            this.richTextBoxEx1.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx1.Location = new System.Drawing.Point(104, 439);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs24\\lang2052\\par\r\n}\r\n";
            this.richTextBoxEx1.Size = new System.Drawing.Size(569, 115);
            this.richTextBoxEx1.TabIndex = 64;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(21, 170);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(93, 26);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "科内会诊：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(686, 438);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(86, 38);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 65;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(686, 478);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(86, 38);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 65;
            this.buttonX2.Text = "更新";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(686, 518);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(86, 38);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 65;
            this.buttonX3.Text = "删除";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonX4.Location = new System.Drawing.Point(738, 8);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(34, 87);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 66;
            this.buttonX4.Text = "发起会诊";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click_1);
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX5.Location = new System.Drawing.Point(738, 106);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(34, 47);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 67;
            this.buttonX5.Text = "保存";
            this.buttonX5.Click += new System.EventHandler(this.buttonX5_Click);
            // 
            // Frm_hzgl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(785, 556);
            this.Controls.Add(this.buttonX5);
            this.Controls.Add(this.buttonX4);
            this.Controls.Add(this.buttonX3);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.richTextBoxEx1);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.richTextBoxEx2);
            this.Controls.Add(this.richTextBoxEx3);
            this.Controls.Add(this.report_bl_datetime);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_hzgl";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "会诊意见";
            this.Load += new System.EventHandler(this.Frm_hzgl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.DateTimePicker report_bl_datetime;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx2;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx1;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
    }
}