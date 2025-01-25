namespace MoneyMaster.Domain.Entities.Entities
{
    /// <summary>Тип транзакции</summary>
    public class TransactionType<TKey> : NamedTimedEntity<TKey>, ISoftDeletable
    {
        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }

        /// <summary>Указывает на то что тип системный и его удалять нельзя</summary>
        public bool IsSystem { get; set; }

        /// <summary>Коллекция категорий</summary>
        public ICollection<Category>? Categories { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<Transaction>? Transactions { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class TransactionType : TransactionType<Guid> { }
}
