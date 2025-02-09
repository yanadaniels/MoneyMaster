using AutoMapper;
using MoneyMasterService.Services.Contracts.User;
using MoneyMasterService.WebAPI.Models.User;

namespace MoneyMasterService.WebAPI.Mapping
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
