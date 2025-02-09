using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="ITransactionTypeRepository"/></summary>
    public class TransactionTypeRepository : Repository<TransactionType, Guid>, ITransactionTypeRepository
    {
        /// <summary><inheritdoc cref="ITransactionTypeRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public TransactionTypeRepository(MoneyMasterServiceContext context) : base(context) { }
    }
}
