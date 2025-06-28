namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель авторизации пользователя</summary>
    public class UserAuthorizeModelRequest
    {
        /// <summary>Имя пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string Password { get; set; }
    }
}
