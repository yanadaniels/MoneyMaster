using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Transaction;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с транзакциями
    /// </summary>
    public interface ITransactionRepository : IRepository<Transaction, Guid>
    {
        /// <summary>
        /// Получить все транзакции для указанного счета.
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Коллекция транзакций для данного счета</returns>
        Task<IReadOnlyCollection<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken);
    }
}
