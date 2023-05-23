
/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using GHIBMS.Interface;
using GHIBMS.Common;
using cn.jpush.api;
在此之后:
using cn.jpush.api;
using cn.Collections.api.push.mode;
using GHIBMS.Common;
using GHIBMS.Interface;
using System;
using GHIBMS.Generic;
using System.Linq;
using System.Text;
*/
using cn.jpush.api;
using cn.jpush.api.push.mode;
using GHIBMS.Common;

/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using System.Collections.Common;
using System;
在此之后:
using System;
using System.Collections.Common;
*/
using System;
using System.Collections.Generic;
using System.Threading;

namespace GHIBMS.Server
{
    public class PushMsgController
    {

        public static List<PushMessage> MessageList = new List<PushMessage>();

        private int SLEEP_TIME = 1000;
        private bool active = false;
        private Thread thread;
        private JPushClient client = new JPushClient(ServerConfig.app_key, ServerConfig.master_secret);
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

        public void AddMessage(PushMessage msg)
        {
            lock (synObj)
            {
                if (!MessageList.Contains(msg))
                    MessageList.Add(msg);
                if (MessageList.Count > 50)
                    MessageList.RemoveAt(0);
            }
        }
        public List<PushMessage> GetMessage()
        {
            lock (synObj)
            {
                if (MessageList.Count > 0)
                {
                    List<PushMessage> msgs = new List<PushMessage>();
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
                List<PushMessage> msglist = GetMessage();
                if (msglist != null)
                {
                    PushMessage[] msgArray = msglist.ToArray();
                    foreach (PushMessage msg in msgArray)
                    {
                        PushPayload payload = PushObject_All_All_Alert(msg);
                        try
                        {
                            var result = client.SendPush(payload);

                        }
                        catch (Exception ex)
                        {
                            Logger.GetInstance().LogError(ex.ToString());

                        }
                    }
                }
            }
        }
        public PushPayload PushObject_All_All_Alert(PushMessage msg)
        {
            PushPayload pushPayload = new PushPayload();
            pushPayload.platform = Platform.all();
            pushPayload.audience = Audience.all();
            pushPayload.notification = new Notification().setAlert(msg.Message);
            return pushPayload;
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {

            thread = new Thread(new ThreadStart(PerformPushTask));
            //线程名，调试用
            thread.Name = "PerformResetTask_thread";
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
                        Console.WriteLine(e.ToString());
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
    public class PushMessage
    {
        public PushMessage(string tag, string msg)
        {
            this.devTag = tag;
            this.msg = msg;

        }
        private string devTag = "";
        public string DevTag
        {
            get { return devTag; }
            set { devTag = value; }
        }
        private string msg = "";
        public string Message
        {
            get { return msg; }
            set { msg = value; }
        }
    }
}
