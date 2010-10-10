using System;
using System.Collections.Generic;
using System.Linq;

namespace DeanIS.YBSquare.DataAccess.Services
{
    public class CheckInsReposoitory : ICheckInsRepository
    {
        public IEnumerable<UserCheckin> GetRecentCheckIns()
        {
            using (var objectContext = new ybsquareEntities())
            {
                return objectContext.UserCheckins.OrderByDescending(ci => ci.CheckedIn).Take(20).AsEnumerable().ToList();
            }
        }

        public int CheckIn(Guid userId, long listingId)
        {
            var checkin = new UserCheckin {CheckedIn = DateTime.Now, ListingId = listingId, UserId = userId};

            using (var objectContext = new ybsquareEntities())
            {
                objectContext.UserCheckins.AddObject(checkin);
                objectContext.SaveChanges();
            }

            return checkin.Id;
        }
    }
}