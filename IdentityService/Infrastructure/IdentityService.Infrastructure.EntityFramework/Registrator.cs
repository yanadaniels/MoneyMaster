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

               var t = Configuration.GetConnectionString(type!);

               switch (type)
               {
                   case null: throw new InvalidOperationException("Не определён тип БД");
                   default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                   case "MSSQL":
                       opt.UseSqlServer(Configuration.GetConnectionString(type),
                                                                providerOptions =>
                                                                {
                                                                    providerOptions.CommandTimeout(180);
                                                                }
                                        );
                       break;
                   case "SQLite":
                       opt.UseSqlite(Configuration.GetConnectionString(type), b => b.MigrationsAssembly("IdentityService.Infrastructure.EntityFramework"));
                       break;
               };
               opt.EnableSensitiveDataLogging(false);
           })
        ;
    }
}
