namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель пользователя</summary>
    public class UserModel 
    {
        /// <summary>Первичный ключ </summary>
        public Guid Id { get; set; }

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

    
}
