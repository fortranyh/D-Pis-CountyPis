namespace PIS_Sys.qcgl
{
    partial class FrmCkhd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCkhd));
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
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.ribbonBar1 = new DevComponents.DotNetBar.RibbonBar();
            this.btn_ckhd = new DevComponents.DotNetBar.ButtonItem();
            this.btn_clear = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btn_all = new DevComponents.DotNetBar.ButtonItem();
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
            this.gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn3.DataPropertyName = "meterial_no";
            this.gridColumn3.HeaderText = "蜡块号";
            this.gridColumn3.Name = "meterial_no";
            this.gridColumn3.ReadOnly = true;
            this.gridColumn3.Width = 50;
            // 
            // gridColumn4
            // 
            this.gridColumn4.DataPropertyName = "barcode";
            this.gridColumn4.HeaderText = "蜡块条码";
            this.gridColumn4.Name = "barcode";
            this.gridColumn4.ReadOnly = true;
            // 
            // gridColumn5
            // 
            this.gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn5.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn5.DataPropertyName = "specimens_class";
            this.gridColumn5.HeaderText = "标本大类";
            this.gridColumn5.Name = "specimens_class";
            this.gridColumn5.ReadOnly = true;
            this.gridColumn5.Width = 80;
            // 
            // gridColumn6
            // 
            this.gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn6.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn6.DataPropertyName = "work_source";
            this.gridColumn6.HeaderText = "任务来源";
            this.gridColumn6.Name = "work_source";
            this.gridColumn6.ReadOnly = true;
            // 
            // gridColumn7
            // 
            this.gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn7.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn7.DataPropertyName = "group_num";
            this.gridColumn7.HeaderText = "组织块数";
            this.gridColumn7.Name = "group_num";
            this.gridColumn7.ReadOnly = true;
            this.gridColumn7.Width = 60;
            // 
            // gridColumn8
            // 
            this.gridColumn8.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn8.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn8.DataPropertyName = "group_unite";
            this.gridColumn8.HeaderText = "单位";
            this.gridColumn8.Name = "group_unite";
            this.gridColumn8.ReadOnly = true;
            this.gridColumn8.Width = 40;
            // 
            // gridColumn9
            // 
            this.gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn9.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn9.DataPropertyName = "draw_doctor_name";
            this.gridColumn9.HeaderText = "取材医生";
            this.gridColumn9.Name = "draw_doctor_name";
            this.gridColumn9.ReadOnly = true;
            this.gridColumn9.Width = 80;
            // 
            // gridColumn10
            // 
            this.gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn10.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn10.DataPropertyName = "parts";
            this.gridColumn10.HeaderText = "取材部位";
            this.gridColumn10.Name = "parts";
            this.gridColumn10.ReadOnly = true;
            this.gridColumn10.Width = 150;
            // 
            // gridColumn11
            // 
            this.gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn11.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn11.DataPropertyName = "hd_flag";
            this.gridColumn11.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
            this.gridColumn11.HeaderText = "验收";
            this.gridColumn11.Name = "hd_flag";
            this.gridColumn11.Width = 40;
            // 
            // gridColumn12
            // 
            this.gridColumn12.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn12.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.gridColumn12.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn12.CellStyles.Default.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn12.DataPropertyName = "memo_note";
            this.gridColumn12.HeaderText = "备注";
            this.gridColumn12.Name = "memo_note";
            this.gridColumn12.ReadOnly = true;
            this.gridColumn12.Width = 200;
            // 
            // gridColumn13
            // 
            this.gridColumn13.DataPropertyName = "sfys";
            this.gridColumn13.HeaderText = "sfys";
            this.gridColumn13.Name = "sfys";
            this.gridColumn13.Visible = false;
            // 
            // gridColumn14
            // 
            this.gridColumn14.DataPropertyName = "bm_doc_name";
            this.gridColumn14.HeaderText = "bm_doc_name";
            this.gridColumn14.Name = "bm_doc_name";
            this.gridColumn14.Visible = false;
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
            this.superGridControl1.PrimaryGrid.MinRowHeight = 30;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.RowWithCellHighlight;
            this.superGridControl1.PrimaryGrid.ShowRowHeaders = false;
            this.superGridControl1.Size = new System.Drawing.Size(944, 555);
            this.superGridControl1.TabIndex = 0;
            this.superGridControl1.Text = "superGridControl1";
            // 
            // ribbonBar1
            // 
            this.ribbonBar1.AutoOverflowEnabled = true;
            this.ribbonBar1.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundMouseOverStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar1.ContainerControlProcessDialogKey = true;
            this.ribbonBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ribbonBar1.DragDropSupport = true;
            this.ribbonBar1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ribbonBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btn_ckhd,
            this.btn_clear,
            this.buttonItem1,
            this.btn_all});
            this.ribbonBar1.ItemSpacing = 20;
            this.ribbonBar1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.ribbonBar1.Location = new System.Drawing.Point(0, 555);
            this.ribbonBar1.Name = "ribbonBar1";
            this.ribbonBar1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ribbonBar1.Size = new System.Drawing.Size(944, 46);
            this.ribbonBar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonBar1.TabIndex = 40;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.ribbonBar1.TitleStyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonBar1.TitleVisible = false;
            // 
            // btn_ckhd
            // 
            this.btn_ckhd.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btn_ckhd.FontBold = true;
            this.btn_ckhd.Image = global::PIS_Sys.Properties.Resources.jczt;
            this.btn_ckhd.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_ckhd.Name = "btn_ckhd";
            this.btn_ckhd.SubItemsExpandWidth = 14;
            this.btn_ckhd.Text = "核对";
            this.btn_ckhd.Click += new System.EventHandler(this.btn_ckhd_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btn_clear.FontBold = true;
            this.btn_clear.Image = global::PIS_Sys.Properties.Resources.check_box_outline_blank_32px;
            this.btn_clear.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.SubItemsExpandWidth = 14;
            this.btn_clear.Text = "部分选择";
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // buttonItem1
            // 
            this.buttonItem1.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.buttonItem1.FontBold = true;
            this.buttonItem1.Image = global::PIS_Sys.Properties.Resources.adjust_32px;
            this.buttonItem1.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.SubItemsExpandWidth = 14;
            this.buttonItem1.Text = "反向选择";
            this.buttonItem1.Click += new System.EventHandler(this.buttonItem1_Click);
            // 
            // btn_all
            // 
            this.btn_all.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            this.btn_all.FontBold = true;
            this.btn_all.Image = global::PIS_Sys.Properties.Resources.select_all_32px;
            this.btn_all.ImagePosition = DevComponents.DotNetBar.eImagePosition.Right;
            this.btn_all.Name = "btn_all";
            this.btn_all.SubItemsExpandWidth = 14;
            this.btn_all.Text = "全部选择";
            this.btn_all.Click += new System.EventHandler(this.btn_all_Click);
            // 
            // FrmCkhd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(944, 601);
            this.Controls.Add(this.superGridControl1);
            this.Controls.Add(this.ribbonBar1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCkhd";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "材块核对";
            this.Load += new System.EventHandler(this.FrmCkhd_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.RibbonBar ribbonBar1;
        private DevComponents.DotNetBar.ButtonItem btn_ckhd;
        private DevComponents.DotNetBar.ButtonItem btn_clear;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btn_all;
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