using MoneyMaster.Common.Entities;
using MoneyMaster.Common.Interfaces.Entities;
using MoneyMasterService.Domain.Entities.Enums;

namespace MoneyMasterService.Domain.Entities
{
    /// <summary>Отчет</summary>
    public class Report<TKey> : NamedTimedEntity, ISoftDeletable
    {
        /// <summary>Идентификатор счета</summary>
        public TKey? AccountId { get; set; }

        /// <summary><inheritdoc cref="Domain.Entities.Account"/></summary>
        public Account? Account { get; set; }

        /// <summary>Тип отчета</summary>
        public ReportType Type { get; set; }

        /// <summary>Отчетные данные</summary>
        public string? ReportData { get; set; }

        /// <summary>Путь к файлу</summary>
        public string? FilePath { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }
    }
    /// <summary><inheritdoc/></summary>
    public class Report : Report<Guid> { }
}
