// Ignore Spelling: Dto

namespace IdentityService.Services.Contracts.User
{
    /// <summary>DTO авторизации пользователя</summary>
    public class UserAuthorizeDto
    {
        /// <summary>Имя пользователя</summary>
        public required string Email { get; set; }

        /// <summary>Пароль</summary>
        public required string Password { get; set; }
    }
}
