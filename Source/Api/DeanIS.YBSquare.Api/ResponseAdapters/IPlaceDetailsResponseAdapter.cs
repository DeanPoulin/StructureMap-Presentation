using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public interface IPlaceDetailsResponseAdapter
    {
        PlaceDetailsResponse Create(BusinessDetailResults25 business);
    }
}