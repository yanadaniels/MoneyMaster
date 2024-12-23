using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.UserSetting;

namespace MoneyMaster.Services.Contracts.User
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

        /// <summary>Аккаунт</summary>
        public AccountDto? Account { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class UserDto : UserDto<Guid>;

}
