using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.DAL.Entities;

namespace MoneyMaster.DAL.Configurations
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
