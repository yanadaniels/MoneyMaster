using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Account;

namespace MoneyMasterService.Services.Implementations.Mapping
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
                .ForMember(account => account.Transactions, memberConfiguration => memberConfiguration.Ignore())
                ;
        }
    }
}
