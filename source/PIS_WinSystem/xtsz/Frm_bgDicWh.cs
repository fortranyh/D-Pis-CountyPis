using DevComponents.AdvTree;
using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


namespace PIS_Sys.xtsz
{
    public partial class Frm_bgDicWh : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bgDicWh()
        {
            InitializeComponent();
        }
        int type_dict = 0;
        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e)
        {
            node4.Nodes.Clear();
            if (comboBox9.Text == "临床符合")
            {
                node4.Text = "<b>临床符合</b>—词典";
                type_dict = 0;
            }
            else if (comboBox9.Text == "诊断编码M")
            {
                node4.Text = "<b>诊断编码M</b>—词典";
                type_dict = 1;
            }
            else if (comboBox9.Text == "诊断编码S")
            {
                node4.Text = "<b>诊断编码S</b>—词典";
                type_dict = 2;
            }
            else if (comboBox9.Text == "诊断意见")
            {
                node4.Text = "<b>诊断意见</b>—词典";
                type_dict = 3;
            }
            BuildPartsTree();
        }
        //创建字典
        private void BuildPartsTree()
        {
            DBHelper.BLL.exam_bgzd_dict ins = new DBHelper.BLL.exam_bgzd_dict();
            DataTable dt = ins.GetTreeExam_bgzd_dict(type_dict);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.DefaultView.Sort = " order_no asc";
                string fliter = "parent_code='0'";
                DataRow[] drArr = dt.Select(fliter);
                advTree1.BeginUpdate();
                foreach (DataRow dr in drArr)
                {
                    Node node = new Node();
                    node.Tag = dr["id"].ToString();
                    node.TagString = dr["id"].ToString();
                    node.Text = dr["part_name"].ToString();
                    node.ImageExpanded = global::PIS_Sys.Properties.Resources.NoteHS;
                    CreateTreeViewRecursive(node.Nodes, dt, dr["id"].ToString());
                    advTree1.Nodes[0].Nodes.Add(node);
                    if (node.Nodes.Count > 0)
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Visible;
                        node.Image = global::PIS_Sys.Properties.Resources.cc;
                    }
                    else
                    {
                        node.ExpandVisibility = eNodeExpandVisibility.Hidden;
                        node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                    }
                    //移除已添加行，提高性能
                    if (dt.Rows.Count > 0)
                    {
                        dt.Rows.Remove(dr);
                    }
                    else
                    {
                        break;
                    }
                }
                advTree1.EndUpdate();

            }
        }
        private void CreateTreeViewRecursive(NodeCollection nodes, DataTable dataSource, string parentId)
        {
            dataSource.DefaultView.Sort = " order_no asc";
            string fliter = "parent_code='" + parentId + "'";
            //查询子节点
            DataRow[] drArr = dataSource.Select(fliter);
            foreach (DataRow dr in drArr)
            {
                Node node = new Node();
                node.Tag = dr["id"].ToString();
                node.TagString = dr["id"].ToString();
                node.Text = dr["part_name"].ToString();
                node.Image = global::PIS_Sys.Properties.Resources.ShowRulelinesHS;
                nodes.Add(node);
                //移除已添加行，提高性能
                dataSource.Rows.Remove(dr);
            }
        }
        private void Frm_bgDicWh_Load(object sender, EventArgs e)
        {
            comboBox9.SelectedIndex = 0;
        }

        private void advTree1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (advTree1.SelectedNode != null)
                {
                    添加ToolStripMenuItem.Visible = true;
                    删除ToolStripMenuItem.Visible = true;
                    编辑ToolStripMenuItem.Visible = true;

                    if (advTree1.SelectedNode.Parent == null)
                    {
                        删除ToolStripMenuItem.Visible = false;
                        编辑ToolStripMenuItem.Visible = false;
                        Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                        this.contextMenuStrip1.Show(p);
                        return;
                    }

                    if (advTree1.SelectedNode.Parent.Parent == null)
                    {
                        Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                        this.contextMenuStrip1.Show(p);
                    }
                    else
                    {
                        if (advTree1.SelectedNode.TagString != "-1")
                        {
                            添加ToolStripMenuItem.Visible = false;
                            Point p = advTree1.PointToScreen(new Point(e.X, e.Y));
                            this.contextMenuStrip1.Show(p);
                        }

                    }
                }
            }
        }

        private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNode();
        }
        //添加节点
        private void AddNode()
        {
            //添加树形节点
            if (advTree1.SelectedNode != null)
            {
                //----------------------------
                string uid = Helper.GetUidStr();
                //
                string puid = advTree1.SelectedNode.TagString;
                DBHelper.BLL.exam_bgzd_dict ins = new DBHelper.BLL.exam_bgzd_dict();
                Boolean zxResult = false;
                if (advTree1.SelectedNode.TagString == "-1")
                {
                    zxResult = ins.InsertParts(type_dict, uid, "新节点", "0");
                }
                else
                {

                    zxResult = ins.InsertParts(type_dict, uid, "新节点", puid);
                }
                if (zxResult == true)
                {
                    //----------------------------
                    DevComponents.AdvTree.Node lvm;
                    lvm = new DevComponents.AdvTree.Node();
                    lvm.Tag = uid;
                    lvm.Text = "新节点";
                    advTree1.SelectedNode.Nodes.Add(lvm);
                    advTree1.SelectedNode.Expand();
                    advTree1.SelectedNode = lvm;
                    advTree1.CellEdit = true;
                    advTree1.SelectedNode.BeginEdit();
                }
            }
        }
        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifyNode();
        }
        private void modifyNode()
        {
            //更新树形节点
            if (advTree1.SelectedNode != null && advTree1.SelectedNode.Parent != null)
            {
                advTree1.CellEdit = true;
                if (!advTree1.SelectedNode.IsEditing)
                {
                    advTree1.SelectedNode.BeginEdit();
                }
            }

        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelNode();
        }
        //删除节点
        public void DelNode()
        {
            //删除树形节点
            if (advTree1.SelectedNode != null && advTree1.SelectedNode.Parent != null)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;

                if (TaskDialog.Show("删除部位", "确认", "确定要删除此部位么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    DevComponents.AdvTree.Node curPTreeNode;
                    curPTreeNode = advTree1.SelectedNode.Parent;
                    //遍历删除库
                    delTreeDb(advTree1.SelectedNode.Tag.ToString());
                    //遍历删除节点
                    DelNodeCache(advTree1.SelectedNode);
                }

            }
        }
        public void DelNodeCache(DevComponents.AdvTree.Node node)
        {
            if (node.Nodes.Count > 0)
            {
                for (int i = node.Nodes.Count - 1; i >= 0; i--)
                {
                    DelNodeCache(node.Nodes[i]);
                }
            }
            node.Remove();
        }

        private void delTreeDb(string id)
        {
            //删库
            DBHelper.BLL.exam_bgzd_dict ins = new DBHelper.BLL.exam_bgzd_dict();
            DataTable dt = ins.GetChildParts(type_dict, id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    delTreeDb(dt.Rows[i][0].ToString());
                }
            }
            ins.DelParts(type_dict, id);
        }

        private void advTree1_AfterCellEditComplete(object sender, CellEditEventArgs e)
        {
            if (e.Cell != null)
            {
                if (e.NewText.Length > 0)
                {

                    advTree1.SelectedNode.EndEdit(false);
                    e.Cell.Editable = false;
                    advTree1.CellEdit = false;
                    //更新标题
                    DBHelper.BLL.exam_bgzd_dict ins = new DBHelper.BLL.exam_bgzd_dict();
                    ins.updatePartText(type_dict, e.NewText, Convert.ToString(e.Cell.Tag));
                }
                else
                {
                    advTree1.SelectedNode.EndEdit(true);
                    e.Cell.Editable = true;
                    advTree1.SelectedNode.BeginEdit();
                }
            }
        }
    }
}
