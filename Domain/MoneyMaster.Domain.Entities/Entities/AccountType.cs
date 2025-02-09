namespace MoneyMaster.Domain.Entities
{
    /// <summary>Тип счета </summary>
    public class AccountType : NamedEntity, ISoftDeletable
    {
        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Указывает на то что тип системный и его удалять нельзя</summary>
        public bool IsSystem { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }

        /// <summary>Коллекция счетов</summary>
        public ICollection<Account>? Accounts { get; set; }
    }
}
