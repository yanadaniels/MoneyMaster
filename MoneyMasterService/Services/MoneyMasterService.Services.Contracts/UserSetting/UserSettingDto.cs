// Ignore Spelling: Dto

using MoneyMasterService.Services.Contracts.User;

namespace MoneyMasterService.Services.Contracts.UserSetting
{
    /// <summary>DTO настроек пользователя</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class UserSettingDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Пользователь</summary>
        public UserDto? User { get; set; }

        /// <summary>Валюта</summary>
        public string? Currency { get; set; }

        /// <summary>Язык</summary>
        public string? Language { get; set; }

    }

    /// <summary>DTO настроек пользователя</summary>
    public class UserSettingDto : UserSettingDto<Guid>;
}
