using System;

namespace GHRESTFul.Server.Unmarshalling
{
    public interface IResourceUnmarshaller
    {
        object Build(string xml, Type objectType);
    }
}