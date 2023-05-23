using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace GHIBMS.Server
{
    public class SMSService
    {
        #region 变量定义
        private static int SLEEP_TIME = 1000;
        private GsmModem gsm;
        private bool active = false;
        private object synActive = new object();
        private Thread thread;
        private object synSmsCount = new object();
        private int smsCount=0;
        #endregion

        #region 委托事件
        //委托 事件
        public delegate void SendEventHandler(object sender, GSMEventArgs ge);
        //异步调用 每发送一条调用一次
        public SendEventHandler OnSendOneMsg = null;

        public delegate void onErrorDelegate(Exception e);
        public event onErrorDelegate onError;

        public delegate void OnReadSmsDelegate(DecodedMessage msg);
        public event OnReadSmsDelegate OnReadSms;

        public delegate void OnPortOpendelegate(object sender,bool IsOpen);
        public event OnPortOpendelegate OnPortOpen; 

      

        /// <summary>
        /// 收到短信息事件 OnRecieved 
        /// 收到短信将引发此事件
        /// </summary>
        public delegate void OnSmsRecievedDeleagte(object sender, int smsCount);
        public event OnSmsRecievedDeleagte OnSmsRecieved;


   
        #endregion

        #region 构造函数
        public SMSService():this("COM1", 9600)
        {

        }
        public SMSService(string comPort, int baudRate)
        {
            gsm = new GsmModem(comPort, baudRate);
            gsm.SmsRecieved += new GSMMODEM.GsmModem.SmsRecievedDelegate(gsm_SmsRecieved);
             
        }
        public SMSService(string comPort, int baudRate, Parity parity, Handshake handshake)
        {
            gsm = new GsmModem(comPort, baudRate,parity, handshake);
            gsm.SmsRecieved += new GSMMODEM.GsmModem.SmsRecievedDelegate(gsm_SmsRecieved);
        }
        #endregion

        #region 属性

        /// <summary>
        /// 是否已启动
        /// </summary>
        public bool Active
        {
            get
            {
                lock (synActive)
                {
                    return active;
                }
            }
            set
            {
                lock (synActive)
                {
                    active = value;
                }
            }
        }
        /// <summary>
        /// 是否收到短信
        /// </summary>
        public int SmsCount
        {
            get
            {
                lock (synSmsCount)
                {
                    return smsCount;
                }
            }
            set
            {
                lock (synSmsCount)
                {
                    smsCount = value;
                }
            }
        }
       /// <summary>
       /// 发送队列
       /// </summary>
        private SmsQueue _SmsMsgSentQueue;
        public  SmsQueue  SmsMsgSentQueue
        {
            set { _SmsMsgSentQueue = value; }
            get { return _SmsMsgSentQueue; }
        }
        #endregion

        /// <summary>
        /// 开始线程
        /// </summary>
        public void Start()
        {
            if (thread != null)
                return;
            thread = new Thread(new ThreadStart(SendMsgThread));
            //线程名，调试用
            //thread.Name = "SentSmsMsg_thread";
            //thread.IsBackground = true;
            active = true;
            thread.Start();
            if (gsm.Open())
            {
                if (OnPortOpen!=null)
                    OnPortOpen(this,true);
            }
        }
        /// <summary>
        /// 停止线程
        /// </summary>
        public void Stop()
        {
            gsm.Close();
            if (OnPortOpen != null)
                OnPortOpen(this, false);
            if (thread == null)
                return;
            Active = false;

        }
        /// <summary>
        /// 短信接收事件
        /// </summary>
        private void gsm_SmsRecieved(object sender, int smsCount)
        {
            if (OnSmsRecieved != null)
            {
                OnSmsRecieved(sender, smsCount);
            }
            this.SmsCount = smsCount;
        }


        //内部线程
        private void SendMsgThread()
        {
            while (Active)
            {
                try
                {
                    //发送短信
                    SmsMsg sms = _SmsMsgSentQueue.OutFromSmsQueue();
                    if (sms != null)
                    {
                        if (sms.Phone != null && sms.Phone.Length != 0 &&
                            sms.Msg != null && sms.Msg.Length != 0 
                            && gsm.IsOpen)
                        {
                            try
                            {
                                gsm.SendMsg(sms.Phone, sms.Msg);
                                GSMEventArgs ge = new GSMEventArgs(sms.Phone, sms.Msg,true);
                                if (OnSendOneMsg != null)
                                    OnSendOneMsg(this, ge);
                              
                            }
                            catch
                            {
                                GSMEventArgs ge = new GSMEventArgs(sms.Phone,sms.Msg,false);
                                if (OnSendOneMsg != null)
                                    OnSendOneMsg(this, ge);
                                return;
                            }
                        }
                    }
                    //接收短信

                    if (gsm.IsOpen && this.SmsCount>0)
                    {
                         DecodedMessage de =  gsm.ReadNewMsg();
                         if (OnReadSms != null)
                             OnReadSms(de);
                    }

                    try
                    {
                        System.Threading.Thread.Sleep(SLEEP_TIME);
                    }
                    catch { }
                }
                catch (Exception e)
                {
                    if (onError != null)
                        onError(e);
                }
            }
            thread = null;
        }
    }
  
}
