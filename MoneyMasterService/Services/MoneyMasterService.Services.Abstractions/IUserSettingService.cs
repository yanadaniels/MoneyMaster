using MoneyMasterService.Services.Contracts.UserSetting;

namespace MoneyMasterService.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с настройками пользователя </summary>
    public interface IUserSettingService
    {
        /// <summary>
        /// Получить настройку по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО настройки пользователя. </returns>
        Task<UserSettingDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список настроек всех пользователей.
        /// </summary>
        /// <returns> Список DTO настроек пользователей. </returns>
        Task<ICollection<UserSettingDto>> GetAllAsync();
    }
}
