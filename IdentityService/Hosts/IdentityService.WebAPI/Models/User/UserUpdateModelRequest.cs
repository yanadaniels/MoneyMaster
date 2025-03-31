using System.Text.Json.Serialization;

namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель обновления пользователя</summary>
    public class UserUpdateModelRequest
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public required Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UserName { get; set; }

        /// <summary>
        /// Почтовый адрес пользователя
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; }

        /// <summary>
        /// Имя пользователя в телеграмм
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? TelegramUserName { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Role { get; set; }
    }
}
