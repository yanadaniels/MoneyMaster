using MoneyMaster.Domain.Entities.Enums;

namespace MoneyMaster.Domain.Entities
{
    /// <summary>Отчет</summary>
    public class Report<TKey> : NamedTimedEntity
    {
        /// <summary>Идентификатор Аккаунта</summary>
        public TKey? AccountId { get; set; }

        public required Account Account { get; set; }

        /// <summary>Тип отчета</summary>
        public ReportType Type { get; set; }

        /// <summary>Отчетные данные</summary>
        public string? ReportData { get; set; }

        /// <summary>Путь к файлу</summary>
        public string? FilePath { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }
    }
    /// <summary><inheritdoc/></summary>
    public class Report : Report<Guid> { }
}
