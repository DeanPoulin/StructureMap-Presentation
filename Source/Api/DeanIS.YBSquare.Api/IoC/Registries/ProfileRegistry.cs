using DeanIS.Net.Apis.Yellowbook;
using DeanIS.Net.Mail;
using StructureMap.Configuration.DSL;

namespace DeanIS.YBSquare.Api.IoC.Registries
{
    public class ProfileRegistry : Registry
    {
        public ProfileRegistry()
        {
            //Profile("stress", profile =>
            //            {
            //                For<IYellowbookService>().Use<CachedYellowbookService>();
            //                For<IPostOffice>().Use<FileSystemPostOffice>();
            //            });

            //Profile("development", profile => For<IPostOffice>()
            //    .Use<FileSystemPostOffice>());
        }
    }
}