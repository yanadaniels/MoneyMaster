using MoneyMaster.Common.Extensions;
using MoneyMasterService.Infrastructure.EntityFramework;
using MoneyMasterService.Services.Implementations.Service;
using MoneyMasterService.WebAPI.Data;
using MoneyMasterService.WebAPI.Extensions;
using MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Service;
using System.Reflection;
namespace MoneyMasterService.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services
                  //.AddMapper()
                  //.AddModelMapper()
                  .AddDatabase(builder.Configuration.GetSection("Database"))
                  .AddRepositories()
                  .AddServices();

            builder.Services.AddTransient<DbInitializer>();
            //    .BuildServiceProvider()
            //    .CreateAsyncScope()
            //    .ServiceProvider
            //    .GetRequiredService<DbInitializer>()
            //    .InitializeAsync().Wait();



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xml = $"{Assembly.GetAssembly(typeof(UserDto)).GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName), includeControllerXmlComments: true);
                //opt.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xml));
                opt.SupportNonNullableReferenceTypes();
            });

            //Добавляем авторизацию
            builder.Services.AddCustomJWTAuthentification();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                await dbInitializer.InitializeAsync(); // Асинхронно инициализируем БД
            }

            app.UseExceptionHandlingMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(o =>
                {
                    o.InjectStylesheet("/css/swagger-custom.css");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();   // добавление middleware аутентификации 

            app.UseAuthorization();   // добавление middleware авторизации 


            app.MapControllers();

            app.Run();
        }
    }
}
