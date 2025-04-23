using AutoMapper;
using MoneyMasterService.Services.Contracts.Account;
using MoneyMasterService.WebAPI.Models.Account;

namespace MoneyMasterService.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности счетов.</summary>
    public class CreatingAccountInfoModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountModelMappingsProfile"/> </summary>
        public CreatingAccountInfoModelMappingsProfile()
        {
            CreateMap<CreatingAccountInfoDto, CreatingAccountInfoModelResponse>();
        }
    }
}
