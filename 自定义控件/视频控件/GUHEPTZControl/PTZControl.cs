using DevComponents.DotNetBar;
using GHIBMS.Common;
using System;
using System.Timers;
using System.Windows.Forms;

namespace GHIBMS.PTZControl
{
    public partial class PTZControl : UserControl
    {

        //云台发送开始
        private System.Timers.Timer timerSentPTZStartCmd = new System.Timers.Timer();
        //云台停止开始
        private System.Timers.Timer timerSentPTZStopCmd = new System.Timers.Timer();
        //云台最长运行时间，防止漏发停止指令
        private System.Timers.Timer timerSentPTZOvertime = new System.Timers.Timer();
        //镜头控制开始
        private System.Timers.Timer timerSentDomeStartCmd = new System.Timers.Timer();
        //镜头控制停止
        private System.Timers.Timer timerSentDomeStopCmd = new System.Timers.Timer();



        private int Count_timerSentPTZStart = 0;
        private int Count_timerSentPTZStop = 0;
        private int Count_timerSentDomeStart = 0;
        private int Count_timerSentDomeStop = 0;


        VideoCommandArgs cmd = new VideoCommandArgs();

        private int x = 0;


        #region 属性
        public void SelectCammera(VideoCommandArgs cmdArgs)
        {
            cmd.CamName = cmdArgs.CamName;
            cmd.MonName = cmdArgs.MonName;
            cmd.SubVideoOut = cmd.SubVideoOut;

        }

        #endregion

        #region 事件
        public delegate void VideoControlDelegate(object sender, VideoCommandArgs e);
        public event VideoControlDelegate OnVideoControl;

        #endregion

        #region 构造函数
        public PTZControl()
        {


            InitializeComponent();
            // Set the value of the double-buffering style bits to true.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
               ControlStyles.UserPaint |
               ControlStyles.AllPaintingInWmPaint,
               true);
            this.UpdateStyles();

            timerSentPTZStartCmd.Interval = ClientConfig.PtzCmdSendInterval;
            timerSentPTZStartCmd.Elapsed += new System.Timers.ElapsedEventHandler(timerPTZSentStartCmd_Elapsed);
            timerSentPTZStopCmd.Interval = ClientConfig.PtzCmdSendInterval; ;
            timerSentPTZStopCmd.Elapsed += new System.Timers.ElapsedEventHandler(timerPTZSentStopCmd_Elapsed);
            timerSentPTZOvertime.Interval = 10000;
            timerSentPTZOvertime.Elapsed += new ElapsedEventHandler(timerSentPTZOvertime_Elapsed);

            timerSentDomeStartCmd.Interval = ClientConfig.PtzCmdSendInterval; ;
            timerSentDomeStartCmd.Elapsed += new System.Timers.ElapsedEventHandler(timerSentDomeCmdStart_Elapsed);
            timerSentDomeStopCmd.Interval = ClientConfig.PtzCmdSendInterval; ;
            timerSentDomeStopCmd.Elapsed += new ElapsedEventHandler(timerSentDomeStopCmd_Elapsed);
            panControl.OnMouseDown += new GHIBMS.PTZControl.PanControl.OnMouseDowndelegate(panControl_OnMouseDown);
            panControl.OnMouseUp += new GHIBMS.PTZControl.PanControl.OnMouseUpdelegate(panControl_OnMouseUp);
            panControl.ValueChanged += new EventHandler<DevComponents.Instrumentation.ValueChangedEventArgs>(panControl_ValueChanged);
        }
        #endregion

