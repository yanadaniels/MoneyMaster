using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Services.Implementations
{
    /// <summary>Сервис работы с аккаунтом</summary>
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;

        /// <summary><inheritdoc cref="AccountService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="accountRepository">Репозиторий</param>
        public AccountService(IMapper mapper, IAccountRepository accountRepository)
        {
            _mapper = mapper;
            _accountRepository = accountRepository;
        }

        public async Task<ICollection<AccountDto>> GetAllAsync()
        {
            ICollection<Account> entities = _accountRepository.GetAll().ToList();
            return _mapper.Map<ICollection<Account>, ICollection<AccountDto>>(entities);
        }

        public async Task<AccountDto> GetByIdAsync(Guid id)
        {
            var account = await _accountRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Account, AccountDto>(account);
        }
    }
}
