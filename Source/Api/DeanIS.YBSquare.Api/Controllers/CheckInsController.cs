using System.Web.Mvc;
using DeanIS.YBSquare.Api.Models;
using DeanIS.YBSquare.Api.ResponseAdapters;
using DeanIS.YBSquare.DataAccess.Services;
using DeanIS.Web.Mvc;
using DeanIS.YBSquare.DataTypes.Responses;

namespace DeanIS.YBSquare.Api.Controllers
{
    public class CheckInsController : Controller
    {
        private readonly ICheckInsService _checkInsService;
        private readonly ICheckInsResponseAdapter _checkInsResponseAdapter;

        public CheckInsController(ICheckInsService checkInsService, ICheckInsResponseAdapter checkInsResponseAdapter)
        {
            _checkInsService = checkInsService;
            _checkInsResponseAdapter = checkInsResponseAdapter;

        }

        [HttpPost]
        public ActionResult CheckIn(CheckInModel model)
        {
            if (ModelState.IsValid)
            {
                var checkInId = _checkInsService.CheckIn(model.UserId, model.PlaceId);

                return checkInId != default(int)
                           ? new ObjectResult<CheckInResponse>(new CheckInResponse {CheckInId = checkInId, Successful = true})
                           : new ObjectResult<CheckInResponse>(new CheckInResponse() {Successful = false});
            }
            return new ObjectResult<CheckInResponse>(new CheckInResponse() {Successful = false});
        }

        public ActionResult Recent()
        {
            var recentCheckIns = _checkInsService.GetRecentCheckIns();

            var checkInsResponse = _checkInsResponseAdapter.Create(recentCheckIns);
            return new ObjectResult<CheckInsResponse>(checkInsResponse);
        }

    }
}
