using MoneyMaster.Services.Contracts.Account;

namespace MoneyMaster.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с аккаунтом</summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получить аккаунт по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО аккаунта. </returns>
        Task<AccountDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список аккаунтов.
        /// </summary>
        /// <returns> Список DTO аккаунтов. </returns>
        Task<ICollection<AccountDto>> GetAllAsync();
    }
}
