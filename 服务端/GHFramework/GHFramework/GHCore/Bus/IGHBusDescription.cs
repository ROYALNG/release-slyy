using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 总线定义描述类
    /// </summary>
    public interface IGHBusDescription
    {
        /// <summary>
        /// 总线唯一Key
        /// </summary>
        string Key { get; set; }
        /// <summary>
        /// 总线名称
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 总线描述
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// 反射执行类
        /// </summary>
        string Type { get; set; }

    }
}
