using AutoMapper;
using MoneyMaster.Services.Implementations.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyMaster.Services.Implementations.Service
{
    public static class MapperRegistrator
    {
        public static IServiceCollection AddMapper(this IServiceCollection services) =>
            services
            .AddSingleton<IMapper>(new Mapper(MapperConfig.GetMapperConfiguration()));
    }
}
