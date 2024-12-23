using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности типа учетной записи.</summary>
    public class AccountTypeMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountTypeMappingsProfile"/> </summary>
        public AccountTypeMappingsProfile()
        {
            CreateMap<AccountType, AccountTypeDto>();
        }
    }
}
