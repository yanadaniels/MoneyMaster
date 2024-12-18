using MoneyMaster.Domain.Entities.Enums;

namespace MoneyMaster.Domain.Entities
{
    /// <summary>Транзакция</summary>
    public class Transaction<TKey> : TimedEntity<TKey>
    {
        /// <summary>Количество</summary>
        public decimal Amount { get; set; }

        /// <summary>Идентификатор категории</summary>
        public TKey? CategoryId { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Тип транзакции</summary>
        public TransactionType Type { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary>Идентификатор пользователя</summary>
        //public TKey? UserId { get; set; }
    }

    /// <summary> <inheritdoc/></summary>
    public class Transaction : Transaction<Guid> { }

}
