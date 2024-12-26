using AutoMapper;
using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.WebAPI.Models.AccountType;

namespace MoneyMaster.WebAPI.Mapping
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
