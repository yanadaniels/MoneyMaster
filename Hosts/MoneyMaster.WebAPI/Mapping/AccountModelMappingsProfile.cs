using AutoMapper;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.WebAPI.Models.Account;

namespace MoneyMaster.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности счетов.</summary>
    public class AccountModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountModelMappingsProfile"/> </summary>
        public AccountModelMappingsProfile()
        {
            CreateMap<AccountDto, AccountModel>();
        }
    }
}
