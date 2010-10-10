using System.Configuration;
using DeanIS.YBSquare.Web.IoC.Registries;
using StructureMap;

namespace DeanIS.YBSquare.Web.IoC
{
    public static class Bootstrapper
    {
        public static readonly string IocProfile = ConfigurationManager.AppSettings["Ioc.Profile"] ?? string.Empty;

        public static void Bootstrap()
        {
            Bootstrap(IocProfile);
        }

        public static void Bootstrap(string profile)
        {
            ObjectFactory.Initialize(
                x =>
                    {
                        x.AddRegistry(new ControllerRegistry());
                        x.AddRegistry(new ServiceRegistry());
                    }
                );

            if (!string.IsNullOrEmpty(profile))
                ObjectFactory.Profile = profile;

            ObjectFactory.AssertConfigurationIsValid();
        }
    }
}
