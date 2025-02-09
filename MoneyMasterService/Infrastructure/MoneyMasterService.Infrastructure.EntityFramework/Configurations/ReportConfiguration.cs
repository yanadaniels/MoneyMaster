using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы отчетов</summary>
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports").HasKey(x => x.Id);
        }
    }
}
