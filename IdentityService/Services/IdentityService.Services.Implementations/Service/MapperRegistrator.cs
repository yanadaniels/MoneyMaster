// Ignore Spelling: Registrator

using AutoMapper;
using IdentityService.Services.Implementations.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Services.Implementations.Service
{
    public static class MapperRegistrator
    {
        public static IServiceCollection AddMapper(this IServiceCollection services) =>
            services
            .AddSingleton<IMapper>(new Mapper(MapperConfig.GetMapperConfiguration()));
    }
}
