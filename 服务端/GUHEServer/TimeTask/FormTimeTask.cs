using GHIBMS.Common;
using System;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormTimeTask : Form
    {
        private TimeTaskInfo SelTask = null;
        private ListViewItem SelItem = null;
        public FormTimeTask()
        {
            InitializeComponent();
            this.lsvTask.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvTask_MouseClick);

            this.dtBegin.CustomFormat = "HH:mm";
            this.dtBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.CustomFormat = "HH:mm";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.textBoxValue.KeyPress += pubFun.NubOnly_KeyPress;
            this.lblEnd.Visible = chkKeep.Checked;
            this.dtEnd.Visible = chkKeep.Checked;

        }
        private void CreateItemHeader()
        {
            lsvTask.Clear();
            System.Windows.Forms.ColumnHeader columnHeader1;
            System.Windows.Forms.ColumnHeader columnHeader2;
            System.Windows.Forms.ColumnHeader columnHeader3;
            System.Windows.Forms.ColumnHeader columnHeader4;
            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            columnHeader2 = new System.Windows.Forms.ColumnHeader();
            columnHeader3 = new System.Windows.Forms.ColumnHeader();
            columnHeader4 = new System.Windows.Forms.ColumnHeader();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "任务名称";
            columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            columnHeader2.Text = "开始时间";
            columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            columnHeader3.Text = "结束时间";
            columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader3.Width = 70;
            // 
            // columnHeader4
            // 
            columnHeader4.Text = "动作数值";
            columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader4.Width = 70;

            lsvTask.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            columnHeader2,
            columnHeader3,
            columnHeader4});
            lsvTask.HideSelection = false;
        }
        private void AddNewTask(TimeTaskInfo task)
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            task.TaskName,
            task.BeginTime,
            task.EndTime,
            task.TaskValue.ToString()}, 0);
            listViewItem1.Tag = task;
            lsvTask.Items.Add(listViewItem1);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string uniqueName = TimeDb.GetUniqueCloneName("新建任务0");
            TimeTaskInfo task = new TimeTaskInfo();
            TimeDb.TimeTaskList.Add(task);
            task.TaskName = uniqueName;
            AddNewTask(task);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lsvTask.SelectedItems.Count > 0)
            {
                int c = this.lsvTask.SelectedItems.Count;
                for (int i = 0; i < c; i++)
                {
                    TimeTaskInfo task = this.lsvTask.SelectedItems[0].Tag as TimeTaskInfo;
                    if (TimeDb.TimeTaskList.Contains(task))
                        TimeDb.TimeTaskList.Remove(task);

                    this.lsvTask.Items.Remove(this.lsvTask.SelectedItems[0]);

                }
                lsvTask.Invalidate();
                ResetTaskDetail();
            }
        }

        private void btnAddVar_Click(object sender, EventArgs e)
        {
            //System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            //"ddddd"},1);
            //lstVariable.Items.Add(listViewItem1);
            if (SelTask != null)
            {
                FormVarList frm = new FormVarList();
                if (frm.ShowDialog() == DialogResult.OK)
                {

                    string s = frm.VarID;
                    if (!string.IsNullOrEmpty(s) && !SelTask.VarNameList.Contains(s))
                    {
                        SelTask.VarNameList.Add(s);
                        AddNewVariable(s);
                    }

                }
            }

        }
        private void AddNewVariable(string var)
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            var}, 1);
            listViewItem1.Tag = var;
            lstVariable.Items.Add(listViewItem1);


        }

        private void btnDelVar_Click(object sender, EventArgs e)
        {
            if (lstVariable.SelectedItems.Count > 0)
            {
                int c = this.lstVariable.SelectedItems.Count;
                for (int i = 0; i < c; i++)
                {
                    if (this.lstVariable.SelectedItems[0].Tag != null)
                    {
                        if (SelTask != null)
                            SelTask.VarNameList.Remove(this.lstVariable.SelectedItems[0].Tag as string);
                    }
                    this.lstVariable.Items.Remove(this.lstVariable.SelectedItems[0]);

                }
                lstVariable.Invalidate();
            }

        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lsvTask.SelectedItems.Count > 0)
            {

                int n = lsvTask.SelectedItems[0].Index;
                if (n > 0)
                {
                    ListViewItem item = lsvTask.SelectedItems[0];
                    TimeTaskInfo task = item.Tag as TimeTaskInfo;
                    if (TimeDb.TimeTaskList.Contains(task))
                    {
                        TimeDb.TimeTaskList.Remove(task);
                        TimeDb.TimeTaskList.Insert(n - 1, task);

                    }
                    item.Remove();
                    lsvTask.Items.Insert(n - 1, item);
                    lsvTask.Invalidate();
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lsvTask.SelectedItems.Count > 0)
            {

                int n = lsvTask.SelectedItems[0].Index;
                if (n < lsvTask.Items.Count - 1)
                {
                    ListViewItem item = lsvTask.SelectedItems[0];
                    TimeTaskInfo task = item.Tag as TimeTaskInfo;
                    if (TimeDb.TimeTaskList.Contains(task))
                    {
                        TimeDb.TimeTaskList.Remove(task);
                        TimeDb.TimeTaskList.Insert(n + 1, task);

                    }
                    item.Remove();
                    lsvTask.Items.Insert(n + 1, item);
                    lsvTask.Invalidate();
                }
            }
        }
        private void LoadTimeTask()
        {
            foreach (TimeTaskInfo task in TimeDb.TimeTaskList)
            {
                AddNewTask(task);
            }
        }

        private void lsvTask_MouseClick(object sender, MouseEventArgs e)
        {
            //Point ClickPoint = new Point(e.X, e.Y);
            ListViewItem item = lsvTask.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                TimeTaskInfo task = item.Tag as TimeTaskInfo;
                SelTask = task;
                SelItem = item;
                txtName.Text = task.TaskName;
                chkKeep.Checked = task.KeepAction;
                //swEnable.Value = task.Enable;
                chkActive.Checked = task.Enable;
                dtBegin.Value = Convert.ToDateTime(task.BeginTime);
                dtEnd.Value = Convert.ToDateTime(task.EndTime);
                //swTaskValue.Value = (task.TaskValue == 1) ? true : false;
                textBoxValue.Text = task.TaskValue.ToString();
                chkWeek1.Checked = task.EnableWeek1;
                chkWeek2.Checked = task.EnableWeek2;
                chkWeek3.Checked = task.EnableWeek3;
                chkWeek4.Checked = task.EnableWeek4;
                chkWeek5.Checked = task.EnableWeek5;
                chkWeek6.Checked = task.EnableWeek6;
                chkWeek7.Checked = task.EnableWeek7;
                lstVariable.Items.Clear();
                foreach (string var in task.VarNameList)
                {
                    AddNewVariable(var);
                }
                groupBoxDetail.Enabled = true;


            }
        }

        private void ResetTaskDetail()
        {
            // swEnable.Value = false;
            chkActive.Checked = false;
            dtBegin.Value = Convert.ToDateTime("2000-01-01 00:00:00");
            dtEnd.Value = Convert.ToDateTime("2000-01-01 00:00:00"); ;
            textBoxValue.Text = "0";
            //swTaskValue.Value = false;
            chkWeek1.Checked = false;
            chkWeek2.Checked = false;
            chkWeek3.Checked = false;
            chkWeek4.Checked = false;
            chkWeek5.Checked = false;
            chkWeek6.Checked = false;
            chkWeek7.Checked = false;
            lstVariable.Items.Clear();
            groupBoxDetail.Enabled = false;
            SelItem = null;
            SelTask = null;

        }

        private void FormTimeTask_Shown(object sender, EventArgs e)
        {
            CreateItemHeader();
            foreach (TimeTaskInfo task in TimeDb.TimeTaskList)
            {
                AddNewTask(task);
            }
            ResetTaskDetail();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
            {
                string hours = dtBegin.Value.Hour.ToString();
                string minute = dtBegin.Value.Minute.ToString();
                SelTask.BeginTime = hours + ":" + minute;

                try
                {
                    if (chkKeep.Checked)
                    {
                        DateTime dt1 = Convert.ToDateTime(SelTask.BeginTime);
                        DateTime dt2 = Convert.ToDateTime(SelTask.EndTime);
                        if (dt2.CompareTo(dt1) < 0)
                        {
                            MessageBox.Show("开始时间大于结束时间，请重新设置！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                foreach (TimeTaskInfo info in TimeDb.TimeTaskList)
                {
                    if (info.TaskName == SelTask.TaskName)
                    {
                        info.BeginTime = SelTask.BeginTime;
                        info.DateStamp = DateTime.Now;
                        info.DateStamp2 = DateTime.Now;
                        info.tryCount = 0;
                    }
                }
            }
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
            {
                string hours = dtEnd.Value.Hour.ToString();
                string minute = dtEnd.Value.Minute.ToString();
                SelTask.EndTime = hours + ":" + minute;
                try
                {
                    if (chkKeep.Checked)
                    {
                        DateTime dt1 = Convert.ToDateTime(SelTask.BeginTime);
                        DateTime dt2 = Convert.ToDateTime(SelTask.EndTime);
                        if (dt2.CompareTo(dt1) < 0)
                        {
                            MessageBox.Show("开始时间大于结束时间，请重新设置！");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                foreach (TimeTaskInfo info in TimeDb.TimeTaskList)
                {
                    if (info.TaskName == SelTask.TaskName)
                    {
                        info.EndTime = SelTask.EndTime;
                        info.DateStamp = DateTime.Now;
                        info.DateStamp2 = DateTime.Now;
                        info.tryCount = 0;
                    }
                }
            }
        }

        private void swEnable_ValueChanged(object sender, EventArgs e)
        {
            //if (SelTask != null)
            //    SelTask.Enable = swEnable.Value;
        }

        private void swTaskValue_ValueChanged(object sender, EventArgs e)
        {
            //if (SelTask != null)
            //    SelTask.TaskValue = swTaskValue.Value ? 1 : 0;
        }

        private void chkWeek1_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek1 = chkWeek1.Checked;
        }

        private void chkWeek2_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek2 = chkWeek2.Checked;

        }

        private void chkWeek3_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek3 = chkWeek3.Checked;
        }

        private void chkWeek4_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek4 = chkWeek4.Checked;
        }

        private void chkWeek5_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek5 = chkWeek5.Checked;
        }

        private void chkWeek6_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek6 = chkWeek6.Checked;
        }

        private void chkWeek7_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.EnableWeek7 = chkWeek7.Checked;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("任务名称不能为空！");
                return;
            }
            if (SelTask != null)
            {
                SelTask.TaskName = txtName.Text;

            }
            if (SelItem != null)
            {
                SelItem.Text = txtName.Text;
            }

        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.Enable = chkActive.Checked;
        }

        private void textBoxValue_TextChanged(object sender, EventArgs e)
        {
            if (SelTask != null)
                SelTask.TaskValue = pubFun.IsInt(textBoxValue.Text, 0);
        }

        private void chkKeep_CheckedChanged(object sender, EventArgs e)
        {
            this.lblEnd.Visible = chkKeep.Checked;
            this.dtEnd.Visible = chkKeep.Checked;
            if (SelTask != null)
                SelTask.KeepAction = chkKeep.Checked;
        }
    }
}
