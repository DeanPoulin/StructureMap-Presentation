using System.Web.Mvc;
using DeanIS.Net.IoC;
using StructureMap.Configuration.DSL;

namespace DeanIS.YBSquare.Api.IoC.Registries
{
    public class ControllerRegistry : Registry
    {
        public ControllerRegistry()
        {
            // Tell MVC that I want to use the StructureMapControllerFactory instead of the built in controller factory.
            For<IControllerFactory>().Use<StructureMapControllerFactory>();

            // scan for all the controllers in the application and register them by name (I.E "places").
            Scan( x =>
                      {
                          x.TheCallingAssembly();
                          x.AddAllTypesOf<IController>().NameBy(
                              type => type.Name.Replace("Controller", string.Empty).ToLower());
                      });
        }
    }
}