using System.Web.Mvc;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.Web.Mvc;
using DeanIS.YBSquare.Api.ResponseAdapters;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IYellowbookService _yellowbookService;
        private readonly IPlacesResponseAdapter _placesResponseAdapter;
        private readonly IPlaceDetailsResponseAdapter _placeDetailsResponseAdapter;

        public PlacesController(IYellowbookService yellowbookService, IPlacesResponseAdapter placesResponseAdapter, IPlaceDetailsResponseAdapter placeDetailsResponseAdapter)
        {
            _yellowbookService = yellowbookService;
            _placesResponseAdapter = placesResponseAdapter;
            _placeDetailsResponseAdapter = placeDetailsResponseAdapter;
        }

        public ActionResult Index(string what, string where = null, string latitude = null, string longitude = null, int pagesize = 15, int pagenumber = 1, int radius = 1)
        {
            var searchResults = !string.IsNullOrEmpty(where)
                                    ? _yellowbookService.GetListings(what, where)
                                    : _yellowbookService.GetListingsAroundMe(what, latitude, longitude, radius, pagenumber, pagesize);

            if (searchResults == null)
            {
                return new ObjectResult<PlacesResponse>(new PlacesResponse() {Status = Status.UnhandledException});
            }

            if (searchResults.Status != StatusInfo25.Ok)
            {
                return new ObjectResult<PlacesResponse>(new PlacesResponse() {Status = Status.Error});
            }

            var placesResponse = _placesResponseAdapter.Create(searchResults);
            return new ObjectResult<PlacesResponse>(placesResponse);
        }

        public ActionResult Details(long id)
        {
            var listing = _yellowbookService.GetListingDetails(id);

            var placeDetailsResponse = _placeDetailsResponseAdapter.Create(listing);

            return new ObjectResult<PlaceDetailsResponse>(placeDetailsResponse);
        }
    }
}
