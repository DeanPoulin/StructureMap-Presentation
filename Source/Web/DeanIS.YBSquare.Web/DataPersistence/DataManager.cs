using System.Web;

namespace DeanIS.YBSquare.Web.DataPersistence
{
    public class DataManager : IDataManager
    {
        public IPersistable[] PersistableData { get; set; }

        public void Load(HttpContextBase httpContext, GlobalContext globalContext)
        {
            foreach (var persistable in PersistableData)
            {
                persistable.Load(httpContext, globalContext);
            }
        }

        public void Save(HttpContextBase httpContext, GlobalContext globalContext)
        {
            foreach (var persistable in PersistableData)
            {
                persistable.Save(httpContext, globalContext);
            }
        }
    }
}
