using MoneyMaster.Domain.Entities;
using MoneyMaster.Domain.Entities.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с транзакциями
    /// </summary>
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
    }
}
