using MoneyMaster.DAL.Entities.Base;
using MoneyMaster.DAL.Enums;

namespace MoneyMaster.DAL.Entities
{
    public class Report<TKey> : NamedTimedEntity
    {
        public TKey? UserId { get; set; }

        public ReportType Type { get; set; }

        public string? ReportData { get; set; }

        public string? FilePath { get; set; }
    }

    public class Report: Report<Guid> { }
}
