using GHRESTFul.Server.Marshalling.Serializers;
using GHRESTFul.Server.Unmarshalling.Deserializers;

namespace GHRESTFul.Server.MediaTypes
{
    public interface IDriver
    {
        IResourceSerializer Serializer { get; set; }
        IHypermediaInjector HypermediaInjector { get; set; }
        IResourceDeserializer Deserializer { get; set; }
    }
}