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
    }
}
