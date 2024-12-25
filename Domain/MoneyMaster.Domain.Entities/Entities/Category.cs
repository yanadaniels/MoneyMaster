using MoneyMaster.Domain.Entities.Entities;
using MoneyMaster.Domain.Entities.Enums;

namespace MoneyMaster.Domain.Entities
{
    /// <summary>Категория</summary>
    public class Category<TKey> : NamedTimedEntity<TKey>
    {
        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary></summary>
        public bool IsSystem { get; set; }

        /// <summary>Идентификатор типа транзакции</summary>
        public TKey? TransactionTypeId { get; set; }

        /// <summary><inheritdoc cref="TransactionType{T}"/></summary>
        public required TransactionType TransactionType { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<Transaction>? Transactions { get; set; }
    }

    /// <summary><inheritdoc/> </summary>
    public class Category : Category<Guid> { }
}
