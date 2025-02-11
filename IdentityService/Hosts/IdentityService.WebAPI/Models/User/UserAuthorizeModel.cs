namespace IdentityService.WebAPI.Models.User
{
    /// <summary>Модель авторизации пользователя</summary>
    public class UserAuthorizeModel
    {
        /// <summary>Имя пользователя</summary>
        public required string UserName { get; set; }

        /// <summary>Пароль</summary>
        public required string PasswordHash { get; set; }
    }
}
