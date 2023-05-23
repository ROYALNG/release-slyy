using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHNETBASE.RTDB.Models
{
    public class DevIOServer
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string  LastTime { get; set; }
        public string IPAddress { get; set; }
        public string Host { get; set; }

        public bool State { get; set; }
    }

    public class DevChannel
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string IOServerID { get; set; }
        public bool State { get; set; }
        public string ProtocolName { get; set; }
        public string  DateStamp { get; set; }
        public int Interval { get; set; }
        public bool Enable { get; set; }
    }

    public class DevController
    {
        public string ID { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string IOServerID { get; set; }
        public string ChlID { get; set; }
        public bool State { get; set; }
        public bool Enable { get; set; }
        public string  DateStamp { get; set; }
    }

    public class DevVariable
    {
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 变量值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public GHIBMS.Common.ValueType ValueType { get; set; }
        /// <summary>
        /// 数据变更时间戳
        /// </summary>
        public string  Timestamp { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public bool Status { get; set; }
        public string IOServerID { get; set; }
        public string CtrlID { get; set; }
        public string ChlID { get; set; }
        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 设备级别
        /// </summary>
        public int Level { get; set; }
        public bool Enable { get; set; }
        public string Address { get; set; }
    }
    public class DevVariablePorperty:DevVariable
    {
       public string Propery { get; set; }
    }


}