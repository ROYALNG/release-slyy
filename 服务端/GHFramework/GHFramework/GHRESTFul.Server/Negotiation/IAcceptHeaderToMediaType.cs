using GHRESTFul.Server.MediaTypes;

namespace GHRESTFul.Server.Negotiation
{
    public interface IAcceptHeaderToMediaType
    {
        IMediaType GetMediaType(string acceptHeader);
    }
}