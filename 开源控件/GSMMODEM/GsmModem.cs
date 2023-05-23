

using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using GHIBMS.Common;


namespace GSMMODEM
{
    /// <summary>
    /// “猫”设备类，完成短信发送 接收等
    /// </summary>
    public class GsmModem
    {
        #region 构造函数
        /// <summary>
        /// 默认构造函数 完成有关初始化工作
        /// </summary>
        /// <remarks>默认 端口号：COM1，波特率：9600</remarks>
        public GsmModem()
            : this("COM1", 9600)
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comPort">串口号</param>
        /// <param name="baudRate">波特率</param>
        public GsmModem(string comPort, int baudRate)
        {
            _com = new MyCom();
            _com.PortName = comPort;          //
            _com.BaudRate = baudRate;
            _com.ReadTimeout = 15000;         //读超时时间 发送短信时间的需要
            _com.WriteTimeout = 10000;         //串口发送超时
            _com.Handshake = Handshake.None;
            _com.RtsEnable = true;            //必须为true 这样串口才能接收到数据
            _com.DataReceived += new EventHandler(sp_DataReceived);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comPort">串口号</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity">效验</param>
        /// <param name="handshake">流控制</param>
        public GsmModem(string comPort, int baudRate, Parity parity,Handshake handshake)
        {
            _com = new MyCom();
            _com.PortName = comPort;          //
            _com.BaudRate = baudRate;
            _com.Parity = parity;
            _com.ReadTimeout = 15000;         //读超时时间 发送短信时间的需要
            _com.WriteTimeout = 10000;         //串口发送超时
            _com.Handshake = handshake;
            _com.RtsEnable = true;            //必须为true 这样串口才能接收到数据
            _com.DataReceived += new EventHandler(sp_DataReceived);
        }


        //单元测试用构造函数
        internal GsmModem(ICom com)
        {
            _com = com;

            _com.ReadTimeout = 15000;         //读超时时间 发送短信时间的需要
            _com.RtsEnable = true;            //必须为true 这样串口才能接收到数据

            _com.DataReceived += new EventHandler(sp_DataReceived);
        }

        #endregion 构造函数

        #region 私有字段
        private ICom _com;              //私有字段 串口对象

        private Queue<int> newMsgIndexQueue = new Queue<int>();            //新消息序号

        private string msgCenter = string.Empty;           //短信中心号码

        #endregion 私有字段

        #region 属性

        /// <summary>
        /// 串口号 运行时只读 设备打开状态写入将引发异常
        /// 提供对串口端口号的访问
        /// </summary>
        public string ComPort
        {
            get
            {
                return _com.PortName;
            }
            set
            {
                _com.PortName = value;
            }
        }

        /// <summary>
        /// 波特率 可读写
        /// 提供对串口波特率的访问
        /// </summary>
        public int BaudRate
        {
            get
            {
                return _com.BaudRate;
            }
            set
            {
                _com.BaudRate = value;
            }
        }

        /// <summary>
        /// 设备是否打开
        /// 对串口IsOpen属性访问
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return _com.IsOpen;
            }
        }

        private bool autoDelMsg = false;

        /// <summary>
        /// 对autoDelMsg访问
        /// 设置是否在阅读短信后自动删除 SIM 卡内短信存档
        /// 默认为 false 
        /// </summary>
        public bool AutoDelMsg
        {
            get
            {
                return autoDelMsg;
            }
            set
            {
                autoDelMsg = value;
            }
        }
     

        #endregion

        #region 收到短信事件

        /// <summary>
        /// 收到短信息事件 OnRecieved 
        /// 收到短信将引发此事件
        /// </summary>
        public delegate void SmsRecievedDelegate(object sender,int smsCount);
        /// <summary>
        /// 收到短信事件
        /// </summary>
        public event SmsRecievedDelegate SmsRecieved;

        #endregion

        #region 串口收到数据检测短信收到

