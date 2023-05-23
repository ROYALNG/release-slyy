using System;
using System.Web.Mvc;

namespace GHRESTFul.Server.Unmarshalling.Resolver
{
    public interface IUnmarshallerResolver
    {
        bool HasResource { get; }
        Type ParameterType { get; }
        string ParameterName { get; }
        void DetectIn(ActionExecutingContext context);
    }

    public interface IUnmarshallerResolverApi
    {
        bool HasResource { get; }
        Type ParameterType { get; }
        string ParameterName { get; }
        void DetectIn(System.Web.Http.Controllers.HttpActionContext context);
    }
}