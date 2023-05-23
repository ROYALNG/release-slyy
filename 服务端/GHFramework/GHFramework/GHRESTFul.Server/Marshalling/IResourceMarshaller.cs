using GHRESTFul.Server.Http;

namespace GHRESTFul.Server.Marshalling
{
    public interface IResourceMarshaller
    {
        string Build(object model, IRequestInfoFinder finder);
    }
}