using System.Collections.Generic;
using System.Linq;
using DeanIS.YBSquare.DataAccess;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public class CheckInsResponseAdapter : ICheckInsResponseAdapter
    {
        public CheckInsResponse Create(IEnumerable<UserCheckin> userCheckIns)
        {
            return new CheckInsResponse()
                       {
                           CheckIns = (userCheckIns.Select(userCheckin => new CheckIn
                                                                              {
                                                                                  CheckInTime = userCheckin.CheckedIn,
                                                                                  ListingId = userCheckin.ListingId,
                                                                                  UserId = userCheckin.UserId
                                                                              })).ToList()
                       };
        }
    }
}