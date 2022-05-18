using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys
{
    public partial class FrmMessage : DevComponents.DotNetBar.Office2007Form
    {
        public FrmMessage()
        {
            InitializeComponent();
        }
        Font fontTime = new Font("微软雅黑", 10, FontStyle.Regular);
        Font fontMessage = new Font("宋体", 14.5f, FontStyle.Regular);
        Color colorMessage = Color.Black;
        Color colorTime = Color.SeaGreen;
        private int MessageMaxId = 0;
        private void FrmMessage_Load(object sender, EventArgs e)
        {
            DBHelper.BLL.sys_user sys_user_ins = new DBHelper.BLL.sys_user();
            DataTable dt = sys_user_ins.GetMsgUserInfo();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DevComponents.Editors.ComboItem cmbitem = new DevComponents.Editors.ComboItem();
                    cmbitem.Text = dt.Rows[i]["user_name"].ToString();
                    cmbitem.Value = dt.Rows[i]["user_code"].ToString();
                    this.comboBoxItem1.Items.Add(cmbitem);
                }
                comboBoxItem1.SelectedIndex = 0;
                dt.Clear();
            }
            this.ActiveControl = richTextBoxEx2;
            richTextBoxEx2.Focus();
        }
        private void RefreshMessage()
        {
            DBHelper.BLL.dept_message ins = new DBHelper.BLL.dept_message();
            if (richTextBoxEx1.Text.Trim().Equals(""))
            {
                //先插入全部当天已经读取的消息
                DataTable dtMsg = ins.GetYDMessageInfo(Program.User_Code);
                if (dtMsg != null)
                {
                    for (int i = 0; i < dtMsg.Rows.Count; i++)
                    {
                        string time = dtMsg.Rows[i]["create_datetime"].ToString();
                        string Form_user = dtMsg.Rows[i]["fromUser_Name"].ToString();
                        string message = dtMsg.Rows[i]["message"].ToString();
                        //插入消息展示
                        InsertMessage(Form_user, time, message);
                    }
                    //文本框滚动到最后
                    richTextBoxEx1.Select(richTextBoxEx1.Text.Length, 0);
                    richTextBoxEx1.ScrollToCaret();
                }
            }
            //所有未读消息
            DataTable dtMessage = ins.GetMessageInfo(Program.User_Code);
            if (dtMessage != null)
            {
                for (int i = 0; i < dtMessage.Rows.Count; i++)
                {
                    string time = dtMessage.Rows[i]["create_datetime"].ToString();
                    string Form_user = dtMessage.Rows[i]["fromUser_Name"].ToString();
                    string message = dtMessage.Rows[i]["message"].ToString();
                    MessageMaxId = Convert.ToInt32(dtMessage.Rows[i]["id"]);
                    //插入消息展示
                    InsertMessage(Form_user, time, message);
                }
                //设置已经读取
                Boolean flag = ins.UpdateMessageInfo(Program.User_Code, MessageMaxId);
                //文本框滚动到最后
                richTextBoxEx1.Select(richTextBoxEx1.Text.Length, 0);
                richTextBoxEx1.ScrollToCaret();
            }
        }
        private void InsertMessage(string Form_user, string time, string message)
        {
            int count = richTextBoxEx1.Text.Length;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}来自({1})的消息:", time, Form_user);
            sb.AppendLine();
            richTextBoxEx1.AppendText(sb.ToString());
            richTextBoxEx1.Select(count, sb.ToString().Length);
            richTextBoxEx1.SelectionColor = colorTime;
            richTextBoxEx1.SelectionFont = fontTime;
            sb.Clear();

            count = richTextBoxEx1.Text.Length;
            richTextBoxEx1.AppendText(message);
            richTextBoxEx1.Select(count, message.Length);
            richTextBoxEx1.SelectionColor = colorMessage;
            richTextBoxEx1.SelectionFont = fontMessage;
            //添加换行
            sb.AppendLine();
            richTextBoxEx1.AppendText(sb.ToString());
            sb.Clear();
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            RefreshMessage();
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
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (richTextBoxEx2.Text.Trim() == "")
            {
                Frm_TJInfo("提示", "发送消息不可为空！");
                richTextBoxEx2.Focus();
                return;
            }
            string User_value_Str = ((DevComponents.Editors.ComboItem)(comboBoxItem1.SelectedItem)).Value.ToString();
            string User_name_Str = ((DevComponents.Editors.ComboItem)(comboBoxItem1.SelectedItem)).Text.ToString();
            //开始入库
            if (User_value_Str.Equals("9999"))
            {

                int total = 0;
                foreach (DevComponents.Editors.ComboItem cmbitem in comboBoxItem1.Items)
                {
                    if (cmbitem.Value.ToString().Equals("9999"))
                    {
                        continue;
                    }
                    DBHelper.Model.dept_message insMinfo = new DBHelper.Model.dept_message();
                    insMinfo.fromUser_Code = Program.User_Code;
                    insMinfo.fromUser_Name = Program.User_Name;
                    insMinfo.toUser_Code = cmbitem.Value.ToString();
                    insMinfo.toUser_Name = cmbitem.Text.ToString();
                    insMinfo.message = richTextBoxEx2.Text.Trim();
                    DBHelper.BLL.dept_message ins = new DBHelper.BLL.dept_message();
                    if (ins.InsertMessage(insMinfo))
                    {
                        total++;
                    }
                    else
                    {
                        total--;
                    }
                }
                if (total == comboBoxItem1.Items.Count - 1)
                {
                    Frm_TJInfo("提示", "全部发送消息成功！");
                    richTextBoxEx2.Text = "";
                    richTextBoxEx2.Focus();

                }
                else
                {
                    Frm_TJInfo("提示", "全部发送消息失败！");
                }

            }
            else
            {
                DBHelper.Model.dept_message insMinfo = new DBHelper.Model.dept_message();
                insMinfo.fromUser_Code = Program.User_Code;
                insMinfo.fromUser_Name = Program.User_Name;
                insMinfo.toUser_Code = User_value_Str;
                insMinfo.toUser_Name = User_name_Str;
                insMinfo.message = richTextBoxEx2.Text.Trim();
                DBHelper.BLL.dept_message ins = new DBHelper.BLL.dept_message();
                if (ins.InsertMessage(insMinfo))
                {
                    if (Program.User_Code != insMinfo.toUser_Code)
                    {
                        string time = DateTime.Now.ToString();
                        string Form_user = Program.User_Name;
                        string message = richTextBoxEx2.Text.Trim();
                        //插入消息展示
                        InsertMessage(Form_user, time, message);
                        //文本框滚动到最后
                        richTextBoxEx1.Select(richTextBoxEx1.Text.Length, 0);
                        richTextBoxEx1.ScrollToCaret();
                    }
                    Frm_TJInfo("提示", "发送消息成功！");
                    richTextBoxEx2.Text = "";
                    richTextBoxEx2.Focus();
                }
                else
                {
                    Frm_TJInfo("提示", "发送消息失败！");
                }
            }
            //入库结束刷新
            RefreshMessage();
        }



        private void FrmMessage_Activated(object sender, EventArgs e)
        {
            //入库结束刷新
            RefreshMessage();
        }
    }
}
