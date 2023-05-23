using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHRESTFul.Client
{
    public class MessageVisibilityChangeResponse : MQSResponse
    {
        public string ReceiptHandle { set; get; }
        public long NextVisibleTime { set; get; }
    }

    public class MessageVisibilityChangeRequest : MQSRequest
    {
        public string ReceiptHandle { set; get; }
        public long VisibilityTimeOut { set; get; }
    }
}
