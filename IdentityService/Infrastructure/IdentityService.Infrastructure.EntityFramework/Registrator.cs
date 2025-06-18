// Ignore Spelling: Registrator

using IdentityService.Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityService.Infrastructure.EntityFramework
{
    public static class Registrator
    {
        public static IServiceCollection AddIdentityDatabase(this IServiceCollection services, IConfiguration Configuration) => services
           .AddDbContext<IdentityContext>(opt =>
           {
               var type = Configuration["Type"];
               if (string.IsNullOrEmpty(type))
                   throw new InvalidOperationException("Не указан тип БД в конфигурации (Database.Type)");

               var connectionString = Configuration[$"ConnectionStrings:{type}"];
               if (string.IsNullOrEmpty(connectionString))
                   throw new InvalidOperationException($"Не найдена строка подключения для типа {type} в Database.ConnectionStrings");

               switch (type)
               {
                   case "MSSQL":
                       opt.UseSqlServer(connectionString, providerOptions =>
                       {
                           providerOptions.CommandTimeout(180);
                       });
                       break;

                   case "SQLite":
                       opt.UseSqlite(Configuration.GetConnectionString(type), b =>
                           b.MigrationsAssembly("IdentityService.Infrastructure.EntityFramework"));
                       break;
                   case "Postrge":
                       opt.UseNpgsql(Configuration.GetConnectionString(type), x => x.MigrationsHistoryTable("__MigrationHistory", "identity"));
                       break;
                   default:
                       throw new InvalidOperationException($"Тип подключения {type} не поддерживается.");
               }
               ;

               opt.EnableSensitiveDataLogging(false);
           });
    }
}
