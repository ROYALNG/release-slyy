using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GHIBMS.NetVideo
{
    
    public partial class MultiNetVideo : UserControl
    {

        private List<NetVideoControl> videoList = new List<NetVideoControl>();
        public MultiNetVideo()
        {
            InitializeComponent();
       
            //netVideo1.Click+=new EventHandler(netVideo1_Click);
        }
        private void MultiNetVideo_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Point point = new Point(pnl.Left - 1,
            //                         pnl.Top - 1);
            //Size size = new Size(pnl.Width + 2,
            //                         pnl.Height + 2);
            //Rectangle rect = new Rectangle(point, size);
            //g.DrawRectangle(new Pen(Color.Red, 2), rect);
           
        }

        //除视频播放面器
        private void DisposeNetVideoPlayer()
        {
            foreach (NetVideoControl video in videoList)
            {
               //停止播放
                video.StopPlay();
                video.Dispose();
            }
            this.Controls.Clear();
            videoList.Clear();

        }
        
        //分类清除所有SDK占用的资源 
        //此除要注意用户登出、清除SDK一定要静态，属于类才可以
         private void DisposeNetVideoSDK()
         {
               //海康 
               HikDvrRealPlayer.DVR_Logout();
               HikDvrRealPlayer.DVR_Cleanup();

         }

         private void button1_Click(object sender, EventArgs e)
         {

             VideoRealPlayArgs arg = new VideoRealPlayArgs();
             arg.DvrIp = "202.197.111.146";
             arg.DvrCh = 2;
             arg.DvrPort = 8000;
             arg.UserName = "admin";
             arg.Password = "12345";
             arg.TCPMode = TCPModeEnum.TCP;
             arg.EncodeMode = EncodeTypeEnum.主码流;
             arg.VodMode = TransferTypeEnum.DVR直播;
          
         }
    }
}
