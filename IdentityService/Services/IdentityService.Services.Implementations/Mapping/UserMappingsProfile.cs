using AutoMapper;
using IdentityService.Domain.Entities;
using IdentityService.Services.Contracts.User;

namespace IdentityService.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности пользователя</summary>
    public class UserMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="UserMappingsProfile"/> </summary>
        public UserMappingsProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreatingUserDto, User>()
                .ForMember(d => d.IsDelete, map => map.Ignore())
                ;
        }
    }
}
