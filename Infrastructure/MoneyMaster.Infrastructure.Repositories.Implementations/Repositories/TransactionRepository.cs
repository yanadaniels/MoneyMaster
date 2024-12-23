using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="ITransactionRepository"/></summary>
    public class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        /// <summary><inheritdoc cref="ITransactionRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public TransactionRepository(DbContext context) : base(context) { }
    }
}
