namespace IdentityService.Services.Contracts.User
{
    /// <summary>
    /// DTO для обновления данных пользователя
    /// </summary>
    public record class UserUpdateDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Имя пользователя в телеграмм
        /// </summary>
        public string? TelegramUserName { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public string Role { get; set; }
    }
}
