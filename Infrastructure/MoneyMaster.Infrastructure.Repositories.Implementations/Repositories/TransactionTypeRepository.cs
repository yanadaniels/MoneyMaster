using MoneyMaster.Domain.Entities.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="ITransactionTypeRepository"/></summary>
    public class TransactionTypeRepository : Repository<TransactionType, Guid>, ITransactionTypeRepository
    {
        /// <summary><inheritdoc cref="ITransactionTypeRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public TransactionTypeRepository(MoneyMasterContext context) : base(context) { }
    }
}
