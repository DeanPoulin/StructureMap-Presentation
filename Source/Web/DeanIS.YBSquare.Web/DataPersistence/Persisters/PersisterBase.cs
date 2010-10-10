using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DeanIS.YBSquare.Web.DataPersistence.Persisters
{
    public abstract class PersisterBase
    {
        protected HttpContextBase HttpContext;

        protected PersisterBase (HttpContextBase httpContext)
        {
            HttpContext = httpContext;
        }
    }
}
