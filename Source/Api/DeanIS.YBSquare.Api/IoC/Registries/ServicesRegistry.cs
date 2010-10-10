using DeanIS.Net;
using DeanIS.Net.Apis.Yellowbook;
using DeanIS.YBSquare.Api.ResponseAdapters;
using DeanIS.YBSquare.DataAccess.Services;
using StructureMap.Configuration.DSL;

namespace DeanIS.YBSquare.Api.IoC.Registries
{
    public class ServicesRegistry : Registry
    {
        public ServicesRegistry()
        {
            For<IYellowbookServiceConfiguration>().Use<YellowbookServiceConfiguration>();
            For<IDigestAuthenticationClient>().Use<DigestAuthenticationClient>();
            For<IYellowbookService>().Use<YellowbookService>();
            For<IDigestAuthenticationClient>().Singleton().Use<DigestAuthenticationClient>();

            For<ICheckInsService>().Use<CheckInsService>();
            For<ICheckInsResponseAdapter>().Use<CheckInsResponseAdapter>();

            For<ICheckInsRepository>().Use<CheckInsReposoitory>();
        }
    }
}