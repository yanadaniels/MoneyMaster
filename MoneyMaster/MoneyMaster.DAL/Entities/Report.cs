using MoneyMaster.DAL.Entities.Base;
using MoneyMaster.DAL.Enums;

namespace MoneyMaster.DAL.Entities
{
    /// <summary>Отчет</summary>
    public class Report<TKey> : NamedTimedEntity
    {
        /// <summary>Идентификатор пользователя</summary>
        //public TKey? UserId { get; set; }

        /// <summary>Тип отчета</summary>
        public ReportType Type { get; set; }

        /// <summary>Отчетные данные</summary>
        public string? ReportData { get; set; }

        /// <summary>Путь к файлу</summary>
        public string? FilePath { get; set; }

        /// <summary>Мягкое удаление</summary>
        public bool IsDelete { get; set; }
    }

    public class Report: Report<Guid> { }
}
