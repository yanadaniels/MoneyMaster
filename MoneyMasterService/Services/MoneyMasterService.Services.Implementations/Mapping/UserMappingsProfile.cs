using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.User;

namespace MoneyMasterService.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности пользователя</summary>
    public class UserMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="UserMappingsProfile"/> </summary>
        public UserMappingsProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<CreatingUserDto, User>()
                .ForMember(user => user.Id, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(user => user.IsDeleted, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(user => user.Accounts, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(user => user.UserSetting, memberConfiguration => memberConfiguration.Ignore());
        }
    }
}
