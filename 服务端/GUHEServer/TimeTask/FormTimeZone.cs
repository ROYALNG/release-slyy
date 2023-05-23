using GHIBMS.Common;
using System;
using System.Diagnostics;
using System.Windows.Forms;


namespace GHIBMS.Server
{
    public partial class FormTimeZone : Form
    {
        private GHIBMS.Common.TimeZoneClt SelZone = null;
        private ListViewItem SelItem = null;
        private ComboBoxItemCustom week0 = new ComboBoxItemCustom("日", "");
        private ComboBoxItemCustom week1 = new ComboBoxItemCustom("一", "");
        private ComboBoxItemCustom week2 = new ComboBoxItemCustom("二", "");
        private ComboBoxItemCustom week3 = new ComboBoxItemCustom("三", "");
        private ComboBoxItemCustom week4 = new ComboBoxItemCustom("四", "");
        private ComboBoxItemCustom week5 = new ComboBoxItemCustom("五", "");
        private ComboBoxItemCustom week6 = new ComboBoxItemCustom("六", "");

        private ComboBoxItemCustom CurrentItem;
        public FormTimeZone()
        {
            InitializeComponent();
            combWeek.KeyPress += pubFun.No_KeyPress;
            combCopy.KeyPress += pubFun.No_KeyPress;
            this.lsvZone.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvZone_MouseClick);

            combWeek.Items.Add(week0);
            combWeek.Items.Add(week1);
            combWeek.Items.Add(week2);
            combWeek.Items.Add(week3);
            combWeek.Items.Add(week4);
            combWeek.Items.Add(week5);
            combWeek.Items.Add(week6);

            combWeek.SelectedIndex = 0;

            combCopy.Items.Add("全部");
            combCopy.Items.Add("星期日");
            combCopy.Items.Add("星期一");
            combCopy.Items.Add("星期二");
            combCopy.Items.Add("星期三");
            combCopy.Items.Add("星期四");
            combCopy.Items.Add("星期五");
            combCopy.Items.Add("星期六");

            combCopy.SelectedIndex = 0;

            CurrentItem = week0;

