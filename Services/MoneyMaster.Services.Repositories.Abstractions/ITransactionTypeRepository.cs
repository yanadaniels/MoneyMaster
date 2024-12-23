using MoneyMaster.Domain.Entities.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с типами транзакций
    /// </summary>
    public interface ITransactionTypeRepository : IRepository<TransactionType, Guid>
    {
    }
}
