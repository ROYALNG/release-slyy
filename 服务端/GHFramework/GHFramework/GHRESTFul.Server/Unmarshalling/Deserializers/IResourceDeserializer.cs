using System;

namespace GHRESTFul.Server.Unmarshalling.Deserializers
{
    public interface IResourceDeserializer
    {
        object Deserialize(string content, Type objectType);
    }
}