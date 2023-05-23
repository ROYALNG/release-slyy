using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using GHIBMS.Common;


namespace GHIBMS.Server
{
    /// <summary>
    /// 海恩门禁串口协议
    /// </summary>
    public class SerialPortHaien
    {
        private int port = 1;
        private int baud = 9600;
        private Parity parity = Parity.None;
        private bool hasConn = false;
        private const char HEAD = '@';
        private const int FrameLength = 17;
        private BaseChannel currentChan=null;
        //Thread thread = null;
 
        private bool active = false;
        private static StringBuilder stbRex = new StringBuilder();
        private static SerialPort serialPort =new SerialPort();
        
        public delegate void activeDelegate(BaseChannel chan,bool actived);
        public delegate void receiveDelegeate(BaseChannel currentChan,int ControllAddr,int DoorAddr,AccessAlarmTypeEnum AlarmType,string OtherInfo);

        public event activeDelegate OnActive;
        public event receiveDelegeate OnReceive;
        public delegate void onErrorDelegate(Exception e);
        public event onErrorDelegate onError;

        //数据同步
        private object synIn = new object();
        private object synHeart = new object();
        //private static int SLEEP_TIME = 300;
        /// <summary>
        /// 端口
        /// </summary>
        public int Port
        {
            get { return port; }
            set
            {
                if (!active)
                    port = value;
            }
        }
        public int Baud
        {
            get { return baud; }
            set
            {
                if (!active)
                    baud = value;
            }
        }
        public bool HasConn
        {
            get { return hasConn; }
            set {
                lock (synHeart)
               {
                hasConn = value;
               }
            }

        }
      
        public SerialPortHaien(BaseChannel chan)
        {
            try
            {
                port = chan.Port;
                baud = chan.Baud;
                parity = chan.Parity;
                currentChan = chan;
                serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

            }
            catch (Exception e)
            {
                if (onError != null)
                    onError(e);
            }
        }
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
          
                SerialPort sp = (SerialPort)sender;
                int count = sp.BytesToRead;
                byte[] buffer = new byte[count];
                sp.Read(buffer, 0, count);
                              
                for (int i = 0; i < count; i++)
                {
                    stbRex.Append((char)(buffer[i]));
                }

                //解码处理
                string _strRex = stbRex.ToString();

                if ((_strRex.Length >= FrameLength) && (_strRex.IndexOf(HEAD) >= 0))
                {
                    while (_strRex.IndexOf(HEAD) != 0)
                        _strRex = _strRex.Substring(_strRex.IndexOf(HEAD));
                    String[] lines = _strRex.Split(new Char[] { HEAD }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (String line in lines)
                        if (line.Length ==FrameLength-1)
                        {

                            int controllAddr = Convert.ToInt32(line.Substring(0, 4), 16);
                            AccessAlarmTypeEnum alarmType = new AccessAlarmTypeEnum();
                            string code = "";
                            int doorAddr = 0;
                            char charType = line.Substring(4, 1)[0];
                            switch (charType)
                            {
                                case 'F':
                                    alarmType = AccessAlarmTypeEnum.火警;
                                    break;
                                case 'M':
                                    alarmType = AccessAlarmTypeEnum.门磁超时;
                                    doorAddr = Convert.ToInt32(line.Substring(5, 2), 16);
                                    break;
                                case 'C':
                                    alarmType = AccessAlarmTypeEnum.强迫开门;
                                    doorAddr = Convert.ToInt32(line.Substring(5, 2), 16);
                                    break;
                                case 'U':
                                    alarmType = AccessAlarmTypeEnum.未知卡;
                                    code = line.Substring(5, 6);
                                    doorAddr = Convert.ToInt32(line.Substring(11, 2), 16);
                                    break;
                                case 'I':
                                    alarmType = AccessAlarmTypeEnum.非法卡;
                                    code = line.Substring(5, 6);
                                    doorAddr = Convert.ToInt32(line.Substring(11, 2), 16);
                                    break;
                                case 'R':
                                    alarmType = AccessAlarmTypeEnum.遥控报警;
                                    break;
                                case 'T':
                                    alarmType = AccessAlarmTypeEnum.无效的时区;
                                    code = line.Substring(5, 6);
                                    doorAddr = Convert.ToInt32(line.Substring(11, 2), 16);
                                    break;
                                case 'D':
                                    alarmType = AccessAlarmTypeEnum.无效的门区;
                                    code = line.Substring(5, 6);
                                    doorAddr = Convert.ToInt32(line.Substring(11, 2), 16);
                                    break;
                                case 'A':
                                    alarmType = AccessAlarmTypeEnum.试图反潜;
                                    code = line.Substring(5, 6);
                                    doorAddr = Convert.ToInt32(line.Substring(11, 2), 16);
                                    break;
                                case 'B':
                                    alarmType = AccessAlarmTypeEnum.防区报警;
                                    doorAddr = Convert.ToInt32(line.Substring(5, 2), 16);
                                    break;
                                case 'E':
                                    alarmType = AccessAlarmTypeEnum.防拆报警;
                                    break;
                                case 'G':
                                    alarmType = AccessAlarmTypeEnum.控制器三次密码错误报警;
                                    break;
                                case 'H':
                                    alarmType = AccessAlarmTypeEnum.读卡器丢失;
                                    doorAddr = Convert.ToInt32(line.Substring(5, 2), 16);
                                    break;
                            }
                            if (OnReceive != null)
                                OnReceive(currentChan,controllAddr, doorAddr, alarmType, code);
                         }
                    stbRex.Remove(0, stbRex.Length);
                    if (lines[lines.Length - 1].Length < FrameLength - 1)
                    {
                        stbRex.Append('@');
                        stbRex.Append(lines[lines.Length-1]);
                    }
                }
        }

