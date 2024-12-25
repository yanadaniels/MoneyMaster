using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности пользователя</summary>
    public class UserMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="UserMappingsProfile"/> </summary>
        public UserMappingsProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreatingUserDto, User>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.IsDelete, map => map.Ignore())
                .ForMember(d => d.Accounts, map => map.Ignore())
                .ForMember(d => d.UserSetting, map => map.Ignore());
        }
    }
}
