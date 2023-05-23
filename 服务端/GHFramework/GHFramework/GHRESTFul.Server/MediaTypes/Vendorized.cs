using GHRESTFul.Server.Marshalling.Serializers.XmlAndHypermedia;
using GHRESTFul.Server.Unmarshalling.Deserializers.Xml;

namespace GHRESTFul.Server.MediaTypes
{
    public class Vendorized : RestfulieMediaType
    {
        private readonly string[] synonyms;

        public Vendorized(string format)
        {
            synonyms = new[] {format};
            Driver = new Driver(new XmlSerializer(), new XmlHypermediaInjector(), new XmlDeserializer());
        }

        public override string[] Synonyms
        {
            get { return synonyms; }
        }
    }
}