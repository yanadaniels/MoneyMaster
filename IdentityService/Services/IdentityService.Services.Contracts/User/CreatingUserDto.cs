// Ignore Spelling: Dto

namespace IdentityService.Services.Contracts.User
{
    /// <summary>Dto создания пользователя</summary>
    public class CreatingUserDto
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        /// <summary>Имя пользователя в телеграмм</summary>
        public string? TelegramUserName { get; set; }

        /// <summary>Роли пользователя</summary>
        public string Role { get; set; } = "User";

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
