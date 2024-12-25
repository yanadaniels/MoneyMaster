using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.AccountType;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности типа счетов.</summary>
    public class AccountTypeMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountTypeMappingsProfile"/> </summary>
        public AccountTypeMappingsProfile()
        {
            CreateMap<AccountType, AccountTypeDto>();
        }
    }
}
