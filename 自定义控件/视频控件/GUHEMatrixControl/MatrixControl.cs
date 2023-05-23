using DevComponents.DotNetBar;
using GHIBMS.Common;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GHIBMS.Matrix
{
    public partial class MatrixControl : UserControl
    {
        public delegate void VideoControlDelegate(object sender, VideoCommandArgs e);
        public event VideoControlDelegate OnVideoControl;
        //public delegate void MatrixVideoCtrlDelegate(object sender ,VideoCommandArgs e);
        //public event MatrixVideoCtrlDelegate OnVideoControl;
        private Monitor currentMon;
        public int SelectMonID = 1;
        private int SelectCamID = 1;

        private int currentChildMonID = 1;

        private string currentMonName = "";
        private string currentCamName = "";
        private bool bEdit = false;
        private CRectControl CRectCtl;
        List<BaseMonitorVariable> monitorList = new List<BaseMonitorVariable>();
        public static void NubOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            //阻止从键盘输入键
            e.Handled = true;

            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == (char)8))
            {
                e.Handled = false;
                return;
            }
            else
            {
                MessageBoxEx.Show("编号只能输入数字！");
            }
        }
        public MatrixControl(int MaxPannel, List<BaseMonitorVariable> monList)
        {

            InitializeComponent();
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            initCamPresetList();
            initCamCruiseList();
            initCamTrackList();
            // CreateVideoControl(MaxPannel); //默认16通道
            ptzControl1.OnVideoControl += new GHIBMS.PTZControl.PTZControl.VideoControlDelegate(ptzControl1_OnVideoCommand);
            txtInput.KeyPress += MatrixControl.NubOnly_KeyPress;
            monitorList = monList;
            //iniGridPannelConfig();
            //LoadPannelDefaultConfig(MaxPannel);
            //if (ClientConfig.MatrixList.Count!= 0)
            //    LoadPannelConfig();
            // SetMonitor();
            cmbMonitor.KeyPress += MatrixControl.NubOnly_KeyPress;
            foreach (BaseMonitorVariable mon in monList)
                cmbMonitor.Items.Add(mon.Name);
            LoadMon();
        }


        //视频切换
        public void SelectCamera(string camname)
        {
            if (currentMon != null)
            {
                currentCamName = camname;
                lblCam.Text = "摄像机:" + camname;
                if (OnVideoControl != null)
                {
                    VideoCommandArgs arg = new VideoCommandArgs();
                    arg.CamName = camname;
                    arg.MonName = currentMon.MonName;
                    arg.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
                    arg.SubVideoOut = currentChildMonID;

                    OnVideoControl(this, arg);
                    ptzControl1.SelectCammera(arg);
                }

            }
        }

        private void ptzControl1_OnVideoCommand(object sender, VideoCommandArgs e)
        {
            if (OnVideoControl != null)
                OnVideoControl(this, e);
        }
        //预置位
        private void initCamPresetList()
        {

            System.Windows.Forms.ColumnHeader columnHeader1 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader columnHeader2 = new System.Windows.Forms.ColumnHeader();

            columnHeader1.Text = "预置位编号";
            columnHeader2.Text = "预置位描述";
            columnHeader1.Width = 100;
            columnHeader2.Width = 150;
            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader2.TextAlign = HorizontalAlignment.Center;

            lsvCamPreset.Border.Class = "ListViewBorder";
            lsvCamPreset.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                    columnHeader1,
                    columnHeader2,

            });
            lsvCamPreset.FullRowSelect = true;

            for (int i = 1; i <= 128; i++)
            {
                ListViewItem listViewItem = new ListViewItem(new string[]
               {
                   i.ToString("00"),"预置位"+i.ToString("00")

               }, 0);
                lsvCamPreset.Items.Insert(i - 1, listViewItem);
            }

        }
        //巡航
        private void initCamCruiseList()
        {

            System.Windows.Forms.ColumnHeader columnHeader1 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader columnHeader2 = new System.Windows.Forms.ColumnHeader();

            columnHeader1.Text = "巡航路径编号";
            columnHeader2.Text = "巡航路径描述";
            columnHeader1.Width = 100;
            columnHeader2.Width = 150;
            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader2.TextAlign = HorizontalAlignment.Center;

            lsvCamCruise.Border.Class = "ListViewBorder";
            lsvCamCruise.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                    columnHeader1,
                    columnHeader2,

            });
            lsvCamCruise.FullRowSelect = true;

            for (int i = 1; i <= 32; i++)
            {
                ListViewItem listViewItem = new ListViewItem(new string[]
               {
                   i.ToString("00"),"巡航路径"+i.ToString("00")

               }, 1);
                lsvCamCruise.Items.Insert(i - 1, listViewItem);
            }

        }
        //轨迹
        private void initCamTrackList()
        {

            System.Windows.Forms.ColumnHeader columnHeader1 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader columnHeader2 = new System.Windows.Forms.ColumnHeader();

            columnHeader1.Text = "轨迹编号";
            columnHeader2.Text = "轨迹描述";
            columnHeader1.Width = 100;
            columnHeader2.Width = 150;
            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader2.TextAlign = HorizontalAlignment.Center;

            lsvCamTrack.Border.Class = "ListViewBorder";
            lsvCamTrack.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                    columnHeader1,
                    columnHeader2,

            });
            lsvCamTrack.FullRowSelect = true;

            for (int i = 1; i <= 32; i++)
            {
                ListViewItem listViewItem = new ListViewItem(new string[]
               {
                   i.ToString("00"),"轨迹"+i.ToString("00")

               }, 2);
                lsvCamTrack.Items.Insert(i - 1, listViewItem);
            }

        }
        private Monitor CreateNewVideoControl(int split)
        {
            Monitor mon = new Monitor(split);
            mon.OnSelectedWnd += new Monitor.OnSelectedWnddelegate(mon_OnSelectedWnd);
            mon.Location = new System.Drawing.Point(100, 100);
            mon.Size = new System.Drawing.Size(200, 200);
            mon.MouseDown += new MouseEventHandler(mon_MouseDown);
            //mon.PannelID = i;
            //mon.Tag = i;
            panelExMonList.Controls.Add(mon);
            return mon;

        }
        private void mon_MouseDown(object sender, MouseEventArgs e)
        {
            if (bEdit)
            {
                Control c = sender as Control;
                c.BringToFront();
                c.Capture = false;
                if (this.Controls.Contains(CRectCtl))
                    this.Controls.Remove(CRectCtl);
                CRectCtl = new CRectControl(c);
                this.Controls.Add(CRectCtl);
                CRectCtl.BringToFront();
                CRectCtl.Draw();
            }
        }
        /*private void CreateVideoControl(int MaxVideoNubs)
        {
            foreach (Control con in panelExMonList.Controls)
            {
                con.Dispose();
            }
            Monitor.MonID = 0;
            panelExMonList.Controls.Clear();

            this.panelExMonList.SuspendLayout();
            for (int i = 1; i <= MaxVideoNubs; i++)
            {
                Monitor mon = new Monitor();
                mon.OnSelectedWnd+=new Monitor.OnSelectedWnddelegate(mon_OnSelectedWnd);
                mon.Location = new System.Drawing.Point(0, 0);
                mon.Size = new System.Drawing.Size(10, 10);
                mon.PannelID = i;
                mon.Tag = i;
                panelExMonList.Controls.Add(mon);

            }
            currentMon = panelExMonList.Controls[0] as Monitor;
            this.panelExMonList.ResumeLayout(false);
            MakeVidoPanelShow(MaxVideoNubs);
        }*/
        private void pnl_MouseClick(object sender, MouseEventArgs e)
        {
            //PanelEx pnl = sender as PanelEx;
            //pnl.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            //pnl.Style.BackColor2.Color = System.Drawing.Color.Teal;
            //if (OnSelectedWnd != null)
            //    OnSelectedWnd(sender, e);

        }
        private void mon_OnSelectedWnd(object sender, EventArgs e)
        {
            foreach (Control con in panelExMonList.Controls)
            {
                ((Monitor)con).SelectChildCH(0);
            }

            int childCH = ((Monitor)sender).MonChildCH;
            currentMonName = ((Monitor)sender).MonName;
            currentChildMonID = childCH;
            lblMon.Text = "监视器:" + currentMonName;
            //2014-4-20取消 选中监视器不再切换视频
            //if (OnVideoControl != null)
            //{
            //    VideoCommandArgs arg = new VideoCommandArgs();
            //    arg.MonName = currentMonName;
            //    arg.VideoCommand = PTZCmdCodeEnum.MAT_MON_SW;
            //    arg.SubVideoOut = currentChildMonID;
            //    OnVideoControl(this, arg);
            //}
            currentMon = (Monitor)sender;
            lblMonControlName.Text = currentMon.MonID;
            cmbMonitor.Text = currentMon.MonName;

            currentMon.SelectChildCH(childCH);
        }



        private void txtInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        private void btnN01_Click(object sender, EventArgs e)
        {
            ButtonX but = sender as ButtonX;
            txtInput.Text = txtInput.Text + but.Text;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
        }

        private void btnMon_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() == "" || txtInput.Text.Trim() == "0")
            {
                MessageBoxEx.Show("请输入监视器序号！");
                return;
            }
            SelectMonID = pubFun.IsInt(txtInput.Text, 1);
            if (SelectMonID > 128)
            {
                MessageBoxEx.Show("监视器序号输入错误，最大值：128！");
                txtInput.Text = "";
                return;
            }
            txtInput.Text = "";
            lblMon.Text = "监视器:" + SelectMonID.ToString("D2");
            if (OnVideoControl != null)
            {
                VideoCommandArgs arg = new VideoCommandArgs();
                arg.MonName = currentMonName;
                arg.VideoCommand = PTZCmdCodeEnum.MAT_MON_SW;
                arg.VideoOut = SelectMonID;
                arg.VideoIn = SelectCamID;
                arg.SubVideoOut = currentChildMonID;
                OnVideoControl(this, arg);
            }
        }


        private void btnCam_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() == "" || txtInput.Text.Trim() == "0")
            {
                MessageBoxEx.Show("请输入摄像机序号！");
                return;
            }
            SelectCamID = pubFun.IsInt(txtInput.Text, 1);
            if (SelectCamID > 9999)
            {
                MessageBoxEx.Show("摄像机序号输入错误，最大值：9999！");
                txtInput.Text = "";
                return;
            }
            txtInput.Text = "";
            lblCam.Text = "摄像机:" + SelectCamID.ToString("D3");
            if (OnVideoControl != null)
            {
                VideoCommandArgs arg = new VideoCommandArgs();

                arg.MonName = currentMonName;
                arg.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
                arg.VideoOut = SelectMonID;
                arg.VideoIn = SelectCamID;
                arg.SubVideoOut = currentChildMonID;
                OnVideoControl(this, arg);
                ptzControl1.SelectCammera(arg);
            }

        }

        private void panelExMonList_SizeChanged(object sender, EventArgs e)
        {
            //MakeVidoPanelShow(currentPanelNubs);
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() == "")
            {
                MessageBoxEx.Show("请输入调用的巡回序号！");
                return;
            }
            uint index = UInt32.Parse(txtInput.Text);
            txtInput.Text = "";

            if (OnVideoControl != null)
            {
                VideoCommandArgs arg = new VideoCommandArgs();
                arg.MonName = currentMonName;
                arg.VideoCommand = PTZCmdCodeEnum.MAT_RUN;
                arg.VideoOut = SelectMonID;
                arg.VideoIn = SelectCamID;
                arg.AutoRunIndex = index;
                arg.SubVideoOut = currentChildMonID;
                OnVideoControl(this, arg);
            }

        }

        private void btnGroup_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() != "")
            {
                uint index = UInt32.Parse(txtInput.Text);
                txtInput.Text = "";

                if (OnVideoControl != null)
                {
                    VideoCommandArgs arg = new VideoCommandArgs();
                    arg.MonName = currentMonName;
                    arg.VideoCommand = PTZCmdCodeEnum.MAT_GROUP;
                    arg.VideoOut = SelectMonID;
                    arg.VideoIn = SelectCamID;
                    arg.GroupIndex = index;
                    arg.SubVideoOut = currentChildMonID;
                    OnVideoControl(this, arg);
                }
            }
            else
            {
                if (txtInput.Text.Trim() == "")
                {
                    MessageBoxEx.Show("请输入调用的群组序号！");
                    return;
                }
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (SelectCamID > 1)
                SelectCamID--;
            lblCam.Text = "摄像机:" + SelectCamID.ToString("D3");
            if (OnVideoControl != null)
            {
                VideoCommandArgs arg = new VideoCommandArgs();
                arg.MonName = currentMonName;
                arg.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
                arg.VideoOut = SelectMonID;
                arg.VideoIn = SelectCamID;
                arg.SubVideoOut = currentChildMonID;
                OnVideoControl(this, arg);
                ptzControl1.SelectCammera(arg);
            }

        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            if (SelectCamID < 1024)
                SelectCamID++;
            lblCam.Text = "摄像机:" + SelectCamID.ToString("D3");
            if (OnVideoControl != null)
            {
                VideoCommandArgs arg = new VideoCommandArgs();
                arg.MonName = currentMonName;
                arg.VideoCommand = PTZCmdCodeEnum.MAT_VIDEO_SW;
                arg.VideoOut = SelectMonID;
                arg.VideoIn = SelectCamID;
                arg.SubVideoOut = currentChildMonID;
                OnVideoControl(this, arg);
                ptzControl1.SelectCammera(arg);
            }

        }

        private void btnItemPreset_Click(object sender, EventArgs e)
        {

        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnHold_Click(object sender, EventArgs e)
        {
            if (txtInput.Text.Trim() != "")
            {
                int index = Int32.Parse(txtInput.Text);
                SelectMonID = index;
            }
            txtInput.Text = "";

            if (OnVideoControl != null)
            {
                VideoCommandArgs arg = new VideoCommandArgs();
                arg.MonName = currentMonName;
                arg.VideoCommand = PTZCmdCodeEnum.MAT_HOLD;
                arg.VideoOut = SelectMonID;
                arg.VideoIn = SelectCamID;
                arg.SubVideoOut = currentChildMonID;
                OnVideoControl(this, arg);
            }

        }
        private void SaveMon()
        {
            ClientConfig.MonitorList.Clear();
            foreach (Monitor mon in panelExMonList.Controls)
            {
                ClientConfig.MonitorList.Add(new MonitorPannel(mon.MonName, mon.Top, mon.Left, mon.Height, mon.Width, mon.SplitNubs));

            }
            ClientConfig.saveToFile();

        }
        private void LoadMon()
        {
            panelExMonList.Controls.Clear();
            foreach (MonitorPannel mon in ClientConfig.MonitorList)
            {
                Monitor m = CreateNewVideoControl(mon.Split);
                m.MonName = mon.MonName;
                m.Top = mon.Top;
                m.Left = mon.Left;
                m.Width = mon.Width;
                m.Height = mon.Height;
            }
        }
        /*
        private void iniGridPannelConfig()
        {
            grid1.BorderStyle = BorderStyle.FixedSingle;
            grid1.SelectionMode = GridSelectionMode.Row;
            grid1.ColumnsCount = 5;
            grid1.FixedRows = 1;
            grid1.Rows.Insert(0);

            //ColumnHeader view

            SourceGrid.Cells.Views.ColumnHeader boldHeader = new SourceGrid.Cells.Views.ColumnHeader();
            boldHeader.Font = new Font("宋体", 9, FontStyle.Regular);
            boldHeader.TextAlignment = DevAge.Drawing.ContentAlignment.MiddleCenter;


            grid1[0, 0] = new SourceGrid.Cells.ColumnHeader("编号");
            grid1[0, 1] = new SourceGrid.Cells.ColumnHeader("监视器");
            grid1[0, 2] = new SourceGrid.Cells.ColumnHeader("通道");
            grid1[0, 3] = new SourceGrid.Cells.ColumnHeader("分割");

            grid1[0, 0].View = boldHeader;
            grid1[0, 1].View = boldHeader;
            grid1[0, 2].View = boldHeader;
            grid1[0, 3].View = boldHeader;

            grid1.AutoStretchColumnsToFitWidth = true;

        }
        private void LoadPannelDefaultConfig(int MaxPannel)
        {
            //矩阵列表
            string[] arrs = new string[monitorList.Count];
            for (int i = 0; i < monitorList.Count; i++)
                arrs[i] = monitorList[i].Name;


            SourceGrid.Cells.Editors.ComboBox combMatrix;
            combMatrix = new SourceGrid.Cells.Editors.ComboBox(typeof(string), arrs, false);
            combMatrix.KeyPress += pubFun.NoKey_KeyPress;
            string defName = "";
            if (monitorList.Count > 0)
                defName = monitorList[0].Name;
            for (int i = 1; i <= MaxPannel; i++)
            {
                int n = grid1.RowsCount;
                grid1.Rows.Insert(n);

                grid1[n, 0] = new SourceGrid.Cells.Cell(n.ToString());
                grid1[n, 1] = new SourceGrid.Cells.Cell(defName, combMatrix);
                grid1[n, 2] = new SourceGrid.Cells.Cell(n.ToString(), typeof(int));
                grid1[n, 3] = new SourceGrid.Cells.Cell(1, typeof(int));

            }
           
        }
        private void LoadPannelConfig()
        {
            foreach (MatrixPannelConfig cfg in ClientConfig.MatrixList)
            {
                foreach (SourceGrid.Grid.GridRow row in grid1.Rows)
                {
                    if (row[grid1.Columns[0]].Value.ToString() == cfg.PannelID.ToString())
                    {
                        row[grid1.Columns[1]].Value = cfg.MatrixName;
                        row[grid1.Columns[2]].Value = cfg.VideoOutChannel.ToString();
                        row[grid1.Columns[3]].Value = cfg.VideoSplit.ToString();
                    }
                }
            }
        }
        public void SetMonitor()
        {
            for (int r = 1; r < grid1.Rows.Count; r++)
            {
                CellContext[] context = new CellContext[grid1.Columns.Count];
                for (int c = 0; c < grid1.Columns.Count; c++)
                {
                    SourceGrid.Cells.ICellVirtual cell = grid1.GetCell(r, c);
                    Position pos = new Position(r, c);
                    context[c] = new CellContext(grid1, pos, cell);

                }

                int id=   pubFun.IsNumeric(context[0].DisplayText);
                string name=      context[1].DisplayText;
                int ch=       pubFun.IsNumeric(context[2].DisplayText);
                int split=       pubFun.IsNumeric(context[3].DisplayText);
                foreach (Control con in panelExMonList.Controls)
                {
                    Monitor mon=con as Monitor;
                    if (mon !=null)
                    {
                        if (mon.PannelID == id)
                        {
                            mon.MonName = name;
                            mon.CreateSubVideoControl(split);
                        }
                    }
                }

            }
            this.Invalidate();
       

        }
        private void SavePannelConfig()
        {
            ClientConfig.MatrixList.Clear();
            for (int r = 1; r < grid1.Rows.Count; r++)
            {
                CellContext[] context = new CellContext[grid1.Columns.Count];
                for (int c = 0; c < grid1.Columns.Count; c++)
                {
                    SourceGrid.Cells.ICellVirtual cell = grid1.GetCell(r, c);
                    Position pos = new Position(r, c);
                    context[c] = new CellContext(grid1, pos, cell);

                }

                MatrixPannelConfig cfg = new MatrixPannelConfig(
                        pubFun.IsNumeric(context[0].DisplayText),
                        context[1].DisplayText,
                        pubFun.IsNumeric(context[2].DisplayText),
                        pubFun.IsNumeric(context[3].DisplayText));


                ClientConfig.MatrixList.Add(cfg);

             
            }
        }

        private void btnSavePannelCfg_Click(object sender, EventArgs e)
        {
            SavePannelConfig();
            SetMonitor();
        }

        private void LoadPannelCfg_Click(object sender, EventArgs e)
        {
            LoadPannelConfig();
        }*/

        private void buttonItemADD_Click(object sender, EventArgs e)
        {
            CreateNewVideoControl(1);
        }

        private void buttonItemEdit_Click(object sender, EventArgs e)
        {
            if (bEdit)
            {
                bEdit = false;
                if (this.Controls.Contains(CRectCtl))
                    this.Controls.Remove(CRectCtl);
            }
            else
                bEdit = true;

        }

        private void cmbMonitor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (currentMon != null)
                currentMon.MonName = cmbMonitor.Text;
        }

        private void buttonItemDEL_Click(object sender, EventArgs e)
        {
            if (this.Controls.Contains(CRectCtl))
                this.Controls.Remove(CRectCtl);
            if (currentMon != null)
            {
                panelExMonList.Controls.Remove(currentMon);
            }
            SaveMon();
        }

        private void btnSavePannelCfg_Click(object sender, EventArgs e)
        {
            if (this.Controls.Contains(CRectCtl))
                this.Controls.Remove(CRectCtl);
            SaveMon();
        }

        private void LoadPannelCfg_Click(object sender, EventArgs e)
        {
            if (this.Controls.Contains(CRectCtl))
                this.Controls.Remove(CRectCtl);
            LoadMon();
        }


    }

}
