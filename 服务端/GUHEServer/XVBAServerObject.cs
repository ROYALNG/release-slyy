
/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using System;
using System.Diagnostics;
在此之后:
using GHIBMS.Common;
using GHIBMS.Interface;
*/
using GHIBMS.Common;
using GHIBMS.Interface;

/* 项目“WindowsServiceIO”的未合并的更改
在此之前:
using GHIBMS.Common;
在此之后:
using System.Diagnostics;
*/
namespace GHIBMS.Server
{
    public class XVBAServerObject
    {

        public XVBAServerObject()
        {

        }

        #region GH-IBMS
        /// <summary>
        /// 读取变量数值
        /// </summary>
        /// <param name="varID">变量ID</param>
        /// <returns>变量值</returns>
        public string GetValue(string chlid, string conid, string varID)
        {
            IVariable var = Rtdb.GetVariableByID(chlid, conid, varID);
            if (var != null)
                return var.Value.ToString();
            else
                return "[null]";
        }
        /// <summary>
        /// 设置变量数值
        /// </summary>
        /// <param name="varName">变量ID</param>
        /// <param name="val">变量数值</param>
        /// <returns></returns>
        public bool SetValue(string chlid, string conid, string varID, object val)
        {
            if (string.IsNullOrEmpty(chlid) || string.IsNullOrEmpty(conid) || string.IsNullOrEmpty(varID))
                return false;
            IVariable var = Rtdb.GetVariableByID(chlid, conid, varID);
            if (var != null)
                return var.WriteValue(val);
            else
                return false;

        }

        #endregion
    }
}

