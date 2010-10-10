namespace DeanIS.YBSquare.Web.Services
{
    public class ApiConfiguration : IApiConfiguration
    {
        public string BasePath
        {
            get { return ApiConfigurationSection.LoadBySectionName(ApiConfigurationSection.SectionName).BasePath; }
        }
    }
}