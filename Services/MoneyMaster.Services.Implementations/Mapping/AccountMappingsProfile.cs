using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.Account;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности счетов.</summary>
    public class AccountMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountMappingsProfile"/> </summary>
        public AccountMappingsProfile()
        {
            CreateMap<Account, AccountDto>();
        }
    }
}
