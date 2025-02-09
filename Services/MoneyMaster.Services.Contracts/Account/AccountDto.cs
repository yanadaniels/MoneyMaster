// Ignore Spelling: Dto

using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.Services.Contracts.Report;
using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Contracts.Account
{
    /// <summary>DTO счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public record AccountDto<TKey>
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

        /// <summary>Идентификатор типа счета</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary>Тип счета</summary>
        public AccountTypeDto? AccountType { get; set; }

        /// <summary>Коллекция отчетов </summary>
        public ICollection<ReportDto>? Reports { get; set; }

        /// <summary>Коллекция транзакций </summary>
        public ICollection<TransactionResponse>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO счета </summary>
    public record AccountDto:AccountDto<Guid>;
}
