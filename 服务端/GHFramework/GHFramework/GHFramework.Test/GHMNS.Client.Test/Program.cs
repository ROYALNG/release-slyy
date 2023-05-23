using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GHRESTFul.Client;

namespace GHMQS.Client.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:17639"; //Create Queue
            MQSClient client = new MQSClient(url, "280C3D25EBD94DC198E2A7FCFB326793", "");
            var mqsqueue = client.getQueue("/api/testQueue003");

            //dynamic ret = mqsqueue.setAttribute();

            //ret = mqsqueue.getAttribute();

            //ret = mqsqueue.sendMessage("hello world", 0, 8);

            //var ret = mqsqueue.popMessage();

            //mqsqueue.popMessageAsync(new Action<MessageReceiveResponse>(res =>
            //{
            //    var r = res;
            //}));

            var ret = mqsqueue.peekMessage();

            //ret = mqsqueue.changeVisibility("receipt", 0);

            //ret = mqsqueue.deleteMessage("receipt");
        }
    }
}
