using GHIBMS.Interface;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace GHIBMS.Server
{
    public enum BacnetWritePriority
    {
        NO_PRIORITY = 0,
        MANUAL_LIFE_SAFETY = 1,
        AUTOMATIC_LIFE_SAFETY = 2,
        UNSPECIFIED_LEVEL_3 = 3,
        UNSPECIFIED_LEVEL_4 = 4,
        CRITICAL_EQUIPMENT_CONTROL = 5,
        MINIMUM_ON_OFF = 6,
        UNSPECIFIED_LEVEL_7 = 7,
        MANUAL_OPERATOR = 8,
        UNSPECIFIED_LEVEL_9 = 9,
        UNSPECIFIED_LEVEL_10 = 10,
        UNSPECIFIED_LEVEL_11 = 11,
        UNSPECIFIED_LEVEL_12 = 12,
        UNSPECIFIED_LEVEL_13 = 13,
        UNSPECIFIED_LEVEL_14 = 14,
        UNSPECIFIED_LEVEL_15 = 15,
        LOWEST_AND_DEFAULT = 16
    }

    public partial class FormPriority : Form
    {
        IVariable curVar;
        public FormPriority(IVariable var)
        {
            curVar = var;
            InitializeComponent();
            cmbNewPriority.DataSource = Enum.GetNames(typeof(BacnetWritePriority));
            lblCurValue.Text = var.Value.ToString();
            lblCurPriority.Text = ((BacnetWritePriority)var.WritePriority).ToString();
            this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度

            listView1.Columns.Add(new ColumnHeader() { Text = "序号", Width = 50 });
            listView1.Columns.Add(new ColumnHeader() { Text = "优先级", Width = 250 });
            listView1.Columns.Add(new ColumnHeader() { Text = "数值" });

            string[] arr = null;
            try
            {
                if (var.Priority.ToString() != "" && var.Priority.ToString().IndexOf(',') > -1)
                    arr = JsonConvert.DeserializeObject<string[]>(var.Priority.ToString());
            }
            catch { }
            try
            {
                for (int i = 0; i < 17; i++)   //添加17行数据
                {
                    ListViewItem lvi = new ListViewItem();

                    lvi.Text = i.ToString();
                    lvi.SubItems.Add(((BacnetWritePriority)i).ToString());
                    if (i > 0 && arr != null && i <= arr.Length)
                        lvi.SubItems.Add(arr[i - 1]);
                    else lvi.SubItems.Add("");
                    this.listView1.Items.Add(lvi);
                }
            }
            catch { }
            // 第一列宽度 + 第二列宽度 = 工作区宽度
            listView1.Columns[2].Width = listView1.ClientSize.Width - listView1.Columns[0].Width - listView1.Columns[1].Width;



            this.listView1.EndUpdate();  //结束数据处理，UI界面一次性绘制。
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Items.Count > cmbNewPriority.SelectedIndex)
                listView1.Items[cmbNewPriority.SelectedIndex].Selected = true;
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNewValue.Text))
            {
                string value = txtNewValue.Text;
                uint priority = (uint)cmbNewPriority.SelectedIndex;
                string[] args = new string[3];
                args[0] = curVar.ID;
                args[1] = priority.ToString();
                args[2] = txtNewValue.Text;
                int BacnetCmdCode = 101;
                curVar.ControllerObject.ChannelObject.ExecCommand(curVar.ControllerObject, BacnetCmdCode, args);
            }
            else
            {
                MessageBox.Show("设定值不能为空！");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            lblCurValue.Text = curVar.Value.ToString();
            lblCurPriority.Text = ((BacnetWritePriority)curVar.WritePriority).ToString();

            string[] arr = null;
            try
            {
                if (curVar.Priority.ToString() != "" && curVar.Priority.ToString().IndexOf(',') > -1)
                    arr = JsonConvert.DeserializeObject<string[]>(curVar.Priority.ToString());
            }
            catch { }
            try
            {
                for (int i = 0; i < 17; i++)
                {
                    ListViewItem lvi = listView1.Items[i];
                    if (i > 0 && arr != null && i < arr.Length)
                        lvi.SubItems[2].Text = arr[i - 1];
                    else lvi.SubItems[2].Text = "";
                }
            }
            catch { }
        }

        private void btnRelingquish_Click(object sender, EventArgs e)
        {

            uint priority = (uint)cmbNewPriority.SelectedIndex;
            string[] args = new string[3];
            args[0] = curVar.ID;
            args[1] = priority.ToString();
            int BacnetCmdCode = 100;
            curVar.ControllerObject.ChannelObject.ExecCommand(curVar.ControllerObject, BacnetCmdCode, args);

        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                cmbNewPriority.SelectedIndex = listView1.SelectedItems[0].Index;
            }
        }
    }
}
