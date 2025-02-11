// Ignore Spelling: Dto

using MoneyMasterService.Domain.Entities.Enums;
using MoneyMasterService.Services.Contracts.Account;

namespace MoneyMasterService.Services.Contracts.Report
{
    /// <summary>DTO отчета</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class ReportDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Идентификатор счета</summary>
        public TKey? AccountId { get; set; }

        /// <summary>Dto счета</summary>
        public required AccountDto Account { get; set; }

        /// <summary>Тип отчета</summary>
        public ReportType Type { get; set; }

        /// <summary>Отчетные данные</summary>
        public string? ReportData { get; set; }

        /// <summary>Путь к файлу</summary>
        public string? FilePath { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO отчета</summary>
    public class ReportDto: ReportDto<Guid>;
}
