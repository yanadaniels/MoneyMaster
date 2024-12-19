namespace MoneyMaster.Domain.Entities
{
    /// <summary>Пользователь</summary>
    public class User : TimedEntity
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        /// <summary>Настройки пользователя </summary>
        public UserSetting? UserSetting { get; set; }

        /// <summary>Аккаунт</summary>
        public Account? Account { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }
    }
}
