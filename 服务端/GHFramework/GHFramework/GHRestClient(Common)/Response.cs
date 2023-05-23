using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHRestClient
{
    public abstract class Response
    {
        public string Code { set; get; } //错误码 
        public string Message { set; get; } //错误消息
        public string RequestId { set; get; } //请求id
        public string HostId { set; get; }
    }
}