            this.dt1Begin.CustomFormat = "HH:mm";
            this.dt1Begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt1End.CustomFormat = "HH:mm";
            this.dt1End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dt2Begin.CustomFormat = "HH:mm";
            this.dt2Begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt2End.CustomFormat = "HH:mm";
            this.dt2End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dt3Begin.CustomFormat = "HH:mm";
            this.dt3Begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt3End.CustomFormat = "HH:mm";
            this.dt3End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            this.dt4Begin.CustomFormat = "HH:mm";
            this.dt4Begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dt4End.CustomFormat = "HH:mm";
            this.dt4End.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

        }
        private void ShowWeekItem(ComboBoxItemCustom item)
        {
            if (item.Value.ToString() == "")
            {
                ResetZoneDetail();
            }
            else
            {
                string s = item.Value.ToString();
                string[] zoneArr = s.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (zoneArr.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        string[] timeArr = zoneArr[i].Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        if (timeArr.Length == 2)
                        {
                            if (i == 0)
                            {
                                dt1Begin.Value = Convert.ToDateTime(timeArr[0]);
                                dt1End.Value = Convert.ToDateTime(timeArr[1]);
                            }
                            else if (i == 1)
                            {
                                dt2Begin.Value = Convert.ToDateTime(timeArr[0]);
                                dt2End.Value = Convert.ToDateTime(timeArr[1]);
                            }
                            else if (i == 2)
                            {
                                dt3Begin.Value = Convert.ToDateTime(timeArr[0]);
                                dt3End.Value = Convert.ToDateTime(timeArr[1]);
                            }
                            else if (i == 3)
                            {
                                dt4Begin.Value = Convert.ToDateTime(timeArr[0]);
                                dt4End.Value = Convert.ToDateTime(timeArr[1]);
                            }
                        }
                    }
                }
            }
        }
        private void SaveWeekItem()
        {
            if (CurrentItem != null)
            {
                CurrentItem.Value =
                    dt1Begin.Value.ToString("HH:mm:ss") + "-"
                    + dt1End.Value.ToString("HH:mm:ss")
                    + "|" +
                    dt2Begin.Value.ToString("HH:mm:ss") + "-"
                    + dt2End.Value.ToString("HH:mm:ss")
                     + "|" +
                    dt3Begin.Value.ToString("HH:mm:ss") + "-"
                    + dt3End.Value.ToString("HH:mm:ss")
                     + "|" +
                    dt4Begin.Value.ToString("HH:mm:ss") + "-"
                    + dt4End.Value.ToString("HH:mm:ss");
            }

        }
        private void CreateItemHeader()
        {
            lsvZone.Clear();
            System.Windows.Forms.ColumnHeader columnHeader1;

            columnHeader1 = new System.Windows.Forms.ColumnHeader();
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "名称";
            columnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            columnHeader1.Width = 300;
            // 
            // columnHeader2
            // 
            //columnHeader2.Text = "开始时间";
            //columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //columnHeader2.Width = 70;
            //// 
            //// columnHeader3
            //// 
            //columnHeader3.Text = "结束时间";
            //columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //columnHeader3.Width = 70;
            //// 
            //// columnHeader4
            //// 
            //columnHeader4.Text = "动作数值";
            //columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            //columnHeader4.Width = 70;

            lsvZone.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader1,
            //columnHeader2,
            //columnHeader3,
            //columnHeader4
            });
            lsvZone.HideSelection = false;
        }
        private void AddNewZone(GHIBMS.Common.TimeZoneClt zone)
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            zone.ZoneName,
     }, 0);
            listViewItem1.Tag = zone;
            lsvZone.Items.Add(listViewItem1);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            string uniqueName = TimeZoneDb.GetUniqueCloneName("新建时限0");
            TimeZoneClt zone = new TimeZoneClt();
            TimeZoneDb.TimeZoneList.Add(zone);
            zone.ZoneName = uniqueName;
            AddNewZone(zone);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (lsvZone.SelectedItems.Count > 0)
            {
                int c = this.lsvZone.SelectedItems.Count;
                for (int i = 0; i < c; i++)
                {
                    TimeZoneClt zone = this.lsvZone.SelectedItems[0].Tag as TimeZoneClt;
                    if (TimeZoneDb.TimeZoneList.Contains(zone))
                        TimeZoneDb.TimeZoneList.Remove(zone);

                    this.lsvZone.Items.Remove(this.lsvZone.SelectedItems[0]);

                }
                lsvZone.Invalidate();
                ResetZoneDetail();
            }
        }



        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lsvZone.SelectedItems.Count > 0)
            {

                int n = lsvZone.SelectedItems[0].Index;
                if (n > 0)
                {
                    ListViewItem item = lsvZone.SelectedItems[0];
                    TimeZoneClt zone = item.Tag as TimeZoneClt;
                    if (TimeZoneDb.TimeZoneList.Contains(zone))
                    {
                        TimeZoneDb.TimeZoneList.Remove(zone);
                        TimeZoneDb.TimeZoneList.Insert(n - 1, zone);

                    }
                    item.Remove();
                    lsvZone.Items.Insert(n - 1, item);
                    lsvZone.Invalidate();
                }
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lsvZone.SelectedItems.Count > 0)
            {

                int n = lsvZone.SelectedItems[0].Index;
                if (n < lsvZone.Items.Count - 1)
                {
                    ListViewItem item = lsvZone.SelectedItems[0];
                    TimeZoneClt zone = item.Tag as TimeZoneClt;
                    if (TimeZoneDb.TimeZoneList.Contains(zone))
                    {
                        TimeZoneDb.TimeZoneList.Remove(zone);
                        TimeZoneDb.TimeZoneList.Insert(n + 1, zone);

                    }
                    item.Remove();
                    lsvZone.Items.Insert(n + 1, item);
                    lsvZone.Invalidate();
                }
            }
        }
        private void LoadTimeZone()
        {
            foreach (TimeZoneClt zone in TimeZoneDb.TimeZoneList)
            {
                AddNewZone(zone);
            }
        }

        private void lsvZone_MouseClick(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("lsvZone_MouseClick");
            //Point ClickPoint = new Point(e.X, e.Y);
            ListViewItem item = lsvZone.GetItemAt(e.X, e.Y);
            if (item != null)
            {
                TimeZoneClt zone = item.Tag as TimeZoneClt;
                SelZone = zone;
                SelItem = item;
                txtName.Text = zone.ZoneName;
                combWeek.SelectedIndex = zone.selWeek;
                //dtBegin.Value =Convert.ToDateTime(zone.BeginTime);
                //dtEnd.Value = Convert.ToDateTime(zone.EndTime);


                groupBoxDetail.Enabled = true;
                if (zone.TimeInfo != null)
                {
                    string[] strArr = zone.TimeInfo.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArr.Length == 7)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            ((ComboBoxItemCustom)combWeek.Items[i]).Value = strArr[i];
                        }
                    }

                    ShowWeekItem((ComboBoxItemCustom)combWeek.Items[zone.selWeek]);
                }

            }
        }

        private void ResetZoneDetail()
        {
            dt1Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dt1End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

            dt2Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dt2End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

            dt3Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dt3End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

            dt4Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dt4End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");


            //groupBoxDetail.Enabled = false;
            //SelItem = null;
            //SelZone = null;

        }

        private void FormTimeZone_Shown(object sender, EventArgs e)
        {
            CreateItemHeader();
            foreach (TimeZoneClt zone in TimeZoneDb.TimeZoneList)
            {
                AddNewZone(zone);
            }
            ResetZoneDetail();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCurrentTimeZone();
            //this.DialogResult = DialogResult.OK;
        }
        private void SaveCurrentTimeZone()
        {
            //保存当天
            SaveWeekItem();
            //合并一周
            string s = "";
            for (int i = 0; i < 7; i++)
            {
                if (((ComboBoxItemCustom)combWeek.Items[i]).Value.ToString() != "")
                {
                    s = s + ((ComboBoxItemCustom)combWeek.Items[i]).Value.ToString() + "#";
                }
                else
                    s = s + "00:00:00-00:00:00#";
            }

            if (SelZone != null)
            {
                SelZone.TimeInfo = s;
                SelZone.selWeek = combWeek.SelectedIndex;
            }


        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void dtBegin_ValueChanged(object sender, EventArgs e)
        {

            SaveCurrentTimeZone();
        }


        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("任务名称不能为空！");
                return;
            }
            if (SelZone != null)
            {
                SelZone.ZoneName = txtName.Text;

            }
            if (SelItem != null)
            {
                SelItem.Text = txtName.Text;
            }

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void combWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            CurrentItem = (ComboBoxItemCustom)combWeek.SelectedItem;
            ShowWeekItem(CurrentItem);
        }

        private void dt1Begin_Leave(object sender, EventArgs e)
        {

            try
            {
                DateTime dt1 = Convert.ToDateTime(dt1Begin.Value.ToString("HH:mm"));
                DateTime dt2 = Convert.ToDateTime(dt1End.Value.ToString("HH:mm"));
                if (dt2.CompareTo(dt1) < 0)
                {
                    MessageBox.Show("开始时间大于结束时间，请重新设置！");
                    dt1Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                    dt1End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

                }

                dt1 = Convert.ToDateTime(dt2Begin.Value.ToString("HH:mm"));
                dt2 = Convert.ToDateTime(dt2End.Value.ToString("HH:mm"));
                if (dt2.CompareTo(dt1) < 0)
                {
                    MessageBox.Show("开始时间大于结束时间，请重新设置！");
                    dt2Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                    dt2End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

                }

                dt1 = Convert.ToDateTime(dt3Begin.Value.ToString("HH:mm"));
                dt2 = Convert.ToDateTime(dt3End.Value.ToString("HH:mm"));
                if (dt2.CompareTo(dt1) < 0)
                {
                    MessageBox.Show("开始时间大于结束时间，请重新设置！");
                    dt3Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                    dt3End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

                }

                dt1 = Convert.ToDateTime(dt4Begin.Value.ToString("HH:mm"));
                dt2 = Convert.ToDateTime(dt4End.Value.ToString("HH:mm"));
                if (dt2.CompareTo(dt1) < 0)
                {
                    MessageBox.Show("开始时间大于结束时间，请重新设置！");
                    dt4Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
                    dt4End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");

                }
                SaveWeekItem();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void lsvZone_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SaveWeekItem(ref LastItem);
            //string s = "";
            //for (int i = 0; i < 7; i++)
            //{
            //    s = s + ((ComboBoxItemCustom)combWeek.Items[i]).Value.ToString() + "#";
            //}

            //if (SelZone != null)
            //    SelZone.TimeInfo = s;
            Debug.WriteLine("lsvZone_SelectedIndexChanged");
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            SaveWeekItem();
            string txt = combCopy.Text;
            string str2 = (combWeek.SelectedItem as ComboBoxItemCustom).Value.ToString();
            switch (txt)
            {
                case "全部":
                    foreach (ComboBoxItemCustom item in combWeek.Items)
                    {
                        item.Value = str2;
                    }
                    break;
                case "星期一":
                    week1.Value = str2;
                    break;
                case "星期二":
                    week2.Value = str2;
                    break;
                case "星期三":
                    week3.Value = str2;
                    break;
                case "星期四":
                    week4.Value = str2;
                    break;
                case "星期五":
                    week5.Value = str2;
                    break;
                case "星期六":
                    week6.Value = str2;
                    break;
                case "星期日":
                    week0.Value = str2;
                    break;


            }
        }

        private void groupBoxDetail_Enter(object sender, EventArgs e)
        {

        }

        private void btnWorkTime_Click(object sender, EventArgs e)
        {
            ResetZoneDetail();
            dt1Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00");
            dt1End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 17:30:00");
            SaveWeekItem();
            string str2 = (combWeek.SelectedItem as ComboBoxItemCustom).Value.ToString();

            foreach (ComboBoxItemCustom item in combWeek.Items)
            {
                item.Value = str2;
            }


            SaveCurrentTimeZone();
        }

        private void btnRestTime_Click(object sender, EventArgs e)
        {
            ResetZoneDetail();
            dt1Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00");
            dt1End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 08:30:00");

            dt2Begin.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 17:30:00");
            dt2End.Value = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59");
            SaveWeekItem();
            string str2 = (combWeek.SelectedItem as ComboBoxItemCustom).Value.ToString();

            foreach (ComboBoxItemCustom item in combWeek.Items)
            {
                item.Value = str2;
            }

            SaveCurrentTimeZone();
        }



    }
}
