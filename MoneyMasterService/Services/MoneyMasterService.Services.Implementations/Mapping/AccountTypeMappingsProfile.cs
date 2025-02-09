using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.AccountType;

namespace MoneyMasterService.Services.Implementations.Mapping
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
