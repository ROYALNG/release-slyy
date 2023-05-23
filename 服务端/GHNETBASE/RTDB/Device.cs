using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GHNETBASE.RTDB.Models
{
    public class DevChannel
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
    public class DevController
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ChlID { get; set; }
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
        public ValueType ValueType { get; set; }
        /// <summary>
        /// 数据变更时间戳
        /// </summary>
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public int Status { get; set; }
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
    }

    public enum ValueType
    {
        Int = 0,//IntDataItem - JavaScript integer 
        Float,//FloatDataItem - JavaScript float 
        Bool,//BoolDataItem - JavaScript bool 
        String,//StringDataItem - JavaScript string 
        Enum//StringDataItem - JavaScript string 
    }
}