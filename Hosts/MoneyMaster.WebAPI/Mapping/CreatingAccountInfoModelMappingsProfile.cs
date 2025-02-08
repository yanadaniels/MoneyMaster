using AutoMapper;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.WebAPI.Models.Account;

namespace MoneyMaster.WebAPI.Mapping
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
