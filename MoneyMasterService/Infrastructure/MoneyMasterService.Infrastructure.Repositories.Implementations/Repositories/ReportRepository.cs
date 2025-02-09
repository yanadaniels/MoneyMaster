using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IReportRepository"/></summary>
    public class ReportRepository : Repository<Report, Guid>, IReportRepository
    {
        /// <summary><inheritdoc cref="IReportRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public ReportRepository(MoneyMasterServiceContext context) : base(context) { }
    }
}
