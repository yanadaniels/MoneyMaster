using AutoMapper;
using MoneyMasterService.Services.Contracts.AccountType;
using MoneyMasterService.WebAPI.Models.AccountType;

namespace MoneyMasterService.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности типа счетов.</summary>
    public class AccountTypeModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountTypeModelMappingsProfile"/> </summary>
        public AccountTypeModelMappingsProfile()
        {
            CreateMap<AccountTypeDto, AccountTypeModel>();
        }
    }
}
