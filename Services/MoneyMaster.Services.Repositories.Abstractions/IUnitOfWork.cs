namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// UOW.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary><inheritdoc cref="IAccountRepository"/></summary>
        IAccountRepository AccountRepository { get; }

        /// <summary><inheritdoc cref="IAccountTypeRepository"/></summary>
        IAccountTypeRepository AccountTypeRepository { get; }

        /// <summary><inheritdoc cref="ICategoryRepository"/></summary>
        ICategoryRepository CategoryRepository { get; }

        /// <summary><inheritdoc cref="IReportRepository"/></summary>
        IReportRepository ReportRepository { get; }

        /// <summary><inheritdoc cref="ITransactionRepository"/></summary>
        ITransactionRepository TransactionRepository { get; }

        /// <summary><inheritdoc cref="IUserRepository"/></summary>
        IUserRepository UserRepository { get; }

        /// <summary><inheritdoc cref="IUserSettingRepository"/></summary>
        IUserSettingRepository UserSettingRepository { get; }

        /// <summary>Сохранить изменения</summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
