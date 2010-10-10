using System.Collections.Generic;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Web.Services
{
    public interface IYBSquareService
    {
        PlacesResponse GetPlaces(string category, decimal latitude, decimal longitude);
        PlacesResponse GetPlaces(string what, string where);
    }
}