        /// <summary>
        /// 从串口收到数据 串口事件
        /// 程序未完成需要的可自己添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void sp_DataReceived(object sender, EventArgs e)
        {
            string temp = _com.ReadLine();
            if (temp.Length > 8)
            {
                if (temp.Substring(0, 6) == "+CMTI:")
                {
                    newMsgIndexQueue.Enqueue(Convert.ToInt32(temp.Split(',')[1]));  //存储新信息序号
                    OnSmsRecieved(newMsgIndexQueue.Count);                                //触发事件
                }
            }
        }

        /// <summary>
        /// 保护虚方法，引发收到短信事件
        /// </summary>
        /// <param name="smsCount">事件数据</param>
        protected virtual void OnSmsRecieved(int smsCount)
        {
            if (SmsRecieved != null)
            {
                SmsRecieved(this, smsCount);
            }
        }

        #endregion

        #region 方法

        #region 设备打开与关闭

        /// <summary>
        /// 设备打开函数，无法时打开将引发异常
        /// </summary>
        public bool Open()
        {
            try
            {
                //如果串口已打开 则先关闭
                if (_com.IsOpen)
                {
                    _com.Close();
                }

                try
                {
                    _com.Open();
                }
                catch
                {
                    return false;
                }
                  



                //初始化设备
                if (_com.IsOpen)
                {
                    _com.Write("ATE0\r");
                    Thread.Sleep(50);
                    _com.Write("AT+CMGF=0\r");
                    Thread.Sleep(50);
                    _com.Write("AT+CNMI=2,1\r");
                }
                return true;
            }
            catch 
            {
                return false;
            }

        }

        /// <summary>
        /// 设备关闭函数
        /// </summary>
        public bool Close()
        {
            try
            {
                _com.Close();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine( ex.ToString());
                return false;
            }
        }

        #endregion 设备打开与关闭

        #region 获取和设置设备有关信息

        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns>机器码字符串（设备厂商，本机号码）</returns>
        public string GetMachineNo()
        {
            string result = SendAT("AT+CGMI");
            if (result.Substring(result.Length - 4, 3).Trim() == "OK")
            {
                result = result.Substring(0, result.Length - 5).Trim();
            }
            else
            {
                throw new Exception("获取机器码失败");
            }
            return result;
        }

        /// <summary>
        /// 设置短信中心号码
        /// </summary>
        /// <param name="msgCenterNo">短信中心号码</param>
        public void SetMsgCenterNo(string msgCenterNo)
        {
            msgCenter = msgCenterNo;
        }

        /// <summary>
        /// 获取短信中心号码
        /// </summary>
        /// <returns></returns>
        public string GetMsgCenterNo()
        {
            string tmp = string.Empty;
            if (msgCenter != null && msgCenter.Length != 0)
            {
                return msgCenter;
            }
            else
            {
                tmp = SendAT("AT+CSCA?");
                if (tmp.Substring(tmp.Length - 4, 3).Trim() == "OK")
                {
                    return tmp.Split('\"')[1].Trim();
                }
                else
                {
                    throw new Exception("获取短信中心失败");
                }
            }
        }

        #endregion 获取和设置设备有关信息

        #region 发送AT指令

        /// <summary>
        /// 发送AT指令 逐条发送AT指令 调用一次发送一条指令
        /// 能返回一个OK或ERROR算一条指令
        /// </summary>
        /// <param name="ATCom">AT指令</param>
        /// <returns>发送指令后返回的字符串</returns>
        public string SendAT(string ATCom)
        {
            string result = string.Empty;
            //忽略接收缓冲区内容，准备发送
            _com.DiscardInBuffer();

            //注销事件关联，为发送做准备
            _com.DataReceived -= sp_DataReceived;

            //发送AT指令
            try
            {
                _com.Write(ATCom + "\r");
            }
            catch (Exception ex)
            {
                _com.DataReceived += sp_DataReceived;
                throw ex;
            }

            //接收数据 循环读取数据 直至收到“OK”或“ERROR”
            try
            {
                string temp = string.Empty;
                while (temp.Trim() != "OK" && temp.Trim() != "ERROR")
                {
                    temp = _com.ReadLine();
                    result += temp;
                }
                return result;
            }
            catch (Exception ex)
            {
                Logger.GetInstance().LogError(ex.ToString());
            }
            finally
            {
                //事件重新绑定 正常监视串口数据
                _com.DataReceived += sp_DataReceived;
            }
            return "";
        }

