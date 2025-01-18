using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности счетов.</summary>
    public class AccountMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="AccountMappingsProfile"/> </summary>
        public AccountMappingsProfile()
        {
            CreateMap<Account, AccountDto>();
            CreateMap<AccountDto, Account>();
            CreateMap<UpdatingAccountDto, Account>();

            CreateMap<CreatingAccountDto, Account>()
                .ForMember(d => d.Id, map => map.Ignore())
                .ForMember(d => d.IsDelete, map => map.Ignore())
                .ForMember(d => d.AccountType, map => map.Ignore())
                .ForMember(d => d.Reports, map => map.Ignore())
                .ForMember(d => d.Transactions, map => map.Ignore())
                .ForMember(d => d.User, map => map.Ignore());
        }
    }
}
