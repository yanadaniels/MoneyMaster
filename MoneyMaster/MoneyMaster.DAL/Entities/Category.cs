using MoneyMaster.DAL.Entities.Base;
using MoneyMaster.DAL.Enums;

namespace MoneyMaster.DAL.Entities
{
    public class Category: NamedTimedEntity
    {
        public string? Icon { get; set; }

        public bool IsSystem { get; set; }

        public TransactionType Type { get; set; }
    }
}
