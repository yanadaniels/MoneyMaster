using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с транзакциями
    /// </summary>
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
    }
}
