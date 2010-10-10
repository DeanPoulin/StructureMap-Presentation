using System;
using System.Collections.Generic;

namespace DeanIS.YBSquare.DataAccess.Services
{
    public class CheckInsService : ICheckInsService
    {
        private readonly ICheckInsRepository _checkInsRepository;

        public CheckInsService(ICheckInsRepository checkInsRepository)
        {
            _checkInsRepository = checkInsRepository;
        }

        public IEnumerable<UserCheckin> GetRecentCheckIns()
        {
            return _checkInsRepository.GetRecentCheckIns();
        }

        public int CheckIn(Guid userId, long placeId)
        {
            return _checkInsRepository.CheckIn(userId, placeId);
        }
    }
}
