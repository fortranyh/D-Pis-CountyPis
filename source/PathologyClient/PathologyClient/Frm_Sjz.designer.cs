namespace PathologyClient
{
    partial class Frm_Sjz
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sjz));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.labelZoom = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.comboLayoutType = new System.Windows.Forms.ComboBox();
            this.comboLayout = new System.Windows.Forms.ComboBox();
            this.treeGX1 = new DevComponents.Tree.TreeGX();
            this.node1 = new DevComponents.Tree.Node();
            this.nodeConnector2 = new DevComponents.Tree.NodeConnector();
            this.nodeStyle = new DevComponents.Tree.ElementStyle();
            this.nodeConnector1 = new DevComponents.Tree.NodeConnector();
            this.node8 = new DevComponents.Tree.Node();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeGX1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.buttonX2);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Controls.Add(this.labelZoom);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.trackBar1);
            this.panel1.Controls.Add(this.comboLayoutType);
            this.panel1.Controls.Add(this.comboLayout);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(886, 36);
            this.panel1.TabIndex = 1;
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Location = new System.Drawing.Point(751, 6);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(51, 25);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 8;
            this.buttonX2.Text = "打印";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(694, 6);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(54, 25);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 7;
            this.buttonX1.Text = "保存";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // labelZoom
            // 
            this.labelZoom.Location = new System.Drawing.Point(282, 9);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(44, 16);
            this.labelZoom.TabIndex = 6;
            this.labelZoom.Text = "100%";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(243, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "缩放: ";
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBar1.AutoSize = false;
            this.trackBar1.LargeChange = 50;
            this.trackBar1.Location = new System.Drawing.Point(332, 5);
            this.trackBar1.Maximum = 400;
            this.trackBar1.Minimum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(345, 26);
            this.trackBar1.SmallChange = 10;
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBar1.Value = 100;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // comboLayoutType
            // 
            this.comboLayoutType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLayoutType.Location = new System.Drawing.Point(119, 6);
            this.comboLayoutType.Name = "comboLayoutType";
            this.comboLayoutType.Size = new System.Drawing.Size(120, 22);
            this.comboLayoutType.TabIndex = 3;
            this.comboLayoutType.SelectedIndexChanged += new System.EventHandler(this.comboLayoutType_SelectedIndexChanged);
            // 
            // comboLayout
            // 
            this.comboLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboLayout.Location = new System.Drawing.Point(6, 6);
            this.comboLayout.Name = "comboLayout";
            this.comboLayout.Size = new System.Drawing.Size(107, 22);
            this.comboLayout.TabIndex = 2;
            this.comboLayout.SelectedIndexChanged += new System.EventHandler(this.comboLayout_SelectedIndexChanged);
            // 
            // treeGX1
            // 
            this.treeGX1.AllowDrop = true;
            this.treeGX1.AutoScrollMinSize = new System.Drawing.Size(660, 364);
            this.treeGX1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.treeGX1.BackgroundStyle.BorderTop = DevComponents.Tree.eStyleBorderType.Dash;
            this.treeGX1.BackgroundStyle.BorderTopColor = System.Drawing.Color.CornflowerBlue;
            this.treeGX1.BackgroundStyle.BorderTopWidth = 1;
            this.treeGX1.CommandBackColorGradientAngle = 90;
            this.treeGX1.CommandMouseOverBackColor2SchemePart = DevComponents.Tree.eColorSchemePart.ItemHotBackground2;
            this.treeGX1.CommandMouseOverBackColorGradientAngle = 90;
            this.treeGX1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeGX1.ExpandLineColorSchemePart = DevComponents.Tree.eColorSchemePart.BarDockedBorder;
            this.treeGX1.LicenseKey = "EB364C34-3CE3-4cd6-BB1B-13513ABE0D62";
            this.treeGX1.Location = new System.Drawing.Point(0, 36);
            this.treeGX1.Name = "treeGX1";
            this.treeGX1.Nodes.AddRange(new DevComponents.Tree.Node[] {
            this.node1});
            this.treeGX1.NodesConnector = this.nodeConnector2;
            this.treeGX1.NodeStyle = this.nodeStyle;
            this.treeGX1.PathSeparator = ";";
            this.treeGX1.RootConnector = this.nodeConnector1;
            this.treeGX1.Size = new System.Drawing.Size(886, 360);
            this.treeGX1.Styles.Add(this.nodeStyle);
            this.treeGX1.SuspendPaint = false;
            this.treeGX1.TabIndex = 2;
            this.treeGX1.Text = "treeGX1";
            // 
            // node1
            // 
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = "ACME Corp.";
            // 
            // nodeConnector2
            // 
            this.nodeConnector2.LineWidth = 5;
            // 
            // nodeStyle
            // 
            this.nodeStyle.BackColor2SchemePart = DevComponents.Tree.eColorSchemePart.BarBackground2;
            this.nodeStyle.BackColorGradientAngle = 90;
            this.nodeStyle.BackColorSchemePart = DevComponents.Tree.eColorSchemePart.BarBackground;
            this.nodeStyle.BorderBottom = DevComponents.Tree.eStyleBorderType.Solid;
            this.nodeStyle.BorderBottomWidth = 1;
            this.nodeStyle.BorderColorSchemePart = DevComponents.Tree.eColorSchemePart.BarDockedBorder;
            this.nodeStyle.BorderLeft = DevComponents.Tree.eStyleBorderType.Solid;
            this.nodeStyle.BorderLeftWidth = 1;
            this.nodeStyle.BorderRight = DevComponents.Tree.eStyleBorderType.Solid;
            this.nodeStyle.BorderRightWidth = 1;
            this.nodeStyle.BorderTop = DevComponents.Tree.eStyleBorderType.Solid;
            this.nodeStyle.BorderTopWidth = 1;
            this.nodeStyle.CornerDiameter = 4;
            this.nodeStyle.CornerType = DevComponents.Tree.eCornerType.Rounded;
            this.nodeStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nodeStyle.Name = "nodeStyle";
            this.nodeStyle.PaddingBottom = 5;
            this.nodeStyle.PaddingLeft = 5;
            this.nodeStyle.PaddingRight = 5;
            this.nodeStyle.PaddingTop = 5;
            this.nodeStyle.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))));
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineWidth = 5;
            // 
            // node8
            // 
            this.node8.Expanded = true;
            this.node8.Name = "node8";
            this.node8.Text = "EmployeeCard";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "png";
            this.saveFileDialog1.Filter = "PNG Files | *.png";
            this.saveFileDialog1.Title = "Save TreeGX control content to image";
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.UseAntiAlias = true;
            this.printPreviewDialog1.Visible = false;
            // 
            // printDocument1
            // 
            this.printDocument1.OriginAtMargins = true;
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // Frm_Sjz
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(886, 396);
            this.Controls.Add(this.treeGX1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_Sjz";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检查时间轴";
            this.Activated += new System.EventHandler(this.Frm_Sjz_Activated);
            this.Load += new System.EventHandler(this.Frm_Sjz_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeGX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.ComboBox comboLayoutType;
        private System.Windows.Forms.ComboBox comboLayout;
        private DevComponents.Tree.TreeGX treeGX1;
        private DevComponents.Tree.Node node1;
        private DevComponents.Tree.NodeConnector nodeConnector2;
        private DevComponents.Tree.ElementStyle nodeStyle;
        private DevComponents.Tree.NodeConnector nodeConnector1;
        private DevComponents.Tree.Node node8;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
    }
}