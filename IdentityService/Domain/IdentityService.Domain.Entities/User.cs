using MoneyMaster.Common.Entities;

namespace IdentityService.Domain.Entities
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

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary>Имя пользователя в телеграмм</summary>
        public string? TelegramUserName { get; set; }

        /// <summary>Подтвержден ли аккаунт в телеграмм</summary>
        public bool TelegramUserNameConfirmed { get; set; }

        /// <summary>Роли пользователя</summary>
        public required string Role { get; set; }
    }
}
