// Ignore Spelling: Dto

namespace IdentityService.Services.Contracts.User
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
 
        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }

        /// <summary>Имя пользователя в телеграмм</summary>
        public string? TelegramUserName { get; set; }

        /// <summary>Роли пользователя</summary>
        public required string Role { get; set; }
    }

    /// <summary>DTO пользователя</summary>
    public class UserDto : UserDto<Guid>;
}
