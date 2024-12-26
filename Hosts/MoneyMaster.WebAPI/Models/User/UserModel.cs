using MoneyMaster.WebAPI.Models.Account;
using MoneyMaster.WebAPI.Models.UserSetting;

namespace MoneyMaster.WebAPI.Models.User
{
    /// <summary>Модель пользователя</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class UserModel<TKey>
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
        public UserSettingModel? UserSetting { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<AccountModel>? Accounts { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель пользователя</summary>
    public class UserModel : UserModel<Guid>;
}
