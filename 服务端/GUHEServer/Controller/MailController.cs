using GHIBMS.Common;

/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using System.Collections.Generic;
using System.Linq;
using System.Text;
在此之后:
using GHIBMS.Interface;
using System;
using System.Collections.Generic;
*/
using System;
using System.Collections.Generic;
using System.Threading;
/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using GHIBMS.Interface;
using GHIBMS.Common;
在此之后:
using System.Text;
using System.Threading;
*/



namespace GHIBMS.Server
{
    public class MailController
    {

        public static List<mailContent> MessageList = new List<mailContent>();

        private int SLEEP_TIME = 1000;
        private bool active = false;
        private Thread thread;
        /// <summary>
        /// 是否已启动
        /// </summary>
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        private object synObj = new object();

        public void AddMessage(mailContent msg)
        {
            lock (synObj)
            {
                if (!MessageList.Contains(msg))
                    MessageList.Add(msg);
                if (MessageList.Count > 20)
                    MessageList.RemoveAt(0);

            }
        }
        public List<mailContent> GetMessage()
        {
            lock (synObj)
            {
                if (MessageList.Count > 0)
                {
                    List<mailContent> msgs = new List<mailContent>();
                    msgs.AddRange(MessageList.ToArray());
                    MessageList.Clear();
                    return msgs;
                }
            }
            return null;
        }
        //取得信息记录列表
        private void PushMessage()
        {
            lock (synObj)//锁定信息记录列表
            {
                List<mailContent> msglist = GetMessage();
                if (msglist != null)
                {
                    mailContent[] msgArray = msglist.ToArray();
                    foreach (mailContent msg in msgArray)
                    {
                        string body = "--------------报警消息----------------\r\n";
                        body = body + msg.Message + "\r\n";

                        Email mail = new Email();
                        mail.mailFrom = "postmaster@ghibms.com";
                        mail.mailToArray = new string[] { msg.Address };
                        mail.mailPwd = "Dongdong1997";
                        mail.mailSubject = "集成平台报警";
                        mail.host = "smtp.ghibms.com";
                        mail.isbodyHtml = false;
                        mail.mailBody = body;
                        string ret = "";
                        if (!mail.Send(ref ret))
                        {
                            Logger.GetInstance().LogMsg("发送报警邮件失败！");
                            return;
                        }

                    }
                }
            }
        }


        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {

            thread = new Thread(new ThreadStart(PerformPushTask));
            //线程名，调试用
            thread.Name = "PerformMail_thread";
            thread.IsBackground = true;
            active = true;
            thread.Start();
        }
        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
            active = false;
            for (int i = 0; i < 20; i++)
            {
                if (thread != null)
                    Thread.Sleep(100);
                else
                    break;
            }
            if (thread != null)
            {
                try
                {
                    thread.Interrupt();
                }
                catch (Exception ex)
                {
                }
            }

            thread = null;

        }

        //内部线程
        private void PerformPushTask()
        {
            try
            {
                while (active)
                {
                    try
                    {
                        PushMessage();

                        try
                        {
                            if (SLEEP_TIME < 100) SLEEP_TIME = 100;
                            for (int i = 0; i < (int)(SLEEP_TIME / 100); i++)
                            {
                                if (!active)
                                {
                                    thread = null;
                                    return;
                                }
                                System.Threading.Thread.Sleep(100);
                            }
                        }
                        catch { }
                    }
                    catch (Exception e)
                    {
                        //Console.WriteLine(e.ToString());
                        System.Threading.Thread.Sleep(100);
                    }
                    Thread.Sleep(10);
                }
                thread = null;
            }catch
            {
                thread = null;
            }
        }
    }
    public class mailContent
    {
        public mailContent(string address, string msg)
        {
            this.Address = address;
            this.msg = msg;

        }
        private string address = "";
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string msg = "";
        public string Message
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}
