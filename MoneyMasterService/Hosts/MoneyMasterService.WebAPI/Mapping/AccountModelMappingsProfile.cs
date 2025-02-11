using AutoMapper;
using MoneyMasterService.Services.Contracts.Account;
using MoneyMasterService.WebAPI.Models.Account;

namespace MoneyMasterService.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности счетов.</summary>
    public class AccountModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountModelMappingsProfile"/> </summary>
        public AccountModelMappingsProfile()
        {
            CreateMap<AccountDto, AccountModelResponse>();

            CreateMap<CreatingAccountModelRequest, CreatingAccountDto>();
            CreateMap<CreatingAccountDto, AccountModelResponse>();

            CreateMap<UpdatingAccountDto, AccountModelResponse>();

            CreateMap<UpdatingAccountModelRequest, UpdatingAccountDto>();

        }
    }
}
