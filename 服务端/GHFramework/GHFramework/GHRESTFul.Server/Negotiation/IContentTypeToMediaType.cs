using GHRESTFul.Server.MediaTypes;

namespace GHRESTFul.Server.Negotiation
{
    public interface IContentTypeToMediaType
    {
        IMediaType GetMediaType(string acceptHeader);
    }
}