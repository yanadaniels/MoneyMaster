namespace MoneyMaster.Domain.Entities
{
    /// <summary>Аккаунт</summary>
    public class Account<TKey> : NamedTimedEntity<Guid>
    {
        /// <summary>Баланс</summary>
        public decimal Balance { get; set; }

        /// <summary>Расходы</summary>
        public string? Currency { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Коллекция транзакций </summary>
        public ICollection<Transaction>? Transactions { get; set; }

        public TKey? UserId { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }


        public AccountType? Type { get; set; }
    }
}
