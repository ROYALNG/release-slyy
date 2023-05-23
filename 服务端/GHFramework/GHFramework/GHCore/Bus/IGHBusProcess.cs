using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 总线操作
    /// </summary>
    public interface IGHBusProcess
    {
        /// <summary>
        /// 总线定义描述
        /// </summary>
        IGHBusDescription BusDesc { get; }

        /// <summary>
        /// 过滤
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        RuleState RuleFilter();
        /// <summary>
        /// 入口队列
        /// </summary>
        IEnumerable<string> InStack { get; }
        /// <summary>
        /// 出口队列
        /// </summary>
        IEnumerable<string> OutStack { get; }
        /// <summary>
        /// 推入入口队列数据
        /// </summary>
        /// <param name="content">数据</param>
        bool PushInStack(string data);
        /// <summary>
        /// 获取过滤规则
        /// </summary>
        IEnumerable<IRuleFilter> GetRuleFilter();
    }
}
