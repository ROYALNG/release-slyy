using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 规则过滤状态
    /// </summary>
    public enum RuleState
    {
        /// <summary>
        /// 默认
        /// </summary>
        None = 0,//0
        /// <summary>
        /// 通过
        /// </summary>
        Passed = 1,//1
        /// <summary>
        /// 没通过
        /// </summary>
        NoPass = (1 << 1),//2
        /// <summary>
        /// 取消
        /// </summary>
        Cancel = (1 << 2),//4
        /// <summary>
        /// 异常
        /// </summary>
        Exception = (1 << 3) //8

    }
}
