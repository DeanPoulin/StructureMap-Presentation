using System.Web.Mvc;
using DeanIS.YBSquare.Web.Services;
using DeanIS.YBSquare.WebSite.Controllers;
using Moq;
using NUnit.Framework;

namespace DeanIS.YBSquare.Tests.Web.Controllers
{
    [TestFixture]
    public class when_requesting_the_index_of_the_homecontroller : context
    {
        protected HomeController sut;
        protected ViewResult result;
        protected Mock<IPlacesService> _placesService;

        public override void setupContext()
        {
            _placesService = new Mock<IPlacesService>();
            sut = new HomeController(_placesService.Object);
        }

        public override void act()
        {
            result = sut.Index() as ViewResult;
        }

        [Test]
        public void the_view_data_should_contain_the_correct_message()
        {
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual("Welcome to ASP.NET MVC!", viewData["Message"]);
        }
    }

    [TestFixture]
    public class when_requesting_the_about_action_on_the_homepage_controller : context
    {
        protected HomeController sut;
        protected ViewResult result;
        protected Mock<IPlacesService> _placesService;

        public override void setupContext()
        {
            _placesService = new Mock<IPlacesService>();
            sut = new HomeController(_placesService.Object);
        }

        public override void act()
        {
            result = sut.About() as ViewResult;
        }

        [Test]
        public void eusure_that_the_result_is_not_null()
        {
            Assert.IsNotNull(result);
        }
    }
}
