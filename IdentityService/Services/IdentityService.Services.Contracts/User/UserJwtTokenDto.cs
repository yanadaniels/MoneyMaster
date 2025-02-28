// Ignore Spelling: Jwt Dto

namespace IdentityService.Services.Contracts.User
{
    /// <summary>
    /// DTO JWT токена
    /// </summary>
    public class UserJwtTokenDto
    {
        /// <summary>Первичный ключ </summary>
        public Guid Id { get; set; }

        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Имя пользователя в телеграмм</summary>
        public string? TelegramUserName { get; set; }

        /// <summary>Роли пользователя</summary>
        public required string Role { get; set; }

        /// <summary>Токе доступа</summary>
        public required string AccsessToken { get; set; }

        /// <summary>Токе для обновления токеда доступа</summary>
        public required string RefreshToken { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }

    }
}
