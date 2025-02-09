using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Configurations;
using System.Linq.Expressions;

namespace MoneyMaster.Infrastructure.EntityFramework.Context
{
    /// <summary>Контекст базы данных </summary>
    public class MoneyMasterContext : DbContext
    {
        public MoneyMasterContext(DbContextOptions<MoneyMasterContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreatedAsync();
        }

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

            //Добавляет фильтр для всех сущностей, реализующих ISoftDeletable, исключая помеченные как удаленные.
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var filter = Expression.Lambda(
                        Expression.Equal(
                            Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted)),
                            Expression.Constant(false)
                        ),
                        parameter
                    );

                    builder.Entity(entityType.ClrType).HasQueryFilter(filter);
                }
            }
        }
    }
}