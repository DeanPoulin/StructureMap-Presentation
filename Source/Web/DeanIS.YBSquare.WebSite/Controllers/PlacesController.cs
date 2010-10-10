using System.Web.Mvc;
using DeanIS.YBSquare.Web.Services;

namespace DeanIS.YBSquare.WebSite.Controllers
{
    public class PlacesController : Controller
    {
        private readonly IPlacesService _placesService;

        public PlacesController(IPlacesService placesService)
        {
            _placesService = placesService;
        }


        [Authorize]
        [HttpPost]
        public ActionResult CheckIn(long placeId)
        {
            if (placeId != default(long))
            {
                _placesService.CheckIn(placeId, ControllerContext.HttpContext.User.Identity.Name);
            }

            return View();
        }

    }
}
