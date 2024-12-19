using Microsoft.EntityFrameworkCore;
using MoneyMaster.Infrastructure.EntityFramework.Configurations;

namespace MoneyMaster.Infrastructure.EntityFramework.Context
{
    /// <summary>Контекст базы данных </summary>
    public class MoneyMasterContext : DbContext
    {
        public MoneyMasterContext(DbContextOptions<MoneyMasterContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new AccountTypeConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new ReportConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserSettingConfiguration());
        }
    }
}
