// Ignore Spelling: Registrator

using AutoMapper;
using MoneyMasterService.Services.Implementations.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyMasterService.Services.Implementations.Service
{
    public static class MapperRegistrator
    {
        public static IServiceCollection AddMapper(this IServiceCollection services) =>
            services
            .AddSingleton<IMapper>(new Mapper(MapperConfig.GetMapperConfiguration()));
    }
}
