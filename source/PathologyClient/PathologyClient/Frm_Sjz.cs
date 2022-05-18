using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace PathologyClient
{
    public partial class Frm_Sjz : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Sjz()
        {
            InitializeComponent();
        }
        public static string exam_no = "";
        public static string study_no = "";
        public static string patient_name = "";
        private void Frm_Sjz_Load(object sender, EventArgs e)
        {
            comboLayout.Items.AddRange(Enum.GetNames(typeof(DevComponents.Tree.eNodeLayout)));
            comboLayout.SelectedItem = "Diagram";
            this.node1.Expanded = true;
            this.node1.Name = "node1";
            this.node1.Text = string.Format("病理号:{0} 姓名:{1}", study_no, patient_name);
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            //组织post请求参数
            parameters.Add("exam_no", exam_no);
            string xmldata = PublicBaseLib.PostWebService.PostCallWebServiceForXml(Program.WebServerUrl, "GetSjzXmlStr", parameters);
            DataSet ds = new DataSet();
            ds.ReadXml(new StringReader(xmldata));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                if (Convert.ToInt32(dt.Rows[0]["exam_status"]) > 15)
                {
                    //
                    DevComponents.Tree.Node node2 = new DevComponents.Tree.Node();
                    node2.Name = "node2";
                    node2.Text = string.Format("病人来源：{0} 申请时间：{1} 申请科室：{2} 申请医生：{3}", dt.Rows[0]["patient_source"], dt.Rows[0]["req_date_time"], dt.Rows[0]["req_dept"], dt.Rows[0]["req_physician"]);
                    this.node1.Nodes.AddRange(new DevComponents.Tree.Node[] { node2 });
                    //
                    DevComponents.Tree.Node node3 = new DevComponents.Tree.Node();
                    node3.Name = "node3";
                    node3.Text = string.Format("标本接收：{0} 接收时间：{1} ", dt.Rows[0]["receive_doctor_name"], dt.Rows[0]["received_datetime"]);
                    this.node1.Nodes.AddRange(new DevComponents.Tree.Node[] { node3 });
                }
                if (Convert.ToInt32(dt.Rows[0]["exam_status"]) > 20 && dt.Rows[0]["exam_type"].Equals("PL"))
                {
                    //
                    DevComponents.Tree.Node node4 = new DevComponents.Tree.Node();
                    node4.Name = "node4";
                    node4.Text = string.Format("取材医师：{0} 取材时间：{1}", dt.Rows[0]["qucai_doctor_name"], dt.Rows[0]["qucai_datetime"]);
                    this.node1.Nodes.AddRange(new DevComponents.Tree.Node[] { node4 });
                }
                if (Convert.ToInt32(dt.Rows[0]["exam_status"]) >= 27 && dt.Rows[0]["exam_type"].Equals("PL"))
                {
                    //
                    DevComponents.Tree.Node node5 = new DevComponents.Tree.Node();
                    node5.Name = "node5";
                    node5.Text = string.Format("包埋医师：{0} 包埋时间：{1}", dt.Rows[0]["baomai_doctor_name"], dt.Rows[0]["baomai_datetime"]);
                    this.node1.Nodes.AddRange(new DevComponents.Tree.Node[] { node5 });
                }
                if (Convert.ToInt32(dt.Rows[0]["exam_status"]) >= 30 && dt.Rows[0]["exam_type"].Equals("PL"))
                {
                    //
                    DevComponents.Tree.Node node6 = new DevComponents.Tree.Node();
                    node6.Name = "node6";
                    node6.Text = string.Format("制片医师：{0} 制片时间：{1} ", dt.Rows[0]["zhipian_doctor_name"], dt.Rows[0]["zhipian_datetime"]);
                    this.node1.Nodes.AddRange(new DevComponents.Tree.Node[] { node6 });
                }

            }
        }

        private void comboLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboLayoutType.Items.Clear();

            if (comboLayout.SelectedItem == null)
            {
                return;
            }

            DevComponents.Tree.eNodeLayout layout = (DevComponents.Tree.eNodeLayout)Enum.Parse(typeof(DevComponents.Tree.eNodeLayout), comboLayout.SelectedItem.ToString());
            if (treeGX1.LayoutType != layout)
            {
                treeGX1.LayoutType = layout;
                treeGX1.RecalcLayout();
            }

            if (layout == DevComponents.Tree.eNodeLayout.Map)
            {
                comboLayoutType.Items.AddRange(Enum.GetNames(typeof(DevComponents.Tree.eMapFlow)));
                comboLayoutType.SelectedItem = treeGX1.MapLayoutFlow.ToString();
            }
            else if (layout == DevComponents.Tree.eNodeLayout.Diagram)
            {
                comboLayoutType.Items.AddRange(Enum.GetNames(typeof(DevComponents.Tree.eDiagramFlow)));
                comboLayoutType.SelectedItem = treeGX1.DiagramLayoutFlow.ToString();
            }
        }

        private void comboLayoutType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboLayoutType.SelectedItem == null || comboLayout.SelectedItem == null)
            {
                return;
            }

            DevComponents.Tree.eNodeLayout layout = (DevComponents.Tree.eNodeLayout)Enum.Parse(typeof(DevComponents.Tree.eNodeLayout), comboLayout.SelectedItem.ToString());
            if (layout == DevComponents.Tree.eNodeLayout.Map)
            {
                DevComponents.Tree.eMapFlow mapFlow = (DevComponents.Tree.eMapFlow)Enum.Parse(typeof(DevComponents.Tree.eMapFlow), comboLayoutType.SelectedItem.ToString());
                if (treeGX1.MapLayoutFlow != mapFlow)
                {
                    treeGX1.MapLayoutFlow = mapFlow;
                    treeGX1.RecalcLayout();
                    treeGX1.Refresh();
                }
            }
            else if (layout == DevComponents.Tree.eNodeLayout.Diagram)
            {
                DevComponents.Tree.eDiagramFlow diagramFlow = (DevComponents.Tree.eDiagramFlow)Enum.Parse(typeof(DevComponents.Tree.eDiagramFlow), comboLayoutType.SelectedItem.ToString());
                if (treeGX1.DiagramLayoutFlow != diagramFlow)
                {
                    treeGX1.DiagramLayoutFlow = diagramFlow;
                    treeGX1.RecalcLayout();
                    treeGX1.Refresh();
                }
            }
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            treeGX1.Zoom = (float)trackBar1.Value / 100;
            labelZoom.Text = trackBar1.Value.ToString() + "%";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (Bitmap bmp = new Bitmap(treeGX1.TreeSize.Width, treeGX1.TreeSize.Height))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        // Perform actual rendering
                        treeGX1.PaintTo(g, true, Rectangle.Empty);
                    }

                    bmp.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                Frm_TJInfo("提示", "保存图片成功！");
            }
        }
        //提示窗体
        public void Frm_TJInfo(string Title, string B_info)
        {
            FrmAlert Frm_AlertIns = new FrmAlert();
            Rectangle r = Screen.GetWorkingArea(this);
            Frm_AlertIns.Location = new Point(r.Right - Frm_AlertIns.Width, r.Bottom - Frm_AlertIns.Height);
            Frm_AlertIns.AutoClose = true;
            Frm_AlertIns.AutoCloseTimeOut = 3;
            Frm_AlertIns.AlertAnimation = eAlertAnimation.BottomToTop;
            Frm_AlertIns.AlertAnimationDuration = 300;
            Frm_AlertIns.SetInfo(Title, B_info);
            Frm_AlertIns.Show(false);
        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Bounds = this.Bounds;
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // Center tree on the page
            Point p =
                new Point((e.PageBounds.Width - treeGX1.TreeSize.Width) / 2, (e.PageBounds.Height - treeGX1.TreeSize.Height) / 2);
            if (p.X > 0 && p.Y > 0)
            {
                e.Graphics.TranslateTransform(p.X, p.Y, MatrixOrder.Append);
            }

            // Perform actual rendering
            treeGX1.PaintTo(e.Graphics, false, e.PageBounds);
        }

        private void Frm_Sjz_Activated(object sender, EventArgs e)
        {
            this.Focus();
        }
    }
}
