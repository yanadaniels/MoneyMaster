using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;
using MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations
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

        private readonly MoneyMasterServiceContext _context;

        public IUserRepository UserRepository => _userRepository;

        public IUserSettingRepository UserSettingRepository => _userSettingRepository;

        public IAccountTypeRepository AccountTypeRepository => _accountTypeRepository;

        public IAccountRepository AccountRepository => _accountRepository;

        public ICategoryRepository CategoryRepository => _categoryRepository;

        public ITransactionRepository TransactionRepository => _transactionRepository;

        public IReportRepository ReportRepository => _reportRepository;

        public ITransactionTypeRepository TransactionTypeRepository => _transactionTypeRepository;

        /// <summary><inheritdoc cref="IUnitOfWork"/></summary>
        /// <param name="context">Контекст БД</param>
        public UnitOfWork(MoneyMasterServiceContext context)
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
