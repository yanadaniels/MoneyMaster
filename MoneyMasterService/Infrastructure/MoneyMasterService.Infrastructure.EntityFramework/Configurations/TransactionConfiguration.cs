using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы транзакций</summary>
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions").HasKey(x => x.Id);
        }
    }
}
