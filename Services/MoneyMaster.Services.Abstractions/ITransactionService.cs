using MoneyMaster.Services.Contracts.Transaction;

namespace MoneyMaster.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с транзакциями</summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Получить транзакцию по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО транзакции. </returns>
        Task<TransactionDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список транзакций.
        /// </summary>
        /// <returns> Список DTO транзакций. </returns>
        Task<ICollection<TransactionDto>> GetAllAsync();
    }
}
