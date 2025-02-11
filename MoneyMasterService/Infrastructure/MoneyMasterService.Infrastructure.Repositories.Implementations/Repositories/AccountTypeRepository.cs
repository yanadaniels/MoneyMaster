using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IAccountTypeRepository"/></summary>
    public class AccountTypeRepository : Repository<AccountType, Guid>, IAccountTypeRepository
    {
        /// <summary><inheritdoc cref="IAccountTypeRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public AccountTypeRepository(MoneyMasterServiceContext context) : base(context) { } 
    }
}
