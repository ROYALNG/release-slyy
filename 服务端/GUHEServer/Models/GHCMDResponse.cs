using System.Collections.Generic;

namespace GHIBMS.Server.Models
{

    public class GHCMDResponse
    {
        public string clientId { get; set; }
        public string batchDefinitionId { get; set; }
        public int area { get; set; }
        public int level { get; set; }
        public List<GHCMDRequestItem> requestItems { get; set; } = new List<GHCMDRequestItem>();

    }
    public class GHCMDRequestItem
    {

        public string iosvrKey { get; set; }
        public string chlKey { get; set; }
        public string ctrlKey { get; set; }
        public string varKey { get; set; }
        public int cmdCode { get; set; }
        public string[] cmdParams { get; set; }

    }
}
