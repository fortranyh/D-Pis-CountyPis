namespace PIS_Sys.blzd
{
    partial class Frm_BgHj
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_BgHj));
            this.gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.superGridControl2 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.SuspendLayout();
            // 
            // gridColumn1
            // 
            this.gridColumn1.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn1.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn1.DataPropertyName = "zdyj";
            this.gridColumn1.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn1.HeaderStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn1.HeaderText = "诊断";
            this.gridColumn1.Name = "zdyj";
            this.gridColumn1.Width = 260;
            // 
            // gridColumn2
            // 
            this.gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
            this.gridColumn2.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn2.DataPropertyName = "rysj";
            this.gridColumn2.HeaderStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn2.HeaderStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gridColumn2.HeaderText = "所见";
            this.gridColumn2.Name = "rysj";
            this.gridColumn2.Width = 260;
            // 
            // gridColumn3
            // 
            this.gridColumn3.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn3.DataPropertyName = "xbms";
            this.gridColumn3.FilterPopupMaxItems = 160;
            this.gridColumn3.HeaderStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn3.HeaderText = "描述";
            this.gridColumn3.Name = "xbms";
            this.gridColumn3.Width = 160;
            // 
            // gridColumn4
            // 
            this.gridColumn4.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn4.CellStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn4.DataPropertyName = "zdpz";
            this.gridColumn4.HeaderStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn4.HeaderText = "建议";
            this.gridColumn4.Name = "zdpz";
            this.gridColumn4.Width = 160;
            // 
            // gridColumn5
            // 
            this.gridColumn5.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn5.CellStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn5.DataPropertyName = "save_doc_name";
            this.gridColumn5.HeaderStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn5.HeaderText = "操作医生";
            this.gridColumn5.Name = "save_doc_name";
            // 
            // gridColumn6
            // 
            this.gridColumn6.CellStyles.Default.AllowWrap = DevComponents.DotNetBar.SuperGrid.Style.Tbool.True;
            this.gridColumn6.CellStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn6.DataPropertyName = "save_datetime";
            this.gridColumn6.HeaderStyles.Default.Font = new System.Drawing.Font("宋体", 14.25F);
            this.gridColumn6.HeaderText = "操作时间";
            this.gridColumn6.Name = "save_datetime";
            this.gridColumn6.Width = 160;
            // 
            // superGridControl2
            // 
            this.superGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl2.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl2.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.superGridControl2.Location = new System.Drawing.Point(0, 0);
            this.superGridControl2.Name = "superGridControl2";
            // 
            // 
            // 
            this.superGridControl2.PrimaryGrid.Columns.Add(this.gridColumn1);
            this.superGridControl2.PrimaryGrid.Columns.Add(this.gridColumn2);
            this.superGridControl2.PrimaryGrid.Columns.Add(this.gridColumn3);
            this.superGridControl2.PrimaryGrid.Columns.Add(this.gridColumn4);
            this.superGridControl2.PrimaryGrid.Columns.Add(this.gridColumn5);
            this.superGridControl2.PrimaryGrid.Columns.Add(this.gridColumn6);
            this.superGridControl2.PrimaryGrid.MultiSelect = false;
            this.superGridControl2.PrimaryGrid.VirtualMode = true;
            this.superGridControl2.PrimaryGrid.VirtualRowHeight = 30;
            this.superGridControl2.Size = new System.Drawing.Size(1341, 655);
            this.superGridControl2.TabIndex = 2;
            this.superGridControl2.Text = "superGridControl2";
            // 
            // Frm_BgHj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CaptionFont = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(1341, 655);
            this.Controls.Add(this.superGridControl2);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Frm_BgHj";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "报告书写痕迹浏览";
            this.Activated += new System.EventHandler(this.Frm_Qcxx_Activated);
            this.Load += new System.EventHandler(this.Frm_Qcxx_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5;
        private DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6;
    }
}