using MoneyMaster.Domain.Entities.Entities;

namespace MoneyMaster.Domain.Entities
{
    /// <summary>Транзакция</summary>
    public class Transaction<TKey> : TimedEntity<TKey>
    {
        /// <summary>Количество</summary>
        public decimal Amount { get; set; }

        /// <summary>Идентификатор категории</summary>
        public TKey? CategoryId { get; set; }

        /// <summary><inheritdoc cref="MoneyMaster.Domain.Entities.Category"/></summary>
        public required Category Category { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Идентификатор типа транзакции</summary>
        public TKey? TransactionTypeId { get; set; }

        /// <summary><inheritdoc cref="TransactionType{T}"/></summary>
        public required TransactionType TransactionType { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary>Идентификатор счета </summary>
        public TKey? AccountId { get; set; }

        /// <summary><inheritdoc cref="MoneyMaster.Domain.Entities.Account"/></summary>
        public required Account Account { get; set; }
    }

    /// <summary> <inheritdoc/></summary>
    public class Transaction : Transaction<Guid> { }


}
