using DevComponents.DotNetBar;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PIS_Sys.blzd
{
    public partial class Frm_tjyz : DevComponents.DotNetBar.Office2007Form
    {
        public string BLH = "";
        public string SQD = "";
        public string BRXM = "";
        public Frm_tjyz()
        {
            InitializeComponent();
            this.Width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            this.CenterToScreen();
            //列表样式
            GridPanel panel2 = superGridControl2.PrimaryGrid;
            panel2.MinRowHeight = 30;
            for (int i = 0; i < panel2.Columns.Count; i++)
            {
                panel2.Columns[i].ColumnSortMode = ColumnSortMode.None;
                panel2.Columns[i].CellStyles.Default.Font = this.Font;
            }
        }

        private void Frm_tjyz_Load(object sender, EventArgs e)
        {
            txt_blh.Text = BLH;
            txt_sqd.Text = SQD;
            txt_brxm.Text = BRXM;
            //
            DBHelper.BLL.exam_tjyz insFx = new DBHelper.BLL.exam_tjyz();
            List<string> lst_TjResults = insFx.GetTj_Results();
            if (lst_TjResults.Count > 0)
            {
                superGridControl2.PrimaryGrid.Columns["fx_result"].EditorType = typeof(FragrantComboBox);
                superGridControl2.PrimaryGrid.Columns["fx_result"].EditorParams = new object[] { lst_TjResults.ToArray() };
                lst_TjResults.Clear();
            }
            //高度自动适应
            superGridControl2.PrimaryGrid.DefaultRowHeight = 0;
            superGridControl2.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.DisplayedCells;
            RefreshData();
        }
        private void RefreshData()
        {
            //展示特检医嘱信息
            DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
            DataTable dt = ins.GetDataTjyz(BLH);
            if (dt != null && dt.Rows.Count > 0)
            {
                superGridControl2.PrimaryGrid.DataSource = dt;
            }
            else
            {
                superGridControl2.PrimaryGrid.DataSource = null;
            }

        }
        public string barcode;
        public string parts;
        public int qcid;
        private void buttonX1_Click(object sender, EventArgs e)
        {
            //查询是否已经录入了标本信息 exam_specimens
            DBHelper.BLL.exam_specimens insSpec = new DBHelper.BLL.exam_specimens();
            DataTable dt = insSpec.GetSpecimensInfo(SQD);
            if (dt != null && dt.Rows.Count > 0)
            {
                Frm_chancelk ins = new Frm_chancelk();
                ins.Owner = this;
                ins.BLH = txt_blh.Text.Trim();
                ins.SQD = SQD;
                ins.BringToFront();
                if (ins.ShowDialog() == DialogResult.OK)
                {
                    qcid = ins.id;
                    parts = ins.parts;
                    barcode = ins.barcode;
                    txt_lkh.Text = barcode;
                }
            }
            else
            {
                Frm_TJInfo("提示", "此检查没有录入标本信息！");
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
        //添加
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (!txt_lkh.Text.Trim().Equals(""))
            {
                //生成医嘱编号
                DBHelper.BLL.sys_sequence sys_sequence_ins = new DBHelper.BLL.sys_sequence();
                DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                Frm_Tjbjw InsTj = new Frm_Tjbjw();
                InsTj.Owner = this;
                InsTj.BringToFront();
                if (InsTj.ShowDialog() == DialogResult.OK)
                {
                    Boolean result = false;
                    string tjitems = InsTj.tjItems;
                    if (tjitems == "")
                    {
                        return;
                    }
                    int tjcount = InsTj.tjcount;
                    string tjtype = InsTj.tjType;
                    string taocan_type = InsTj.taocan_type;
                    string[] txts = tjitems.Split(',');
                    for (int j = 0; j < txts.Length; j++)
                    {
                        string yzstr = sys_sequence_ins.GetYZID_Sequence();
                        if (tjtype != "个选")
                        {
                            string myzh_yl = "";
                            if (taocan_type.Equals("免疫组化"))
                            {
                                myzh_yl = "甲级";
                            }
                            string fz_hg = "";
                            if (taocan_type.Equals("分子病理"))
                            {
                                fz_hg = "合格";
                            }
                            result = ins.InsertData(yzstr, BLH, barcode, tjtype, txts[j], tjcount, Program.User_Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "申请", "", textBoxX1.Text.Trim(), 1, qcid, taocan_type, myzh_yl, fz_hg);
                        }
                        else
                        {
                            //获取标记物类型
                            DBHelper.BLL.tjbjw insBjw = new DBHelper.BLL.tjbjw();
                            string bjw_type = insBjw.GetbjwType(txts[j]);
                            string myzh_yl = "";

                            if (bjw_type.Equals("免疫组化"))
                            {
                                myzh_yl = "甲级";
                            }
                            string fz_hg = "";
                            if (bjw_type.Equals("分子病理"))
                            {
                                fz_hg = "合格";
                            }
                            result = ins.InsertData(yzstr, BLH, barcode, txts[j], txts[j], tjcount, Program.User_Name, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "申请", "", textBoxX1.Text.Trim(), 1, qcid, bjw_type, myzh_yl, fz_hg);
                        }

                    }

                    if (result == true)
                    {
                        //qcid = 0;
                        //parts = "";
                        //barcode = "";
                        //txt_lkh.Text = "";
                        Frm_TJInfo("提示", "添加特检医嘱成功！");
                        RefreshData();
                    }
                }
            }
            else
            {
                Frm_TJInfo("提示", "请先选择蜡块号！");
            }
        }
        //删除
        private void buttonX4_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                eTaskDialogButton Curbutton = eTaskDialogButton.Ok | eTaskDialogButton.Cancel;
                TaskDialog.EnableGlass = false;
                if (TaskDialog.Show("删除特检医嘱", "确认", "确定要删除这条特检医嘱信息么？", Curbutton) == eTaskDialogResult.Ok)
                {
                    if (superGridControl2.ActiveRow == null)
                    {
                        return;
                    }
                    int index = superGridControl2.ActiveRow.Index;
                    if (index == -1)
                    {
                        return;
                    }
                    //开始执行删除操作
                    int id = Convert.ToInt32(((GridRow)superGridControl2.PrimaryGrid.Rows[index]).Cells["id"].Value);
                    string barcode = ((GridRow)superGridControl2.PrimaryGrid.Rows[index]).Cells["barcode"].Value.ToString();
                    string yz_flag = ((GridRow)superGridControl2.PrimaryGrid.Rows[index]).Cells["yz_flag"].Value.ToString();

                    DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                    Boolean result = ins.DelData(id);
                    if (result == true)
                    {
                        Frm_TJInfo("提示", "删除特检医嘱成功！");
                        if (!barcode.Equals(""))
                        {
                            DBHelper.BLL.exam_filmmaking InsFi = new DBHelper.BLL.exam_filmmaking();
                            InsFi.DelQpBarcode(barcode);
                        }
                        RefreshData();
                    }
                    //删除技术制片列表中的

                }
            }
            else
            {
                Frm_TJInfo("提示", "不存在特检医嘱！");
            }
        }

        //写入特检结果
        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (superGridControl2.PrimaryGrid.Rows.Count > 0)
            {
                DataTable dt = superGridControl2.PrimaryGrid.DataSource as DataTable;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string bj_name = Convert.ToString(dt.Rows[i]["bj_name"].ToString());
                    string fx_result = Convert.ToString(dt.Rows[i]["fx_result"].ToString());
                    if (fx_result != "")
                    {
                        sb.Append(bj_name);
                        sb.Append("(");
                        sb.Append(fx_result);
                        sb.Append("),");
                    }
                }
                string result = sb.ToString();
                if (result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                    //写入特检结果
                    FrmBlzd ins = this.Owner as FrmBlzd;
                    ins.WriteTjResult(result);
                    Frm_TJInfo("提示", "写入特检结果成功！");
                }
            }
        }
        //更新特检结果
        private void superGridControl2_CellValueChanged(object sender, GridCellValueChangedEventArgs e)
        {
            try
            {
                //更新当前行备注
                GridCell cell = e.GridCell;
                if (cell == null)
                {
                    return;
                }
                GridRow row = cell.GridRow;
                if (row == null)
                {
                    return;
                }
                if (row.Index == -1)
                {
                    return;
                }
                int id = Convert.ToInt32(row.Cells["id"].Value);

                if (cell.GridColumn.Name.Equals("fx_result") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新特检结果
                        DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                        ins.UpdateResultData(id, e.NewValue.ToString());
                    }
                }
                else if (cell.GridColumn.Name.Equals("memo_note") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新特检结果
                        DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                        ins.Updatememo_note(id, e.NewValue.ToString());
                    }
                }
                else if (cell.GridColumn.Name.Equals("myzh_bz") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新特检结果
                        DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                        ins.Updatemyzh_bz(id, e.NewValue.ToString());
                    }
                }
                else if (cell.GridColumn.Name.Equals("fz_bz") == true)
                {
                    if (e.NewValue != e.OldValue)
                    {
                        //更新特检结果
                        DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
                        ins.Updatefz_bz(id, e.NewValue.ToString());
                    }
                }


            }
            catch
            {

            }
            e.GridCell.InvalidateLayout();
        }

        private void Frm_tjyz_FormClosing(object sender, FormClosingEventArgs e)
        {
            //更新医嘱可用
            DBHelper.BLL.exam_tjyz ins = new DBHelper.BLL.exam_tjyz();
            ins.UpdateZCData(BLH);
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            string level = "";
            if (checkBoxX1.Checked == true)
            {
                level = checkBoxX1.Text;
            }
            else if (checkBoxX2.Checked == true)
            {
                level = checkBoxX2.Text;

            }
            else if (checkBoxX3.Checked == true)
            {

                level = checkBoxX3.Text;
            }

            SelectedElementCollection col = superGridControl2.PrimaryGrid.GetSelectedRows();
            if (col.Count > 0)
            {
                for (int i = 0; i < col.Count; i++)
                {
                    //只更新已经选中的记录
                    GridRow ins = col[i] as GridRow;
                    if (ins.Cells["taocan_type"].Value.ToString().Equals("免疫组化"))
                    {
                        //更新级别
                        DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
                        if (insTJ.UpdateMyzh_jyb(Convert.ToInt32(ins.Cells["id"].Value), level, textBoxX2.Text.Trim()) == 1)
                        {
                            RefreshData();
                            Frm_TJInfo("提示", "免疫组化切片评级成功！");
                        }

                    }
                    else
                    {
                        Frm_TJInfo("提示", "此医嘱非免疫组化，不能评级！");
                    }
                }
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            string level = "";
            if (checkBoxX6.Checked == true)
            {
                level = checkBoxX6.Text;
            }
            else if (checkBoxX5.Checked == true)
            {
                level = checkBoxX5.Text;

            }


            SelectedElementCollection col = superGridControl2.PrimaryGrid.GetSelectedRows();
            if (col.Count > 0)
            {
                for (int i = 0; i < col.Count; i++)
                {
                    //只更新已经选中的记录
                    GridRow ins = col[i] as GridRow;
                    if (ins.Cells["taocan_type"].Value.ToString().Equals("分子病理"))
                    {
                        //更新
                        DBHelper.BLL.exam_tjyz insTJ = new DBHelper.BLL.exam_tjyz();
                        if (insTJ.UpdateFzhg(Convert.ToInt32(ins.Cells["id"].Value), level, textBoxX3.Text.Trim()) == 1)
                        {
                            RefreshData();
                            Frm_TJInfo("提示", "分子病理质控设置成功！");
                        }
                    }
                    else
                    {
                        Frm_TJInfo("提示", "此医嘱非分子病理，不能质控设置！");
                    }
                }
            }
        }
        //先定义一个combox类
        internal class FragrantComboBox : GridComboBoxExEditControl
        {
            public FragrantComboBox(IEnumerable orderArray)
            {
                DataSource = orderArray;
                AutoCompleteMode = AutoCompleteMode.Suggest;
            }
        }
    }
}
