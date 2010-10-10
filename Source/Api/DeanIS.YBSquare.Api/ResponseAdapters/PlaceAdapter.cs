using System;
using System.Collections.Generic;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.Net.Utilities;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.ResponseAdapters
{
    public class PlaceAdapter : IPlaceAdapter
    {
        public Place Create(Listing25 listing)
        {
            var place = new Place { Id = listing.ListingId, Name = listing.Name };

            AddContactInfo(listing, place);
            AddAddressInfo(listing, place);

            decimal distance;
            if (decimal.TryParse(listing.Distance, out distance))
                place.Distance = distance;

            return place;
        }

        private static void AddAddressInfo(Listing25 listing, Place place)
        {
            if (listing.Locations.IsNullOrEmpty())
                return;

            var sourceAddress = listing.Locations[0];

            place.Address = new Address
                                {
                                    Address1 = sourceAddress.Address1,
                                    Address2 = sourceAddress.Address2,
                                    City = sourceAddress.City,
                                    State = sourceAddress.State,
                                    ZipCode = sourceAddress.Zip,
                                    Latitude = sourceAddress.Latitude,
                                    Longitude = sourceAddress.Longitude
                                };
        }

        private static void AddContactInfo(Listing25 listing, Place place)
        {
            if (listing.ContactInfo == null)
                return;

            place.PhoneNumber = GetPhoneNumber(listing.ContactInfo);
            place.WebSite = GetWebSite(listing.ContactInfo.Websites);
        }

        private static string GetPhoneNumber(ContactInfo25 contactInfo)
        {
            return string.IsNullOrEmpty(contactInfo.OfficeNumber)
                       ? string.IsNullOrEmpty(contactInfo.TollFreeNumber) ? string.Empty : contactInfo.TollFreeNumber
                       : contactInfo.OfficeNumber;
        }

        private static WebSite GetWebSite(IList<Website25> websites)
        {
            if (websites == null || websites.Count == 0)
                return null;

            Uri uri;

            var website = websites[0];

            return Uri.TryCreate(website.Url, UriKind.Absolute, out uri)
                       ? null
                       : new WebSite { Title = website.Name, Uri = uri };
        }
    }
}