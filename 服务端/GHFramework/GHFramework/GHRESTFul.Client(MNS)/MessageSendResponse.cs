﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHRESTFul.Client
{
    public class MessageSendResponse : MQSResponse
    {
        public string MessageId { set; get; }

        public string MessageBodyMD5 { set; get; }
    }
}
