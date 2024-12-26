using AutoMapper;
using MoneyMaster.Services.Contracts.UserSetting;
using MoneyMaster.WebAPI.Models.UserSetting;

namespace MoneyMaster.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности настройки пользователя</summary>
    public class UserSettingModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="UserSettingModelMappingsProfile"/> </summary>
        public UserSettingModelMappingsProfile()
        {
            CreateMap<UserSettingDto, UserSettingModel>();
        }
    }
}
