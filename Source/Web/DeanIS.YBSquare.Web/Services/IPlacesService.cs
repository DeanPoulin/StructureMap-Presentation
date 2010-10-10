using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Web.Services
{
    public interface IPlacesService
    {
        PlacesResponse GetPlaces(string category, decimal latitude, decimal longitude);
        PlacesResponse GetPlaces(string what, string where);
        CheckInResponse CheckIn(long placeId, string name);
    }
}