using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GHIBMS.Server.Pub
{
 
    public class AlarmMsg
    {
        public string AlarmValue { get; set; }
        public string AlarmIOServer { get; set; }
        public string AlarmChannel { get; set; }
        public string AlarmController { get; set; }
        public string AlarmVariableID { get; set; }
        public string AlarmVariableDesc { get; set; }
    }

}
