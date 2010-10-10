using System;
using System.Collections.Generic;
using System.Linq;

namespace DeanIS.YBSquare.DataAccess.Services
{
    public interface ICheckInsRepository
    {
        IEnumerable<UserCheckin> GetRecentCheckIns();
        int CheckIn(Guid userId, long listingId);
    }
}