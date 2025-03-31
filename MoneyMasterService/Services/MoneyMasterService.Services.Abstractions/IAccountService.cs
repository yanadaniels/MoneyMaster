using MoneyMaster.Common;
using MoneyMasterService.Services.Contracts.Account;

namespace MoneyMasterService.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с счетами</summary>
    public interface IAccountService
    {
        /// <summary>
        /// Получить счет по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО счета. </returns>
        Task<AccountDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список счетов.
        /// </summary>
        /// <param name="parameters"> Параметры пагинации и фильтрации </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Кортеж со списком ДТО счета и общим количеством </returns>
        Task<(ICollection<AccountDto> Data, int TotalCount)> GetAllAsync(
            PaginationParameters parameters,
            CancellationToken cancellationToken);

        /// <summary>
        /// Получить список удаленных счетов.
        /// </summary>
        /// <param name="parameters"> Параметры пагинации и фильтрации </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> 
        /// Кортеж со списком ДТО счетов которые были удалены и общим количеством
        /// </returns>
        Task<(ICollection<AccountDto> Data, int TotalCount)> GetAllDeletedAsync(
            PaginationParameters parameters,
            CancellationToken cancellationToken);

        /// <summary>
        /// Добавить новый счёт
        /// </summary>
        /// <param name="accountDto"> Принимает DTO в качестве нового счёта </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> DTO счёта </returns>
        Task<AccountDto> AddAsync(
            CreatingAccountDto newAccountDto, CancellationToken cancellationToken);

        /// <summary>
        /// Для получения всей необходимой информации при создании счёта
        /// </summary>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns></returns>
        Task<CreatingAccountInfoDto> GetInfoNeedToCreateAccountAsync(
            CancellationToken cancellationToken);

        /// <summary>
        /// Редактировать новый счёт
        /// </summary>
        /// <param name="updateAccountDto"> Принимает DTO в качестве обновленного счёта</param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns></returns>
        Task<UpdatingAccountDto?> UpdateAsync(
            UpdatingAccountDto updateAccountDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить счёт по id
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Подтверждение удаления </returns>
        Task<AccountDto?> DeleteAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Востановить счёт по id
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Подтверждение востановления </returns>
        Task<AccountDto?> RestoreAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Прибавить к счету
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <param name="amount">Сумма</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task IncreaseBalanceAsync(
            Guid accountId,
            decimal amount,
            CancellationToken cancellationToken);

        /// <summary>
        /// Снять с счета
        /// </summary>
        /// <param name="accountId">Идентификатор счета</param>
        /// <param name="amount">Сумма</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns></returns>
        Task DecreaseBalanceAsync(
            Guid accountId,
            decimal amount, 
            CancellationToken cancellationToken);
    }
}
