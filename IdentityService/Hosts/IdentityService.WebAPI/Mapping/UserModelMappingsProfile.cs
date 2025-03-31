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
            CreateMap<UserDto, UserModelResponse>();

            CreateMap<CreatingUserModelRequest, CreatingUserDto>();
            CreateMap<CreatingUserDto, CreatingUserModelRequest>();

            CreateMap<UserAuthorizeModelRequest, UserAuthorizeDto>().ReverseMap();

            CreateMap<RefreshTokenRequest, UserRefreshTokenDto>();
            CreateMap<UserRefreshJwtTokenDto, UserRefreshJwtTokenResponse>();

            CreateMap<UserJwtTokenDto, UserJwtTokenResponse>();

            CreateMap<UserUpdateModelRequest, UserUpdateDto>();
            CreateMap<UserUpdateDto, UserModelResponse>();
        }
    }
}
