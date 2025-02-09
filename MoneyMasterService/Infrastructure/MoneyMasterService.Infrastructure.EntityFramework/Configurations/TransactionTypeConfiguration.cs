using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Infrastructure.EntityFramework.Configurations
{
    public class TransactionTypeConfiguration : IEntityTypeConfiguration<TransactionType>
    {
        public void Configure(EntityTypeBuilder<TransactionType> builder)
        {
            builder.ToTable("TransactionTypes").HasKey(x => x.Id);

            //Связь с таблицей Transaction один к многим
            builder.HasMany(p => p.Transactions)
                .WithOne(x => x.TransactionType)
                .HasForeignKey(x => x.TransactionTypeId);

            //Связь с таблицей Category один к многим
            builder.HasMany(p => p.Categories)
                .WithOne(x => x.TransactionType)
                .HasForeignKey(x => x.TransactionTypeId);
        }
    }
}
