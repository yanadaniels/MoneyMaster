using IdentityService.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Services.Implementations.Service
{
    public static class ServiceRegistrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services) =>
            services
            .AddTransient<IUserService, UserService>()
            ;

    }
}
