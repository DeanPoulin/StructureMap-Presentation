using System.Collections.Generic;
using System.Web.Mvc;
using DeanIS.Framework.Testing;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.Web.Mvc;
using DeanIS.YBSquare.Api.Controllers;
using DeanIS.YBSquare.Api.ResponseAdapters;
using DeanIS.YBSquare.DataTypes.Responses;
using Moq;
using NUnit.Framework;

namespace DeanIS.YBSquare.Api.Tests.Api.Controllers
{
    [TestFixture]
    public class when_getting_places_from_the_yellowbook_service_by_what_and_where : context
    {
        private PlacesController sut;
        private Mock<IYellowbookService> _yellowbookService;
        private Mock<IPlacesResponseAdapter> _placesResponseAdapter;
        private Mock<IPlaceDetailsResponseAdapter> _placeDetailsResponseAdapter;

        private ActionResult result;

        private readonly BusinessSearchResults25 _knownResults = new BusinessSearchResults25
                    {
                        InAreaListings =
                            new ListingSummaryCollection25
                                {
                                    Listing =
                                        new[]
                                            {
                                                new Listing25
                                                    {
                                                        Name = "First Listing",
                                                        ListingId = 12345
                                                    }
                                            },
                                    TotalListings = 1
                                }
                    };

        private readonly List<Place> _expectedPlaces = new List<Place> 
            { new Place { Id = 12345, Name = "First Listing"}};

        public override void setupContext()
        {
            _yellowbookService = new Mock<IYellowbookService>();

            _yellowbookService.Setup(ybs => ybs.GetListings(It.IsAny<string>(), 
                It.IsAny<string>())).Returns(_knownResults);

            _placesResponseAdapter = new Mock<IPlacesResponseAdapter>();

            _placesResponseAdapter
                .Setup(adapter => adapter.Create(It.IsAny<BusinessSearchResults25>()))
                .Returns(new PlacesResponse
                             {
                                 Places = _expectedPlaces,
                                 Status = Status.Ok
                             });

            _placeDetailsResponseAdapter = new Mock<IPlaceDetailsResponseAdapter>();

            sut = new PlacesController(_yellowbookService.Object, 
                _placesResponseAdapter.Object, _placeDetailsResponseAdapter.Object);
        }

        public override void act()
        {
            result = sut.Index("pizza", "King of Prussia, PA");
        }

        [Test]
        public void the_places_response_is_not_null()
        {
            Assert.That(result is ObjectResult<PlacesResponse>, Is.True);
        }

        [Test]
        public void we_get_one_place_back()
        {
            var placesResponse = (ObjectResult<PlacesResponse>) result;
            Assert.That(placesResponse.Data.Places.Count, Is.EqualTo(1));
            
        }

        [Test]
        public void the_name_of_the_listing_is_correct()
        {
            var placesResponse = (ObjectResult<PlacesResponse>)result;
            Assert.That(placesResponse.Data.Places[0].Name, Is.EqualTo("First Listing"));
        }
    }
}
