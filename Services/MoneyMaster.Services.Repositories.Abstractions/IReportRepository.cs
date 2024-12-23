using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с отчетами
    /// </summary>
    public interface IReportRepository: IRepository<Report, Guid>
    {
    }
}
