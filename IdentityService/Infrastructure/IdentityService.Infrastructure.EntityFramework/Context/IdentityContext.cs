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


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
