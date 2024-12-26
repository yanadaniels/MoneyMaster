using MoneyMaster.WebAPI.Models.AccountType;
using MoneyMaster.WebAPI.Models.Report;
using MoneyMaster.WebAPI.Models.Transaction;
using MoneyMaster.WebAPI.Models.User;

namespace MoneyMaster.WebAPI.Models.Account
{
    /// <summary>Модель счета </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class AccountModel<TKey>
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
        public UserModel? User { get; set; }

        /// <summary>Идентификатор типа счета</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary>Тип счета</summary>
        public AccountTypeModel? AccountType { get; set; }

        /// <summary>Коллекция отчетов </summary>
        public ICollection<ReportModel>? Reports { get; set; }

        /// <summary>Коллекция транзакций </summary>
        public ICollection<TransactionModel>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель счета </summary>
    public class AccountModel : AccountModel<Guid>;
}
