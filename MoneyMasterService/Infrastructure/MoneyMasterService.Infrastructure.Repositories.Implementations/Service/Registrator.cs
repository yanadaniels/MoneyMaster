// Ignore Spelling: Registrator

using Microsoft.Extensions.DependencyInjection;
using MoneyMasterService.Services.Repositories.Abstractions;
using MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Service
{
    public static class Registrator
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)=>
            services
            .AddTransient<IAccountRepository,AccountRepository>()
            .AddTransient<IAccountTypeRepository,AccountTypeRepository>()
            .AddTransient<ICategoryRepository,CategoryRepository>()
            .AddTransient<ITransactionRepository,TransactionRepository>()
            .AddTransient<IUserSettingRepository,UserSettingRepository>()
            .AddTransient<IUnitOfWork, UnitOfWork>()
            ;
    }
}
