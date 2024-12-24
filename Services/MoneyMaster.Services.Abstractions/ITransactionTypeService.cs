using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.Services.Contracts.TransactionType;

namespace MoneyMaster.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с типами транзакций</summary>
    public interface ITransactionTypeService
    {
        /// <summary>
        /// Получить тип транзакции по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО типа транзакции. </returns>
        Task<TransactionTypeDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список типов транзакций.
        /// </summary>
        /// <returns> Список DTO типа транзакций. </returns>
        Task<ICollection<TransactionTypeDto>> GetAllAsync();
    }
}
