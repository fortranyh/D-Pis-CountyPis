using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class Frm_BgTempletImport : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_BgTempletImport()
        {
            InitializeComponent();
        }
        public int TempletType = 0;
        private void Frm_TempletImport_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            DataTable dt = ins.SysShowAllUsers(Program.Dept_Code);
            if (dt != null)
            {
                ComboBox1.DataSource = dt;
                ComboBox1.DisplayMember = "user_name";
                ComboBox1.ValueMember = "user_code";
                ComboBox1.Text = Program.User_Name;
                ComboBox1.Enabled = false;
            }
            DataTable dt1 = ins.SysTjAllUsers(Program.User_Code, Program.Dept_Code);
            if (dt1 != null)
            {
                ComboBox2.DataSource = dt1;
                ComboBox2.ValueMember = "user_code";
                ComboBox2.DisplayMember = "user_name";
                ComboBox2.SelectedIndex = -1;
                ComboBox2.SelectedIndexChanged += new EventHandler(ComboBox2_SelectedIndexChanged);
                if (ComboBox2.Items.Count > 0)
                {
                    ComboBox2.SelectedIndex = 0;
                }
            }
            //建立当前用户的模板树
            BuildTree(Program.User_Code, Program.User_Name, tvwSourceTemplete, true);
        }
        public void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox2.SelectedIndex != -1)
            {
                //建立需求用户的模板树
                BuildTree(ComboBox2.SelectedValue.ToString(), ComboBox2.Text.ToString(), tvwCopyTemplete, false);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Button1.Click -= new EventHandler(Button1_Click);
            Button1.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (!TextBox1.Text.Trim().Equals("") && !TextBox2.Text.Trim().Equals(""))
                {
                    BuildCopySubTree(TextBox1.Text, TextBox2.Text, ComboBox1.SelectedValue.ToString(), ComboBox2.SelectedValue.ToString());
                    //刷新需求用户的模板树
                    BuildTree(ComboBox2.SelectedValue.ToString(), ComboBox2.Text.ToString(), tvwCopyTemplete, false);
                }
            }
            catch
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            Button1.Enabled = true;
            Button1.Click += new EventHandler(Button1_Click);
        }

        private void BuildTree(string Usercode, string userName, TreeView tvwTemplete, Boolean flag)
        {
            string RootID;
            string strRootText;
            tvwTemplete.Nodes.Clear();
            //没有根节点，添加根节点
            DBHelper.BLL.blzd_templet ins = new DBHelper.BLL.blzd_templet();
            DataTable dt = ins.GetTreeBlzd_Templet(Usercode);
            if (dt != null && dt.Rows.Count == 0)
            {
                //插入根节点
                string uid = Helper.GetUidStr();
                ins.InsertBLtempletRoot(Usercode, uid);
                dt = ins.GetTreeBlzd_Templet(Usercode);
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
                //添加子菜单的事件
                tvwTemplete.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvwTemplete_NodeMouseClick);
            }
            if (tvwTemplete.Nodes.Count > 0)
            {
                tvwTemplete.ExpandAll();
            }
        }

        private void BuildCopySubTree(String sourceid, String desid, String sourceysbh, String desysbh)
        {
            DBHelper.BLL.blzd_templet ins = new DBHelper.BLL.blzd_templet();
            DataTable dt = ins.GetParentInfo(sourceid, sourceysbh);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["ParentID"].ToString() != "-1")
                {
                    //先查询是否已经存在此节点
                    int icount = ins.GetCount(sourceid, desysbh);
                    if (icount == 0)
                    {
                        DBHelper.Model.bgmb_templet insmodel = new DBHelper.Model.bgmb_templet();
                        insmodel.id = sourceid;
                        insmodel.parentid = desid;
                        insmodel.title = dt.Rows[0]["title"].ToString();
                        insmodel.content = dt.Rows[0]["content"].ToString();
                        insmodel.DocNo = desysbh;
                        insmodel.TreeLevel = Convert.ToInt32(dt.Rows[0]["TreeLevel"].ToString());
                        insmodel.Clicked = Convert.ToInt32(dt.Rows[0]["Clicked"].ToString());
                        insmodel.flag = Convert.ToInt32(dt.Rows[0]["flag"].ToString());
                        ins.InsertOneTemplet(insmodel);
                    }
                    dt = ins.GetChildTreeBlzd_Templet(sourceysbh, sourceid);
                    List<string> id = new List<string>();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            id.Add(dt.Rows[i]["id"].ToString());
                        }
                    }
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        BuildCopySubTree(id[j], sourceid, sourceysbh, desysbh);
                    }
                }
                else
                {
                    dt = ins.GetChildTreeBlzd_Templet(sourceysbh, sourceid);
                    List<string> id = new List<string>();
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            id.Add(dt.Rows[i]["id"].ToString());
                        }
                    }
                    for (int j = 0; j < dt.Rows.Count; j++)
                    {
                        BuildCopySubTree(id[j], desid, sourceysbh, desysbh);
                    }
                }

            }
        }
        //绑定数据库数据
        private void tvwTemplete_NodeMouseClick(Object sender, TreeNodeMouseClickEventArgs e)
        {
            if (((TreeView)sender).Name == "tvwSourceTemplete")
            {
                TextBox1.Text = e.Node.Tag.ToString();
            }
            else if (((TreeView)sender).Name == "tvwCopyTemplete")
            {
                TextBox2.Text = e.Node.Tag.ToString();
            }
            if (e.Node.Parent != null && e.Node.Nodes.Count == 0)
            {
                //查库写当前描述和诊断
                string id = Convert.ToString(e.Node.Tag);
                DBHelper.BLL.blzd_templet ins = new DBHelper.BLL.blzd_templet();
                string content = ins.GetContent(id, Program.User_Code).Trim();
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


    }

}
