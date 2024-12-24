using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.AccountType;

namespace MoneyMaster.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с типом учетной записи</summary>
    public interface IAccountTypeService
    {
        /// <summary>
        /// Получить тип учетной записи по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО типа учетной записи. </returns>
        Task<AccountTypeDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список типа учетной записи.
        /// </summary>
        /// <returns> Список DTO учетной записи. </returns>
        Task<ICollection<AccountTypeDto>> GetAllAsync();
    }
}
