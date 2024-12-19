using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы пользователей</summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(x => x.Id);

            //Связь с таблицей UserSetting один к одному
            builder.HasOne(x => x.Setting)
                   .WithOne(p => p.User)
                   .HasForeignKey<UserSetting>(p => p.UserId);

            //Связь с таблицей Account один к одному
            builder.HasOne(x => x.Account)
                   .WithOne(p => p.User)
                   .HasForeignKey<Account>(p => p.UserId);
        }
    }
}
