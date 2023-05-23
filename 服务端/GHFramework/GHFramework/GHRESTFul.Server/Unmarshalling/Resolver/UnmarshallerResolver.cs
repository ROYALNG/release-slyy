using System;
using System.Linq;
using System.Web.Mvc;

namespace GHRESTFul.Server.Unmarshalling.Resolver
{
    public class UnmarshallerResolver : IUnmarshallerResolver
    {
        private readonly IAcceptHttpVerb httpVerbs;

        public UnmarshallerResolver(IAcceptHttpVerb httpVerbs)
        {
            this.httpVerbs = httpVerbs;
        }

        #region IUnmarshallerResolver Members

        public bool HasResource
        {
            get { return !string.IsNullOrEmpty(ParameterName); }
        }

        public Type ParameterType { get; private set; }
        public string ParameterName { get; private set; }

        public void DetectIn(ActionExecutingContext context)
        {
            if (httpVerbs.IsValid(context) && ActionHasParameters(context))
            {
                var firstParameter = context.ActionDescriptor.GetParameters().First();

                ParameterType = firstParameter.ParameterType;
                ParameterName = firstParameter.ParameterName;
            }
            else
            {
                ParameterType = null;
                ParameterName = "";
            }
        }

        #endregion

        private bool ActionHasParameters(ActionExecutingContext context)
        {
            return context.ActionDescriptor.GetParameters().Length > 0;
        }
    }

    public class UnmarshallerResolverApi : IUnmarshallerResolverApi
    {
        private readonly IAcceptHttpVerbApi httpVerbs;

        public UnmarshallerResolverApi(IAcceptHttpVerbApi httpVerbs)
        {
            this.httpVerbs = httpVerbs;
        }

        #region IUnmarshallerResolver Members

        public bool HasResource
        {
            get { return !string.IsNullOrEmpty(ParameterName); }
        }

        public Type ParameterType { get; private set; }
        public string ParameterName { get; private set; }

        public void DetectIn(System.Web.Http.Controllers.HttpActionContext context)
        {
            if (httpVerbs.IsValid(context) && ActionHasParameters(context))
            {
                //var firstParameter = context.ActionDescriptor.GetParameters().First();
                foreach (var firstParameter in context.ActionDescriptor.GetParameters())
                {
                    if (firstParameter.ParameterName == "value")
                    {
                        ParameterType = firstParameter.ParameterType;
                        ParameterName = firstParameter.ParameterName;

                        return;
                    }
                }
            }
            else
            {
                ParameterType = null;
                ParameterName = "";
            }
        }

        #endregion

        private bool ActionHasParameters(System.Web.Http.Controllers.HttpActionContext context)
        {
            return  context.ActionDescriptor.GetParameters().Count(t => t.ParameterName == "value") > 0;
            //return context.ActionDescriptor.GetParameters().Count > 0;
        }
    }
}