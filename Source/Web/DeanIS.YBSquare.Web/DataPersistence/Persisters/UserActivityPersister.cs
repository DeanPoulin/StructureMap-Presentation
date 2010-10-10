using System;
using System.Linq;
using System.Text;
using System.Web;
using DeanIS.Net.Utilities;

namespace DeanIS.YBSquare.Web.DataPersistence.Persisters
{
    public class UserActivityPersister : IPersistable
    {
        public const string COOKIENAME = "__UserInfo";
        public const string VALUE = "placesViewed";

        public void Load(HttpContextBase httpContext, GlobalContext globalContext)
        {
            var cookie = httpContext.Request.Cookies[COOKIENAME];

            if (cookie == null) return;

            globalContext.UserActivity = globalContext.UserActivity ?? new UserActivity();

            var placesViewed = cookie[VALUE];

            if (placesViewed != null)
            {
                globalContext.UserActivity.PlacesViewed =
                    (from placeViewed in placesViewed.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                     select long.Parse(placeViewed)).ToList();

            }
        }

        public void Save(HttpContextBase httpContext, GlobalContext globalContext)
        {
            var cookie = new HttpCookie(COOKIENAME);

            if (globalContext.UserActivity == null || globalContext.UserActivity.PlacesViewed.IsNullOrEmpty())
            {
                cookie = new HttpCookie(COOKIENAME) {Expires = DateTime.Now.AddDays(-2)};
            }
            else
            {
                var sb = new StringBuilder();
                foreach (var place in globalContext.UserActivity.PlacesViewed)
                {
                    sb.Append(place);
                    sb.Append(",");
                }

                cookie[VALUE] = sb.ToString();
                cookie.Domain = "/";
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            httpContext.Response.Cookies.Add(cookie);
        }
    }
}