        #region 方法
        private void timerPTZSentStartCmd_Elapsed(object sender, ElapsedEventArgs e)
        {
            x = Convert.ToInt32(panControl.Value);

            cmd.VideoCommand = CreatePTZCommand();
            cmd.Speed = (uint)sliderSpeed.Value;
            cmd.Stop = 0;
            if (OnVideoControl != null)
            {
                this.Invoke(new VideoControlDelegate(OnVideoControl), new object[] { this, cmd });
            }

            //True表示一直重发发码，直到手工停止
            if (!ClientConfig.PtzContinueRepeat)
            {
                Count_timerSentPTZStart++;
                if (Count_timerSentPTZStart > ClientConfig.PtzCmdSendRepeat)
                {
                    timerSentPTZStartCmd.Stop();
                    Count_timerSentPTZStart = 0;
                }
            }

            //Debug.WriteLine("-----------timonMousedown");
        }
        private void timerPTZSentStopCmd_Elapsed(object sender, ElapsedEventArgs e)
        {
            x = Convert.ToInt32(panControl.Value);

            cmd.VideoCommand = CreatePTZCommand();
            cmd.Speed = (uint)sliderSpeed.Value;
            cmd.Stop = 1;
            if (OnVideoControl != null)
            {
                this.Invoke(new VideoControlDelegate(OnVideoControl), new object[] { this, cmd });
            }

            //Debug.WriteLine("-----------timonMouseup");
            timerSentPTZStartCmd.Stop();
            timerSentPTZOvertime.Stop();


            Count_timerSentPTZStop++;
            if (Count_timerSentPTZStop > ClientConfig.PtzCmdSendRepeat)
            {
                timerSentPTZStopCmd.Stop();
                Count_timerSentPTZStop = 0;
            }
        }
        private void timerSentDomeCmdStart_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (OnVideoControl != null)
            {
                this.Invoke(new VideoControlDelegate(OnVideoControl), new object[] { this, cmd });
            }
            //True表示一直重发发码，直到手工停止
            if (!ClientConfig.PtzContinueRepeat)
            {

                Count_timerSentDomeStart++;
                if (Count_timerSentDomeStart > ClientConfig.PtzCmdSendRepeat)
                {
                    timerSentDomeStartCmd.Stop();
                    Count_timerSentDomeStart = 0;
                }
            }

        }

        private void timerSentDomeStopCmd_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerSentDomeStartCmd.Stop();
            if (OnVideoControl != null)
            {
                this.Invoke(new VideoControlDelegate(OnVideoControl), new object[] { this, cmd });
            }

            Count_timerSentDomeStop++;
            if (Count_timerSentDomeStop > ClientConfig.PtzCmdSendRepeat)
            {
                timerSentDomeStopCmd.Stop();
                Count_timerSentDomeStop = 0;
            }

        }
        private void timerSentPTZOvertime_Elapsed(object sender, ElapsedEventArgs e)
        {
            timerSentPTZOvertime.Stop();
            timerSentPTZStartCmd.Stop();
            timerSentPTZStopCmd.Start();
        }
        private void panControl_ValueChanged(object sender, DevComponents.Instrumentation.ValueChangedEventArgs e)
        {
            x = Convert.ToInt32(e.NewValue);
        }
        private void panControl_OnMouseDown(object sender, MouseEventArgs e)
        {

            //Debug.WriteLine("-----------onMousedown");
            timerSentPTZStartCmd.Start();

        }
        private void panControl_OnMouseUp(object sender, MouseEventArgs e)
        {

            //Debug.WriteLine("-----------onMouseup");
            timerSentPTZStartCmd.Stop();
            timerSentPTZStopCmd.Start();

        }
        private void PTZCommand_Executed(object sender, EventArgs e)
        {
            ICommandSource source = sender as ICommandSource;
            if (source.CommandParameter is string)
            {

            }
        }

        private void PTZButtonMouseDown(object sender, MouseEventArgs e)
        {
            CreateCurrentCmdCode(sender, true);
            timerSentDomeStartCmd.Start();

        }
        private void PTZButtonMouseUp(object sender, MouseEventArgs e)
        {
            CreateCurrentCmdCode(sender, false);
            timerSentDomeStartCmd.Stop();
            timerSentDomeStopCmd.Start();


        }
        private void CreateCurrentCmdCode(object sender, bool bStart)
        {

            if (sender.GetType().Name == "ButtonX")   //镜头
            {
                if (bStart)
                {
                    //启动
                    //timerSentPTZStartCmd.Stop();
                    //cmd.VideoIn = this.CamID;
                    //cmd.CamName = this.CamName;
                    //cmd.MatrixName = this.MatrixName;
                    //cmd.Channel = this.Channel;
                    cmd.Speed = (uint)sliderSpeed.Value;
                    cmd.Stop = 0;
                    ICommandSource source = sender as ICommandSource;
                    if (source.CommandParameter is string)
                    {
                        cmd.VideoCommand = ((PTZCmdCodeEnum)Enum.Parse(typeof(PTZCmdCodeEnum), source.CommandParameter.ToString()));
                    }
                }
                else
                {
                    //停止
                    //timerSentPTZStartCmd.Stop();
                    //cmd.VideoIn = this.CamID;
                    //cmd.CamName = this.CamName;
                    //cmd.MatrixName = this.MatrixName;
                    //cmd.Channel = this.Channel;
                    cmd.Speed = (uint)sliderSpeed.Value;
                    cmd.Stop = 1;
                    ICommandSource source = sender as ICommandSource;
                    if (source.CommandParameter is string)
                    {
                        cmd.VideoCommand = ((PTZCmdCodeEnum)Enum.Parse(typeof(PTZCmdCodeEnum), source.CommandParameter.ToString()));
                    }
                }
            }
        }
        private PTZCmdCodeEnum CreatePTZCommand()
        {
            PTZCmdCodeEnum cmd = 0;
            if ((x >= 0 && x < 23) || (x >= 337))
            {
                cmd = PTZCmdCodeEnum.PAN_RIGHT;
            }
            else if (x >= 23 && x < 67)
            {
                cmd = PTZCmdCodeEnum.DOWN_RIGHT;
            }
            else if (x >= 67 && x < 112)
            {
                cmd = PTZCmdCodeEnum.TILT_DOWN;
            }
            else if (x >= 112 && x < 157)
            {
                cmd = PTZCmdCodeEnum.DOWN_LEFT;
            }
            else if (x >= 157 && x < 202)
            {
                cmd = PTZCmdCodeEnum.PAN_LEFT;
            }
            else if (x >= 202 && x < 247)
            {
                cmd = PTZCmdCodeEnum.UP_LEFT;
            }
            else if (x >= 247 && x < 292)
            {
                cmd = PTZCmdCodeEnum.TILT_UP;
            }
            else if (x >= 292 && x < 337)
            {
                cmd = PTZCmdCodeEnum.UP_RIGHT;
            }
            else if (x == 400)
            {
                cmd = PTZCmdCodeEnum.ZOOM_IN;
            }
            else if (x == 500)
            {
                cmd = PTZCmdCodeEnum.ZOOM_OUT;
            }

            return cmd;

        }


        #endregion




    }

}
