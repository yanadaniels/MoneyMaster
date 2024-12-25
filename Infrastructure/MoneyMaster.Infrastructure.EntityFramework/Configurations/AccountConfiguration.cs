using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы счетов</summary>
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts").HasKey(x => x.Id);

            //Связь с таблицей Report один к многим
            builder.HasMany(x => x.Reports)
                   .WithOne(x => x.Account)
                   .HasForeignKey(x => x.AccountId);

            //Связь с таблицей Transaction один к многим
            builder.HasMany(x => x.Transactions)
                   .WithOne(x => x.Account)
                   .HasForeignKey(x => x.AccountId);
        }
    }
}
