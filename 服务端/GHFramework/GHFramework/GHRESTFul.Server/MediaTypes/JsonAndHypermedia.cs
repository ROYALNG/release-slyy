using GHRESTFul.Server.Marshalling.Serializers.Json;
using GHRESTFul.Server.Unmarshalling.Deserializers.Json;

namespace GHRESTFul.Server.MediaTypes
{
    public class JsonAndHypermedia : RestfulieMediaType
    {
        public JsonAndHypermedia()
        {
            Driver = new Driver(new JsonSerializer(), new JsonHypermediaInjector(), new JsonDeserializer());
        }

        public override string[] Synonyms
        {
            get { return new[] {"application/json"}; }
        }
    }
}