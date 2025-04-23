using MoneyMaster.Common.Entities;
using MoneyMaster.Common.Interfaces.Entities;

namespace MoneyMasterService.Domain.Entities
{
    /// <summary>Транзакция</summary>
    public class Transaction<TKey> : TimedEntity<TKey>, ISoftDeletable
    {
        /// <summary>Количество</summary>
        public decimal Amount { get; set; }

        /// <summary>Идентификатор категории</summary>
        public TKey? CategoryId { get; set; }

        /// <summary><inheritdoc cref="MoneyMaster.Domain.Entities.Category"/></summary>
        public Category? Category { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }

        /// <summary>Идентификатор счета </summary>
        public TKey? AccountId { get; set; }

        /// <summary><inheritdoc cref="MoneyMaster.Domain.Entities.Account"/></summary>
        public Account? Account { get; set; }
    }

    /// <summary> <inheritdoc/></summary>
    public class Transaction : Transaction<Guid> { }


}
