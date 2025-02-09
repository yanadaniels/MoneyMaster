// Ignore Spelling: Dto

using MoneyMasterService.Services.Contracts.Account;
using MoneyMasterService.Services.Contracts.UserSetting;

namespace MoneyMasterService.Services.Contracts.User
{
    /// <summary>DTO пользователя</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class UserDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        /// <summary>Настройки пользователя </summary>
        public UserSettingDto? UserSetting { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<AccountDto>? Accounts { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO пользователя</summary>
    public class UserDto : UserDto<Guid>;

}
