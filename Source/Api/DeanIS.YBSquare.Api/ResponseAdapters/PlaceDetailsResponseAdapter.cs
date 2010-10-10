using System.Linq;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public class PlaceDetailsResponseAdapter : IPlaceDetailsResponseAdapter
    {
        private readonly IPlaceAdapter _placeAdapter;

        public PlaceDetailsResponseAdapter(IPlaceAdapter placeAdapter)
        {
            _placeAdapter = placeAdapter;
        }

        public PlaceDetailsResponse Create(BusinessDetailResults25 business)
        {
            if (business == null)
                return null;

            var listing = business.Listings.FirstOrDefault();

            if (listing == null)
                return null;

            return new PlaceDetailsResponse
                       {
                           Place = _placeAdapter.Create(listing)
                       };
        }
    }
}