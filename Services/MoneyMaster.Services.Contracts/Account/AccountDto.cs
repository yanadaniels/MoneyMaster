// Ignore Spelling: Dto

using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.Services.Contracts.Report;
using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Contracts.Account
{
    /// <summary>DTO аккаунта </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class AccountDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Баланс</summary>
        public decimal Balance { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Пользователь</summary>
        public UserDto? User { get; set; }

        /// <summary>Идентификатор типа учетной записи</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary>Тип учетной записи</summary>
        public AccountTypeDto? AccountType { get; set; }

        /// <summary>Коллекция отчетов </summary>
        public ICollection<ReportDto>? Reports { get; set; }

        /// <summary>Коллекция транзакций </summary>
        public ICollection<TransactionDto>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class AccountDto:AccountDto<Guid>;
}
