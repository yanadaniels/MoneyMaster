// Ignore Spelling: Dto

using MoneyMaster.Domain.Entities.Enums;
using MoneyMaster.Services.Contracts.Account;

namespace MoneyMaster.Services.Contracts.Report
{
    /// <summary>DTO отчета</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class ReportDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Идентификатор Аккаунта</summary>
        public TKey? AccountId { get; set; }

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

    /// <summary><inheritdoc/></summary>
    public class ReportDto: ReportDto<Guid>;
}
