using IdentityService.Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.EntityFramework.Context
{
    /// <summary>Контекст базы данных </summary>
    public class IdentityContext : DbContext
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            // Database.EnsureCreatedAsync();
        }

        // Этот конструктор нужен только для миграций
        public IdentityContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=Identity.db"); // Заглушка, EF сам подставит правильное значение
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
