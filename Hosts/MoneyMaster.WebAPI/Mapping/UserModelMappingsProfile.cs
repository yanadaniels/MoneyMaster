using AutoMapper;
using MoneyMaster.Services.Contracts.User;
using MoneyMaster.WebAPI.Models.User;

namespace MoneyMaster.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности пользователя</summary>
    public class UserModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="UserModelMappingsProfile"/> </summary>
        public UserModelMappingsProfile()
        {
            CreateMap<UserDto, UserModel>();

            CreateMap<CreatingUserModel, CreatingUserDto>();
            CreateMap<CreatingUserDto, CreatingUserModel>();
        }
    }
}
