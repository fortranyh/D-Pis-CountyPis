using System.Windows.Forms;
namespace PIS_Sys.xtsz
{
    partial class Frm_Xtsz
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
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0xA1 && (int)m.WParam == 2)
            {
                m.Msg = 0x201;
                m.LParam = System.IntPtr.Zero;
            }
            base.WndProc(ref m);
        }
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Xtsz));
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel3 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.buttonX8 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX14 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX12 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX11 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX9 = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel4 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.pictureBox11 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.label13 = new System.Windows.Forms.Label();
            this.pictureBox7 = new System.Windows.Forms.PictureBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.PictureBox6 = new System.Windows.Forms.PictureBox();
            this.PictureBox5 = new System.Windows.Forms.PictureBox();
            this.PictureBox4 = new System.Windows.Forms.PictureBox();
            this.PictureBox3 = new System.Windows.Forms.PictureBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.PictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.PictureBox1 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel6 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonX7 = new DevComponents.DotNetBar.ButtonX();
            this.groupPanel5 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.switchButton18 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton7 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton16 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton5 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX22 = new DevComponents.DotNetBar.LabelX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            this.labelX21 = new DevComponents.DotNetBar.LabelX();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.Label16 = new System.Windows.Forms.Label();
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.switchButton1 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.cmbUsbType = new System.Windows.Forms.ComboBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.labelX33 = new DevComponents.DotNetBar.LabelX();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel3 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.switchButton3 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel2 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.switchButton17 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX20 = new DevComponents.DotNetBar.LabelX();
            this.switchButton14 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton12 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton11 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton8 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX31 = new DevComponents.DotNetBar.LabelX();
            this.labelX29 = new DevComponents.DotNetBar.LabelX();
            this.labelX28 = new DevComponents.DotNetBar.LabelX();
            this.labelX25 = new DevComponents.DotNetBar.LabelX();
            this.switchButton4 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.switchButton19 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton15 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton13 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton10 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton9 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.switchButton6 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.labelX24 = new DevComponents.DotNetBar.LabelX();
            this.labelX32 = new DevComponents.DotNetBar.LabelX();
            this.labelX30 = new DevComponents.DotNetBar.LabelX();
            this.labelX27 = new DevComponents.DotNetBar.LabelX();
            this.labelX26 = new DevComponents.DotNetBar.LabelX();
            this.labelX23 = new DevComponents.DotNetBar.LabelX();
            this.switchButton2 = new DevComponents.DotNetBar.Controls.SwitchButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.superTabItem3 = new DevComponents.DotNetBar.SuperTabItem();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.group10 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.buttonX16 = new DevComponents.DotNetBar.ButtonX();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.memo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CURRENT_VALUE = new DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn();
            this.INCREMENT = new DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn();
            this.memo_note = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.show_flag = new DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn();
            this.groupPanel9 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.buttonX15 = new DevComponents.DotNetBar.ButtonX();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.groupPanel7 = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.ftppwd = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ftpuser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.ftpPort = new DevComponents.Editors.IntegerInput();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.labip = new DevComponents.DotNetBar.LabelX();
            this.ftpIP = new DevComponents.Editors.IpAddressInput();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.superTabControlPanel3.SuspendLayout();
            this.groupPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).BeginInit();
            this.groupPanel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            this.groupPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.groupPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.groupPanel2.SuspendLayout();
            this.groupPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.superTabControlPanel2.SuspendLayout();
            this.group10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupPanel9.SuspendLayout();
            this.groupPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ftpPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ftpIP)).BeginInit();
            this.SuspendLayout();
            // 
            // superTabControl1
            // 
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Controls.Add(this.superTabControlPanel3);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.Location = new System.Drawing.Point(0, 0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(911, 601);
            this.superTabControl1.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Bottom;
            this.superTabControl1.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.superTabControl1.TabIndex = 0;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem3,
            this.superTabItem2});
            this.superTabControl1.Text = "本地参数维护";
            // 
            // superTabControlPanel3
            // 
            this.superTabControlPanel3.Controls.Add(this.buttonX8);
            this.superTabControlPanel3.Controls.Add(this.buttonX14);
            this.superTabControlPanel3.Controls.Add(this.buttonX12);
            this.superTabControlPanel3.Controls.Add(this.buttonX11);
            this.superTabControlPanel3.Controls.Add(this.buttonX9);
            this.superTabControlPanel3.Controls.Add(this.groupPanel4);
            this.superTabControlPanel3.Controls.Add(this.groupPanel6);
            this.superTabControlPanel3.Controls.Add(this.groupPanel5);
            this.superTabControlPanel3.Controls.Add(this.groupPanel3);
            this.superTabControlPanel3.Controls.Add(this.groupPanel2);
            this.superTabControlPanel3.Controls.Add(this.groupPanel1);
            this.superTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel3.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel3.Name = "superTabControlPanel3";
            this.superTabControlPanel3.Size = new System.Drawing.Size(911, 573);
            this.superTabControlPanel3.TabIndex = 0;
            this.superTabControlPanel3.TabItem = this.superTabItem3;
            // 
            // buttonX8
            // 
            this.buttonX8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX8.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX8.Location = new System.Drawing.Point(463, 519);
            this.buttonX8.Name = "buttonX8";
            this.buttonX8.Size = new System.Drawing.Size(184, 37);
            this.buttonX8.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX8.TabIndex = 15;
            this.buttonX8.Text = "融合接口直连HIS接口测试";
            this.buttonX8.Click += new System.EventHandler(this.buttonX8_Click);
            // 
            // buttonX14
            // 
            this.buttonX14.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX14.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX14.Location = new System.Drawing.Point(653, 519);
            this.buttonX14.Name = "buttonX14";
            this.buttonX14.Size = new System.Drawing.Size(206, 37);
            this.buttonX14.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX14.TabIndex = 5;
            this.buttonX14.Text = "蜡块包埋盒和玻片打号机接口";
            this.buttonX14.Click += new System.EventHandler(this.buttonX14_Click);
            // 
            // buttonX12
            // 
            this.buttonX12.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX12.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX12.Location = new System.Drawing.Point(123, 519);
            this.buttonX12.Name = "buttonX12";
            this.buttonX12.Size = new System.Drawing.Size(126, 37);
            this.buttonX12.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX12.TabIndex = 3;
            this.buttonX12.Text = "报告模板导入";
            this.buttonX12.Click += new System.EventHandler(this.buttonX12_Click);
            // 
            // buttonX11
            // 
            this.buttonX11.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX11.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX11.Location = new System.Drawing.Point(9, 519);
            this.buttonX11.Name = "buttonX11";
            this.buttonX11.Size = new System.Drawing.Size(108, 37);
            this.buttonX11.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX11.TabIndex = 3;
            this.buttonX11.Text = "大体模板导入";
            this.buttonX11.Click += new System.EventHandler(this.buttonX11_Click);
            // 
            // buttonX9
            // 
            this.buttonX9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX9.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX9.Location = new System.Drawing.Point(255, 519);
            this.buttonX9.Name = "buttonX9";
            this.buttonX9.Size = new System.Drawing.Size(202, 37);
            this.buttonX9.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX9.TabIndex = 14;
            this.buttonX9.Text = "诊断编码/临床符合字典维护";
            this.buttonX9.Click += new System.EventHandler(this.buttonX9_Click);
            // 
            // groupPanel4
            // 
            this.groupPanel4.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel4.Controls.Add(this.pictureBox11);
            this.groupPanel4.Controls.Add(this.pictureBox10);
            this.groupPanel4.Controls.Add(this.pictureBox9);
            this.groupPanel4.Controls.Add(this.pictureBox8);
            this.groupPanel4.Controls.Add(this.label13);
            this.groupPanel4.Controls.Add(this.pictureBox7);
            this.groupPanel4.Controls.Add(this.label14);
            this.groupPanel4.Controls.Add(this.label17);
            this.groupPanel4.Controls.Add(this.label18);
            this.groupPanel4.Controls.Add(this.label19);
            this.groupPanel4.Controls.Add(this.PictureBox6);
            this.groupPanel4.Controls.Add(this.PictureBox5);
            this.groupPanel4.Controls.Add(this.PictureBox4);
            this.groupPanel4.Controls.Add(this.PictureBox3);
            this.groupPanel4.Controls.Add(this.Label6);
            this.groupPanel4.Controls.Add(this.PictureBox2);
            this.groupPanel4.Controls.Add(this.label7);
            this.groupPanel4.Controls.Add(this.PictureBox1);
            this.groupPanel4.Controls.Add(this.label8);
            this.groupPanel4.Controls.Add(this.label9);
            this.groupPanel4.Controls.Add(this.label10);
            this.groupPanel4.Controls.Add(this.label11);
            this.groupPanel4.Controls.Add(this.comboBoxEx1);
            this.groupPanel4.Controls.Add(this.buttonX5);
            this.groupPanel4.Controls.Add(this.labelX8);
            this.groupPanel4.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel4.Location = new System.Drawing.Point(8, 3);
            this.groupPanel4.Name = "groupPanel4";
            this.groupPanel4.Size = new System.Drawing.Size(314, 206);
            // 
            // 
            // 
            this.groupPanel4.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel4.Style.BackColorGradientAngle = 90;
            this.groupPanel4.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel4.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderBottomWidth = 1;
            this.groupPanel4.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel4.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderLeftWidth = 1;
            this.groupPanel4.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderRightWidth = 1;
            this.groupPanel4.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel4.Style.BorderTopWidth = 1;
            this.groupPanel4.Style.CornerDiameter = 4;
            this.groupPanel4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel4.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel4.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel4.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel4.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel4.TabIndex = 2;
            this.groupPanel4.Text = "全局系统参数设置";
            // 
            // pictureBox11
            // 
            this.pictureBox11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox11.Location = new System.Drawing.Point(226, 121);
            this.pictureBox11.Name = "pictureBox11";
            this.pictureBox11.Size = new System.Drawing.Size(78, 20);
            this.pictureBox11.TabIndex = 29;
            this.pictureBox11.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox10.Location = new System.Drawing.Point(226, 98);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(78, 20);
            this.pictureBox10.TabIndex = 28;
            this.pictureBox10.TabStop = false;
            // 
            // pictureBox9
            // 
            this.pictureBox9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox9.Location = new System.Drawing.Point(226, 76);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(78, 20);
            this.pictureBox9.TabIndex = 31;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox8.Location = new System.Drawing.Point(226, 53);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(78, 20);
            this.pictureBox8.TabIndex = 30;
            this.pictureBox8.TabStop = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(159, 124);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 14);
            this.label13.TabIndex = 22;
            this.label13.Text = "归档报告";
            // 
            // pictureBox7
            // 
            this.pictureBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox7.Location = new System.Drawing.Point(226, 31);
            this.pictureBox7.Name = "pictureBox7";
            this.pictureBox7.Size = new System.Drawing.Size(78, 20);
            this.pictureBox7.TabIndex = 27;
            this.pictureBox7.TabStop = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(159, 101);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 14);
            this.label14.TabIndex = 21;
            this.label14.Text = "已打印";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(159, 78);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(49, 14);
            this.label17.TabIndex = 23;
            this.label17.Text = "已审核";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(159, 56);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(63, 14);
            this.label18.TabIndex = 25;
            this.label18.Text = "初步报告";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(159, 34);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(63, 14);
            this.label19.TabIndex = 24;
            this.label19.Text = "延时报告";
            // 
            // PictureBox6
            // 
            this.PictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox6.Location = new System.Drawing.Point(65, 136);
            this.PictureBox6.Name = "PictureBox6";
            this.PictureBox6.Size = new System.Drawing.Size(78, 20);
            this.PictureBox6.TabIndex = 14;
            this.PictureBox6.TabStop = false;
            // 
            // PictureBox5
            // 
            this.PictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox5.Location = new System.Drawing.Point(65, 115);
            this.PictureBox5.Name = "PictureBox5";
            this.PictureBox5.Size = new System.Drawing.Size(78, 20);
            this.PictureBox5.TabIndex = 17;
            this.PictureBox5.TabStop = false;
            // 
            // PictureBox4
            // 
            this.PictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox4.Location = new System.Drawing.Point(65, 94);
            this.PictureBox4.Name = "PictureBox4";
            this.PictureBox4.Size = new System.Drawing.Size(78, 20);
            this.PictureBox4.TabIndex = 16;
            this.PictureBox4.TabStop = false;
            // 
            // PictureBox3
            // 
            this.PictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox3.Location = new System.Drawing.Point(65, 73);
            this.PictureBox3.Name = "PictureBox3";
            this.PictureBox3.Size = new System.Drawing.Size(78, 20);
            this.PictureBox3.TabIndex = 19;
            this.PictureBox3.TabStop = false;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(10, 139);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(49, 14);
            this.Label6.TabIndex = 8;
            this.Label6.Text = "已制片";
            // 
            // PictureBox2
            // 
            this.PictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox2.Location = new System.Drawing.Point(65, 52);
            this.PictureBox2.Name = "PictureBox2";
            this.PictureBox2.Size = new System.Drawing.Size(78, 20);
            this.PictureBox2.TabIndex = 18;
            this.PictureBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 118);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 14);
            this.label7.TabIndex = 10;
            this.label7.Text = "已包埋";
            // 
            // PictureBox1
            // 
            this.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBox1.Location = new System.Drawing.Point(65, 31);
            this.PictureBox1.Name = "PictureBox1";
            this.PictureBox1.Size = new System.Drawing.Size(78, 20);
            this.PictureBox1.TabIndex = 15;
            this.PictureBox1.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 98);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 14);
            this.label8.TabIndex = 9;
            this.label8.Text = "已取材";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 14);
            this.label9.TabIndex = 11;
            this.label9.Text = "已接收";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 14);
            this.label10.TabIndex = 13;
            this.label10.Text = "申请";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(13, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 14);
            this.label11.TabIndex = 12;
            this.label11.Text = "预约";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 18;
            this.comboBoxEx1.Location = new System.Drawing.Point(83, 5);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(220, 24);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 7;
            this.comboBoxEx1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEx1_SelectedIndexChanged);
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX5.Location = new System.Drawing.Point(224, 147);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Size = new System.Drawing.Size(75, 30);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 6;
            this.buttonX5.Text = "保存";
            this.buttonX5.Click += new System.EventHandler(this.buttonX5_Click);
            // 
            // labelX8
            // 
            this.labelX8.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(14, 6);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(84, 23);
            this.labelX8.TabIndex = 1;
            this.labelX8.Text = "系统主题：";
            // 
            // groupPanel6
            // 
            this.groupPanel6.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel6.Controls.Add(this.textBoxX1);
            this.groupPanel6.Controls.Add(this.numericUpDown4);
            this.groupPanel6.Controls.Add(this.label12);
            this.groupPanel6.Controls.Add(this.label4);
            this.groupPanel6.Controls.Add(this.comboBox13);
            this.groupPanel6.Controls.Add(this.label5);
            this.groupPanel6.Controls.Add(this.buttonX7);
            this.groupPanel6.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel6.Location = new System.Drawing.Point(8, 369);
            this.groupPanel6.Name = "groupPanel6";
            this.groupPanel6.Size = new System.Drawing.Size(314, 144);
            // 
            // 
            // 
            this.groupPanel6.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel6.Style.BackColorGradientAngle = 90;
            this.groupPanel6.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel6.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderBottomWidth = 1;
            this.groupPanel6.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel6.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderLeftWidth = 1;
            this.groupPanel6.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderRightWidth = 1;
            this.groupPanel6.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel6.Style.BorderTopWidth = 1;
            this.groupPanel6.Style.CornerDiameter = 4;
            this.groupPanel6.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel6.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel6.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel6.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel6.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel6.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel6.TabIndex = 1;
            this.groupPanel6.Text = "特检管理模块参数";
            // 
            // textBoxX1
            // 
            // 
            // 
            // 
            this.textBoxX1.Border.Class = "TextBoxBorder";
            this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.textBoxX1.Location = new System.Drawing.Point(126, 62);
            this.textBoxX1.Name = "textBoxX1";
            this.textBoxX1.PreventEnterBeep = true;
            this.textBoxX1.Size = new System.Drawing.Size(173, 23);
            this.textBoxX1.TabIndex = 22;
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(126, 32);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(173, 23);
            this.numericUpDown4.TabIndex = 21;
            this.numericUpDown4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(4, 68);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(133, 14);
            this.label12.TabIndex = 20;
            this.label12.Text = "玻片条码科室名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(20, 38);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 14);
            this.label4.TabIndex = 20;
            this.label4.Text = "条码打印份数：";
            // 
            // comboBox13
            // 
            this.comboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Location = new System.Drawing.Point(123, 5);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(176, 22);
            this.comboBox13.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(10, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 14);
            this.label5.TabIndex = 18;
            this.label5.Text = "制片条码打印机：";
            // 
            // buttonX7
            // 
            this.buttonX7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX7.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX7.Location = new System.Drawing.Point(224, 91);
            this.buttonX7.Name = "buttonX7";
            this.buttonX7.Size = new System.Drawing.Size(75, 26);
            this.buttonX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX7.TabIndex = 6;
            this.buttonX7.Text = "保存";
            this.buttonX7.Click += new System.EventHandler(this.buttonX7_Click);
            // 
            // groupPanel5
            // 
            this.groupPanel5.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel5.Controls.Add(this.switchButton18);
            this.groupPanel5.Controls.Add(this.switchButton7);
            this.groupPanel5.Controls.Add(this.switchButton16);
            this.groupPanel5.Controls.Add(this.switchButton5);
            this.groupPanel5.Controls.Add(this.labelX22);
            this.groupPanel5.Controls.Add(this.labelX14);
            this.groupPanel5.Controls.Add(this.labelX18);
            this.groupPanel5.Controls.Add(this.labelX21);
            this.groupPanel5.Controls.Add(this.numericUpDown2);
            this.groupPanel5.Controls.Add(this.Label16);
            this.groupPanel5.Controls.Add(this.comboBox10);
            this.groupPanel5.Controls.Add(this.Label15);
            this.groupPanel5.Controls.Add(this.switchButton1);
            this.groupPanel5.Controls.Add(this.buttonX6);
            this.groupPanel5.Controls.Add(this.comboBox9);
            this.groupPanel5.Controls.Add(this.cmbUsbType);
            this.groupPanel5.Controls.Add(this.comboBox7);
            this.groupPanel5.Controls.Add(this.labelX13);
            this.groupPanel5.Controls.Add(this.labelX12);
            this.groupPanel5.Controls.Add(this.labelX33);
            this.groupPanel5.Controls.Add(this.labelX10);
            this.groupPanel5.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel5.Location = new System.Drawing.Point(328, 351);
            this.groupPanel5.Name = "groupPanel5";
            this.groupPanel5.Size = new System.Drawing.Size(576, 162);
            // 
            // 
            // 
            this.groupPanel5.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel5.Style.BackColorGradientAngle = 90;
            this.groupPanel5.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel5.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderBottomWidth = 1;
            this.groupPanel5.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel5.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderLeftWidth = 1;
            this.groupPanel5.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderRightWidth = 1;
            this.groupPanel5.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel5.Style.BorderTopWidth = 1;
            this.groupPanel5.Style.CornerDiameter = 4;
            this.groupPanel5.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel5.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel5.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel5.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel5.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel5.TabIndex = 1;
            this.groupPanel5.Text = "报告管理模块参数";
            // 
            // switchButton18
            // 
            // 
            // 
            // 
            this.switchButton18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton18.Location = new System.Drawing.Point(143, 106);
            this.switchButton18.Name = "switchButton18";
            this.switchButton18.Size = new System.Drawing.Size(66, 22);
            this.switchButton18.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton18.TabIndex = 5;
            // 
            // switchButton7
            // 
            // 
            // 
            // 
            this.switchButton7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton7.Location = new System.Drawing.Point(143, 80);
            this.switchButton7.Name = "switchButton7";
            this.switchButton7.Size = new System.Drawing.Size(66, 22);
            this.switchButton7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton7.TabIndex = 5;
            // 
            // switchButton16
            // 
            // 
            // 
            // 
            this.switchButton16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton16.Location = new System.Drawing.Point(360, 81);
            this.switchButton16.Name = "switchButton16";
            this.switchButton16.Size = new System.Drawing.Size(66, 22);
            this.switchButton16.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton16.TabIndex = 15;
            // 
            // switchButton5
            // 
            // 
            // 
            // 
            this.switchButton5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton5.Location = new System.Drawing.Point(360, 57);
            this.switchButton5.Name = "switchButton5";
            this.switchButton5.Size = new System.Drawing.Size(66, 22);
            this.switchButton5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton5.TabIndex = 15;
            // 
            // labelX22
            // 
            this.labelX22.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX22.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX22.Location = new System.Drawing.Point(6, 106);
            this.labelX22.Name = "labelX22";
            this.labelX22.Size = new System.Drawing.Size(162, 20);
            this.labelX22.TabIndex = 4;
            this.labelX22.Text = "是否谁取材报告谁写：";
            // 
            // labelX14
            // 
            this.labelX14.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(6, 82);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(138, 20);
            this.labelX14.TabIndex = 4;
            this.labelX14.Text = "是否编辑图片名称：";
            // 
            // labelX18
            // 
            this.labelX18.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Location = new System.Drawing.Point(220, 81);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(152, 21);
            this.labelX18.TabIndex = 16;
            this.labelX18.Text = "是否启动质控提醒：";
            // 
            // labelX21
            // 
            this.labelX21.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX21.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX21.Location = new System.Drawing.Point(220, 59);
            this.labelX21.Name = "labelX21";
            this.labelX21.Size = new System.Drawing.Size(152, 21);
            this.labelX21.TabIndex = 16;
            this.labelX21.Text = "是否加载大体图像：";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(520, 2);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(43, 23);
            this.numericUpDown2.TabIndex = 13;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Location = new System.Drawing.Point(413, 7);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(105, 14);
            this.Label16.TabIndex = 12;
            this.Label16.Text = "报告打印份数：";
            // 
            // comboBox10
            // 
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Location = new System.Drawing.Point(283, 3);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(124, 22);
            this.comboBox10.TabIndex = 11;
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.BackColor = System.Drawing.Color.Transparent;
            this.Label15.Location = new System.Drawing.Point(197, 7);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(91, 14);
            this.Label15.TabIndex = 10;
            this.Label15.Text = "报告打印机：";
            // 
            // switchButton1
            // 
            // 
            // 
            // 
            this.switchButton1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton1.Location = new System.Drawing.Point(143, 55);
            this.switchButton1.Name = "switchButton1";
            this.switchButton1.Size = new System.Drawing.Size(66, 22);
            this.switchButton1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton1.TabIndex = 7;
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX6.Location = new System.Drawing.Point(487, 73);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(76, 41);
            this.buttonX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX6.TabIndex = 6;
            this.buttonX6.Text = "保存";
            this.buttonX6.Click += new System.EventHandler(this.buttonX6_Click);
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Location = new System.Drawing.Point(334, 28);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(229, 22);
            this.comboBox9.TabIndex = 5;
            // 
            // cmbUsbType
            // 
            this.cmbUsbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUsbType.FormattingEnabled = true;
            this.cmbUsbType.Items.AddRange(new object[] {
            "通用",
            "官方"});
            this.cmbUsbType.Location = new System.Drawing.Point(122, 27);
            this.cmbUsbType.Name = "cmbUsbType";
            this.cmbUsbType.Size = new System.Drawing.Size(66, 22);
            this.cmbUsbType.TabIndex = 5;
            this.cmbUsbType.SelectedIndexChanged += new System.EventHandler(this.cmbUsbType_SelectedIndexChanged);
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "今天",
            "昨天",
            "三天",
            "四天",
            "五天",
            "六天",
            "一周",
            "两周",
            "三周",
            "一月"});
            this.comboBox7.Location = new System.Drawing.Point(102, 1);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(86, 22);
            this.comboBox7.TabIndex = 5;
            // 
            // labelX13
            // 
            this.labelX13.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(3, 59);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(152, 21);
            this.labelX13.TabIndex = 1;
            this.labelX13.Text = "是否主动打开摄像头：";
            // 
            // labelX12
            // 
            this.labelX12.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(200, 29);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(139, 23);
            this.labelX12.TabIndex = 1;
            this.labelX12.Text = "报告图像采集设备：";
            // 
            // labelX33
            // 
            this.labelX33.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX33.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX33.Location = new System.Drawing.Point(6, 26);
            this.labelX33.Name = "labelX33";
            this.labelX33.Size = new System.Drawing.Size(123, 23);
            this.labelX33.TabIndex = 1;
            this.labelX33.Text = "采集摄像头类型：";
            // 
            // labelX10
            // 
            this.labelX10.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(6, 1);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(123, 23);
            this.labelX10.TabIndex = 1;
            this.labelX10.Text = "刷新数据按钮：";
            // 
            // groupPanel3
            // 
            this.groupPanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel3.Controls.Add(this.switchButton3);
            this.groupPanel3.Controls.Add(this.labelX15);
            this.groupPanel3.Controls.Add(this.numericUpDown3);
            this.groupPanel3.Controls.Add(this.label1);
            this.groupPanel3.Controls.Add(this.comboBox11);
            this.groupPanel3.Controls.Add(this.label2);
            this.groupPanel3.Controls.Add(this.buttonX4);
            this.groupPanel3.Controls.Add(this.comboBox5);
            this.groupPanel3.Controls.Add(this.labelX7);
            this.groupPanel3.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel3.Location = new System.Drawing.Point(8, 212);
            this.groupPanel3.Name = "groupPanel3";
            this.groupPanel3.Size = new System.Drawing.Size(314, 154);
            // 
            // 
            // 
            this.groupPanel3.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel3.Style.BackColorGradientAngle = 90;
            this.groupPanel3.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel3.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderBottomWidth = 1;
            this.groupPanel3.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel3.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderLeftWidth = 1;
            this.groupPanel3.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderRightWidth = 1;
            this.groupPanel3.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel3.Style.BorderTopWidth = 1;
            this.groupPanel3.Style.CornerDiameter = 4;
            this.groupPanel3.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel3.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel3.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel3.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel3.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel3.TabIndex = 1;
            this.groupPanel3.Text = "制片管理模块参数";
            // 
            // switchButton3
            // 
            // 
            // 
            // 
            this.switchButton3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton3.Location = new System.Drawing.Point(120, 100);
            this.switchButton3.Name = "switchButton3";
            this.switchButton3.Size = new System.Drawing.Size(71, 22);
            this.switchButton3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton3.TabIndex = 19;
            // 
            // labelX15
            // 
            this.labelX15.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(17, 100);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(102, 23);
            this.labelX15.TabIndex = 18;
            this.labelX15.Text = "是否双联：";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(120, 64);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(164, 23);
            this.numericUpDown3.TabIndex = 17;
            this.numericUpDown3.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "条码打印份数：";
            // 
            // comboBox11
            // 
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Location = new System.Drawing.Point(117, 32);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(167, 22);
            this.comboBox11.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 14);
            this.label2.TabIndex = 14;
            this.label2.Text = "制片条码打印机：";
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX4.Location = new System.Drawing.Point(209, 94);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Size = new System.Drawing.Size(75, 30);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 6;
            this.buttonX4.Text = "保存";
            this.buttonX4.Click += new System.EventHandler(this.buttonX4_Click);
            // 
            // comboBox5
            // 
            this.comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "今天",
            "昨天",
            "三天",
            "四天",
            "五天",
            "六天",
            "一周",
            "两周",
            "三周",
            "一月"});
            this.comboBox5.Location = new System.Drawing.Point(117, 2);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(167, 22);
            this.comboBox5.TabIndex = 5;
            // 
            // labelX7
            // 
            this.labelX7.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(12, 1);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(123, 23);
            this.labelX7.TabIndex = 1;
            this.labelX7.Text = "刷新数据按钮：";
            // 
            // groupPanel2
            // 
            this.groupPanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel2.Controls.Add(this.switchButton17);
            this.groupPanel2.Controls.Add(this.labelX20);
            this.groupPanel2.Controls.Add(this.switchButton14);
            this.groupPanel2.Controls.Add(this.switchButton12);
            this.groupPanel2.Controls.Add(this.switchButton11);
            this.groupPanel2.Controls.Add(this.switchButton8);
            this.groupPanel2.Controls.Add(this.labelX31);
            this.groupPanel2.Controls.Add(this.labelX29);
            this.groupPanel2.Controls.Add(this.labelX28);
            this.groupPanel2.Controls.Add(this.labelX25);
            this.groupPanel2.Controls.Add(this.switchButton4);
            this.groupPanel2.Controls.Add(this.labelX19);
            this.groupPanel2.Controls.Add(this.comboBox12);
            this.groupPanel2.Controls.Add(this.label3);
            this.groupPanel2.Controls.Add(this.buttonX3);
            this.groupPanel2.Controls.Add(this.comboBox6);
            this.groupPanel2.Controls.Add(this.comboBox4);
            this.groupPanel2.Controls.Add(this.labelX9);
            this.groupPanel2.Controls.Add(this.labelX6);
            this.groupPanel2.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel2.Location = new System.Drawing.Point(621, 3);
            this.groupPanel2.Name = "groupPanel2";
            this.groupPanel2.Size = new System.Drawing.Size(283, 344);
            // 
            // 
            // 
            this.groupPanel2.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel2.Style.BackColorGradientAngle = 90;
            this.groupPanel2.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel2.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderBottomWidth = 1;
            this.groupPanel2.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel2.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderLeftWidth = 1;
            this.groupPanel2.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderRightWidth = 1;
            this.groupPanel2.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel2.Style.BorderTopWidth = 1;
            this.groupPanel2.Style.CornerDiameter = 4;
            this.groupPanel2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel2.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel2.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel2.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel2.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel2.TabIndex = 1;
            this.groupPanel2.Text = "取材管理模块参数";
            // 
            // switchButton17
            // 
            // 
            // 
            // 
            this.switchButton17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton17.Location = new System.Drawing.Point(153, 235);
            this.switchButton17.Name = "switchButton17";
            this.switchButton17.Size = new System.Drawing.Size(66, 22);
            this.switchButton17.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton17.TabIndex = 24;
            // 
            // labelX20
            // 
            this.labelX20.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX20.Location = new System.Drawing.Point(55, 234);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new System.Drawing.Size(107, 23);
            this.labelX20.TabIndex = 23;
            this.labelX20.Text = "数码相机模式：";
            // 
            // switchButton14
            // 
            // 
            // 
            // 
            this.switchButton14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton14.Location = new System.Drawing.Point(153, 210);
            this.switchButton14.Name = "switchButton14";
            this.switchButton14.Size = new System.Drawing.Size(66, 22);
            this.switchButton14.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton14.TabIndex = 22;
            // 
            // switchButton12
            // 
            // 
            // 
            // 
            this.switchButton12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton12.Location = new System.Drawing.Point(153, 185);
            this.switchButton12.Name = "switchButton12";
            this.switchButton12.Size = new System.Drawing.Size(66, 22);
            this.switchButton12.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton12.TabIndex = 22;
            // 
            // switchButton11
            // 
            // 
            // 
            // 
            this.switchButton11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton11.Location = new System.Drawing.Point(153, 160);
            this.switchButton11.Name = "switchButton11";
            this.switchButton11.Size = new System.Drawing.Size(66, 22);
            this.switchButton11.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton11.TabIndex = 22;
            // 
            // switchButton8
            // 
            // 
            // 
            // 
            this.switchButton8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton8.Location = new System.Drawing.Point(153, 131);
            this.switchButton8.Name = "switchButton8";
            this.switchButton8.Size = new System.Drawing.Size(66, 22);
            this.switchButton8.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton8.TabIndex = 21;
            // 
            // labelX31
            // 
            this.labelX31.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX31.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX31.Location = new System.Drawing.Point(29, 209);
            this.labelX31.Name = "labelX31";
            this.labelX31.Size = new System.Drawing.Size(135, 23);
            this.labelX31.TabIndex = 20;
            this.labelX31.Text = "取材信息自动保存：";
            // 
            // labelX29
            // 
            this.labelX29.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX29.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX29.Location = new System.Drawing.Point(29, 185);
            this.labelX29.Name = "labelX29";
            this.labelX29.Size = new System.Drawing.Size(135, 23);
            this.labelX29.TabIndex = 20;
            this.labelX29.Text = "材块保存自动核对：";
            // 
            // labelX28
            // 
            this.labelX28.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX28.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX28.Location = new System.Drawing.Point(2, 160);
            this.labelX28.Name = "labelX28";
            this.labelX28.Size = new System.Drawing.Size(164, 23);
            this.labelX28.TabIndex = 20;
            this.labelX28.Text = "取材信息自动添加一个：";
            // 
            // labelX25
            // 
            this.labelX25.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX25.Location = new System.Drawing.Point(41, 131);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new System.Drawing.Size(125, 23);
            this.labelX25.TabIndex = 20;
            this.labelX25.Text = "材块号是否自增：";
            // 
            // switchButton4
            // 
            // 
            // 
            // 
            this.switchButton4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton4.Location = new System.Drawing.Point(153, 103);
            this.switchButton4.Name = "switchButton4";
            this.switchButton4.Size = new System.Drawing.Size(66, 22);
            this.switchButton4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton4.TabIndex = 19;
            // 
            // labelX19
            // 
            this.labelX19.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX19.Location = new System.Drawing.Point(13, 102);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new System.Drawing.Size(152, 23);
            this.labelX19.TabIndex = 18;
            this.labelX19.Text = "是否主动打开摄像头：";
            // 
            // comboBox12
            // 
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Location = new System.Drawing.Point(132, 72);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(142, 22);
            this.comboBox12.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(5, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "待制片打印机：";
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX3.Location = new System.Drawing.Point(196, 270);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Size = new System.Drawing.Size(78, 39);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 6;
            this.buttonX3.Text = "保存";
            this.buttonX3.Click += new System.EventHandler(this.buttonX3_Click);
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Location = new System.Drawing.Point(132, 43);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(142, 22);
            this.comboBox6.TabIndex = 5;
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "今天",
            "昨天",
            "三天",
            "四天",
            "五天",
            "六天",
            "一周",
            "两周",
            "三周",
            "一月"});
            this.comboBox4.Location = new System.Drawing.Point(132, 14);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(142, 22);
            this.comboBox4.TabIndex = 5;
            // 
            // labelX9
            // 
            this.labelX9.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(3, 42);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(122, 23);
            this.labelX9.TabIndex = 1;
            this.labelX9.Text = "大体采集摄像头：";
            // 
            // labelX6
            // 
            this.labelX6.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(3, 13);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(122, 23);
            this.labelX6.TabIndex = 1;
            this.labelX6.Text = "刷新数据按钮：";
            // 
            // groupPanel1
            // 
            this.groupPanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel1.Controls.Add(this.switchButton19);
            this.groupPanel1.Controls.Add(this.switchButton15);
            this.groupPanel1.Controls.Add(this.switchButton13);
            this.groupPanel1.Controls.Add(this.switchButton10);
            this.groupPanel1.Controls.Add(this.switchButton9);
            this.groupPanel1.Controls.Add(this.switchButton6);
            this.groupPanel1.Controls.Add(this.labelX24);
            this.groupPanel1.Controls.Add(this.labelX32);
            this.groupPanel1.Controls.Add(this.labelX30);
            this.groupPanel1.Controls.Add(this.labelX27);
            this.groupPanel1.Controls.Add(this.labelX26);
            this.groupPanel1.Controls.Add(this.labelX23);
            this.groupPanel1.Controls.Add(this.switchButton2);
            this.groupPanel1.Controls.Add(this.numericUpDown1);
            this.groupPanel1.Controls.Add(this.comboBox3);
            this.groupPanel1.Controls.Add(this.comboBox2);
            this.groupPanel1.Controls.Add(this.comboBox1);
            this.groupPanel1.Controls.Add(this.buttonX1);
            this.groupPanel1.Controls.Add(this.labelX5);
            this.groupPanel1.Controls.Add(this.labelX4);
            this.groupPanel1.Controls.Add(this.labelX3);
            this.groupPanel1.Controls.Add(this.labelX2);
            this.groupPanel1.Controls.Add(this.labelX1);
            this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel1.Location = new System.Drawing.Point(328, 3);
            this.groupPanel1.Name = "groupPanel1";
            this.groupPanel1.Size = new System.Drawing.Size(287, 344);
            // 
            // 
            // 
            this.groupPanel1.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel1.Style.BackColorGradientAngle = 90;
            this.groupPanel1.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel1.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderBottomWidth = 1;
            this.groupPanel1.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel1.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderLeftWidth = 1;
            this.groupPanel1.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderRightWidth = 1;
            this.groupPanel1.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel1.Style.BorderTopWidth = 1;
            this.groupPanel1.Style.CornerDiameter = 4;
            this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel1.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel1.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel1.TabIndex = 0;
            this.groupPanel1.Text = "标本登记模块参数";
            // 
            // switchButton19
            // 
            // 
            // 
            // 
            this.switchButton19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton19.Location = new System.Drawing.Point(122, 293);
            this.switchButton19.Name = "switchButton19";
            this.switchButton19.Size = new System.Drawing.Size(66, 22);
            this.switchButton19.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton19.TabIndex = 13;
            // 
            // switchButton15
            // 
            // 
            // 
            // 
            this.switchButton15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton15.Location = new System.Drawing.Point(102, 269);
            this.switchButton15.Name = "switchButton15";
            this.switchButton15.Size = new System.Drawing.Size(66, 22);
            this.switchButton15.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton15.TabIndex = 13;
            // 
            // switchButton13
            // 
            // 
            // 
            // 
            this.switchButton13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton13.Location = new System.Drawing.Point(145, 241);
            this.switchButton13.Name = "switchButton13";
            this.switchButton13.Size = new System.Drawing.Size(66, 22);
            this.switchButton13.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton13.TabIndex = 12;
            // 
            // switchButton10
            // 
            // 
            // 
            // 
            this.switchButton10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton10.Location = new System.Drawing.Point(145, 210);
            this.switchButton10.Name = "switchButton10";
            this.switchButton10.Size = new System.Drawing.Size(66, 22);
            this.switchButton10.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton10.TabIndex = 11;
            // 
            // switchButton9
            // 
            // 
            // 
            // 
            this.switchButton9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton9.Location = new System.Drawing.Point(135, 184);
            this.switchButton9.Name = "switchButton9";
            this.switchButton9.Size = new System.Drawing.Size(66, 22);
            this.switchButton9.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton9.TabIndex = 11;
            // 
            // switchButton6
            // 
            // 
            // 
            // 
            this.switchButton6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton6.Location = new System.Drawing.Point(135, 158);
            this.switchButton6.Name = "switchButton6";
            this.switchButton6.Size = new System.Drawing.Size(66, 22);
            this.switchButton6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton6.TabIndex = 9;
            // 
            // labelX24
            // 
            this.labelX24.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX24.Location = new System.Drawing.Point(6, 292);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new System.Drawing.Size(143, 23);
            this.labelX24.TabIndex = 10;
            this.labelX24.Text = "HIS基本信息提取：";
            // 
            // labelX32
            // 
            this.labelX32.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX32.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX32.Location = new System.Drawing.Point(6, 269);
            this.labelX32.Name = "labelX32";
            this.labelX32.Size = new System.Drawing.Size(123, 23);
            this.labelX32.TabIndex = 10;
            this.labelX32.Text = "是否提示合并：";
            // 
            // labelX30
            // 
            this.labelX30.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX30.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX30.Location = new System.Drawing.Point(4, 240);
            this.labelX30.Name = "labelX30";
            this.labelX30.Size = new System.Drawing.Size(154, 23);
            this.labelX30.TabIndex = 10;
            this.labelX30.Text = "病理号是否自动生成：";
            // 
            // labelX27
            // 
            this.labelX27.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX27.Location = new System.Drawing.Point(4, 211);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new System.Drawing.Size(154, 23);
            this.labelX27.TabIndex = 10;
            this.labelX27.Text = "新建病人开启新窗体：";
            // 
            // labelX26
            // 
            this.labelX26.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX26.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX26.Location = new System.Drawing.Point(4, 183);
            this.labelX26.Name = "labelX26";
            this.labelX26.Size = new System.Drawing.Size(154, 23);
            this.labelX26.TabIndex = 10;
            this.labelX26.Text = "登记是否区分类别：";
            // 
            // labelX23
            // 
            this.labelX23.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX23.Location = new System.Drawing.Point(4, 158);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new System.Drawing.Size(144, 23);
            this.labelX23.TabIndex = 10;
            this.labelX23.Text = "标本是否必须登记：";
            // 
            // switchButton2
            // 
            // 
            // 
            // 
            this.switchButton2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.switchButton2.Location = new System.Drawing.Point(132, 70);
            this.switchButton2.Name = "switchButton2";
            this.switchButton2.Size = new System.Drawing.Size(66, 22);
            this.switchButton2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.switchButton2.TabIndex = 8;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(132, 102);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(143, 23);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "今天",
            "昨天",
            "三天",
            "四天",
            "五天",
            "六天",
            "一周",
            "两周",
            "三周",
            "一月"});
            this.comboBox3.Location = new System.Drawing.Point(132, 128);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(143, 22);
            this.comboBox3.TabIndex = 4;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(132, 43);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(143, 22);
            this.comboBox2.TabIndex = 4;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "扫描条码",
            "姓名",
            "快速检索"});
            this.comboBox1.Location = new System.Drawing.Point(132, 14);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(143, 22);
            this.comboBox1.TabIndex = 3;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(200, 270);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(78, 41);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelX5
            // 
            this.labelX5.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(3, 128);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(123, 23);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "刷新数据按钮：";
            // 
            // labelX4
            // 
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(3, 99);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(102, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "打印份数：";
            // 
            // labelX3
            // 
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(3, 70);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(102, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "是否打印：";
            // 
            // labelX2
            // 
            this.labelX2.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(3, 41);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(102, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "条码打印机：";
            // 
            // labelX1
            // 
            this.labelX1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(3, 12);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(123, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "清空后光标位置：";
            // 
            // superTabItem3
            // 
            this.superTabItem3.AttachedControl = this.superTabControlPanel3;
            this.superTabItem3.GlobalItem = false;
            this.superTabItem3.Name = "superTabItem3";
            this.superTabItem3.Text = "本地参数维护";
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Controls.Add(this.group10);
            this.superTabControlPanel2.Controls.Add(this.groupPanel9);
            this.superTabControlPanel2.Controls.Add(this.groupPanel7);
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 0);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(911, 573);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // group10
            // 
            this.group10.CanvasColor = System.Drawing.SystemColors.Control;
            this.group10.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.group10.Controls.Add(this.buttonX16);
            this.group10.Controls.Add(this.dataGridView1);
            this.group10.DisabledBackColor = System.Drawing.Color.Empty;
            this.group10.Location = new System.Drawing.Point(533, 3);
            this.group10.Name = "group10";
            this.group10.Size = new System.Drawing.Size(371, 219);
            // 
            // 
            // 
            this.group10.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.group10.Style.BackColorGradientAngle = 90;
            this.group10.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.group10.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.group10.Style.BorderBottomWidth = 1;
            this.group10.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.group10.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.group10.Style.BorderLeftWidth = 1;
            this.group10.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.group10.Style.BorderRightWidth = 1;
            this.group10.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.group10.Style.BorderTopWidth = 1;
            this.group10.Style.CornerDiameter = 4;
            this.group10.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.group10.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.group10.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.group10.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.group10.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.group10.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.group10.TabIndex = 4;
            this.group10.Text = "病理号调整";
            // 
            // buttonX16
            // 
            this.buttonX16.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX16.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX16.Location = new System.Drawing.Point(135, 153);
            this.buttonX16.Name = "buttonX16";
            this.buttonX16.Size = new System.Drawing.Size(82, 39);
            this.buttonX16.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX16.TabIndex = 1;
            this.buttonX16.Text = "保存";
            this.buttonX16.Click += new System.EventHandler(this.buttonX16_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightSteelBlue;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NAME,
            this.memo,
            this.CURRENT_VALUE,
            this.INCREMENT,
            this.memo_note,
            this.show_flag});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(365, 151);
            this.dataGridView1.TabIndex = 0;
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "名称";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.Visible = false;
            // 
            // memo
            // 
            this.memo.DataPropertyName = "memo";
            this.memo.HeaderText = "检查类型名称";
            this.memo.Name = "memo";
            this.memo.ReadOnly = true;
            this.memo.Width = 150;
            // 
            // CURRENT_VALUE
            // 
            // 
            // 
            // 
            this.CURRENT_VALUE.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.CURRENT_VALUE.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.CURRENT_VALUE.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.CURRENT_VALUE.BackgroundStyle.TextColor = System.Drawing.SystemColors.ControlText;
            this.CURRENT_VALUE.DataPropertyName = "CURRENT_VALUE";
            this.CURRENT_VALUE.HeaderText = "当前流水号";
            this.CURRENT_VALUE.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.CURRENT_VALUE.Name = "CURRENT_VALUE";
            this.CURRENT_VALUE.Width = 120;
            // 
            // INCREMENT
            // 
            // 
            // 
            // 
            this.INCREMENT.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.INCREMENT.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.INCREMENT.DataPropertyName = "INCREMENT";
            this.INCREMENT.HeaderText = "INCREMENT";
            this.INCREMENT.Name = "INCREMENT";
            this.INCREMENT.ReadOnly = true;
            this.INCREMENT.Visible = false;
            // 
            // memo_note
            // 
            this.memo_note.DataPropertyName = "memo_note";
            this.memo_note.HeaderText = "memo_note";
            this.memo_note.Name = "memo_note";
            this.memo_note.ReadOnly = true;
            this.memo_note.Visible = false;
            // 
            // show_flag
            // 
            // 
            // 
            // 
            this.show_flag.BackgroundStyle.Class = "DataGridViewNumericBorder";
            this.show_flag.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.show_flag.DataPropertyName = "show_flag";
            this.show_flag.HeaderText = "show_flag";
            this.show_flag.Name = "show_flag";
            this.show_flag.ReadOnly = true;
            this.show_flag.Visible = false;
            // 
            // groupPanel9
            // 
            this.groupPanel9.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel9.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel9.Controls.Add(this.buttonX15);
            this.groupPanel9.Controls.Add(this.checkedListBox1);
            this.groupPanel9.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel9.Location = new System.Drawing.Point(343, 3);
            this.groupPanel9.Name = "groupPanel9";
            this.groupPanel9.Size = new System.Drawing.Size(184, 219);
            // 
            // 
            // 
            this.groupPanel9.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel9.Style.BackColorGradientAngle = 90;
            this.groupPanel9.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel9.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderBottomWidth = 1;
            this.groupPanel9.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel9.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderLeftWidth = 1;
            this.groupPanel9.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderRightWidth = 1;
            this.groupPanel9.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel9.Style.BorderTopWidth = 1;
            this.groupPanel9.Style.CornerDiameter = 4;
            this.groupPanel9.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel9.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel9.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel9.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel9.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel9.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel9.TabIndex = 3;
            this.groupPanel9.Text = "工作站类型维护";
            // 
            // buttonX15
            // 
            this.buttonX15.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX15.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX15.Location = new System.Drawing.Point(44, 153);
            this.buttonX15.Name = "buttonX15";
            this.buttonX15.Size = new System.Drawing.Size(90, 39);
            this.buttonX15.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX15.TabIndex = 1;
            this.buttonX15.Text = "保存";
            this.buttonX15.Click += new System.EventHandler(this.buttonX15_Click);
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(4, 3);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(169, 148);
            this.checkedListBox1.TabIndex = 0;
            // 
            // groupPanel7
            // 
            this.groupPanel7.CanvasColor = System.Drawing.SystemColors.Control;
            this.groupPanel7.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.groupPanel7.Controls.Add(this.buttonX2);
            this.groupPanel7.Controls.Add(this.ftppwd);
            this.groupPanel7.Controls.Add(this.ftpuser);
            this.groupPanel7.Controls.Add(this.ftpPort);
            this.groupPanel7.Controls.Add(this.labelX17);
            this.groupPanel7.Controls.Add(this.labelX16);
            this.groupPanel7.Controls.Add(this.labelX11);
            this.groupPanel7.Controls.Add(this.labip);
            this.groupPanel7.Controls.Add(this.ftpIP);
            this.groupPanel7.DisabledBackColor = System.Drawing.Color.Empty;
            this.groupPanel7.Location = new System.Drawing.Point(8, 3);
            this.groupPanel7.Name = "groupPanel7";
            this.groupPanel7.Size = new System.Drawing.Size(329, 219);
            // 
            // 
            // 
            this.groupPanel7.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.groupPanel7.Style.BackColorGradientAngle = 90;
            this.groupPanel7.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.groupPanel7.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderBottomWidth = 1;
            this.groupPanel7.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.groupPanel7.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderLeftWidth = 1;
            this.groupPanel7.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderRightWidth = 1;
            this.groupPanel7.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.groupPanel7.Style.BorderTopWidth = 1;
            this.groupPanel7.Style.CornerDiameter = 4;
            this.groupPanel7.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.groupPanel7.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.groupPanel7.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.groupPanel7.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.groupPanel7.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.groupPanel7.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.groupPanel7.TabIndex = 2;
            this.groupPanel7.Text = "FTP服务器设置";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(108, 138);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(110, 45);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 4;
            this.buttonX2.Text = "保存";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // ftppwd
            // 
            // 
            // 
            // 
            this.ftppwd.Border.Class = "TextBoxBorder";
            this.ftppwd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ftppwd.Location = new System.Drawing.Point(121, 104);
            this.ftppwd.Name = "ftppwd";
            this.ftppwd.PasswordChar = '*';
            this.ftppwd.PreventEnterBeep = true;
            this.ftppwd.Size = new System.Drawing.Size(164, 23);
            this.ftppwd.TabIndex = 3;
            // 
            // ftpuser
            // 
            // 
            // 
            // 
            this.ftpuser.Border.Class = "TextBoxBorder";
            this.ftpuser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ftpuser.Location = new System.Drawing.Point(121, 75);
            this.ftpuser.Name = "ftpuser";
            this.ftpuser.PreventEnterBeep = true;
            this.ftpuser.Size = new System.Drawing.Size(164, 23);
            this.ftpuser.TabIndex = 3;
            // 
            // ftpPort
            // 
            // 
            // 
            // 
            this.ftpPort.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ftpPort.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ftpPort.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ftpPort.Location = new System.Drawing.Point(121, 46);
            this.ftpPort.Name = "ftpPort";
            this.ftpPort.ShowUpDown = true;
            this.ftpPort.Size = new System.Drawing.Size(80, 23);
            this.ftpPort.TabIndex = 2;
            this.ftpPort.Value = 21;
            // 
            // labelX17
            // 
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(40, 104);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(75, 23);
            this.labelX17.TabIndex = 1;
            this.labelX17.Text = "密码";
            // 
            // labelX16
            // 
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(40, 75);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(75, 23);
            this.labelX16.TabIndex = 1;
            this.labelX16.Text = "用户名";
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(40, 46);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(75, 23);
            this.labelX11.TabIndex = 1;
            this.labelX11.Text = "端口";
            // 
            // labip
            // 
            // 
            // 
            // 
            this.labip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labip.Location = new System.Drawing.Point(40, 17);
            this.labip.Name = "labip";
            this.labip.Size = new System.Drawing.Size(75, 23);
            this.labip.TabIndex = 1;
            this.labip.Text = "IP地址";
            // 
            // ftpIP
            // 
            this.ftpIP.AutoOverwrite = true;
            // 
            // 
            // 
            this.ftpIP.BackgroundStyle.Class = "DateTimeInputBackground";
            this.ftpIP.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ftpIP.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.ftpIP.ButtonFreeText.Visible = true;
            this.ftpIP.Location = new System.Drawing.Point(121, 17);
            this.ftpIP.Name = "ftpIP";
            this.ftpIP.Size = new System.Drawing.Size(186, 23);
            this.ftpIP.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ftpIP.TabIndex = 0;
            this.ftpIP.Value = "127.0.0.1";
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.Text = "服务端参数维护";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Frm_Xtsz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(911, 601);
            this.Controls.Add(this.superTabControl1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Xtsz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "病理中心管理系统";
            this.Load += new System.EventHandler(this.Frm_Xtsz_Load);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.superTabControlPanel3.ResumeLayout(false);
            this.groupPanel4.ResumeLayout(false);
            this.groupPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox1)).EndInit();
            this.groupPanel6.ResumeLayout(false);
            this.groupPanel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            this.groupPanel5.ResumeLayout(false);
            this.groupPanel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.groupPanel3.ResumeLayout(false);
            this.groupPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.groupPanel2.ResumeLayout(false);
            this.groupPanel2.PerformLayout();
            this.groupPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.superTabControlPanel2.ResumeLayout(false);
            this.group10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupPanel9.ResumeLayout(false);
            this.groupPanel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ftpPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ftpIP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel3;
        private DevComponents.DotNetBar.SuperTabItem superTabItem3;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox2;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ComboBox comboBox3;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel2;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private System.Windows.Forms.ComboBox comboBox4;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private System.Windows.Forms.ComboBox comboBox5;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private DevComponents.DotNetBar.LabelX labelX8;
        private System.Windows.Forms.ComboBox comboBox6;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel6;
        private DevComponents.DotNetBar.ButtonX buttonX7;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel5;
        private DevComponents.DotNetBar.ButtonX buttonX6;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.ComboBox comboBox7;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton1;
        internal System.Windows.Forms.NumericUpDown numericUpDown2;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.ComboBox comboBox10;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.NumericUpDown numericUpDown3;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.ComboBox comboBox11;
        internal System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton2;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton3;
        private DevComponents.DotNetBar.LabelX labelX15;
        internal System.Windows.Forms.ComboBox comboBox12;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.NumericUpDown numericUpDown4;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox comboBox13;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.PictureBox pictureBox11;
        internal System.Windows.Forms.PictureBox pictureBox10;
        internal System.Windows.Forms.PictureBox pictureBox9;
        internal System.Windows.Forms.PictureBox pictureBox8;
        internal System.Windows.Forms.Label label13;
        internal System.Windows.Forms.PictureBox pictureBox7;
        internal System.Windows.Forms.Label label14;
        internal System.Windows.Forms.Label label17;
        internal System.Windows.Forms.Label label18;
        internal System.Windows.Forms.Label label19;
        internal System.Windows.Forms.PictureBox PictureBox6;
        internal System.Windows.Forms.PictureBox PictureBox5;
        internal System.Windows.Forms.PictureBox PictureBox4;
        internal System.Windows.Forms.PictureBox PictureBox3;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.PictureBox PictureBox2;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.PictureBox PictureBox1;
        internal System.Windows.Forms.Label label8;
        internal System.Windows.Forms.Label label9;
        internal System.Windows.Forms.Label label10;
        internal System.Windows.Forms.Label label11;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel7;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private DevComponents.DotNetBar.Controls.TextBoxX ftppwd;
        private DevComponents.DotNetBar.Controls.TextBoxX ftpuser;
        private DevComponents.Editors.IntegerInput ftpPort;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.LabelX labelX16;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.LabelX labip;
        private DevComponents.Editors.IpAddressInput ftpIP;
        private DevComponents.DotNetBar.ButtonX buttonX9;
        private DevComponents.DotNetBar.ButtonX buttonX12;
        private DevComponents.DotNetBar.ButtonX buttonX11;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton4;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton5;
        private DevComponents.DotNetBar.LabelX labelX21;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton6;
        private DevComponents.DotNetBar.LabelX labelX23;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton7;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton8;
        private DevComponents.DotNetBar.LabelX labelX25;
        private DevComponents.DotNetBar.LabelX labelX27;
        private DevComponents.DotNetBar.LabelX labelX26;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton10;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton9;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton12;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton11;
        private DevComponents.DotNetBar.LabelX labelX29;
        private DevComponents.DotNetBar.LabelX labelX28;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton13;
        private DevComponents.DotNetBar.LabelX labelX30;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton14;
        private DevComponents.DotNetBar.LabelX labelX31;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton15;
        private DevComponents.DotNetBar.LabelX labelX32;
        private DevComponents.DotNetBar.ButtonX buttonX14;
        private DevComponents.DotNetBar.Controls.GroupPanel groupPanel9;
        private DevComponents.DotNetBar.ButtonX buttonX15;
        private CheckedListBox checkedListBox1;
        private DevComponents.DotNetBar.Controls.GroupPanel group10;
        private DevComponents.DotNetBar.ButtonX buttonX16;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn NAME;
        private DataGridViewTextBoxColumn memo;
        private DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn CURRENT_VALUE;
        private DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn INCREMENT;
        private DataGridViewTextBoxColumn memo_note;
        private DevComponents.DotNetBar.Controls.DataGridViewIntegerInputColumn show_flag;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton16;
        private DevComponents.DotNetBar.LabelX labelX18;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton18;
        private DevComponents.DotNetBar.LabelX labelX22;
        private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
        internal Label label12;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton19;
        private DevComponents.DotNetBar.LabelX labelX24;
        private DevComponents.DotNetBar.ButtonX buttonX8;
        private DevComponents.DotNetBar.Controls.SwitchButton switchButton17;
        private DevComponents.DotNetBar.LabelX labelX20;
        private ComboBox cmbUsbType;
        private DevComponents.DotNetBar.LabelX labelX33;
    }
}