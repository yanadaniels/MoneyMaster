using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с отчетами
    /// </summary>
    public interface IReportRepository: IRepository<Report, Guid>
    {
    }
}
