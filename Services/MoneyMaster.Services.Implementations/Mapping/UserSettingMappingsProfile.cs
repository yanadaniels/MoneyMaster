using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.UserSetting;

namespace MoneyMaster.Services.Implementations.Mapping
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
