namespace PIS_Sys.dagl
{
    partial class Frm_Sjckdj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sjckdj));
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonX7 = new DevComponents.DotNetBar.ButtonX();
            this.doubleInput1 = new DevComponents.Editors.DoubleInput();
            this.doubleInput2 = new DevComponents.Editors.DoubleInput();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.chkEnd = new System.Windows.Forms.CheckBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.ChkStart = new System.Windows.Forms.CheckBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.label6 = new System.Windows.Forms.Label();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput2)).BeginInit();
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
            this.gridColumn2.DataPropertyName = "sjmc";
            this.gridColumn2.HeaderText = "试剂名称";
            this.gridColumn2.Name = "sjmc";
            this.gridColumn2.Width = 150;
            // 
            // gridColumn3
            // 
            this.gridColumn3.DataPropertyName = "ckl";
            this.gridColumn3.HeaderText = "出库量";
            this.gridColumn3.Name = "ckl";
            this.gridColumn3.Width = 120;
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "cksj";
            this.gridColumn4.HeaderText = "出库时间";
            this.gridColumn4.Name = "cksj";
            this.gridColumn4.Width = 150;
            // 
            // gridColumn5
            // 
            this.gridColumn5.DataPropertyName = "ckr";
            this.gridColumn5.HeaderText = "出库人";
            this.gridColumn5.Name = "ckr";
            // 
            // gridColumn6
            // 
            this.gridColumn6.DataPropertyName = "bz";
            this.gridColumn6.HeaderText = "备注";
            this.gridColumn6.Name = "bz";
            this.gridColumn6.Width = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.buttonX7);
            this.panel1.Controls.Add(this.doubleInput1);
            this.panel1.Controls.Add(this.doubleInput2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.dateTimePicker2);
            this.panel1.Controls.Add(this.chkEnd);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.dtEnd);
            this.panel1.Controls.Add(this.ChkStart);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.buttonX6);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 313);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 107);
            this.panel1.TabIndex = 1;
            // 
            // buttonX7
            // 
            this.buttonX7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX7.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX7.Location = new System.Drawing.Point(675, 12);
            this.buttonX7.Name = "buttonX7";
            this.buttonX7.Size = new System.Drawing.Size(81, 44);
            this.buttonX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX7.TabIndex = 81;
            this.buttonX7.Text = "导出Excel";
            this.buttonX7.Click += new System.EventHandler(this.buttonX7_Click);
            // 
            // doubleInput1
            // 
            // 
            // 
            // 
            this.doubleInput1.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInput1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInput1.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInput1.Increment = 1D;
            this.doubleInput1.Location = new System.Drawing.Point(359, 63);
            this.doubleInput1.MinValue = 0D;
            this.doubleInput1.Name = "doubleInput1";
            this.doubleInput1.ShowUpDown = true;
            this.doubleInput1.Size = new System.Drawing.Size(80, 23);
            this.doubleInput1.TabIndex = 79;
            // 
            // doubleInput2
            // 
            // 
            // 
            // 
            this.doubleInput2.BackgroundStyle.Class = "DateTimeInputBackground";
            this.doubleInput2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.doubleInput2.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.doubleInput2.Enabled = false;
            this.doubleInput2.Increment = 1D;
            this.doubleInput2.Location = new System.Drawing.Point(206, 64);
            this.doubleInput2.MinValue = 0D;
            this.doubleInput2.Name = "doubleInput2";
            this.doubleInput2.ShowUpDown = true;
            this.doubleInput2.Size = new System.Drawing.Size(80, 23);
            this.doubleInput2.TabIndex = 80;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(80, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(124, 22);
            this.comboBox1.TabIndex = 77;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(513, 63);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(100, 23);
            this.dateTimePicker2.TabIndex = 75;
            // 
            // chkEnd
            // 
            this.chkEnd.AutoSize = true;
            this.chkEnd.Checked = true;
            this.chkEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnd.Enabled = false;
            this.chkEnd.Location = new System.Drawing.Point(315, 28);
            this.chkEnd.Name = "chkEnd";
            this.chkEnd.Size = new System.Drawing.Size(40, 18);
            this.chkEnd.TabIndex = 73;
            this.chkEnd.TabStop = false;
            this.chkEnd.Text = "到";
            this.chkEnd.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "yyyy-MM-dd";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(183, 23);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(120, 23);
            this.dateTimePicker1.TabIndex = 71;
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(367, 23);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(114, 23);
            this.dtEnd.TabIndex = 72;
            // 
            // ChkStart
            // 
            this.ChkStart.AutoSize = true;
            this.ChkStart.Checked = true;
            this.ChkStart.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkStart.Enabled = false;
            this.ChkStart.Location = new System.Drawing.Point(128, 26);
            this.ChkStart.Name = "ChkStart";
            this.ChkStart.Size = new System.Drawing.Size(40, 18);
            this.ChkStart.TabIndex = 74;
            this.ChkStart.TabStop = false;
            this.ChkStart.Text = "从";
            this.ChkStart.UseVisualStyleBackColor = true;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(494, 12);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(61, 44);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 29;
            this.buttonX1.Text = "查询";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(442, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 14);
            this.label6.TabIndex = 27;
            this.label6.Text = "出库时间:";
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX6.Location = new System.Drawing.Point(587, 12);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(61, 44);
            this.buttonX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX6.TabIndex = 25;
            this.buttonX6.Text = "出库";
            this.buttonX6.Click += new System.EventHandler(this.buttonX6_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(815, 64);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(110, 23);
            this.textBox4.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(773, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "备注:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(694, 64);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(72, 23);
            this.textBox3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(622, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "出 库 人:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(289, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "出 库 量:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "试剂名称:";
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
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.superGridControl1.PrimaryGrid.VirtualMode = true;
            this.superGridControl1.PrimaryGrid.VirtualRowHeight = 30;
            this.superGridControl1.Size = new System.Drawing.Size(1014, 313);
            this.superGridControl1.TabIndex = 29;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // Frm_Sjckdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1014, 420);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Sjckdj";
            this.Text = "试剂出库登记管理";
            this.Load += new System.EventHandler(this.Frm_Sjckdj_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleInput2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.DateTimePicker dateTimePicker2;
        internal System.Windows.Forms.CheckBox chkEnd;
        internal System.Windows.Forms.DateTimePicker dateTimePicker1;
        internal System.Windows.Forms.DateTimePicker dtEnd;
        internal System.Windows.Forms.CheckBox ChkStart;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.ButtonX buttonX6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.Editors.DoubleInput doubleInput1;
        private System.Windows.Forms.ComboBox comboBox1;
        private DevComponents.Editors.DoubleInput doubleInput2;
        private DevComponents.DotNetBar.ButtonX buttonX7;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
    }
}