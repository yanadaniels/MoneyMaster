namespace MoneyMaster.Domain.Entities.Entities
{
    /// <summary>Тип транзакции</summary>
    public class TransactionType<TKey> : NamedTimedEntity<TKey>
    {
        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary></summary>
        public bool IsSystem { get; set; }

        /// <summary>Коллекция категорий</summary>
        public ICollection<Category>? Categories { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<Transaction>? Transactions { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class TransactionType : TransactionType<Guid> { }
}
