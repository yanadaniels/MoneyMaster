using System.Xml;

namespace MoneyMaster.Domain.Entities
{
    /// <summary>Пользователь</summary>
    public class User : TimedEntity, ISoftDeletable
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        /// <summary>Настройки пользователя </summary>
        public UserSetting? UserSetting { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<Account>? Accounts { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }
    }
}
