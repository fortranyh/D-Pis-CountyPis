using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_xbx_Templet : DevComponents.DotNetBar.Office2007Form
    {
        Boolean boolEditing = false;

        public Frm_xbx_Templet()
        {
            InitializeComponent();
        }

        private void Frm_DtmsTemplet_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = false;
            btnCancle.Enabled = false;
            BuildXbxTree();

        }

        //创建模版
        private void BuildXbxTree()
        {
            string RootID;
            string strRootText;

            DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
            DataTable dt = ins.GetTreeXbx_Templet(Program.User_Code);
            if (dt != null && dt.Rows.Count == 0)
            {
                //插入根节点
                string uid = Helper.GetUidStr();
                string suid = Helper.GetUidStr();
                ins.InsertXbxtempletRoot(Program.User_Code, uid, suid);
                dt = ins.GetTreeXbx_Templet(Program.User_Code);
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
                    tvwTemplete.Nodes[0].Nodes.Add(node);

                    CreateTreeViewRecursive2(tvwTemplete.Nodes[0].Nodes[i].Nodes, dt, RootID);


                    //移除已添加行，提高性能
                    dt.Rows.Remove(dr);
                    i += 1;
                }
                tvwTemplete.EndUpdate();
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
                DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
                DBHelper.Model.bgnr_templet Inscontent = ins.GetContent(id, Program.User_Code);
                this.txtDescription.Focus();
                txtDescription.Text = Inscontent.content;
                txtDescription.SelectionStart = Inscontent.content.Length;
            }
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
                    DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
                    ins.updateXbxtempletTitle(Program.User_Code, Convert.ToString(e.Node.Tag), e.Label);
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
                DBHelper.Model.bgmb_templet insModel;
                DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
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
            DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
            DataTable dt = ins.GetChildTreeXbx_Templet(Program.User_Code, id);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = dt.Rows.Count - 1; i >= 0; i--)
                {
                    delTreeDb(dt.Rows[i][0].ToString());
                }
            }
            ins.DelChildTreeXbx_Templet(Program.User_Code, id);
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
                DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
                int result = ins.updateXbxtempletContent(Program.User_Code, tvwTemplete.SelectedNode.Tag.ToString(), txtDescription.Text.Trim());
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
                DBHelper.BLL.xbx_templet ins = new DBHelper.BLL.xbx_templet();
                DBHelper.Model.bgnr_templet result = ins.GetContent(e.Node.Tag.ToString(), Program.User_Code);

                txtDescription.Text = result.content;

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
