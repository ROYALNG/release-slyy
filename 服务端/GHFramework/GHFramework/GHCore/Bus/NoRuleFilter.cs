using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 无过滤(默认)
    /// </summary>
    public class NoRuleFilter : IRuleFilter
    {
        /// <summary>
        /// 执行过滤 返回结果
        /// </summary>
        /// <returns>RuleState</returns>
        public RuleState Filter()
        {
            return RuleState.Passed;
        }
    }
}
