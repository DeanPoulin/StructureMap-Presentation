using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public interface IPlacesResponseAdapter
    {
        PlacesResponse Create(BusinessSearchResults25 searchResults);
    }
}