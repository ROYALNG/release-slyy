using System;

namespace GHRESTFul.Server.Unmarshalling
{
    public class UnmarshallingException : Exception
    {
        public UnmarshallingException(string message) : base(message) {}
    }
}