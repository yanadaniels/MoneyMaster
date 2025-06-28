using FluentValidation;
using IdentityService.WebAPI.Models.User;

namespace IdentityService.WebAPI.Validation.User
{
    /// <summary>
    /// Валидатор для авторизации пользователя.
    /// Проверяет, что имя пользователя и пароль не пустые.
    /// </summary>
    public class UserAuthorizeValidation : AbstractValidator<UserAuthorizeModelRequest>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="UserAuthorizeValidation"/>.
        /// Устанавливает правила валидации для имени пользователя и пароля.
        /// </summary>
        public UserAuthorizeValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email не может быть пустым");


            RuleFor(p => p.Password).NotEmpty().WithMessage("Ваш пароль не может быть пустым");
        }
    }
}
