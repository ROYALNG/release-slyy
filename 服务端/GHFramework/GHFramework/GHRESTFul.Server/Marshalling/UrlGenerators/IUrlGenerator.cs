using System.Collections.Generic;

namespace GHRESTFul.Server.Marshalling.UrlGenerators
{
    public interface IUrlGenerator
    {
        string For(string controller, string action, IDictionary<string, object> values);
    }
}