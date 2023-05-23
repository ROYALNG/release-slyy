using System;
using GHRESTFul.Server.Marshalling;
using GHRESTFul.Server.Unmarshalling;

namespace GHRESTFul.Server.MediaTypes
{
    public class UrlEncoded : MediaType
    {
        public override string[] Synonyms
        {
            get { return new[] {"application/x-www-form-urlencoded"}; }
        }

        public override IResourceMarshaller BuildMarshaller()
        {
            throw new Exception("Marshaller for UrlEncoded not available");
        }

        public override IResourceUnmarshaller BuildUnmarshaller()
        {
            return new NoUnmarshaller();
        }
    }
}