// Ignore Spelling: Registrator

using FluentValidation;
using IdentityService.WebAPI.Models.User;
using IdentityService.WebAPI.Validation.User;

namespace IdentityService.WebAPI.Extensions
{
    public static class Registrator
    {
        public static IServiceCollection AddValidation(this IServiceCollection services) => services
            .AddScoped<IValidator<CreatingUserModel>,CreatingUserValidation>()
            .AddScoped<IValidator<UserAuthorizeModel>, UserAuthorizeValidation>()
            .AddScoped<IValidator<UpdateUserModel>, UpdateUserValidation>()
            ;

    }
}
