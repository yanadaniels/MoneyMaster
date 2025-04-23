using Microsoft.Extensions.Configuration;

namespace MoneyMaster.Common
{
    public static class CommonConfigurationManager
    {
        public static readonly IConfigurationRoot Configuration;

        static CommonConfigurationManager()
        {
            Configuration = new ConfigurationBuilder().AddJsonFile("commonsettings.json").Build();
        }
    }
}
