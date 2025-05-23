﻿using MoneyMasterService.Services.Contracts.Report;

namespace MoneyMasterService.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с отчетами</summary>
    public interface IReportService
    {
        /// <summary>
        /// Получить отчет по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО отчета. </returns>
        Task<ReportDto> GetByIdAsync(Guid id);

        /// <summary>
        /// Получить список отчетов.
        /// </summary>
        /// <returns> Список DTO отчетов. </returns>
        Task<ICollection<ReportDto>> GetAllAsync();
    }
}