        #endregion 发送AT指令

        #region 发送短信

        /// <summary>
        /// 发送短信
        /// 发送失败将引发异常
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="msg">短信内容</param>
        public void SendMsg(string phone, string msg)
        {

            PDUEncoding pe = new PDUEncoding();
            pe.ServiceCenterAddress = msgCenter;                    //短信中心号码 服务中心地址

            string tmp = string.Empty;
            foreach (CodedMessage cm in pe.PDUEncoder(phone, msg))
            {
                try
                {
                    //注销事件关联，为发送做准备
                    _com.DataReceived -= sp_DataReceived;

                    _com.Write("AT+CMGS=" + cm.Length.ToString() + "\r");
                    _com.ReadTo(">");
                    _com.DiscardInBuffer();

                    //事件重新绑定 正常监视串口数据
                    _com.DataReceived += sp_DataReceived;

                    tmp = SendAT(cm.PduCode + (char)(26));  //26 Ctrl+Z ascii码
                 
                }
                catch (Exception ex )
                {
                   Logger.GetInstance().LogError ("短信发送失败"+ex.ToString());
                }
                if (tmp.Contains("OK"))
                {
                    continue;
                }

               /// Console.WriteLine("短信发送失败");
            }
        }

        #endregion 发送短信

        #region 读取短信

        /// <summary>
        /// 获取未读信息列表
        /// </summary>
        /// <returns>未读信息列表（中心号码，手机号码，发送时间，短信内容）</returns>
        public List<DecodedMessage> GetUnreadMsg()
        {
            List<DecodedMessage> result = new List<DecodedMessage>();
            string[] temp = null;
            string tmp = string.Empty;

            tmp = SendAT("AT+CMGL=0");
            if (tmp.Contains("OK"))
            {
                temp = tmp.Split('\r');
            }

            PDUEncoding pe = new PDUEncoding();
            foreach (string str in temp)
            {
                if (str != null && str.Length > 18)   //短信PDU长度仅仅短信中心就18个字符
                {
                    result.Add(pe.PDUDecoder(str));
                }
            }

            return result;
        }

        /// <summary>
        /// 读取新消息
        /// </summary>
        /// <returns>新消息解码后内容</returns>
        /// <remarks>建议在收到短信事件中调用</remarks>
        public DecodedMessage ReadNewMsg()
        {
            return ReadMsgByIndex(newMsgIndexQueue.Dequeue());
        }

        /// <summary>
        /// 按序号读取短信
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>信息字符串 (中心号码，手机号码，发送时间，短信内容)</returns>
        public DecodedMessage ReadMsgByIndex(int index)
        {
            string temp = string.Empty;
            //string msgCenter, phone, msg, time;
            PDUEncoding pe = new PDUEncoding();
            try
            {
                temp = SendAT("AT+CMGR=" + index.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (temp.Trim() == "ERROR")
            {
                throw new Exception("没有此短信");
            }
            temp = temp.Split((char)(13))[2];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串

            //pe.PDUDecoder(temp, out msgCenter, out phone, out msg, out time);

            if (AutoDelMsg)
            {
                try
                {
                    DeleteMsgByIndex(index);
                }
                catch
                {

                }
            }

            return pe.PDUDecoder(temp);
            //return msgCenter + "," + phone + "," + time + "," + msg;
        }

        #endregion 读取短信

        #region 删除短信

        /// <summary>
        /// 按索引号删除短信
        /// </summary>
        /// <param name="index">The index.</param>
        public void DeleteMsgByIndex(int index)
        {
            if (SendAT("AT+CMGD=" + index.ToString()).Trim() == "OK")
            {
                return;
            }

            throw new Exception("删除失败");
        }

        #endregion 删除短信

        #endregion
    }

}
