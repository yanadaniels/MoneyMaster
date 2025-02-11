using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы категорий</summary>
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories").HasKey(x => x.Id);

            //Связь с таблицей Transaction один к многим
            builder.HasMany(x => x.Transactions)
                   .WithOne(x => x.Category)
                   .HasForeignKey(x => x.CategoryId);
        }
    }
}
