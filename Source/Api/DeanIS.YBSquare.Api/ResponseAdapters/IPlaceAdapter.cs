using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public interface IPlaceAdapter
    {
        Place Create(Listing25 listing);
    }
}