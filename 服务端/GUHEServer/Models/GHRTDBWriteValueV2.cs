﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHIBMS.Server.Models
{
    public  class GHRTDBWriteValueV2
    {
        public string clientId { get; set; }
        public string batchDefinitionId { get; set; }
        public int level { get; set; }
        public string userName { get; set; }
        public string id { get; set; }
        public string iosvrKey { get; set; }
        public string chlKey { get; set; }
        public string ctrlKey { get; set; }
        public string varKey { get; set; }
        public string value { get; set; }
        public string propName { get; set; }
        public string desc { get; set; }
    }
}
