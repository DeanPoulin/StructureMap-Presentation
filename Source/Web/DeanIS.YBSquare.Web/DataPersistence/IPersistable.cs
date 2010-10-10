using System.Web;

namespace DeanIS.YBSquare.Web.DataPersistence
{
    public interface IPersistable
    {
        void Load(HttpContextBase httpContext, GlobalContext globalContext);
        void Save(HttpContextBase httpContext, GlobalContext globalContext);
    }
}