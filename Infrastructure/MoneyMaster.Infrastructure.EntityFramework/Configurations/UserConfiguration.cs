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
            builder.HasOne(x => x.UserSetting)
                   .WithOne(p => p.User)
                   .HasForeignKey<UserSetting>(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //Связь с таблицей Account один к многим
            builder.HasMany(x => x.Accounts)
                   .WithOne(x => x.User)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
