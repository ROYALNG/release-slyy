using System.Collections.Generic;

namespace GHCore.Task
{
    public interface IGHTask
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">执行参数字典</param>
        void Execute(IEnumerable<KeyValuePair<string, string>> content);
    }
}
