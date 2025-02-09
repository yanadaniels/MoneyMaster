// Ignore Spelling: Registrator

using Microsoft.Extensions.DependencyInjection;
using MoneyMaster.Infrastructure.Repositories.Implementations.Repositories;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Service
{
    public static class Registrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)=>
            services
            .AddTransient<IAccountRepository,AccountRepository>()
            .AddTransient<IAccountTypeRepository,AccountTypeRepository>()
            .AddTransient<ICategoryRepository,CategoryRepository>()
            .AddTransient<IReportRepository,ReportRepository>()
            .AddTransient<ITransactionRepository,TransactionRepository>()
            .AddTransient<IUserRepository,UserRepository>()
            .AddTransient<IUserSettingRepository,UserSettingRepository>()
            .AddTransient<IUnitOfWork, UnitOfWork>()
            ;
    }
}
