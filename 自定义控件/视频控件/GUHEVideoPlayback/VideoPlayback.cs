using DevComponents.DotNetBar;
using GHIBMS.Common;
using justin.time.axis;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GHIBMS.VideoPlayback
{
    public partial class VideoPlayback : UserControl
    {
        #region 构造
        public delegate void OnMessagedelegate(object sender, string msg);
        public event OnMessagedelegate OnMessage;
        private IntPtr mainHandle;
        public VideoPlayback(IntPtr formMainHandle)
        {

            InitializeComponent();
            VideoPlaybackControl.PicPath = ClientConfig.PicPath;
            VideoPlaybackControl.RecPath = ClientConfig.RecPath;
            CreateaALLVideoControl(ClientConfig.MaxPlaybackPannel);
            mainHandle = formMainHandle;
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();
            initFileListCaption();


            string[] item = Enum.GetNames(typeof(DvrFileTypeEnum));
            foreach (string s in item)
            {
                cmbFileType.Items.Add(s);
                cmbFileTypeByTime.Items.Add(s);
            }
            cmbFileType.Text = DvrFileTypeEnum.全部.ToString();
            cmbFileTypeByTime.Text = DvrFileTypeEnum.全部.ToString();
            monthCalByFile.DisplayMonth = DateTime.Today;
            monthCalByTime.DisplayMonth = DateTime.Today;

        }
        private void initFileListCaption()
        {
            lsvFileList.Clear();

            System.Windows.Forms.ColumnHeader columnHeader1 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader columnHeader2 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader columnHeader3 = new System.Windows.Forms.ColumnHeader();
            System.Windows.Forms.ColumnHeader columnHeader4 = new System.Windows.Forms.ColumnHeader();

            columnHeader1.Text = "序号";
            columnHeader2.Text = "开始时间";
            columnHeader3.Text = "结束时间";
            columnHeader4.Text = "大小";
            columnHeader1.Width = 50;
            columnHeader2.Width = 65;
            columnHeader3.Width = 65;
            columnHeader4.Width = 60;

            columnHeader1.TextAlign = HorizontalAlignment.Center;
            columnHeader2.TextAlign = HorizontalAlignment.Center;
            columnHeader3.TextAlign = HorizontalAlignment.Center;
            columnHeader4.TextAlign = HorizontalAlignment.Center;
            lsvFileList.Border.Class = "ListViewBorder";
            lsvFileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
                    columnHeader1,
                    columnHeader2,
                    columnHeader3,
                    columnHeader4,

            });
            lsvFileList.FullRowSelect = true;
        }
        #endregion

        #region 变量声明
        public List<VideoPlaybackControl> vdoPanelList = new List<VideoPlaybackControl>();
        private VideoPlaybackControl currentPlayWnd;
        private int currentVideoNub = 1;
        private int VideoNubshow = 1;
        #endregion

        #region 方法
        private void CreateaALLVideoControl(int MaxVideoNubs)
        {
            this.pnlPlayContainer.SuspendLayout();
            for (int i = 1; i <= MaxVideoNubs; i++)
            {
                VideoPlaybackControl playbackcon = new VideoPlaybackControl();
                playbackcon.BackColor = System.Drawing.Color.RoyalBlue;
                playbackcon.BorderStyle = System.Windows.Forms.BorderStyle.None;
                playbackcon.ForeColor = System.Drawing.Color.MidnightBlue;
                playbackcon.Location = new System.Drawing.Point(1, 1);
                playbackcon.Size = new System.Drawing.Size(352, 288);
                playbackcon.SetWinMessage(mainHandle);
                playbackcon.Visible = false;
                playbackcon.OnSelectedWnd += new VideoPlaybackControl.OnSelectedWnddelegate(PlaybackWnd_OnSelectedWnd);
                playbackcon.OnMessage += new VideoPlaybackControl.OnMessagedelegate(playbackcon_OnMessage);
                playbackcon.OnFullPanel += new VideoPlaybackControl.OnFullPaneldelegate(playbackcon_OnFullPanel);
                playbackcon.OnFileSearch += new VideoPlaybackControl.OnFileSearchdelegate(playbackcon_OnFileSearch);
                playbackcon.OnTimeItemClicked += new VideoPlaybackControl.OnTimeItemClickeddelegate(playbackcon_OnTimeItemClicked);

                this.pnlPlayContainer.Controls.Add(playbackcon);
                vdoPanelList.Add(playbackcon);
            }
            this.pnlPlayContainer.ResumeLayout(false);
            currentPlayWnd = pnlPlayContainer.Controls[0] as VideoPlaybackControl;
        }
        public void MakeVidoPanelShow(int SelNubs)
        {
            if (pnlPlayContainer.Controls.Count == 0) return;
            VideoNubshow = SelNubs;
            this.pnlPlayContainer.SuspendLayout();

            foreach (Control con in pnlPlayContainer.Controls)
            {
                con.Visible = false;
            }
            IList<Rectangle> rectList = CalcPanelRectangle(SelNubs, pnlPlayContainer.Size);
            int i = 0;
            foreach (Rectangle rect in rectList)
            {
                pnlPlayContainer.Controls[i].Location = new System.Drawing.Point(rect.X, rect.Y);
                pnlPlayContainer.Controls[i].Size = new System.Drawing.Size(rect.Width, rect.Height);
                pnlPlayContainer.Controls[i].Visible = true;
                i++;
            }
            this.pnlPlayContainer.ResumeLayout(false);
        }
        /// <summary>
        /// 计算视频面板位置和面积
        /// </summary>
        /// <param name="channelCount"></param>
        /// <param name="TotalSquare">总面积和坐标</param>
        /// <returns></returns>
        private IList<Rectangle> CalcPanelRectangle(int channelCount, Size TotalArea)
        {
            IList<Rectangle> result = new List<Rectangle>();

            //模数
            int moduloW;
            int moduloH;

            switch (channelCount)
            {
                case 1:
                    moduloW = 1;
                    moduloH = 1;
                    break;
                case 4:
                    moduloW = 2;
                    moduloH = 2;
                    break;
                case 6:
                    moduloW = 3;
                    moduloH = 2;
                    break;
                case 9:
                    moduloW = 3;
                    moduloH = 3;
                    break;
                case 12:
                    moduloW = 4;
                    moduloH = 3;
                    break;
                case 16:
                    moduloW = 4;
                    moduloH = 4;
                    break;
                case 20:
                    moduloW = 5;
                    moduloH = 4;
                    break;
                case 25:
                    moduloW = 5;
                    moduloH = 5;
                    break;
                case 30:
                    moduloW = 6;
                    moduloH = 5;
                    break;
                case 36:
                    moduloW = 6;
                    moduloH = 6;
                    break;
                case 42:
                    moduloW = 7;
                    moduloH = 6;
                    break;
                case 49:
                    moduloW = 7;
                    moduloH = 7;
                    break;
                case 56:
                    moduloW = 8;
                    moduloH = 7;
                    break;
                case 64:
                    moduloW = 8;
                    moduloH = 8;
                    break;
                default:
                    moduloW = 2;
                    moduloH = 2;
                    break;

            }

            int width, height;

            //单个画面大小
            width = (TotalArea.Width - moduloW * 2) / moduloW;
            height = (TotalArea.Height - moduloH * 2) / moduloH;

            for (int i = 0; i < channelCount; i++)
            {
                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = height;

                if (i % moduloW == 0) //左边第1列
                {
                    rect.X = 1;
                    if (i == 0)
                        rect.Y = 1;
                    else
                        rect.Y = result[i - moduloW].Y + height + 2;
                }
                else
                {
                    rect.X = result[i - 1].X + width + 2;
                    rect.Y = result[i - 1].Y;
                }
                result.Add(rect);
            }
            return result;
        }
        private void PlaybackWnd_OnSelectedWnd(object sender, EventArgs e)
        {
            currentPlayWnd = sender as VideoPlaybackControl;
            pnlPlayContainer.Invalidate();

            //显示选中的文件列表
            initFileListCaption();
            VideoPlaybackControl videocontrol = sender as VideoPlaybackControl;
            int i = 0;
            foreach (DvrFindData file in videocontrol.FindDataList)
            {
                i++;
                ListViewItem listViewItem = new ListViewItem(new string[]
                {
                    i.ToString(),
                    file.StartTime.ToString("HH:mm:ss"),
                    file.StopTime.ToString("HH:mm:ss"),
                    file.FileSize.ToString()

                 }, 0);
                listViewItem.Tag = file;
                lsvFileList.Items.Add(listViewItem);
            }
            if (currentPlayWnd.BSearching)
            {
                btnSearchFile.Text = "停止查找";
            }
            else
            {
                btnSearchFile.Text = "查找";
            }

        }
        private void playbackcon_OnMessage(object sender, string msg)
        {
            if (msg == "下载完成")
            {
                btnDownload.Image = Properties.Resources.download_start;
                btnDownload.Text = "下载";
            }
            if (OnMessage != null)
                OnMessage(sender, msg);

        }
        private void playbackcon_OnFullPanel(object sender, bool bFull)
        {
            if (bFull)
            {
                foreach (VideoPlaybackControl vdo in vdoPanelList)
                {
                    if (vdo != ((VideoPlaybackControl)sender))
                    {
                        vdo.Visible = false;
                    }
                    else
                    {
                        vdo.Dock = DockStyle.Fill;
                        vdo.FullPanel = true;
                    }
                }
            }
            else
            {
                foreach (VideoPlaybackControl vdo in vdoPanelList)
                {
                    if (vdo != ((VideoPlaybackControl)sender))
                    {
                        vdo.Visible = true;
                    }
                    else
                    {
                        vdo.Dock = DockStyle.None;
                        vdo.FullPanel = false;
                    }
                }




            }
        }
        private void playbackcon_OnFileSearch(object sender, EventArgs e)
        {
            this.BtnSerachBYtime.Text = "查找";
            this.btnSearchFile.Text = "查找";
            currentPlayWnd.BSearching = false;

        }
        private void playbackcon_OnTimeItemClicked(object sender, Timeline.TimeItemEventArgs e)
        {

            foreach (ListViewItem item in lsvFileList.Items)
            {
                DvrFindData dat = item.Tag as DvrFindData;
                if (dat != null)
                {
                    item.Checked = false;

                    if (dat.FileName == e.m_fileName)
                    {
                        item.Checked = true;
                        item.Selected = true;
                        item.Focused = true;
                    }
                }

            }
            dtpStartTime.Value = e.m_time;


            monthCalByTime.SelectedDate = e.m_time;


        }

        public bool OpenVideo(VideoPlaybackArgs args)
        {
            currentPlayWnd.StopPlay();
            lsvFileList.Clear();
            if (currentPlayWnd != null)
                return currentPlayWnd.OpenVideo(args);
            return false;
        }
        public void StopAll()
        {
            foreach (VideoPlaybackControl vdo in vdoPanelList)
            {
                vdo.StopPlay();
            }

        }
        public void DowloadEnable(bool bEnable)
        {
            foreach (VideoPlaybackControl con in vdoPanelList)
            {
                con.DowloadEnable(bEnable);
            }
            btnDownload.Enabled = bEnable;
        }

        #endregion

        #region 事件
        private void btnVideoSplitClick(object sender, EventArgs e)
        {
            bool b = false;
            foreach (VideoPlaybackControl vdo in vdoPanelList)
            {
                if (vdo.WorkStatus == VIDEO_PLAY_STATE.State_Play || vdo.WorkStatus == VIDEO_PLAY_STATE.State_Record ||
                    vdo.WorkStatus == VIDEO_PLAY_STATE.State_Pause)
                {
                    b = true;
                    break;
                }

            }
            if (b)
            {
                MessageBoxEx.Show("请停止所有播放的视频后再操作！", "多画面操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int n = int.Parse(((ButtonX)sender).Tag.ToString());
            if (n > ClientConfig.MaxPlaybackPannel)
            {
                MessageBoxEx.Show("超过最大回放视频通道，请正确设置后再操作！", "多画面操作", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MakeVidoPanelShow(n);
            currentVideoNub = n;
            this.Invalidate();
        }
        private void btnVideoPlayALL_Click(object sender, EventArgs e)
        {
            foreach (VideoPlaybackControl vdo in vdoPanelList)
            {
                vdo.StartPlay();
            }


        }

        private void btnVideoStopAll_Click(object sender, EventArgs e)
        {
            foreach (VideoPlaybackControl vdo in vdoPanelList)
            {
                vdo.StopPlay();
            }

        }

        private void pnlPlayContainer_Paint(object sender, PaintEventArgs e)
        {
            if (currentPlayWnd == null) return;
            Graphics g = e.Graphics;
            Point point = new Point(currentPlayWnd.Left - 1,
                                     currentPlayWnd.Top - 1);
            Size size = new Size(currentPlayWnd.Width + 1,
                                     currentPlayWnd.Height + 1);
            Rectangle rect = new Rectangle(point, size);
            g.DrawRectangle(new Pen(Color.Red, 1), rect);

        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            if (currentPlayWnd.CurrentPlayArgs.Ip == "" || currentPlayWnd.CurrentPlayArgs.Ip == "0.0.0.0"
                || currentPlayWnd.CurrentPlayArgs.Ip == "...") return;
            if (!currentPlayWnd.BSearching)
            {
                btnSearchFile.Text = "停止查找";
                currentPlayWnd.BSearching = true;
                //开始查找
                uint ft = (uint)((DvrFileTypeEnum)Enum.Parse(typeof(DvrFileTypeEnum), cmbFileType.Text));
                string seldate = monthCalByFile.SelectedDate.ToString("yyyy-MM-dd");
                if (seldate == "0001-01-01")
                {
                    seldate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                string st = seldate + " 0:0:0";
                string et = seldate + " 23:59:59";
                currentPlayWnd.FindFile(ft, 0xff, Convert.ToDateTime(st), Convert.ToDateTime(et));

            }
            else
            {
                //停止搜索
                btnSearchFile.Text = "查找";
                currentPlayWnd.FindClose();
                currentPlayWnd.BSearching = false;


            }
        }

        private void lsvFileList_MouseClick(object sender, MouseEventArgs e)
        {
            if (lsvFileList.SelectedItems.Count > 0)
            {
                DvrFindData dat = lsvFileList.Items[0].Tag as DvrFindData;
                if (dat != null)
                    currentPlayWnd.PlayFileName = ((DvrFindData)(lsvFileList.SelectedItems[0].Tag)).FileName;
            }
        }

        private void AppcommandNav_Executed(object sender, EventArgs e)
        {

            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {
                switch (source.CommandParameter.ToString())
                {
                    case "BYTIME":
                        currentPlayWnd.PlaybackMode = PlaybackModeEnum.按时间;

                        string seldate = monthCalByFile.SelectedDate.ToString("yyyy-MM-dd");
                        if (seldate == "0001-01-01")
                        {
                            seldate = DateTime.Now.ToString("yyyy-MM-dd");
                        }
                        string st = seldate + " " + dtpStartTime.Value.ToString("HH:mm:ss");
                        string et = seldate + " " + dtpEndTime.Value.ToString("HH:mm:ss");

                        currentPlayWnd.StartTime = Convert.ToDateTime(st);
                        currentPlayWnd.EndTime = Convert.ToDateTime(et);

                        break;
                    case "BYFILE":
                        currentPlayWnd.PlaybackMode = PlaybackModeEnum.按文件;
                        break;
                    case "BYEVENT":
                        currentPlayWnd.PlaybackMode = PlaybackModeEnum.按事件;
                        break;
                    case "SMART":
                        currentPlayWnd.PlaybackMode = PlaybackModeEnum.智能;
                        break;

                }
            }


        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (lsvFileList.SelectedItems.Count == 0)
            {
                MessageBoxEx.Show("请选中文件后进行播放操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            currentPlayWnd.StartPlay();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            currentPlayWnd.StopPlay();

        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (lsvFileList.SelectedItems.Count == 0)
            {
                MessageBoxEx.Show("请选中文件后进行播放操作！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (currentPlayWnd.BDowloading)
            //{
            //    MessageBoxEx.Show("正在下载请稍后继续！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (!currentPlayWnd.BDowloading)
            {
                //开始下载
                currentPlayWnd.StartDownload();
                btnDownload.Image = Properties.Resources.download_Cancel;
                btnDownload.Text = "停止下载";
            }
            else
            {
                //停止下载
                currentPlayWnd.StopDownload();
                btnDownload.Image = Properties.Resources.download_start;
                btnDownload.Text = "下载";
            }
        }

        private void btnPlaybyTime_Click(object sender, EventArgs e)
        {
            currentPlayWnd.PlaybackMode = PlaybackModeEnum.按时间;

            string seldate = monthCalByTime.SelectedDate.ToString("yyyy-MM-dd");
            if (seldate == "0001-01-01")
            {
                seldate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            string st = seldate + " " + dtpStartTime.Value.ToString("HH:mm:ss");
            string et = seldate + " " + dtpEndTime.Value.ToString("HH:mm:ss");

            currentPlayWnd.StartTime = Convert.ToDateTime(st);
            currentPlayWnd.EndTime = Convert.ToDateTime(et);
            currentPlayWnd.StartPlay();
        }

        private void pnlPlayContainer_SizeChanged(object sender, EventArgs e)
        {
            MakeVidoPanelShow(VideoNubshow);
        }

        private void lsvFileList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lsvFileList.SelectedItems.Count > 0)
            {
                DvrFindData dat = lsvFileList.Items[0].Tag as DvrFindData;
                if (dat != null)
                {
                    currentPlayWnd.PlayFileName = ((DvrFindData)(lsvFileList.SelectedItems[0].Tag)).FileName;
                    currentPlayWnd.StartPlay();
                }
            }
        }

        private void btnStopbyTime_Click(object sender, EventArgs e)
        {
            currentPlayWnd.PlaybackMode = PlaybackModeEnum.按时间;
            currentPlayWnd.StopPlay();

        }

        private void btnDownloadbyTime_Click(object sender, EventArgs e)
        {
            currentPlayWnd.PlaybackMode = PlaybackModeEnum.按时间;

            string seldate = monthCalByTime.SelectedDate.ToString("yyyy-MM-dd");
            if (seldate == "0001-01-01")
            {
                seldate = DateTime.Now.ToString("yyyy-MM-dd");
            }
            string st = seldate + " " + dtpStartTime.Value.ToString("HH:mm:ss");
            string et = seldate + " " + dtpEndTime.Value.ToString("HH:mm:ss");

            currentPlayWnd.StartTime = Convert.ToDateTime(st);
            currentPlayWnd.EndTime = Convert.ToDateTime(et);

            currentPlayWnd.StartDownload();

        }



        #endregion

        private void btnSearchBYtime_Click(object sender, EventArgs e)
        {
            currentPlayWnd.PlaybackMode = PlaybackModeEnum.按时间;
            if (currentPlayWnd.CurrentPlayArgs.Ip == "" || currentPlayWnd.CurrentPlayArgs.Ip == "0.0.0.0"
               || currentPlayWnd.CurrentPlayArgs.Ip == "...") return;
            if (!currentPlayWnd.BSearching)
            {
                btnSearchByTime.Text = "停止查找";
                currentPlayWnd.BSearching = true;

                string seldate = monthCalByTime.SelectedDate.ToString("yyyy-MM-dd");
                if (seldate == "0001-01-01")
                {
                    seldate = DateTime.Now.ToString("yyyy-MM-dd");
                }
                string st = seldate + " " + dtpStartTime.Value.ToString("HH:mm:ss");
                string et = seldate + " " + dtpEndTime.Value.ToString("HH:mm:ss");

                currentPlayWnd.StartTime = Convert.ToDateTime(st);
                currentPlayWnd.EndTime = Convert.ToDateTime(et);
                uint ft = (uint)((DvrFileTypeEnum)Enum.Parse(typeof(DvrFileTypeEnum), cmbFileType.Text));

                //uint dwFileType, byte isLock, DateTime dtStar, DateTime dtEnd)
                currentPlayWnd.FindFile(ft, 0xff, currentPlayWnd.StartTime, currentPlayWnd.EndTime);

            }
            else
            {
                //停止搜索
                btnSearchFile.Text = "查找";
                currentPlayWnd.FindClose();
                currentPlayWnd.BSearching = false;


            }
        }


    }
}
