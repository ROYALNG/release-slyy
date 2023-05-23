using System.Collections.Generic;
using GHRESTFul.Server.Http;

namespace GHRESTFul.Server.Marshalling.Serializers
{
    public interface IHypermediaInjector
    {
        string Inject(string content, Relations relations, IRequestInfoFinder requestInfo);
        string Inject(string content, IList<Relations> relations, IRequestInfoFinder requestInfo);
    }
}