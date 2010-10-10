using System.Linq;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.Net.Utilities;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public class PlacesResponseAdapter : IPlacesResponseAdapter
    {
        private readonly IPlaceAdapter _placeAdapter;

        public PlacesResponseAdapter(IPlaceAdapter placeAdapter)
        {
            _placeAdapter = placeAdapter;
        }

        public PlacesResponse Create(BusinessSearchResults25 searchResults)
        {
            var model = new PlacesResponse();

            if (searchResults == null)
                return null;

            if (searchResults.InAreaListings.Listing.IsNullOrEmpty())
                return null;

            AddPlaces(searchResults, model);

            return model;
        }

        private void AddPlaces(BusinessSearchResults25 searchResults, PlacesResponse placesModel)
        {
            placesModel.Places = searchResults.InAreaListings.Listing.Select(CreatePlace).ToList();
        }

        private Place CreatePlace(Listing25 listing)
        {
            return _placeAdapter.Create(listing);
        }
    }
}