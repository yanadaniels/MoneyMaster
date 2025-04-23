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
            CreateMap<UserDto, User>();

            CreateMap<CreatingUserDto, User>()
                .ForMember(d => d.IsDelete, map => map.Ignore());

            CreateMap<UserUpdateDto, User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
                {
                    var destType = typeof(User).GetProperty(opts.DestinationMember.Name)?.PropertyType;
                    return srcMember != null || (destType != null && Nullable.GetUnderlyingType(destType) != null);
                }));
        }
    }
}
