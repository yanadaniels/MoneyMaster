using MoneyMaster.DAL.Entities.Base;

namespace MoneyMaster.DAL.Entities
{
    public  class AccountType: NamedEntity
    {
        public string? Icon { get; set; }

        public bool IsSystem { get; set; }
    }
}
