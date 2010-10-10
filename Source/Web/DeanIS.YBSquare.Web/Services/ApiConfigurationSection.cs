using System.Configuration;

namespace DeanIS.YBSquare.Web.Services
{
    public class ApiConfigurationSection : ConfigurationSection, IApiConfiguration
    {
        public const string SectionName = "API";

        public static ApiConfigurationSection LoadBySectionName(string sectionName)
        {
            return (ApiConfigurationSection)ConfigurationManager.GetSection(sectionName);
        }

        [ConfigurationProperty("BasePath", IsRequired = true)]
        public string BasePath
        {
            get { return (string)this["BasePath"]; }
            set { this["BasePath"] = value; }
        }
    }
}
