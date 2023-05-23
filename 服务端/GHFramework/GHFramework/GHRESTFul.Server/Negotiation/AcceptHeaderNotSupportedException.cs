using System;

namespace GHRESTFul.Server.Negotiation
{
    public class AcceptHeaderNotSupportedException : Exception
    {
        public AcceptHeaderNotSupportedException() : base("Media type not supported.") {}
    }
}