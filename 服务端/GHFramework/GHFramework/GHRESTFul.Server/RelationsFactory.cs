using GHRESTFul.Server.Marshalling.UrlGenerators;

namespace GHRESTFul.Server
{
    public class RelationsFactory : IRelationsFactory
    {
        private readonly IUrlGenerator urlGenerator;

        public RelationsFactory(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
        }

        public Relations NewRelations()
        {
            return new Relations(urlGenerator);
        }
    }
}
