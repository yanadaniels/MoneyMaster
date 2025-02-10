using AutoMapper;
using IdentityService.Services.Contracts.User;
using IdentityService.WebAPI.Models.User;

namespace IdentityService.WebAPI.Mapping
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

            CreateMap<UserAuthorizeModel, UserAuthorizeDto>().ReverseMap();
        }
    }
}
