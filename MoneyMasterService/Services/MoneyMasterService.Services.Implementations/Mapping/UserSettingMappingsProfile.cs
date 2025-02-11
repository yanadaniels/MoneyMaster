using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.UserSetting;

namespace MoneyMasterService.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности настройки пользователя</summary>
    public class UserSettingMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="UserSettingMappingsProfile"/> </summary>
        public UserSettingMappingsProfile()
        {
            CreateMap<UserSetting, UserSettingDto>();
        }
    }
}
