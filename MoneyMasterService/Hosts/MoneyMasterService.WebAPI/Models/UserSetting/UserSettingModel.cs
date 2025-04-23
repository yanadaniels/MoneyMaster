

namespace MoneyMasterService.WebAPI.Models.UserSetting
{
    /// <summary>Модель настроек пользователя</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class UserSettingModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Валюта</summary>
        public string? Currency { get; set; }

        /// <summary>Язык</summary>
        public string? Language { get; set; }

    }

    /// <summary>Модель настроек пользователя</summary>
    public class UserSettingModel : UserSettingModel<Guid>;
}
