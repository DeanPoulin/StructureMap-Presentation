using System.Configuration;

namespace DeanIS.Net.Apis.Yellowbook
{
    public class YellowbookServiceConfiguration : IYellowbookServiceConfiguration
    {
        private static readonly YellowbookServiceConfigurationSection Configuration =
            YellowbookServiceConfigurationSection.Configuration();

        public string Username
        {
            get { return Configuration.UserName; }
        }

        public string Password
        {
            get { return Configuration.Password; }
        }

        public string BasePath
        {
            get { return Configuration.BasePath; }
        }
    }

    internal class YellowbookServiceConfigurationSection : ConfigurationSection
    {
        public static YellowbookServiceConfigurationSection Configuration()
        {
            return (YellowbookServiceConfigurationSection)ConfigurationManager.GetSection("YellowbookService");
        }

        [ConfigurationProperty("BasePath", IsRequired = true)]
        public string BasePath
        {
            get { return (string)this["BasePath"]; }
            set { this["BasePath"] = value; }
        }

        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this["UserName"]; }
            set { this["UserName"] = value; }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this["Password"]; }
            set { this["Password"] = value; }
        }
    }
}
