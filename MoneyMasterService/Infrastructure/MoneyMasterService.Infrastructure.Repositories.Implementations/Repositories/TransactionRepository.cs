using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="ITransactionRepository"/></summary>
    public class TransactionRepository : Repository<Transaction, Guid>, ITransactionRepository
    {
        /// <summary><inheritdoc cref="ITransactionRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public TransactionRepository(MoneyMasterServiceContext context) : base(context) { }

        public async Task<IReadOnlyCollection<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken)
        {
            return await Context
                .Set<Transaction>()
                .Where(t => t.AccountId == accountId)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyCollection<Transaction?>> GetByDataRange(Guid accountId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken)
        {
            return await Context
               .Set<Transaction>()
               .Where(t => t.AccountId == accountId)
               .Where(d => d.CreateAt >= startDate && d.CreateAt <= endDate)
               .ToListAsync(cancellationToken);
        }
    }
}
