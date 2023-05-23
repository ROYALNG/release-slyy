using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 过滤规则条目
    /// </summary>
    public interface IRuleFilter
    {
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <returns></returns>
        RuleState Filter();

    }
    /// <summary>
    /// 过滤方式 
    /// and 所有条件全部满足
    /// or 满足一个条件
    /// </summary>
    public enum RuleFilterType
    {
        And,
        Or
    }
}
