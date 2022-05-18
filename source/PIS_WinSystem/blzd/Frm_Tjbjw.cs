using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_Tjbjw : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_Tjbjw()
        {
            InitializeComponent();
        }
        public string tjType = "";
        public string tjItems = "";
        public string taocan_type = "";
        public int tjcount = 0;
        //确认特检套餐
        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (comboBoxEx2.Text == "")
            {
                tjType = "个选";
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        tjItems += checkedListBox1.GetItemText(checkedListBox1.Items[i]) + ",";

                    }
                }
                if (!tjItems.Equals(""))
                {
                    tjItems = tjItems.Substring(0, tjItems.Length - 1);
                }
                tjcount = integerInput1.Value;
            }
            else
            {
                int id = Convert.ToInt32(comboBoxEx2.SelectedValue);
                DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                DataTable dt = ins.GetAllTaocanDTBjw(id);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tjItems += dt.Rows[i]["bjw_name"].ToString() + ",";
                    tjType = dt.Rows[i]["taocan_name"].ToString();
                    taocan_type = dt.Rows[i]["taocan_type"].ToString();
                }
                if (!tjItems.Equals(""))
                {
                    tjItems = tjItems.Substring(0, tjItems.Length - 1);
                }
                tjcount = integerInput1.Value;
            }

            DialogResult = DialogResult.OK;
        }

        //添加标记物
        private void buttonX5_Click_1(object sender, EventArgs e)
        {
            Frm_bjw ins = new Frm_bjw();
            ins.Owner = this;
            ins.BringToFront();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                Frm_TJInfo("提示", "标记物添加成功！");
                //刷新标记物

                DBHelper.BLL.tjbjw insbjw = new DBHelper.BLL.tjbjw();
                DataTable bjwDt = insbjw.GetAllDTBjw();
                if (bjwDt != null && bjwDt.Rows.Count > 0)
                {
                    checkedListBox1.DataSource = bjwDt;
                    checkedListBox1.DisplayMember = "bjw_name";
                    checkedListBox1.ValueMember = "id";
                }
                else
                {
                    checkedListBox1.DataSource = null;
                }
            }

        }
        //删除标记物
        private void buttonX1_Click_1(object sender, EventArgs e)
        {
            if (checkedListBox1.Items.Count > 0)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除标记物", "确认", "确定要删除选中的标记物么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.GetItemChecked(i))
                        {
                            checkedListBox1.SelectedIndex = i;//利用SelectedValue取得Value值时，只能取得当前焦点项的值。所以要对整个CheckedListBox中的所有勾选项,让其都做一次焦点项才能取得所有勾选的项的值。
                            int id = Convert.ToInt32(checkedListBox1.SelectedValue);
                            //删除标记物
                            ins.delBjw(id);
                            //删除套餐中的标记物
                            ins.Deltj_bjw(id);
                        }
                    }
                    //刷新标记物


                    DataTable bjwDt = ins.GetAllDTBjw();
                    if (bjwDt != null && bjwDt.Rows.Count > 0)
                    {
                        checkedListBox1.DataSource = bjwDt;
                        checkedListBox1.DisplayMember = "bjw_name";
                        checkedListBox1.ValueMember = "id";
                    }
                    else
                    {
                        checkedListBox1.DataSource = null;
                    }
                }
            }
        }
        //添加套餐
        private void buttonX3_Click(object sender, EventArgs e)
        {
            Frm_taocan ins = new Frm_taocan();
            ins.Owner = this;
            ins.BringToFront();
            if (ins.ShowDialog() == DialogResult.OK)
            {
                Frm_TJInfo("提示", "特检套餐添加成功！");
                //加载所有套餐
                DBHelper.BLL.tjbjw insbjw = new DBHelper.BLL.tjbjw();
                DataTable dtTaocan = insbjw.GetAllDTTaocan();
                if (dtTaocan != null && dtTaocan.Rows.Count > 0)
                {
                    comboBoxEx1.DataSource = dtTaocan;
                    comboBoxEx1.DisplayMember = "taocan_name";
                    comboBoxEx1.ValueMember = "id";
                }
                else
                {
                    comboBoxEx1.DataSource = null;
                }
                DataTable dtTaocan1 = insbjw.GetAllDTTaocan1();
                if (dtTaocan1 != null && dtTaocan1.Rows.Count > 0)
                {
                    comboBoxEx2.DataSource = dtTaocan1;
                    comboBoxEx2.DisplayMember = "taocan_name";
                    comboBoxEx2.ValueMember = "id";
                }
                else
                {
                    comboBoxEx2.DataSource = null;
                }
            }
        }
        //追加至特检套餐
        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.Text.Trim() != "")
            {
                int taocanid = Convert.ToInt32(comboBoxEx1.SelectedValue);
                DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        checkedListBox1.SelectedIndex = i;//利用SelectedValue取得Value值时，只能取得当前焦点项的值。所以要对整个CheckedListBox中的所有勾选项,让其都做一次焦点项才能取得所有勾选的项的值。
                        int id = Convert.ToInt32(checkedListBox1.SelectedValue);
                        if (ins.GetBjwtaocanCount(id, taocanid) == 0)
                        {
                            ins.InsertBjwtaocan(id, taocanid);
                        }
                    }

                }
                Frm_TJInfo("提示", "追加成功！");

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
        //清空选择
        private void buttonX6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, false);
            }
        }

        private void Frm_Tjbjw_Load(object sender, EventArgs e)
        {
            checkedListBox1.CheckOnClick = true;
            //加载所有标记物
            DBHelper.BLL.tjbjw insbjw = new DBHelper.BLL.tjbjw();
            DataTable bjwDt = insbjw.GetAllDTBjw();
            if (bjwDt != null && bjwDt.Rows.Count > 0)
            {
                checkedListBox1.DataSource = bjwDt;
                checkedListBox1.DisplayMember = "bjw_name";
                checkedListBox1.ValueMember = "id";
            }
            //加载所有套餐
            DataTable dtTaocan = insbjw.GetAllDTTaocan();
            if (dtTaocan != null && dtTaocan.Rows.Count > 0)
            {
                comboBoxEx1.DataSource = dtTaocan;
                comboBoxEx1.DisplayMember = "taocan_name";
                comboBoxEx1.ValueMember = "id";
            }
            else
            {
                comboBoxEx1.DataSource = null;
            }
            DataTable dtTaocan1 = insbjw.GetAllDTTaocan1();
            if (dtTaocan1 != null && dtTaocan1.Rows.Count > 0)
            {
                comboBoxEx2.DataSource = dtTaocan1;
                comboBoxEx2.DisplayMember = "taocan_name";
                comboBoxEx2.ValueMember = "id";
            }
            else
            {
                comboBoxEx2.DataSource = null;
            }
            comboBoxEx2.SelectedIndexChanged += new EventHandler(comboBoxEx2_SelectedIndexChanged);
            textBox1.Focus();
        }
        //删除套餐
        private void buttonX7_Click(object sender, EventArgs e)
        {
            if (comboBoxEx1.Items.Count > 0)
            {

                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除特检套餐", "确认", "确定要删除选中的特检套餐么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();

                    if (comboBoxEx1.Text != "")
                    {

                        int id = Convert.ToInt32(comboBoxEx1.SelectedValue);
                        //删除套餐
                        ins.delTaocan(id);
                        //删除套餐
                        ins.Deltj_Taocan(id);
                    }
                    //加载所有套餐
                    DBHelper.BLL.tjbjw insbjw = new DBHelper.BLL.tjbjw();
                    DataTable dtTaocan = insbjw.GetAllDTTaocan();
                    if (dtTaocan != null && dtTaocan.Rows.Count > 0)
                    {
                        comboBoxEx1.DataSource = dtTaocan;
                        comboBoxEx1.DisplayMember = "taocan_name";
                        comboBoxEx1.ValueMember = "id";
                    }
                    else
                    {
                        comboBoxEx1.DataSource = null;
                    }
                    DataTable dtTaocan1 = insbjw.GetAllDTTaocan1();
                    if (dtTaocan1 != null && dtTaocan1.Rows.Count > 0)
                    {
                        comboBoxEx2.DataSource = dtTaocan1;
                        comboBoxEx2.DisplayMember = "taocan_name";
                        comboBoxEx2.ValueMember = "id";
                    }
                    else
                    {
                        comboBoxEx2.DataSource = null;
                    }

                }
            }
        }
        //特检套餐切换
        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxEx2.Text != "")
            {
                ListView1.Items.Clear();
                int id = Convert.ToInt32(comboBoxEx2.SelectedValue);
                DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                DataTable dt = ins.GetAllTaocanDTBjw(id);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ListViewItem lvi = new ListViewItem();
                        lvi.Text = dt.Rows[i]["bjw_name"].ToString();
                        lvi.SubItems.Add(dt.Rows[i]["bjw_id"].ToString());
                        ListView1.Items.Add(lvi);
                    }
                }
            }
        }

        private void ListView1_DoubleClick(object sender, EventArgs e)
        {
            if (comboBoxEx2.Text != "")
            {
                if (ListView1.SelectedItems.Count > 0)
                {
                    eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                    TaskDialog.EnableGlass = false;

                    if (TaskDialog.Show("删除套餐标记物", "确认", "确定要删除套餐中的这条标记物么？", Curbutton) == eTaskDialogResult.Ok)
                    {
                        int taocan_id = Convert.ToInt32(comboBoxEx2.SelectedValue);
                        int bjw_id = Convert.ToInt32(ListView1.SelectedItems[0].SubItems[1].Text);
                        DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
                        int result = ins.DelOneBjwtaocan(bjw_id, taocan_id);
                        if (result > 0)
                        {
                            ListView1.Items.Remove(ListView1.SelectedItems[0]);
                            Frm_TJInfo("提示", "套餐中标记物删除成功！");
                        }
                    }
                }
            }
        }
        //编辑标记物
        private void buttonX8_Click(object sender, EventArgs e)
        {
            DBHelper.BLL.tjbjw ins = new DBHelper.BLL.tjbjw();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SelectedIndex = i;//利用SelectedValue取得Value值时，只能取得当前焦点项的值。所以要对整个CheckedListBox中的所有勾选项,让其都做一次焦点项才能取得所有勾选的项的值。
                    int id = Convert.ToInt32(checkedListBox1.SelectedValue);
                    Frm_edit_bjw ins_e = new Frm_edit_bjw(id);
                    ins_e.Owner = this;
                    ins_e.BringToFront();
                    if (ins_e.ShowDialog() == DialogResult.OK)
                    {
                        Frm_TJInfo("提示", "标记物编辑成功！");
                    }
                }
            }
        }
        //独立添加一条
        private void buttonX9_Click(object sender, EventArgs e)
        {
            string bjw = textBox1.Text.Trim().Replace("'", "");
            if (!bjw.Equals(""))
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = bjw;
                ListView1.Items.Add(lvi);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = checkedListBox1.DataSource as DataTable;

            if (dt != null)
            {
                if (string.IsNullOrEmpty(this.textBox1.Text.Trim()))
                    dt.DefaultView.RowFilter = string.Empty;
                else
                    dt.DefaultView.RowFilter = string.Format("bjw_name like '{0}%'", this.textBox1.Text.Replace("'", ""));
            }
        }
    }
}