        /// <summary>
        /// 启动/关闭服务
        /// </summary>
        public bool Active
        {
            get { return active; }
            set
            {
                if (active && !value)
                {
                     if (serialPort.IsOpen)
                     {
                          serialPort.Close();
                      }
                     //if (thread != null)
                     //{
                     //    active = false; 
                     //}
                   
                     if (OnActive != null)
                         OnActive(currentChan, false);
                }
                else if (!active && value)
                {
                   
                    try
                    {
                        serialPort.PortName ="COM"+port.ToString();
                        serialPort.BaudRate= baud;
                        serialPort.Parity = parity;
                        serialPort.ReadTimeout = 200;//获取或设置读取操作未完成时发生超时之前的毫秒数。
                        serialPort.WriteTimeout = 200;//获取或设置写入操作未完成时发生超时之前的毫秒数           

                        serialPort.Open();
                        //thread = new Thread(new ThreadStart(ComRecive));
                        //thread.Name = "SerialPortHaienRecive";
                        ////thread.IsBackground = true;
                        //thread.Start();
                        active = true;
                        if (OnActive != null)
                            OnActive(currentChan, true);
                    }
                    catch
                    {
                       // serialPort.Close();
                        if (OnActive != null)
                            OnActive(currentChan, false);
                        active = false;
                        
                       // onError(ex);
                    }
                }
            }
        }
       
        /// <summary>
        /// 关闭指定的连接
        /// </summary>
        /// <param name="SerialPort"></param>
        //public void CloseSerialPort()
        //{
        //    if (serialPort.IsOpen)
        //    {
        //          serialPort.Close();
        //          if (thread == null)
        //              return;
        //          active = false;
                  
        //    }
        //}
        public void Send(byte[] buf)
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Write(buf, 0, buf.Length);
            }catch(Exception e)
            {
                if (onError != null)
                    onError(e);
            }

        }

        #region 内部线程
        //监听端口
        /*private void ComRecive()
        {
            while (active)
            {
                try
                {
                    lock (synIn)
                    {
                        string _strRex = stbRex.ToString();

                        if ((_strRex.Length >= FRAMELENGTH) && (_strRex.IndexOf(HEAD) >= 0))
                        {
                            while (_strRex.IndexOf(HEAD) != 0)
                                _strRex = _strRex.Substring(_strRex.IndexOf(HEAD));

                                String[] lines = strReciev.Split(new string[] { HEAD }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (String line in lines)
                                    if (line != "")
                                    {
                                  
                                            int controllAddr  =Convert.ToInt32( line.Substring(0, 4),16);
                                            AccessAlarmTypeEnum alarmType=new AccessAlarmTypeEnum();
                                            string code ="";
                                            int doorAddr=0;
                                            char charType=_strRex.Substring(4,1)[0];
                                            switch (charType)
                                            {
                                                case 'F':
                                                    alarmType = AccessAlarmTypeEnum.火警;
                                                    break;
                                                case 'M':
                                                    alarmType = AccessAlarmTypeEnum.门磁超时;
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(5, 2), 16);
                                                    break;
                                                case 'C':
                                                    alarmType = AccessAlarmTypeEnum.强迫开门;
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(5, 2), 16);
                                                    break;
                                                case 'U':
                                                    alarmType = AccessAlarmTypeEnum.未知卡;
                                                    code = _strRex1.Substring(5, 6);
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(11, 2), 16);
                                                    break;
                                                case 'I':
                                                    alarmType = AccessAlarmTypeEnum.非法卡;
                                                    code = _strRex1.Substring(5, 6);
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(11, 2), 16);
                                                    break;
                                                case 'R':
                                                    alarmType = AccessAlarmTypeEnum.遥控报警;
                                                    break;
                                                case 'T':
                                                    alarmType = AccessAlarmTypeEnum.无效的时区;
                                                    code = _strRex1.Substring(5, 6);
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(11, 2), 16);
                                                    break;
                                                case 'D':
                                                    alarmType = AccessAlarmTypeEnum.无效的门区;
                                                    code = _strRex1.Substring(5, 6);
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(11, 2), 16);
                                                    break;
                                                case 'A':
                                                    alarmType = AccessAlarmTypeEnum.试图反潜;
                                                    code = _strRex1.Substring(5, 6);
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(11, 2), 16);
                                                    break;
                                                case 'B':
                                                    alarmType = AccessAlarmTypeEnum.防区报警;
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(5, 2), 16);
                                                    break;
                                                case 'E':
                                                    alarmType = AccessAlarmTypeEnum.防拆报警;
                                                    break;
                                                case 'G':
                                                    alarmType = AccessAlarmTypeEnum.控制器三次密码错误报警;
                                                    break;
                                                case 'H':
                                                    alarmType = AccessAlarmTypeEnum.读卡器丢失;
                                                    doorAddr = Convert.ToInt32(_strRex1.Substring(5, 2), 16);
                                                    break;
                                            }
                                            if (OnReceive!=null)
                                                OnReceive(controllAddr, doorAddr, alarmType, code);
                                    }
                        }
                    } 
                    try
                    {
                        System.Threading.Thread.Sleep(SLEEP_TIME);
                    }
                    catch { }
                }
                catch { break; }
               
            }
            active = false;
        }*/
      
        #endregion


    }
}
