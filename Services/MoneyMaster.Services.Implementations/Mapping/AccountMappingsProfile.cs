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
            CreateMap<AccountDto, Account>();
            CreateMap<UpdatingAccountDto, Account>();

            CreateMap<CreatingAccountDto, Account>()
                .ForMember(account => account.Id, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(account => account.IsDeleted, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(account => account.AccountType, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(account => account.Reports, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(account => account.Transactions, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(account => account.User, memberConfiguration => memberConfiguration.Ignore());
        }
    }
}
