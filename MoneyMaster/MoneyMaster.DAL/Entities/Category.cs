using MoneyMaster.DAL.Entities.Base;
using MoneyMaster.DAL.Enums;

namespace MoneyMaster.DAL.Entities
{
    /// <summary>Категория</summary>
    public class Category: NamedTimedEntity
    {
        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary></summary>
        public bool IsSystem { get; set; }

        /// <summary>Тип транзакции</summary>
        public TransactionType Type { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }
    }
}
