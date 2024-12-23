using AutoMapper;
using MoneyMaster.Services.Implementations.Mapping;

namespace MoneyMaster.Services.Implementations.Extensions
{
    public static class MapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountMappingsProfile>();
                cfg.AddProfile<AccountTypeMappingsProfile>();
                cfg.AddProfile<CategoryMappingsProfile>();
                cfg.AddProfile<ReportMappingsProfile>();
                cfg.AddProfile<TransactionMappingsProfile>();
                cfg.AddProfile<TransactionTypeMappingsProfile>();
                cfg.AddProfile<UserMappingsProfile>();
                cfg.AddProfile<UserSettingMappingsProfile>();

            });
            //configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}
