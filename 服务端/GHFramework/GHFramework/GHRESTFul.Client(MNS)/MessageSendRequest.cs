using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using RestSharp.Serializers;
namespace GHRESTFul.Client
{
    //[SerializeAs(Name="GHMessage")]
    public class MessageSendRequest : MQSRequest
    {
        public string MessageBody { set; get; }
        public int DelaySeconds { set; get; }
        public int Priority { set; get; }
    }
}
