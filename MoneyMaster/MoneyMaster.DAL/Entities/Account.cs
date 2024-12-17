using MoneyMaster.DAL.Entities.Base;

namespace MoneyMaster.DAL.Entities
{
    public class Account<TKey>: NamedTimedEntity<Guid>
    {
        public decimal Balance { get; set; }

        public string? Currency { get; set; }

        public string? Icon { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }

        public TKey? UserId { get; set; }
        
        public AccountType? Type { get; set; }
    }
}
