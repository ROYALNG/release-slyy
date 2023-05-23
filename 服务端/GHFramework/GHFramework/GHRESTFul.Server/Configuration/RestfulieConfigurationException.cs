using System;

namespace GHRESTFul.Server.Configuration
{
    public class RestfulieConfigurationException : Exception
    {
        public RestfulieConfigurationException(string message) : base(message) {}
    }
}