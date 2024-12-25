namespace MoneyMaster.Domain.Entities
{
    /// <summary>Тип счета </summary>
    public class AccountType : NamedEntity
    {
        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary></summary>
        public bool IsSystem { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<Account>? Accounts { get; set; }
    }
}
