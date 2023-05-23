using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHRestClient
{
    public class RequestException : Exception
    {
        public RequestException(string message)
            : base(message)
        {
            
        }
    }
}
