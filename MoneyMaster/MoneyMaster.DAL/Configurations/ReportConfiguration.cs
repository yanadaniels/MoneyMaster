using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.DAL.Entities;

namespace MoneyMaster.DAL.Configurations
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
