using AutoMapper;

namespace MoneyMasterService.WebAPI.Extensions
{
    public static class MapperModelRegistrator
    {
        public static IServiceCollection AddModelMapper(this IServiceCollection services) =>
            services
            .AddSingleton<IMapper>(new Mapper(MapperModelConfig.GetMapperModelConfiguration()));
    }
}
