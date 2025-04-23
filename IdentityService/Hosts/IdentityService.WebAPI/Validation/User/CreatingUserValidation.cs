using FluentValidation;
using IdentityService.WebAPI.Models.User;

namespace IdentityService.WebAPI.Validation.User
{
    /// <summary>
    /// Валидатор для создания нового пользователя.
    /// Проверяет корректность введенных данных: email, имя пользователя и пароль.
    /// </summary>
    public class CreatingUserValidation : AbstractValidator<CreatingUserModelRequest>
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CreatingUserValidation"/>.
        /// Устанавливает правила валидации для email, имени пользователя и пароля.
        /// </summary>
        public CreatingUserValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Требуется адрес электронной почты.")
                .EmailAddress().WithMessage("Требуется действительный адрес электронной почты.");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Имя пользователя не может быть пустым")
                .MinimumLength(2).WithMessage("Имя пользователя не должна быть менее 2 символов.")
                .MaximumLength(16).WithMessage("Имя пользователя не должна превышать 16 символов.")
                ;


            RuleFor(p => p.Password).NotEmpty().WithMessage("Ваш пароль не может быть пустым")
                    .MinimumLength(8).WithMessage("Длина вашего пароля должна быть не менее 8.")
                    .MaximumLength(16).WithMessage("Длина вашего пароля не должна превышать 16.")
                    .Matches(@"[A-Z]+").WithMessage("Ваш пароль должен содержать хотя бы одну заглавную букву.")
                    .Matches(@"[a-z]+").WithMessage("Ваш пароль должен содержать хотя бы одну строчную букву.")
                    .Matches(@"[0-9]+").WithMessage("Ваш пароль должен содержать хотя бы одну цифру.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Ваш пароль должен содержать хотя бы один символ (!? *.).");
        }
    }
}
