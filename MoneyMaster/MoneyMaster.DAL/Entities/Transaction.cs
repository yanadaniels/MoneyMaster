using MoneyMaster.DAL.Entities.Base;
using MoneyMaster.DAL.Enums;

namespace MoneyMaster.DAL.Entities
{
    public class Transaction<TKey> : TimedEntity<TKey>
    {
        public decimal Amount { get; set; }

        public TKey? CategoryId { get; set; }

        public string? Description { get; set; }

        public TransactionType Type { get; set; }

        public TKey? UserId { get; set; }
    }

    public class Transaction : Transaction<Guid> { }

}
