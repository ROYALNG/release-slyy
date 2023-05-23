using GHRestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHNETBASE.RTDB
{
    public class GHRTDBObjectRequest : IRequest
    {
        public object input { get; set; }
    }
}
