using MoneyMasterService.Services.Contracts.AccountType;

namespace MoneyMasterService.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с типом счета</summary>
    public interface IAccountTypeService
    {
        /// <summary>
        /// Получить тип счета по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО типа счета. </returns>
        Task<AccountTypeDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список типа счетов.
        /// </summary>
        /// <returns> Список DTO счетов. </returns>
        Task<ICollection<AccountTypeDto>> GetAllAsync();
    }
}
