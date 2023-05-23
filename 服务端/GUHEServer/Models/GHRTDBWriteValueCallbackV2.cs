using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHIBMS.Server.Models
{
    public class GHRTDBWriteValueCallbackV2
    {
 
            public string clientId { get; set; }
            public string batchDefinitionId { get; set; }
            public string id { get; set; }
            public string timestamp { get; set; }
            public int valueType { get; set; }
            public int itemStatus { get; set; }
            public string value { get; set; }
            public string desc { get; set; }
            public bool success { get; set; }
  
    }
}
