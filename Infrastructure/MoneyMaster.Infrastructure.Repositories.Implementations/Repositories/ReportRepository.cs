using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IReportRepository"/></summary>
    public class ReportRepository : Repository<Report, Guid>, IReportRepository
    {
        /// <summary><inheritdoc cref="IReportRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public ReportRepository(MoneyMasterContext context) : base(context) { }
    }
}
