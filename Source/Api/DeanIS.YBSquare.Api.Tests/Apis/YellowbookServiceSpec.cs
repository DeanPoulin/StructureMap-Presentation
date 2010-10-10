using System.Linq;
using DeanIS.Framework.Testing;
using DeanIS.Net;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.Api.Tests.Net;
using Moq;
using NUnit.Framework;

namespace DeanIS.YBSquare.Api.Tests.Apis
{
    [TestFixture, Category("Integration")]
    public class when_requesting_listings_from_the_yellowbook_api : context
    {
        protected BusinessSearchResults25 results;
        protected YellowbookService sut;

        #region Credentials

        public const string CorrectUsername = "testusername";
        public const string CorrectPassword = "testpassword";
        public const string InCorrectPassword = "testpassword";

        #endregion

        private const string _correctUrl = "http://api.yellowbook.com/yb_rest/GetListings.ashx?what=pizza&where=kop";

        private Mock<IYellowbookServiceConfiguration> _correctConfiguration;
        private Mock<IYellowbookServiceConfiguration> _incorrectConfiguration;

        public override void setupContext()
        {
            _correctConfiguration = new Mock<IYellowbookServiceConfiguration>();
            _correctConfiguration.Setup(c => c.BasePath).Returns(_correctUrl);
            _correctConfiguration.Setup(c => c.Username).Returns(CorrectUsername);
            _correctConfiguration.Setup(c => c.BasePath).Returns(CorrectPassword);

            sut = new YellowbookService(new DigestAuthenticationClient(),
                                        _correctConfiguration.Object);
        }

        public override void act()
        {
            results = sut.GetListings("pizza", "kop");
        }

        [Test]
        public void verify_the_service_returns_data_in_expected_format()
        {
            Assert.That(results, Is.Not.Null);
            var firstListing = results.InAreaListings.Listing.FirstOrDefault();
            Assert.That(firstListing, Is.Not.Null);
            var listingDetails = sut.GetListingDetails(firstListing.ListingId);
            Assert.That(listingDetails, Is.Not.Null);
        }
    }
}
