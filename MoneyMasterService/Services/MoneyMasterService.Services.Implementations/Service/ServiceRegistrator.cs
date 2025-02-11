// Ignore Spelling: Registrator

using Microsoft.Extensions.DependencyInjection;
using MoneyMasterService.Services.Abstractions;

namespace MoneyMasterService.Services.Implementations.Service
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
            .AddTransient<IAccountService, AccountService>()
            .AddTransient<IAccountTypeService, AccountTypeService>()
            .AddTransient<ICategoryService, CategoryService>()
            .AddTransient<IReportService, ReportService>()  
            .AddTransient<ITransactionService, TransactionService>()
            .AddTransient<ITransactionTypeService, TransactionTypeService>()
            .AddTransient<IUserSettingService, UserSettingService>()
            ;
        
    }
}
