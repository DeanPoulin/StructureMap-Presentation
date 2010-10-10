using System;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Web.Services
{
    public class PlacesService : IPlacesService
    {
        private readonly IYBSquareService _ybService;
        private readonly IMembershipService _membershipService;

        public PlacesService(IYBSquareService ybService, IMembershipService membershipService)
        {
            _ybService = ybService;
            _membershipService = membershipService;
        }

        public PlacesResponse GetPlaces(string category, decimal latitude, decimal longitude)
        {
            return _ybService.GetPlaces(category, latitude, longitude);
        }

        public PlacesResponse GetPlaces(string what, string where)
        {
            return _ybService.GetPlaces(what, where);
        }

        public CheckInResponse CheckIn(long placeId, string name)
        {
            // TODO: This is where I ended up!
            return new CheckInResponse();
        }
    }
}
