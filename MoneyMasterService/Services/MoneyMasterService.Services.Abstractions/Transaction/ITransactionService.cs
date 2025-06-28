using MoneyMasterService.Services.Contracts.Transaction;

namespace MoneyMasterService.Services.Abstractions.Transaction
{
    /// <summary>Интерфейс сервиса работы с транзакциями</summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Получить транзакцию по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        Task<TransactionResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить все транзакции по Id счета
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <returns>Список DTO транзакций</returns>
        Task<IReadOnlyCollection<TransactionResponse>> GetByAccountIdAsync(Guid accountId, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список транзакций.
        /// </summary>
        /// <returns> Список DTO транзакций. </returns>
        Task<IReadOnlyCollection<TransactionResponse?>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Создать новую транзакцию.
        /// </summary>
        Task<Guid> CreateAsync(CreatingTransactionRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Изменить транзакцию.
        /// </summary>
        Task<TransactionResponse> UpdateAsync(Guid id, UpdatingTransactionRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Удвлить транзакцию.
        /// </summary>
        Task DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Восстановить транзакцию.
        /// </summary>
        Task<TransactionResponse> RestoreAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Перевод между счетами
        /// </summary>
        Task<Guid> CreateTransactionTransferAsync(CreateTransactionTransferRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список транзакций по диапазону дат.
        /// </summary>
        /// <returns> Список DTO транзакций. </returns>
        Task<IReadOnlyCollection<TransactionResponse?>> GetByDataRange(Guid accountId,DateTime startDate , DateTime endDate, CancellationToken cancellationToken);
    }
}
