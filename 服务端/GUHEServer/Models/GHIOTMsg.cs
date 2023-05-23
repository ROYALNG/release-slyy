using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHIBMS.Server.Models
{
    public class  GHIOTMsg
    {
        public string code { set; get; } = "";
        public long msgId { set; get; }
        public long   msgTime { set; get; }
        public string key { set; get; } = "";
        public object data { set; get; }
    }
}
