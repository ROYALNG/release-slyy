using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHIBMS.Server.Models
{
    [Serializable]
    public  class History
    {
        public string time { get; set; }
        public string server { get; set; }
        public string channel { get; set; }
        public string controller { get; set; }
        public string varKey { get; set; }
        public string value { get; set; }
    }
}
