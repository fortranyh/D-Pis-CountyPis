namespace PIS_Sys.blzd
{
    partial class Frm_Qpxx
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Qpxx));
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
            this.superGridControl4 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.checkBoxX4 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.checkBoxX3 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn1.DataPropertyName = "id";
            this.gridColumn1.HeaderText = "切片表id";
            this.gridColumn1.Name = "id";
            this.gridColumn1.Visible = false;
            // 
            // gridColumn2
            // 
            this.gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn2.DataPropertyName = "draw_id";
            this.gridColumn2.HeaderText = "取材表id";
            this.gridColumn2.Name = "draw_id";
            this.gridColumn2.Visible = false;
            // 
            // gridColumn3
            // 
            this.gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn3.DataPropertyName = "study_no";
            this.gridColumn3.HeaderText = "病理号";
            this.gridColumn3.Name = "study_no";
            this.gridColumn3.ReadOnly = true;
            // 
            // gridColumn4
            // 
            this.gridColumn4.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn4.DataPropertyName = "draw_barcode";
            this.gridColumn4.HeaderText = "蜡块号";
            this.gridColumn4.Name = "draw_barcode";
            this.gridColumn4.ReadOnly = true;
            // 
            // gridColumn5
            // 
            this.gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn5.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn5.DataPropertyName = "work_source";
            this.gridColumn5.HeaderText = "任务来源";
            this.gridColumn5.Name = "work_source";
            this.gridColumn5.ReadOnly = true;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn6.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn6.DataPropertyName = "film_child_barcode";
            this.gridColumn6.HeaderText = "制片子号";
            this.gridColumn6.Name = "film_child_barcode";
            this.gridColumn6.ReadOnly = true;
            this.gridColumn6.Visible = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn7.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn7.DataPropertyName = "barcode";
            this.gridColumn7.HeaderText = "制片条码号";
            this.gridColumn7.Name = "barcode";
            this.gridColumn7.ReadOnly = true;
            // 
            // gridColumn8
            // 
            this.gridColumn8.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn8.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn8.DataPropertyName = "film_num";
            this.gridColumn8.HeaderText = "玻片数";
            this.gridColumn8.Name = "film_num";
            this.gridColumn8.ReadOnly = true;
            this.gridColumn8.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridTextBoxXEditControl);
            this.gridColumn8.Width = 60;
            // 
            // gridColumn9
            // 
            this.gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn9.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn9.DataPropertyName = "zp_flag";
            this.gridColumn9.HeaderText = "制片状态";
            this.gridColumn9.Name = "zp_flag";
            this.gridColumn9.ReadOnly = true;
            this.gridColumn9.Width = 80;
            // 
            // gridColumn10
            // 
            this.gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn10.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn10.DataPropertyName = "print_flag";
            this.gridColumn10.HeaderText = "打印状态";
            this.gridColumn10.Name = "print_flag";
            this.gridColumn10.ReadOnly = true;
            this.gridColumn10.Width = 80;
            // 
            // gridColumn11
            // 
            this.gridColumn11.DataPropertyName = "make_doc_name";
            this.gridColumn11.HeaderText = "制片医师";
            this.gridColumn11.Name = "make_doc_name";
            // 
            // gridColumn12
            // 
            this.gridColumn12.DataPropertyName = "level";
            this.gridColumn12.HeaderText = "级别";
            this.gridColumn12.Name = "level";
            // 
            // gridColumn13
            // 
            this.gridColumn13.DataPropertyName = "level_memo";
            this.gridColumn13.HeaderText = "评级备注";
            this.gridColumn13.Name = "level_memo";
            // 
            // gridColumn14
            // 
            this.gridColumn14.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn14.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn14.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn14.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn14.DataPropertyName = "memo_note";
            this.gridColumn14.HeaderText = "制片说明";
            this.gridColumn14.Name = "memo_note";
            this.gridColumn14.Width = 130;
            // 
            // superGridControl4
            // 
            this.superGridControl4.DefaultVisualStyles.TitleStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl4.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superGridControl4.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superGridControl4.Location = new System.Drawing.Point(0, 41);
            this.superGridControl4.Name = "superGridControl4";
            // 
            // 
            // 
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn7);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn8);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn9);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn10);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn11);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn12);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn13);
            this.superGridControl4.PrimaryGrid.Columns.Add(this.gridColumn14);
            this.superGridControl4.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl4.PrimaryGrid.ShowRowHeaders = false;
            this.superGridControl4.ShowCustomFilterHelp = false;
            this.superGridControl4.Size = new System.Drawing.Size(864, 539);
            this.superGridControl4.TabIndex = 44;
            this.superGridControl4.Text = "superGridControl4";
            this.superGridControl4.GetCellValue += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetCellValueEventArgs>(this.superGridControl4_GetCellValue);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.comboBoxEx1);
            this.panelEx1.Controls.Add(this.checkBoxX4);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.buttonX1);
            this.panelEx1.Controls.Add(this.checkBoxX3);
            this.panelEx1.Controls.Add(this.checkBoxX2);
            this.panelEx1.Controls.Add(this.checkBoxX1);
            this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(864, 41);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 45;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 21;
            this.comboBoxEx1.Location = new System.Drawing.Point(352, 8);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(151, 27);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 5;
            // 
            // checkBoxX4
            // 
            // 
            // 
            // 
            this.checkBoxX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX4.CheckSignSize = new System.Drawing.Size(24, 24);
            this.checkBoxX4.Location = new System.Drawing.Point(658, 5);
            this.checkBoxX4.Name = "checkBoxX4";
            this.checkBoxX4.Size = new System.Drawing.Size(192, 32);
            this.checkBoxX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX4.TabIndex = 4;
            this.checkBoxX4.Text = "胃镜小标本连切六片";
            this.checkBoxX4.CheckedChanged += new System.EventHandler(this.checkBoxX4_CheckedChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(301, 11);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "备注：";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(523, 6);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(83, 30);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 1;
            this.buttonX1.Text = "切片评级";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // checkBoxX3
            // 
            // 
            // 
            // 
            this.checkBoxX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX3.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX3.CheckSignSize = new System.Drawing.Size(16, 16);
            this.checkBoxX3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxX3.Location = new System.Drawing.Point(208, 10);
            this.checkBoxX3.Name = "checkBoxX3";
            this.checkBoxX3.Size = new System.Drawing.Size(87, 23);
            this.checkBoxX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX3.TabIndex = 0;
            this.checkBoxX3.Text = "丙级";
            // 
            // checkBoxX2
            // 
            // 
            // 
            // 
            this.checkBoxX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX2.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX2.CheckSignSize = new System.Drawing.Size(16, 16);
            this.checkBoxX2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxX2.Location = new System.Drawing.Point(102, 10);
            this.checkBoxX2.Name = "checkBoxX2";
            this.checkBoxX2.Size = new System.Drawing.Size(88, 23);
            this.checkBoxX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX2.TabIndex = 0;
            this.checkBoxX2.Text = "乙级";
            // 
            // checkBoxX1
            // 
            // 
            // 
            // 
            this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.checkBoxX1.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.checkBoxX1.Checked = true;
            this.checkBoxX1.CheckSignSize = new System.Drawing.Size(16, 16);
            this.checkBoxX1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxX1.CheckValue = "Y";
            this.checkBoxX1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxX1.Location = new System.Drawing.Point(12, 10);
            this.checkBoxX1.Name = "checkBoxX1";
            this.checkBoxX1.Size = new System.Drawing.Size(74, 23);
            this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.checkBoxX1.TabIndex = 0;
            this.checkBoxX1.Text = "甲级";
            // 
            // Frm_Qpxx
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CaptionFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(864, 580);
            this.Controls.Add(this.superGridControl4);
            this.Controls.Add(this.panelEx1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Frm_Qpxx";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "切片信息浏览";
            this.Activated += new System.EventHandler(this.Frm_Qpxx_Activated);
            this.Load += new System.EventHandler(this.Frm_Qpxx_Load);
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl4;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
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
    }
}