namespace MoneyMaster.Domain.Entities
{
    /// <summary>Аккаунт</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class Account<TKey> : NamedTimedEntity<TKey>
    {
        /// <summary>Баланс</summary>
        public decimal Balance { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        public TKey? UserId { get; set; }

        /// <summary>Пользователь</summary>
        public User? User { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary>Идентификатор типа учетной записи</summary>
        public TKey? AccountTypeId { get; set; }

        /// <summary>Тип учетной записи</summary>
        public AccountType? AccountType { get; set; }

        /// <summary>Коллекция отчетов </summary>
        public ICollection<Report>? Reports { get; set; }

        /// <summary>Коллекция транзакций </summary>
        public ICollection<Transaction>? Transactions { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class Account : Account<Guid> { }
}
