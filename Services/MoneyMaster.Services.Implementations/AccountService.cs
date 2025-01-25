using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Domain.Entities.Enums;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.Services.Contracts.Common;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Services.Implementations
{
    /// <summary>Сервис работы с счетами</summary>
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IMapper _mapper;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;

        /// <summary><inheritdoc cref="AccountService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="accountRepository">Репозиторий</param>
        public AccountService(ILogger<AccountService> logger, IMapper mapper, IAccountRepository accountRepository, IAccountTypeRepository accountTypeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _accountRepository = accountRepository;
            _accountTypeRepository = accountTypeRepository;
        }

        public async Task<CreatingAccountInfoDto> GetInfoNeedToCreateAccountAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var accountTypes = await _accountTypeRepository.GetAllAsync(cancellationToken);
                var currencies = Enum.GetNames(typeof(CurrencyCode)).ToList();

                var creatingInfo = new CreatingAccountInfoDto()
                {
                    AccountTypes = _mapper.Map<List<AccountTypeDto>>(accountTypes),
                    CurrentCode = currencies
                };

                return creatingInfo;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при доступе к базе данных во время получения данных необходимых для создания нового счета.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {MethodName}.", nameof(GetInfoNeedToCreateAccountAsync));
                throw;
            }
        }

        public async Task<(ICollection<AccountDto> Data, int TotalCount)> GetAllAsync(
            PaginationParameters parameters, 
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _accountRepository.GetAllAsync(parameters, cancellationToken);
                var accountList = _mapper.Map<List<Account>, ICollection<AccountDto>>(result.Items);
                return (accountList, result.TotalCount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при доступе к базе данных во время получения всеx счетов.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {GetAllAsync}.", nameof(GetAllAsync));
                throw;
            }
        }

        public async Task<(ICollection<AccountDto> Data, int TotalCount)> GetAllDeletedAsync(
            PaginationParameters parameters,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _accountRepository.GetAllDeletedAsync(parameters, cancellationToken);
                var deletedAccountList = _mapper.Map<List<Account>, ICollection<AccountDto>>(result.Items);
                return (deletedAccountList, result.TotalCount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при доступе к базе данных во время получения всеx удаленных счетов.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {GetAllDeletedAsync}.", nameof(GetAllDeletedAsync));
                throw;
            }
        }

        public async Task<AccountDto?> GetByIdAsync(Guid id)
        {
            try
            {
                var account = await _accountRepository.GetAsync(id, CancellationToken.None);
                return account is null ? null : _mapper.Map<Account, AccountDto>(account);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при доступе к базе данных во время получения счетов по ID: {id}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {GetByIdAsync}.", nameof(GetByIdAsync));
                throw;
            }
        }

        public async Task<AccountDto> AddAsync(
            CreatingAccountDto newAccountDto, CancellationToken cancellationToken = default)
        {
            if (newAccountDto is null)
            {
                _logger.LogError($"Попытка передать null в аргумент 'newAccountDto' при создании нового счета: {newAccountDto}");
                throw new ArgumentNullException(nameof(newAccountDto), "Переданный объект account не может быть null.");
            }

            var newAccount = _mapper.Map<Account>(newAccountDto);

            try
            {
                var createdAccount = await _accountRepository.AddAsync(newAccount);
                await _accountRepository.SaveChangesAsync();

                return _mapper.Map<AccountDto>(createdAccount);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении счета в базе данных.");
                throw new InvalidOperationException("Не удалось создать новый счет. Проверьте данные и попробуйте снова.");
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {AddAsync}.", nameof(AddAsync));
                throw;
            }
        }

        public async Task<UpdatingAccountDto?> UpdateAsync(
            UpdatingAccountDto updateAccountDto, CancellationToken cancellationToken = default)
        {
            var updateAccount = _mapper.Map<Account>(updateAccountDto);

            try
            {
                _accountRepository.Update(updateAccount);
                await _accountRepository.SaveChangesAsync();
                return updateAccountDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении счета в базе данных");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {UpdateAsync}.", nameof(UpdateAsync));
                throw;
            }

        }

        public async Task<AccountDto?> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var deletedAccount = await _accountRepository.SoftDeleteAsync(id, cancellationToken);
                return deletedAccount is null ? null : _mapper.Map<AccountDto>(deletedAccount);
              
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Мягкое удаление не поддерживается для типа {typeof(Account).Name} in {nameof(DeleteAsync)}.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при доступе к базе данных во время удаления счета");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {DeleteAsync}.", nameof(DeleteAsync));
                throw;
            }
        }

        public async Task<AccountDto?> RestoreAsync(Guid id, CancellationToken cancellationToken = default)
        {
            try
            {
                var restoredAccount = await _accountRepository.RestoreAsync(id, cancellationToken);
                return restoredAccount is null ? null : _mapper.Map<AccountDto>(restoredAccount);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Восстановление не поддерживается для типа {typeof(Account).Name} в методе {nameof(RestoreAsync)}.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при доступе к базе данных во время восстановления счета");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла ошибка при восстановлении счета");
                throw;
            }
        }
    }
}
