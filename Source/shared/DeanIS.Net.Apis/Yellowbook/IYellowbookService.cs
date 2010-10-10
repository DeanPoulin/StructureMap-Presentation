namespace DeanIS.Net.Apis.Yellowbook
{
    public interface IYellowbookService
    {
        BusinessSearchResults25 GetListings(string what, string where);
        BusinessDetailResults25 GetListingDetails(long listingId);
        BusinessSearchResults25 GetListingsAroundMe(string what, string latitude, string longitude, int radius = 1, int pagenumber = 1, int pagesize = 15);
    }
}