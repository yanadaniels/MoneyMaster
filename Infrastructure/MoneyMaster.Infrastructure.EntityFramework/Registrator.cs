using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoneyMaster.Infrastructure.EntityFramework.Context;

namespace MoneyMaster.Infrastructure.EntityFramework
{
    public static class Registrator
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration Configuration) => services
           .AddDbContext<MoneyMasterContext>(opt =>
           {

               var type = Configuration["Type"];

               var t = Configuration.GetConnectionString(type);

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
                       opt.UseSqlite(Configuration.GetConnectionString(type), b => b.MigrationsAssembly("MoneyMaster.Infrastructure.EntityFramework"));
                       break;
               };
               opt.EnableSensitiveDataLogging(false);
           })
        ;
    }
}
