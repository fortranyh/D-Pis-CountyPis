using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.qcgl
{
    public partial class Frm_DtmsTemplet : DevComponents.DotNetBar.Office2007Form
    {
        Boolean boolEditing = false;

        public Frm_DtmsTemplet()
        {
            InitializeComponent();
        }

        private void Frm_DtmsTemplet_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancle.Enabled = false;
            InitControls();
            BuildDTTree();

        }


        private void InitControls()
        {
            //===================================
            //字体
            System.Drawing.Text.InstalledFontCollection font;//安装在系统的所有字体，无法继承
            font = new System.Drawing.Text.InstalledFontCollection();
            foreach (System.Drawing.FontFamily family in font.Families)
            {
                ComboBox1.Items.Add(family.Name);
            }
            //===================================
            //字号
            //系统字体大小
            this.ComboBox2.Items.Add("8");
            this.ComboBox2.Items.Add("9");
            this.ComboBox2.Items.Add("10");
            this.ComboBox2.Items.Add("11");
            this.ComboBox2.Items.Add("12");
            this.ComboBox2.Items.Add("14");
            this.ComboBox2.Items.Add("16");
            this.ComboBox2.Items.Add("18");
            this.ComboBox2.Items.Add("20");
            this.ComboBox2.Items.Add("22");
            this.ComboBox2.Items.Add("24");
            this.ComboBox2.Items.Add("26");
            this.ComboBox2.Items.Add("28");
            this.ComboBox2.Items.Add("36");
            this.ComboBox2.Items.Add("48");
            this.ComboBox2.Items.Add("72");
        }
        //创建大体描述模版
        private void BuildDTTree()
        {
            string RootID;
            string strRootText;

            DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
            DataTable dt = ins.GetTreeDtms_Templet(Program.User_Code);
            if (dt != null && dt.Rows.Count == 0)
            {
                //插入根节点
                string uid = Helper.GetUidStr();
                ins.InsertDTtempletRoot(Program.User_Code, uid);
                dt = ins.GetTreeDtms_Templet(Program.User_Code);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                string fliter = "parentid='-1'";
                DataRow[] drArr = dt.Select(fliter);
                tvwTemplete.BeginUpdate();
                int i = 0;
                foreach (DataRow dr in drArr)
                {
                    RootID = dr["id"].ToString();
                    strRootText = dr["title"].ToString();
                    TreeNode node = new TreeNode();
                    node.Tag = RootID;
                    node.Text = strRootText;
                    tvwTemplete.Nodes.Add(node);

                    CreateTreeViewRecursive2(tvwTemplete.Nodes[i].Nodes, dt, RootID);


                    //移除已添加行，提高性能
                    dt.Rows.Remove(dr);
                    i += 1;
                }
                tvwTemplete.EndUpdate();
                if (tvwTemplete.Nodes[0].Nodes.Count > 0)
                {
                    tvwTemplete.Nodes[0].Expand();
                }
                //添加单击事件处理程序
                this.tvwTemplete.AfterSelect += new TreeViewEventHandler(tvwTemplete_AfterSelect);
            }


        }
        private void CreateTreeViewRecursive2(TreeNodeCollection nodes, DataTable dataSource, string parentId)
        {
            dataSource.DefaultView.Sort = "  TreeLevel asc,autoid asc";
            string fliter = "parentid='" + parentId + "'";
            //查询子节点
            DataRow[] drArr = dataSource.Select(fliter);
            TreeNode node;
            foreach (DataRow dr in drArr)
            {
                node = new TreeNode();
                node.Tag = dr["id"].ToString();
                node.Text = dr["title"].ToString();

                nodes.Add(node);
                //递归创建子节点
                CreateTreeViewRecursive2(node.Nodes, dataSource, Convert.ToString(dr["id"]));
                //移除已添加行，提高性能
                dataSource.Rows.Remove(dr);
            }
        }
        //展示选中的大体描述
        private void tvwTemplete_AfterSelect(object sender, TreeViewEventArgs e)
        {

            if (e.Node.Nodes.Count == 0)
            {
                string id = Convert.ToString(e.Node.Tag);
                DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                string content = ins.GetContent(id, Program.User_Code).Trim();
                this.txtDescription.Focus();
                txtDescription.Text = content;
                txtDescription.SelectionStart = content.Length;
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtDescription.SelectionFont = new Font(this.ComboBox1.Text, this.txtDescription.Font.Size);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtDescription.SelectionFont = new Font(this.txtDescription.Font.Name, Convert.ToSingle(this.ComboBox2.Text));
        }

        TreeNode mySelectedNode;
        private void tvwTemplete_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    e.Node.EndEdit(false);
                    tvwTemplete.LabelEdit = false;
                    //更新标题
                    DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                    ins.updateDTtempletTitle(Program.User_Code, Convert.ToString(e.Node.Tag), e.Label);
                    //重置
                    mySelectedNode = null;
                }
                else
                {
                    e.CancelEdit = true;
                    e.Node.BeginEdit();
                }
            }
        }

        private void tvwTemplete_MouseDown(object sender, MouseEventArgs e)
        {
            mySelectedNode = tvwTemplete.GetNodeAt(e.X, e.Y);

            if (mySelectedNode != null && mySelectedNode.Parent != null)
            {
                this.tvwTemplete.SelectedNode = mySelectedNode;
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
            if (tvwTemplete.SelectedNode != null)
            {
                //----------------------------
                string uid = Helper.GetUidStr();
                //获取父节点信息
                DBHelper.Model.dtms_templet insModel;
                DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                insModel = ins.GetOneTemplet(Program.User_Code, tvwTemplete.SelectedNode.Tag.ToString());
                if (insModel != null)
                {
                    int intLevel;
                    intLevel = insModel.TreeLevel + 1;
                    insModel.id = uid;
                    insModel.parentid = tvwTemplete.SelectedNode.Tag.ToString();
                    insModel.title = "新节点";
                    insModel.TreeLevel = intLevel;
                    insModel.content = "";
                    ins.InsertOneTemplet(insModel);
                }
                else
                {
                    return;
                }
                //----------------------------
                TreeNode lvm;
                lvm = new TreeNode();
                lvm.Tag = uid;
                lvm.Text = "新节点";
                tvwTemplete.SelectedNode.Nodes.Add(lvm);
                tvwTemplete.SelectedNode.Expand();
                tvwTemplete.SelectedNode = lvm;
                tvwTemplete.LabelEdit = true;
                tvwTemplete.SelectedNode.BeginEdit();
            }
        }




        private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            modifyNode();
        }
        private void modifyNode()
        {
            //更新树形节点
            if (tvwTemplete.SelectedNode != null && tvwTemplete.SelectedNode.Parent != null)
            {
                tvwTemplete.LabelEdit = true;
                if (!tvwTemplete.SelectedNode.IsEditing)
                {
                    tvwTemplete.SelectedNode.BeginEdit();
                }
            }

        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DelNode();
        }

        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            AddNode();
        }

        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            modifyNode();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            DelNode();
        }

        //删除节点
        public void DelNode()
        {
            //删除树形节点
            if (tvwTemplete.SelectedNode != null && tvwTemplete.SelectedNode.Parent != null)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("报告模板维护", "确认", "确定要删除么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    TreeNode curPTreeNode;
                    curPTreeNode = tvwTemplete.SelectedNode.Parent;
                    //遍历删除库
                    delTreeDb(tvwTemplete.SelectedNode.Tag.ToString());
                    //遍历删除节点
                    DelNodeCache(tvwTemplete.SelectedNode);
                }
                //重置
                mySelectedNode = null;
            }
        }
        public void DelNodeCache(TreeNode node)
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
            DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
            DataTable dt = ins.GetChildTreeDtms_Templet(Program.User_Code, id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    delTreeDb(dt.Rows[i][0].ToString());
                }
            }
            ins.DelChildTreeDtms_Templet(Program.User_Code, id);
        }


        private void btnEdit_Click(object sender, EventArgs e)
        {
            //修改内容
            //------
            boolEditing = true;
            txtDescription.Focus();
            CommandBars_Update();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //保存
            boolEditing = false;
            if (tvwTemplete.SelectedNode != null)
            {
                DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                int result = ins.updateDTtempletContent(Program.User_Code, tvwTemplete.SelectedNode.Tag.ToString(), txtDescription.Text.Trim());
            }
            CommandBars_Update();
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            //--------
            //取消
            //--------
            boolEditing = false;

            if (this.tvwTemplete.SelectedNode != null)
            {
                ActiveNode(tvwTemplete, new TreeViewEventArgs(this.tvwTemplete.SelectedNode));
            }
            CommandBars_Update();
        }

        //绑定数据库数据
        private void ActiveNode(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                //查库写当前描述和诊断
                DBHelper.BLL.dtms_templet ins = new DBHelper.BLL.dtms_templet();
                string result = ins.GetContent(e.Node.Tag.ToString(), Program.User_Code);

                txtDescription.Text = result;

            }
        }
        private void CommandBars_Update()
        {
            btnSave.Enabled = boolEditing;
            btnCancle.Enabled = boolEditing;
            btnEdit.Enabled = !boolEditing;
        }




        private void decut_Click(object sender, EventArgs e)
        {
            //剪切
            Clipboard.SetText(this.txtDescription.SelectedText);
            txtDescription.SelectedText = String.Empty;
        }

        private void decopy_Click(object sender, EventArgs e)
        {
            //复制
            //-----------
            Clipboard.SetText(txtDescription.SelectedText);
        }

        private void depaste_Click(object sender, EventArgs e)
        {
            txtDescription.SelectedText = Clipboard.GetText();
        }

    }
}
