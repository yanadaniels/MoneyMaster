namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель создания пользователя</summary>
    public class CreatingUserModel
    {
        /// <summary>Идентификатор </summary>
        public Guid Id { get; set; }

        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }

        /// <summary>Имя пользователя в телеграмм</summary>
        public string? TelegramUserName { get; set; }
    }
}
