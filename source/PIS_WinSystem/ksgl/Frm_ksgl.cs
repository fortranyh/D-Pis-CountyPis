using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.ksgl
{
    public partial class Frm_ksgl : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_ksgl()
        {
            InitializeComponent();
        }

        private void Frm_ksgl_Load(object sender, EventArgs e)
        {

            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            int gLflag = ins.GetUserGL(Program.User_Code);
            DataTable dt;
            if (Program.System_Admin == true || gLflag == 1)
            {
                dt = ins.SysAllUsers(Program.Dept_Code);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListBox1.Items.Add("[" + dt.Rows[i]["user_code"].ToString() + "]" + dt.Rows[i]["user_name"]);
                    }
                    dt.Clear();
                }
            }
            else
            {
                toolStrip2.Visible = false;
                ListView1.Enabled = false;
                TreeView1.Enabled = false;
                ListBox1.Items.Add("[" + Program.User_Code + "]" + Program.User_Name);
            }
            //加载所有权限
            int parentNodeInd = TreeView1.Nodes.Add("权限列表").Index;
            dt = ins.GetMenus();
            if (dt != null)
            {
                List<string> listParentMenu = new List<String>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (listParentMenu.Contains(dt.Rows[i]["menu_tag"].ToString()) == false)
                    {
                        listParentMenu.Add(dt.Rows[i]["menu_tag"].ToString());
                        int curPInd = TreeView1.Nodes[parentNodeInd].Nodes.Add(dt.Rows[i]["menu_name"].ToString()).Index;
                        TreeView1.Nodes[parentNodeInd].Nodes[curPInd].Tag = dt.Rows[i]["menu_tag"].ToString();
                    }
                }
            }
            TreeView1.Nodes[parentNodeInd].ExpandAll();
            TreeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(NodeMouseDoubleClick);
            //清空用户权限
            ListView1.Items.Clear();
        }
        //绑定数据库数据
        private void NodeMouseDoubleClick(Object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null)
            {
                var curItem = ListView1.FindItemWithText(e.Node.Text);
                if (curItem == null)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = e.Node.Text;
                    lvi.SubItems.Add(e.Node.Tag.ToString());
                    ListView1.Items.Add(lvi);
                    cmdSave.Enabled = true;
                }
            }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (ListView1.SelectedItems.Count > 0)
            {
                ListView1.Items.Remove(ListView1.SelectedItems[0]);
                cmdSave.Enabled = true;
            }
        }
        //保存用户权限
        private void cmdSave_Click(object sender, EventArgs e)
        {
            string strUserNo;
            if (ListBox1.Items.Count == 0)
            {
                return;
            }
            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            try
            {
                int iEndPos = 0;
                iEndPos = Microsoft.VisualBasic.Strings.InStr(2, ListBox1.SelectedItem.ToString(), "]");
                if (iEndPos == 0)
                {
                    return;
                }
                strUserNo = Microsoft.VisualBasic.Strings.Mid(ListBox1.SelectedItem.ToString(), 2, iEndPos - 2);
                //保存个人权限
                DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
                Boolean zxresult = ins.DelUserQx(strUserNo);
                if (zxresult)
                {
                    for (int i = 0; i < ListView1.Items.Count; i++)
                    {
                        ins.InsertUserQx(strUserNo, ListView1.Items[i].Text, ListView1.Items[i].SubItems[1].Text);
                    }
                }
                cmdSave.Enabled = false;

                ListView1.Items.Clear();

                Frm_TJInfo("提示", "权限处理成功！");
            }
            catch
            {

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
        private void cmdCancle_Click(object sender, EventArgs e)
        {
            cmdSave.Enabled = false;
            ListView1.Items.Clear();
        }

        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListBox1_Click(object sender, EventArgs e)
        {
            ListView1.Items.Clear();
            string strUserNo;
            if (ListBox1.Items.Count == 0)
            {
                return;
            }
            if (ListBox1.SelectedItem == null)
            {
                return;
            }
            int iEndPos = 0;
            iEndPos = Microsoft.VisualBasic.Strings.InStr(2, ListBox1.SelectedItem.ToString(), "]");
            if (iEndPos == 0)
            {
                return;
            }
            strUserNo = Microsoft.VisualBasic.Strings.Mid(ListBox1.SelectedItem.ToString(), 2, iEndPos - 2);

            //加载个人权限
            DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
            DataTable dt = ins.GetUserQx(strUserNo);
            if (dt != null)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ListViewItem lvi = new ListViewItem();
                    lvi.Text = dt.Rows[i]["menu_name"].ToString();
                    lvi.SubItems.Add(dt.Rows[i]["menu_tag"].ToString());
                    ListView1.Items.Add(lvi);
                }
            }

        }
        //添加用户
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Frm_UserAdd ins = new Frm_UserAdd();
            ins.Owner = this;
            ins.BringToFront();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                ListView1.Items.Clear();
                ListBox1.Items.Clear();
                DBHelper.BLL.sys_user insS = new DBHelper.BLL.sys_user();
                DataTable dt = insS.SysAllUsers(Program.Dept_Code);
                if (dt != null)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListBox1.Items.Add("[" + dt.Rows[i]["user_code"].ToString() + "]" + dt.Rows[i]["user_name"]);
                    }
                    dt.Clear();
                }
            }
            this.Focus();
        }
        //更新用户
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string strUserNo;
            if (ListBox1.Items.Count == 0)
            {
                return;
            }
            if (ListBox1.SelectedItem == null)
            {
                Frm_TJInfo("提示", "请选择要更新的用户！");
                return;
            }
            int iEndPos = 0;
            iEndPos = Microsoft.VisualBasic.Strings.InStr(2, ListBox1.SelectedItem.ToString(), "]");
            if (iEndPos == 0)
            {
                return;
            }
            strUserNo = Microsoft.VisualBasic.Strings.Mid(ListBox1.SelectedItem.ToString(), 2, iEndPos - 2);

            Frm_UserUpdate ins = new Frm_UserUpdate();
            ins.Owner = this;
            ins.user_code = strUserNo;
            ins.BringToFront();
            ins.ShowDialog();
            this.Focus();

        }
        //删除用户
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            string strUserNo;
            if (ListBox1.Items.Count == 0)
            {
                return;
            }
            if (ListBox1.SelectedItem == null)
            {
                Frm_TJInfo("提示", "请选择要删除的用户！");

                return;
            }
            int iEndPos = 0;
            iEndPos = Microsoft.VisualBasic.Strings.InStr(2, ListBox1.SelectedItem.ToString(), "]");
            if (iEndPos == 0)
            {
                return;
            }
            strUserNo = Microsoft.VisualBasic.Strings.Mid(ListBox1.SelectedItem.ToString(), 2, iEndPos - 2);
            eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
            TaskDialog.EnableGlass = false;

            if (TaskDialog.Show("删除用户", "确认", "确定要删除此用户么？", Curbutton) == eTaskDialogResult.Ok)
            {
                DBHelper.BLL.sys_user ins = new DBHelper.BLL.sys_user();
                Boolean zxresult = ins.DelUserInfo(strUserNo);
                if (zxresult == true)
                {
                    ins.DelDoctorInfo(strUserNo);
                    Frm_TJInfo("提示", "删除用户成功！");
                    ListView1.Items.Clear();
                    ListBox1.Items.Clear();
                    DataTable dt = ins.SysAllUsers(Program.Dept_Code);
                    if (dt != null)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ListBox1.Items.Add("[" + dt.Rows[i]["user_code"].ToString() + "]" + dt.Rows[i]["user_name"]);
                        }
                        dt.Clear();
                    }
                }
            }
        }


    }
}
