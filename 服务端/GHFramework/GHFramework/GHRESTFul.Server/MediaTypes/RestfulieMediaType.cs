using GHRESTFul.Server.Marshalling;
using GHRESTFul.Server.Marshalling.UrlGenerators;
using GHRESTFul.Server.Unmarshalling;

namespace GHRESTFul.Server.MediaTypes
{
    public abstract class RestfulieMediaType : MediaType
    {
        public override IResourceMarshaller BuildMarshaller()
        {
            return
                new RestfulieMarshaller(new RelationsFactory(new AspNetMvcUrlGenerator()),
                                        Driver.Serializer,
                                        Driver.HypermediaInjector);
        }

        public override IResourceUnmarshaller BuildUnmarshaller()
        {
            return new RestfulieUnmarshaller(Driver.Deserializer);
        }
    }
}