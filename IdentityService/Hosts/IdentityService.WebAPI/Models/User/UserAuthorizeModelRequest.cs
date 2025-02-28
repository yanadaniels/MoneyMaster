namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель авторизации пользователя</summary>
    public class UserAuthorizeModelRequest
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Пароль</summary>
        public required string Password { get; set; }
    }
}
