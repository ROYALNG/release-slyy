using System;
using System.Collections.Generic;
using System.Linq;


namespace GH.RTDB.Server.Models
{
    public abstract class GHRTDBRequest
    {
        public int ObjectID { get; set; }
        public string Code { set; get; } //错误码 
        public string Message { set; get; } //错误消息
        public string RequestID { set; get; } //请求id
        public string HostId { set; get; }
        public string Level { get; set; }
        public string Area { get; set; }

    }
    public class GHRTDBVarPropEditRequest : GHRTDBRequest
    {
        public GHRTDBVarPropEditRequest()
        {
            this.ObjectID = 1;

        }

        public List<GHRTDBVarPropEditRequestItem> RequestItems { get; set; } = new List<GHRTDBVarPropEditRequestItem>();
    }
    public class GHRTDBVarPropEditRequestItem 
    {
        public string IOSvrKey { get; set; }
        public string ChlKey { get; set; }
        public string CtrlKey { get; set; }
        public string VarKey { get; set; }
        public string VarProp { get; set; }
        public string PropValue { get; set; }

    }

    public class GHRTDBWriteValueRequest 
    {
        public string clientId { get; set; }
        public string batchDefinitionId { get; set; }
        public int area { get; set; }
        public int level { get; set; }
        public List<GHRTDBWriteValueRequestItem> RequestItems { get; set; } = new List<GHRTDBWriteValueRequestItem>();
        public GHRTDBWriteValueRequest()
        {
        }
    }

    public class GHRTDBWriteValueRequestItem
    {
        public string id { get; set; }
        public string iosvrKey { get; set; }
        public string chlKey { get; set; }
        public string ctrlKey { get; set; }
        public string varKey { get; set; }
        public string value { get; set; }
        public string propName { get; set; }
        public string desc { get; set; }
    
    }
    public class GHRTDBWriteValueCallback
    {
        public bool success { get; set; }
        public string clientId { get; set; }
        public string batchDefinitionId { get; set; }
        public string message { get; set; }
        public int code { get; set; }
        public List<GHRTDBWriteValueCallbackData> data { get; set; } = new List<GHRTDBWriteValueCallbackData>();

    }
    public class GHRTDBWriteValueCallbackData
    {
          public string id { get; set; }
          public string timestamp { get; set; }
          public int valueType { get; set; }
          public int itemStatus { get; set; }
          public string value { get; set; }
          public string desc { get; set; }
          public bool success { get; set; }
    }

}