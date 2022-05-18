using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace PIS_Sys.xtsz
{
    public partial class Frm_bgd_type : DevComponents.DotNetBar.Office2007Form
    {
        public Frm_bgd_type()
        {
            InitializeComponent();
        }

        private void Frm_bgd_type_Load(object sender, EventArgs e)
        {
            checkedListBox1.CheckOnClick = true;
            DBHelper.BLL.exam_type ins = new DBHelper.BLL.exam_type();
            DataTable dt = ins.GetAllDTExam_Type();
            if (dt != null && dt.Rows.Count > 0)
            {
                checkedListBox1.DataSource = dt;
                checkedListBox1.DisplayMember = "modality_cn";
                checkedListBox1.ValueMember = "modality";
            }
            string str = "";
            //设置选中
            if (!str.Equals(""))
            {
                string[] txts = str.Split(',');
                for (int j = 0; j < txts.Length; j++)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (checkedListBox1.GetItemText(checkedListBox1.Items[i]).Equals(txts[j]))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
            }

        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            string str = "";
            string strText = "";
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    checkedListBox1.SelectedIndex = i;//利用SelectedValue取得Value值时，只能取得当前焦点项的值。所以要对整个CheckedListBox中的所有勾选项,让其都做一次焦点项才能取得所有勾选的项的值。
                    str += string.Format("'{0}'", checkedListBox1.SelectedValue) + ",";
                    strText += checkedListBox1.GetItemText(checkedListBox1.Items[i]) + ",";
                }
            }
            if (str.Equals(""))
            {
                Frm_TJInfo("提示", "必须选择至少一种类型的报告！");
                return;
            }
            else
            {
                Properties.Settings.Default.Save();
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
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
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
