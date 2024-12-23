using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IAccountRepository"/></summary>
    public class AccountRepository : Repository<Account, Guid>, IAccountRepository
    {
        /// <summary><inheritdoc cref="IAccountRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public AccountRepository(DbContext context) : base(context)
        {
        }
    }
}
