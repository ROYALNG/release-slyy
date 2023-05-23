using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RestSharp.Serializers;
namespace GHRESTFul.Client
{
    //[SerializeAs(Name="GHQueue")]
    public class QueueAttributeSetRequest : MQSRequest
    {
        public int VisibilityTimeout { set; get; }
        public int MaximumMessageSize { set; get; }
        public int MessageRetentionPeriod { set; get; }
        public int DelaySeconds { set; get; }
        public int PollingWaitSeconds { set; get; }
    }
}
