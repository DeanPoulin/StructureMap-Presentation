using System.Configuration;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.Api.IoC.Registries;
using StructureMap;

namespace DeanIS.YBSquare.Api.IoC
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
                        x.AddRegistry(new ServicesRegistry());
                        x.AddRegistry(new ResponseAdaptersRegistry());
                        x.AddRegistry(new ProfileRegistry());
                    }
                );

            if (!string.IsNullOrEmpty(profile))
                ObjectFactory.Profile = profile;

            ObjectFactory.AssertConfigurationIsValid();
        }
    }
}