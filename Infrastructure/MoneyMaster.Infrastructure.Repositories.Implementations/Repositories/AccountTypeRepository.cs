using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IAccountTypeRepository"/></summary>
    public class AccountTypeRepository : Repository<AccountType, Guid>, IAccountTypeRepository
    {
        /// <summary><inheritdoc cref="IAccountTypeRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public AccountTypeRepository(MoneyMasterContext context) : base(context) { } 
    }
}
