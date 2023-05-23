namespace GHIBMS.Server
{
    public class GHBaseResponse
    {
        public string Code { set; get; } //错误码 
        public string Message { set; get; } //错误消息
        public string RequestID { set; get; } //请求id
        public string HostId { set; get; }
        public string ClientId { set; get; }
        public string ObjectID { set; get; } //用于指定数据的格式
    }
    public class GHMNSResponse : GHBaseResponse
    {
        public bool success { get; set; }
        //public string message { get; set; }
        public object data { get; set; }
    }

    public class LiveDataResponseItem
    {
        public string ID { get; set; }
        public int ItemStatus { get; set; }
        public string Value { get; set; }
        public int ValueType { get; set; }
        public string Timestamp { get; set; }

        public int Level { get; set; }
        public string Area { get; set; }
    }


}