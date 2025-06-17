// Ignore Spelling: Registrator

using Microsoft.Extensions.DependencyInjection;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.Services.Abstractions.Transaction;

namespace MoneyMasterService.Services.Implementations.Service
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
            .AddTransaction()
            .AddTransient<IAccountService, AccountService>()
            .AddTransient<IAccountTypeService, AccountTypeService>()
            .AddTransient<ICategoryService, CategoryService>()
            .AddTransient<IUserSettingService, UserSettingService>();

        private static IServiceCollection AddTransaction(this IServiceCollection services) =>
            services
            .AddTransient<ITransactionService, TransactionService>()
            .AddTransient<IBalanceChanger, BalanceChanger>();
    }
}
