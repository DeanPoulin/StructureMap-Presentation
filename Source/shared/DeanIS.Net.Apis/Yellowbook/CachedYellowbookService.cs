using System;

namespace DeanIS.Net.Apis.Yellowbook
{
    public class CachedYellowbookService : IYellowbookService
    {
        public BusinessSearchResults25 GetListings(string what, string where)
        {
            throw new NotImplementedException();
        }

        public BusinessDetailResults25 GetListingDetails(long listingId)
        {
            throw new NotImplementedException();
        }

        public BusinessSearchResults25 GetListingsAroundMe(string what, string latitude, string longitude, int radius, int pagenumber, int pagesize)
        {
            throw new NotImplementedException();
        }
    }
}
