using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace DeanIS.Net.IoC
{
    public class StructureMapControllerFactory : IControllerFactory
    {
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            return ObjectFactory.TryGetInstance<IController>(controllerName.ToLowerInvariant());
        }

        public void ReleaseController(IController controller)
        {
            return;
        }
    }
}
