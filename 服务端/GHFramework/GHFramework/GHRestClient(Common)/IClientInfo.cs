using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHRestClient
{
    public interface IClientInfo
    {
        void SetClient(GHRestClient client);
        void SetName(string name);
    }
}
