using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 总线定义描述类
    /// </summary>
    public class GHBusDescription : IGHBusDescription
    {
        /// <summary>
        /// 总线唯一Key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 总线名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 总线描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 接口Bus处理类
        /// </summary>
        public string Type { get; set; }
    }
}
