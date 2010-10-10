using System.Web.Mvc;
using DeanIS.YBSquare.DataTypes.Responses;
using DeanIS.YBSquare.Web.Services;
using DeanIS.YBSquare.WebSite.Models;

namespace DeanIS.YBSquare.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlacesService _placesService;
        private const string DefaultPlace = "Fort Washington, PA";
        private const string DefaultSearch = "software";

        public HomeController(IPlacesService placesService)
        {
            _placesService = placesService;
        }

        public ActionResult Index(string what = null, string where = null, decimal latitude = 0, decimal longitude = 0)
        {
            PlacesResponse placesResponse;

            if (!string.IsNullOrEmpty(what) && latitude != default(decimal) && longitude != default(decimal))
            {
                placesResponse = _placesService.GetPlaces(what, latitude, longitude);
            }
            else
            {
                placesResponse = !string.IsNullOrEmpty(what) && !string.IsNullOrEmpty(where)
                             ? _placesService.GetPlaces(what, where)
                             : _placesService.GetPlaces(DefaultSearch, DefaultPlace);
            }

            var model = new HomeModel {Places = placesResponse.Places};

            return View(model);
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
