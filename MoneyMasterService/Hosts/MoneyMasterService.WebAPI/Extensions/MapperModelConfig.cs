using AutoMapper;
using MoneyMasterService.WebAPI.Mapping;

namespace MoneyMasterService.WebAPI.Extensions
{
    public static class MapperModelConfig
    {
        public static MapperConfiguration GetMapperModelConfiguration()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccountModelMappingsProfile>();
                cfg.AddProfile<AccountTypeModelMappingsProfile>();
                cfg.AddProfile<CategoryModelMappingsProfile>();
                cfg.AddProfile<ReportModelMappingsProfile>();
                cfg.AddProfile<TransactionModelMappingsProfile>();
                cfg.AddProfile<TransactionTypeModelMappingsProfile>();
                cfg.AddProfile<UserModelMappingsProfile>();
                cfg.AddProfile<UserSettingModelMappingsProfile>();

            });
            //configuration.AssertConfigurationIsValid();
            return configuration;
        }
    }
}
