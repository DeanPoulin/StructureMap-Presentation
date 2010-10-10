using System;
using System.IO;
using System.Xml.Serialization;

namespace DeanIS.Net.Apis.Yellowbook
{
    public class YellowbookService : IYellowbookService
    {
        private readonly IDigestAuthenticationClient _digestAuthenticationClient;
        private readonly IYellowbookServiceConfiguration _configuration;

        public YellowbookService(IDigestAuthenticationClient digestAuthenticationClient, 
            IYellowbookServiceConfiguration configuration)
        {
            _digestAuthenticationClient = digestAuthenticationClient;
            _configuration = configuration;
        }

        public BusinessSearchResults25 GetListings(string what, string where)
        {
            try
            {
                var uri = new Uri(string.Format("{0}GetListings.ashx?what={1}&where={2}&version=2.5", _configuration.BasePath, what, where));

                var response = _digestAuthenticationClient.GetResponseString(_configuration.Username, _configuration.Password, uri);

                return ProcessResponse<BusinessSearchResults25>(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public BusinessDetailResults25 GetListingDetails(long listingId)
        {
            try
            {
                var uri = new Uri(string.Format("{0}GetListingDetails.ashx?Id={1}&getAllAds=all&version=2.5&addressId=1", _configuration.BasePath, listingId));

                var response = _digestAuthenticationClient.GetResponseString(_configuration.Username, _configuration.Password, uri);

                return ProcessResponse<BusinessDetailResults25>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public BusinessSearchResults25 GetListingsAroundMe(string what, string latitude, string longitude, int radius = 1, int pagenumber = 1, int pagesize = 15)
        {
            try
            {
                var uri =
                    new Uri(
                        string.Format(
                            "{0}getlistings.ashx?what={1}&filterby=distancelatlong&Latitude={2}&Longitude={3}&radius={4}&sort=distancelatlong&visitorId={5}&sessionId={6}&version=2.5&clientIp=10.0.5.25&browserAgent=firefox&pagenumber={7}&pagesize={8}",
                            _configuration.BasePath, what, latitude, longitude, radius, Guid.NewGuid(), Guid.NewGuid(),  pagenumber, pagesize));

                var response = _digestAuthenticationClient.GetResponseString(_configuration.Username, _configuration.Password, uri);

                return ProcessResponse<BusinessSearchResults25>(response);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static T ProcessResponse<T>(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new ArgumentOutOfRangeException("value", "Cannot thaw a zero-length string");

            var serializer = new XmlSerializer(typeof(T));

            using (var reader = new StringReader(value))
            {
                return (T)serializer.Deserialize(reader);
            }
        }
    }
}
