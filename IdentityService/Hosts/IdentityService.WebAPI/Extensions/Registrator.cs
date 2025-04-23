// Ignore Spelling: Registrator

using FluentValidation;
using IdentityService.WebAPI.Models.User;
using IdentityService.WebAPI.Validation.User;

namespace IdentityService.WebAPI.Extensions
{
    /// <summary>
    /// Класс для регистрации сервисов в контейнере зависимостей
    /// </summary>
    public static class Registrator
    {
        public static IServiceCollection AddValidation(this IServiceCollection services) => services
            .AddScoped<IValidator<CreatingUserModelRequest>,CreatingUserValidation>()
            .AddScoped<IValidator<UserAuthorizeModelRequest>, UserAuthorizeValidation>()
            .AddScoped<IValidator<UserUpdateModelRequest>, UserUpdateValidation>();

    }
}
