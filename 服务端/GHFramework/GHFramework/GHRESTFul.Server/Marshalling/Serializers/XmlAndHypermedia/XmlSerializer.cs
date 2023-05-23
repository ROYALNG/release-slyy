using GHRESTFul.Server.Extensions;

namespace GHRESTFul.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlSerializer : IResourceSerializer
    {
        #region IResourceSerializer Members

        public string Serialize(object resource)
        {
            return resource.AsXml();
        }

        #endregion
    }
}