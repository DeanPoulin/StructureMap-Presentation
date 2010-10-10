using DeanIS.YBSquare.Api.ResponseAdapters;
using StructureMap.Configuration.DSL;

namespace DeanIS.YBSquare.Api.IoC.Registries
{
    public class ResponseAdaptersRegistry : Registry
    {
        public ResponseAdaptersRegistry()
        {
            For<IPlacesResponseAdapter>().Use<PlacesResponseAdapter>();
            For<IPlaceDetailsResponseAdapter>().Use<PlaceDetailsResponseAdapter>();
            For<IPlaceAdapter>().Use<PlaceAdapter>();
        }
    }
}