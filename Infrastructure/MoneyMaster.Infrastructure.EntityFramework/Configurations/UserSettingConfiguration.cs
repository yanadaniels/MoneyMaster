using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Infrastructure.EntityFramework.Configurations
{
    /// <summary>Конфигурация для таблицы настроек пользователя</summary>
    public class UserSettingConfiguration : IEntityTypeConfiguration<UserSetting>
    {
        public void Configure(EntityTypeBuilder<UserSetting> builder)
        {
            builder.ToTable("UserSettings").HasKey(x => x.Id);
        }
    }
}
