// Ignore Spelling: Registrator

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyMaster.Infrastructure.EntityFramework.Context;

namespace MoneyMaster.Infrastructure.EntityFramework
{
    public static class Registrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration) => services
           .AddDbContext<MoneyMasterContext>(opt =>
           {

               var type = configuration["Type"];

               var t = configuration.GetConnectionString(type!);

               switch (type)
               {
                   case null: throw new InvalidOperationException("Не определён тип БД");
                   default: throw new InvalidOperationException($"Тип подключения {type} не поддерживается");

                   case "MSSQL":
                       opt.UseSqlServer(configuration.GetConnectionString(type),
                                                                providerOptions =>
                                                                {
                                                                    providerOptions.CommandTimeout(180);
                                                                }
                                        );
                       break;
                   case "SQLite":
                       opt.UseSqlite(configuration.GetConnectionString(type), b => b.MigrationsAssembly("MoneyMaster.Infrastructure.EntityFramework"));
                       break;
               };
               opt.EnableSensitiveDataLogging(false);
           })
        ;
    }
}
