namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель создания пользователя</summary>
    public class CreatingUserModelRequest
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Почтовый адрес пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string Password { get; set; }
    }
}
