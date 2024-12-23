using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Repositories;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations
{
    /// <summary><inheritdoc cref="IUnitOfWork"/></summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSettingRepository _userSettingRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountTypeRepository _accountTypeRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReportRepository _reportRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionTypeRepository _transactionTypeRepository;

        private readonly MoneyMasterContext _context;

        /// <summary><inheritdoc cref="IUserRepository"/></summary>
        public IUserRepository UserRepository => _userRepository;

        /// <summary><inheritdoc cref="IUserSettingRepository"/></summary>
        public IUserSettingRepository UserSettingRepository => _userSettingRepository;

        /// <summary><inheritdoc cref="IAccountTypeRepository"/></summary>
        public IAccountTypeRepository AccountTypeRepository => _accountTypeRepository;

        /// <summary><inheritdoc cref="IAccountRepository"/></summary>
        public IAccountRepository AccountRepository => _accountRepository;

        /// <summary><inheritdoc cref="ICategoryRepository"/></summary>
        public ICategoryRepository CategoryRepository => _categoryRepository;

        /// <summary><inheritdoc cref="ITransactionRepository"/></summary>
        public ITransactionRepository TransactionRepository => _transactionRepository;

        /// <summary><inheritdoc cref="IReportRepository"/></summary>
        public IReportRepository ReportRepository => _reportRepository;

        /// <summary><inheritdoc cref="ITransactionTypeRepository"/></summary>
        public ITransactionTypeRepository TransactionTypeRepository => _transactionTypeRepository;

        /// <summary><inheritdoc cref="IUnitOfWork"/></summary>
        /// <param name="context">Контекст БД</param>
        public UnitOfWork(MoneyMasterContext context)
        {
            _context = context;
            _userSettingRepository = new UserSettingRepository(context);
            _userRepository = new UserRepository(context);
            _accountRepository = new AccountRepository(context);
            _categoryRepository = new CategoryRepository(context);
            _accountTypeRepository = new AccountTypeRepository(context);
            _transactionRepository = new TransactionRepository(context);
            _transactionTypeRepository = new TransactionTypeRepository(context);
            _reportRepository = new ReportRepository(context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
