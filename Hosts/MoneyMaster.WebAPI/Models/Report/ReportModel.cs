using MoneyMaster.Domain.Entities.Enums;
using MoneyMaster.WebAPI.Models.Account;

namespace MoneyMaster.WebAPI.Models.Report
{
    /// <summary>Модель отчета</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class ReportModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Идентификатор счета</summary>
        public TKey? AccountId { get; set; }

        /// <summary>Модель счета</summary>
        public required AccountModel Account { get; set; }

        /// <summary>Тип отчета</summary>
        public ReportType Type { get; set; }

        /// <summary>Отчетные данные</summary>
        public string? ReportData { get; set; }

        /// <summary>Путь к файлу</summary>
        public string? FilePath { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель отчета</summary>
    public class ReportModel : ReportModel<Guid>;
}
