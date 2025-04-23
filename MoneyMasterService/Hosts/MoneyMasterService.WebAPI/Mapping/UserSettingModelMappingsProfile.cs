using AutoMapper;
using MoneyMasterService.Services.Contracts.UserSetting;
using MoneyMasterService.WebAPI.Models.UserSetting;

namespace MoneyMasterService.WebAPI.Mapping
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
