using System.Web.Security;
using DeanIS.YBSquare.Web.DataPersistence;
using DeanIS.YBSquare.Web.DataPersistence.Persisters;
using DeanIS.YBSquare.Web.Services;
using StructureMap.Configuration.DSL;

namespace DeanIS.YBSquare.Web.IoC.Registries
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IMembershipService>().Use<AccountMembershipService>()
                .Ctor<MembershipProvider>("provider").Is(Membership.Provider);
            For<IFormsAuthenticationService>().Use<FormsAuthenticationService>();

            For<IPlacesService>().Use<PlacesService>();

            For<IYBSquareService>().Use<YBSquareService>();
            For<IApiConfiguration>().Use<ApiConfiguration>();
            For<IDownloader>().Use<HttpDownloader>();

            For<IDataManager>().Use<DataManager>()
                .EnumerableOf<IPersistable>().Contains(
                    x =>
                        {
                            x.Type<UserActivityPersister>();
                            x.Type<AnotherPersister>();
                        });
        }
    }
}
