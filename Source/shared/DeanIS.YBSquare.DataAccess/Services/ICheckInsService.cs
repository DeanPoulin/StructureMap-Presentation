using System;
using System.Collections.Generic;

namespace DeanIS.YBSquare.DataAccess.Services
{
    public interface ICheckInsService
    {
        IEnumerable<UserCheckin> GetRecentCheckIns();
        int CheckIn(Guid userId, long placeId);
    }
}