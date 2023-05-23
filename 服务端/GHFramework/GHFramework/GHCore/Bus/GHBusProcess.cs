using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHCore.Bus
{
    /// <summary>
    /// 总线操作
    /// </summary>
    public abstract class GHBusProcess : IGHBusProcess
    {
        public GHBusProcess(IGHBusDescription busDesc, IEnumerable<string> inStack, IEnumerable<string> outStack)
        {
            this._busDesc = busDesc;
            this._inStack = inStack;
            this._outStack = outStack;
        }

        /// <summary>
        /// 总线定义描述
        /// </summary>
        public IGHBusDescription BusDesc
        {
            get { return _busDesc; }
        }
        IGHBusDescription _busDesc = null;
        /// <summary>
        /// 执行过滤
        /// </summary>
        /// <param name="filters">过滤条件</param>
        /// <returns>过滤状态</returns>
        public virtual RuleState RuleFilter()
        {
            var filters = this.GetRuleFilter();
            RuleState state = RuleState.None;
            foreach (var rf in filters)
            {
                state = state | rf.Filter(); 
            }

            return state;
        }
        /// <summary>
        /// 获取过滤规则
        /// </summary>
        public abstract IEnumerable<IRuleFilter> GetRuleFilter();
        /// <summary>
        /// 入口队列
        /// </summary>
        public IEnumerable<string> InStack
        {
            get { return _inStack; }
        }
        IEnumerable<string> _inStack = new List<string>();
        /// <summary>
        /// 出口队列
        /// </summary>
        public IEnumerable<string> OutStack
        {
            get { return _outStack; }
        }
        IEnumerable<string> _outStack = new List<string>();
        /// <summary>
        /// 推数据进Bus入口队列
        /// </summary>
        /// <param name="data">数据</param>
        /// <returns>bool</returns>
        public abstract bool PushInStack(string data);
    }
}
