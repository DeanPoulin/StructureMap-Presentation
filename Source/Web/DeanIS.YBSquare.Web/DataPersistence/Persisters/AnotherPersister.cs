using System;
using System.Web;

namespace DeanIS.YBSquare.Web.DataPersistence.Persisters
{
    public class AnotherPersister : IPersistable
    {
        public const string COOKIENAME = "__Another";
        public const string VALUE = "value";

        public void Load(HttpContextBase httpContext, GlobalContext globalContext)
        {
            var cookie = httpContext.Request.Cookies[COOKIENAME];

            if (cookie == null) return;

            globalContext.UserActivity = globalContext.UserActivity ?? new UserActivity();

            var anotherValue = cookie[VALUE];

            if (anotherValue != null)
            {
                globalContext.AnotherValue = anotherValue;
            }
        }

        public void Save(HttpContextBase httpContext, GlobalContext globalContext)
        {
            var cookie = new HttpCookie(COOKIENAME) { Path = "/"};

            if (globalContext.UserActivity == null || string.IsNullOrEmpty(globalContext.AnotherValue))
            {
                cookie = new HttpCookie(COOKIENAME) { Expires = DateTime.Now.AddDays(-2)};
                httpContext.Response.Cookies.Add(cookie);
            }
            else
            {
                cookie[VALUE] = globalContext.AnotherValue;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
        }
    }
}
