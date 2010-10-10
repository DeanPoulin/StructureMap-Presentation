using System.Web.Mvc;
using DeanIS.Net.IoC;
using StructureMap.Configuration.DSL;

namespace DeanIS.YBSquare.Web.IoC.Registries
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            For<IControllerFactory>().Use<StructureMapControllerFactory>();

            // scan for all the controllers in the application and register 
            // them by name (I.E PlacesController => "places").
            Scan(x =>
                     {
                         // the controllers are located in the WebSite dll
                         x.Assembly("DeanIS.YBSquare.WebSite");
                         x.AddAllTypesOf<IController>().NameBy(
                             type => type.Name.Replace("Controller", string.Empty).ToLower());
                     });
        }
    }
}
