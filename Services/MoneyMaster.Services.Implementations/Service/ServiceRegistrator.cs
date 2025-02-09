// Ignore Spelling: Registrator

using Microsoft.Extensions.DependencyInjection;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Abstractions.Transaction;
using MoneyMaster.Services.Implementations.Transaction;

namespace MoneyMaster.Services.Implementations.Service
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
                .AddTransaction()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<IAccountTypeService, AccountTypeService>()
                .AddTransient<ICategoryService, CategoryService>()
                .AddTransient<IReportService, ReportService>()
                .AddTransient<IUserService, UserService>()
                .AddTransient<IUserSettingService, UserSettingService>();

        private static IServiceCollection AddTransaction(this IServiceCollection services) =>
            services
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<IBalanceChanger, BalanceChanger>();
    }
}