namespace PIS_Sys.xtsz
{
    partial class Frm_bgDicWh
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_bgDicWh));
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.advTree1 = new DevComponents.AdvTree.AdvTree();
            this.node4 = new DevComponents.AdvTree.Node();
            this.elementStyle3 = new DevComponents.DotNetBar.ElementStyle();
            this.nodeConnector1 = new DevComponents.AdvTree.NodeConnector();
            this.elementStyle1 = new DevComponents.DotNetBar.ElementStyle();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "临床符合",
            "诊断编码M",
            "诊断编码S",
            "诊断意见"});
            this.comboBox9.Location = new System.Drawing.Point(136, 10);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(171, 22);
            this.comboBox9.TabIndex = 6;
            this.comboBox9.SelectedIndexChanged += new System.EventHandler(this.comboBox9_SelectedIndexChanged);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(61, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 27);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "维护类型：";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.contextMenuStrip1.Name = "ContextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 70);
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("添加ToolStripMenuItem.Image")));
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.添加ToolStripMenuItem.Text = "添加";
            this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("编辑ToolStripMenuItem.Image")));
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.编辑ToolStripMenuItem.Text = "编辑";
            this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("删除ToolStripMenuItem.Image")));
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // advTree1
            // 
            this.advTree1.AccessibleRole = System.Windows.Forms.AccessibleRole.Outline;
            this.advTree1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.advTree1.BackgroundStyle.Class = "TreeBorderKey";
            this.advTree1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.advTree1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.advTree1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.advTree1.Location = new System.Drawing.Point(0, 40);
            this.advTree1.Name = "advTree1";
            this.advTree1.Nodes.AddRange(new DevComponents.AdvTree.Node[] {
            this.node4});
            this.advTree1.NodesConnector = this.nodeConnector1;
            this.advTree1.NodeStyle = this.elementStyle1;
            this.advTree1.PathSeparator = ";";
            this.advTree1.Size = new System.Drawing.Size(383, 572);
            this.advTree1.Styles.Add(this.elementStyle1);
            this.advTree1.Styles.Add(this.elementStyle3);
            this.advTree1.TabIndex = 9;
            this.advTree1.Text = "advTree1";
            this.advTree1.AfterCellEditComplete += new DevComponents.AdvTree.CellEditEventHandler(this.advTree1_AfterCellEditComplete);
            this.advTree1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.advTree1_MouseClick);
            // 
            // node4
            // 
            this.node4.Expanded = true;
            this.node4.ExpandVisibility = DevComponents.AdvTree.eNodeExpandVisibility.Visible;
            this.node4.FullRowBackground = true;
            this.node4.Name = "node4";
            this.node4.Style = this.elementStyle3;
            this.node4.TagString = "-1";
            this.node4.Text = "<b>临床符合</b>—词典";
            // 
            // elementStyle3
            // 
            this.elementStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(230)))), ((int)(((byte)(247)))));
            this.elementStyle3.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(168)))), ((int)(((byte)(228)))));
            this.elementStyle3.BackColorGradientAngle = 90;
            this.elementStyle3.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle3.BorderBottomWidth = 1;
            this.elementStyle3.BorderColor = System.Drawing.Color.DarkGray;
            this.elementStyle3.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle3.BorderLeftWidth = 1;
            this.elementStyle3.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle3.BorderRightWidth = 1;
            this.elementStyle3.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.elementStyle3.BorderTopWidth = 1;
            this.elementStyle3.CornerDiameter = 4;
            this.elementStyle3.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle3.Description = "Blue";
            this.elementStyle3.Name = "elementStyle3";
            this.elementStyle3.PaddingBottom = 1;
            this.elementStyle3.PaddingLeft = 1;
            this.elementStyle3.PaddingRight = 1;
            this.elementStyle3.PaddingTop = 1;
            this.elementStyle3.TextColor = System.Drawing.Color.Black;
            // 
            // nodeConnector1
            // 
            this.nodeConnector1.LineColor = System.Drawing.SystemColors.ControlText;
            // 
            // elementStyle1
            // 
            this.elementStyle1.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.elementStyle1.Name = "elementStyle1";
            this.elementStyle1.TextColor = System.Drawing.SystemColors.ControlText;
            // 
            // Frm_bgDicWh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientSize = new System.Drawing.Size(383, 612);
            this.Controls.Add(this.advTree1);
            this.Controls.Add(this.comboBox9);
            this.Controls.Add(this.labelX1);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Frm_bgDicWh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "诊断编码 临床符合字典维护";
            this.Load += new System.EventHandler(this.Frm_bgDicWh_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advTree1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox9;
        private DevComponents.DotNetBar.LabelX labelX1;
        internal System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private DevComponents.AdvTree.AdvTree advTree1;
        private DevComponents.AdvTree.Node node4;
        private DevComponents.DotNetBar.ElementStyle elementStyle3;
        private DevComponents.AdvTree.NodeConnector nodeConnector1;
        private DevComponents.DotNetBar.ElementStyle elementStyle1;
    }
}