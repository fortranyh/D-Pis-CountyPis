namespace PIS_Sys.bbdj
{
    partial class FrmBhgbb
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBhgbb));
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
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.richTextBoxEx1 = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.textBoxX2 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txt_sjks = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.txt_sjys = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.txt_study_no = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX3 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.textBoxX4 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.DataPropertyName = "id";
            this.gridColumn1.Name = "id";
            this.gridColumn1.Visible = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn2.DataPropertyName = "specimens_name";
            this.gridColumn2.HeaderText = "不合格名称";
            this.gridColumn2.Name = "specimens_name";
            // 
            // gridColumn3
            // 
            this.gridColumn3.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn3.DataPropertyName = "qualified_info";
            this.gridColumn3.HeaderText = "不合格原因";
            this.gridColumn3.Name = "qualified_info";
            // 
            // gridColumn4
            // 
            this.gridColumn4.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn4.DataPropertyName = "note_datetime";
            this.gridColumn4.HeaderText = "登记时间";
            this.gridColumn4.Name = "note_datetime";
            // 
            // gridColumn5
            // 
            this.gridColumn5.ColumnSortMode = DevComponents.DotNetBar.SuperGrid.ColumnSortMode.None;
            this.gridColumn5.DataPropertyName = "note_doctor";
            this.gridColumn5.HeaderText = "登记医生";
            this.gridColumn5.Name = "note_doctor";
            // 
            // gridColumn6
            // 
            this.gridColumn6.DataPropertyName = "dept_name";
            this.gridColumn6.HeaderText = "送检科室";
            this.gridColumn6.Name = "dept_name";
            // 
            // gridColumn7
            // 
            this.gridColumn7.DataPropertyName = "doctor_name";
            this.gridColumn7.HeaderText = "送检医生";
            this.gridColumn7.Name = "doctor_name";
            // 
            // gridColumn8
            // 
            this.gridColumn8.DataPropertyName = "study_no";
            this.gridColumn8.HeaderText = "病理号";
            this.gridColumn8.Name = "study_no";
            // 
            // gridColumn9
            // 
            this.gridColumn9.DataPropertyName = "patient_id";
            this.gridColumn9.HeaderText = "病人ID号";
            this.gridColumn9.Name = "patient_id";
            // 
            // gridColumn10
            // 
            this.gridColumn10.DataPropertyName = "input_id";
            this.gridColumn10.HeaderText = "住院号";
            this.gridColumn10.Name = "input_id";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(144, 3);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(129, 27);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "不合格名称：";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(144, 63);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(100, 27);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "不合格原因：";
            // 
            // richTextBoxEx1
            // 
            // 
            // 
            // 
            this.richTextBoxEx1.BackgroundStyle.Class = "RichTextBoxBorder";
            this.richTextBoxEx1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.richTextBoxEx1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.richTextBoxEx1.Location = new System.Drawing.Point(241, 59);
            this.richTextBoxEx1.MaxLength = 50;
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Rtf = "{\\rtf1\\ansi\\ansicpg936\\deff0\\nouicompat\\deflang1033\\deflangfe2052{\\fonttbl{\\f0\\fn" +
    "il\\fcharset134 \\\'cb\\\'ce\\\'cc\\\'e5;}}\r\n{\\*\\generator Riched20 10.0.19041}\\viewkind4" +
    "\\uc1 \r\n\\pard\\f0\\fs21\\lang2052\\par\r\n}\r\n";
            this.richTextBoxEx1.Size = new System.Drawing.Size(231, 66);
            this.richTextBoxEx1.TabIndex = 3;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(157, 129);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(87, 27);
            this.labelX3.TabIndex = 4;
            this.labelX3.Text = "登记时间：";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(444, 129);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(87, 27);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "登 记 人：";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(534, 129);
            this.textBoxX1.MaxLength = 50;
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(141, 23);
            this.textBoxX1.TabIndex = 6;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(322, 161);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(87, 36);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 7;
            this.buttonX1.Text = "取消";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(417, 161);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(87, 36);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 7;
            this.buttonX2.Text = "确认";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // textBoxX2
            // 
            // 
            // 
            // 
            this.textBoxX2.Border.Class = "TextBoxBorder";
            this.textBoxX2.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX2.Location = new System.Drawing.Point(241, 3);
            this.textBoxX2.MaxLength = 50;
            this.textBoxX2.Name = "textBoxX2";
            this.textBoxX2.PreventEnterBeep = true;
            this.textBoxX2.Size = new System.Drawing.Size(231, 23);
            this.textBoxX2.TabIndex = 8;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(241, 129);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(197, 23);
            this.dateTimePicker1.TabIndex = 9;
            // 
            // superGridControl1
            // 
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superGridControl1.Location = new System.Drawing.Point(4, 203);
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
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.RowHeaderIndexOffset = 1;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.superGridControl1.PrimaryGrid.UseAlternateRowStyle = true;
            this.superGridControl1.Size = new System.Drawing.Size(777, 163);
            this.superGridControl1.TabIndex = 10;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(157, 30);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(129, 27);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "送检科室：";
            // 
            // txt_sjks
            // 
            // 
            // 
            // 
            this.txt_sjks.Border.Class = "TextBoxBorder";
            this.txt_sjks.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_sjks.Location = new System.Drawing.Point(241, 32);
            this.txt_sjks.MaxLength = 50;
            this.txt_sjks.Name = "txt_sjks";
            this.txt_sjks.PreventEnterBeep = true;
            this.txt_sjks.Size = new System.Drawing.Size(231, 23);
            this.txt_sjks.TabIndex = 8;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(478, 30);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(129, 27);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "送检医生：";
            // 
            // txt_sjys
            // 
            // 
            // 
            // 
            this.txt_sjys.Border.Class = "TextBoxBorder";
            this.txt_sjys.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_sjys.Location = new System.Drawing.Point(547, 32);
            this.txt_sjys.MaxLength = 20;
            this.txt_sjys.Name = "txt_sjys";
            this.txt_sjys.PreventEnterBeep = true;
            this.txt_sjys.Size = new System.Drawing.Size(128, 23);
            this.txt_sjys.TabIndex = 8;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(492, 1);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(129, 27);
            this.labelX7.TabIndex = 0;
            this.labelX7.Text = "病理号：";
            // 
            // txt_study_no
            // 
            // 
            // 
            // 
            this.txt_study_no.Border.Class = "TextBoxBorder";
            this.txt_study_no.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_study_no.Location = new System.Drawing.Point(547, 3);
            this.txt_study_no.MaxLength = 20;
            this.txt_study_no.Name = "txt_study_no";
            this.txt_study_no.PreventEnterBeep = true;
            this.txt_study_no.Size = new System.Drawing.Size(128, 23);
            this.txt_study_no.TabIndex = 8;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(478, 59);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(129, 27);
            this.labelX8.TabIndex = 0;
            this.labelX8.Text = "病人ID号：";
            // 
            // textBoxX3
            // 
            // 
            // 
            // 
            this.textBoxX3.Border.Class = "TextBoxBorder";
            this.textBoxX3.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX3.Location = new System.Drawing.Point(547, 61);
            this.textBoxX3.MaxLength = 20;
            this.textBoxX3.Name = "textBoxX3";
            this.textBoxX3.PreventEnterBeep = true;
            this.textBoxX3.Size = new System.Drawing.Size(128, 23);
            this.textBoxX3.TabIndex = 8;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(493, 86);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(115, 27);
            this.labelX9.TabIndex = 0;
            this.labelX9.Text = "住院号：";
            // 
            // textBoxX4
            // 
            // 
            // 
            // 
            this.textBoxX4.Border.Class = "TextBoxBorder";
            this.textBoxX4.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX4.Location = new System.Drawing.Point(547, 87);
            this.textBoxX4.MaxLength = 20;
            this.textBoxX4.Name = "textBoxX4";
            this.textBoxX4.PreventEnterBeep = true;
            this.textBoxX4.Size = new System.Drawing.Size(128, 23);
            this.textBoxX4.TabIndex = 8;
            // 
            // FrmBhgbb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(785, 370);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.txt_study_no);
            this.Controls.Add(this.textBoxX4);
            this.Controls.Add(this.textBoxX3);
            this.Controls.Add(this.txt_sjys);
            this.Controls.Add(this.txt_sjks);
            this.Controls.Add(this.textBoxX2);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.textBoxX1);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.richTextBoxEx1);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX7);
            this.Controls.Add(this.labelX9);
            this.Controls.Add(this.labelX8);
            this.Controls.Add(this.labelX6);
            this.Controls.Add(this.labelX5);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBhgbb";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "不合格标本登记";
            this.Load += new System.EventHandler(this.FrmBhgbb_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx richTextBoxEx1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX2;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_sjks;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_sjys;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_study_no;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX3;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX4;
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
    }
}