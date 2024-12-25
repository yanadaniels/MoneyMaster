using MoneyMaster.Services.Contracts.Account;

namespace MoneyMaster.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с счетами</summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получить счет по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО счета. </returns>
        Task<AccountDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список счетов.
        /// </summary>
        /// <returns> Список DTO счетов. </returns>
        Task<ICollection<AccountDto>> GetAllAsync();
    }
}
