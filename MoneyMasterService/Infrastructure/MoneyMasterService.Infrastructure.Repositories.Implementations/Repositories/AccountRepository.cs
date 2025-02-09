using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IAccountRepository"/></summary>
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        /// <summary><inheritdoc cref="IAccountRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public AccountRepository(MoneyMasterServiceContext context) : base(context)
        {
        }
    }
}
