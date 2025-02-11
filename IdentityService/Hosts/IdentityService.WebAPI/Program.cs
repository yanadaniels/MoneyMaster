using IdentityService.Infrastructure.EntityFramework;
using IdentityService.Infrastructure.Repositories.Implementations.Service;
using IdentityService.Services.Implementations.Service;
using IdentityService.WebAPI.Data;
using IdentityService.WebAPI.Extensions;
using MoneyMaster.Common.Extensions;
using System.Reflection;

namespace IdentityService.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services
                  //.AddMapper()
                  //.AddModelMapper()
                  .AddIdentityDatabase(builder.Configuration.GetSection("Database"))
                  .AddRepositories()
                  .AddServices()
                  .AddValidation()

                  ;
            builder.Services.AddTransient<DbInitializer>().BuildServiceProvider().CreateAsyncScope().ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();



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

            //ƒобавл€ем авторизацию
            builder.Services.AddCustomJWTAuthentification();

            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                //app.UseSwaggerUI(o =>
                //{
                //    o.InjectStylesheet("/css/swagger-custom.css");
                //});
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();   // добавление middleware аутентификации 

            app.UseAuthorization();   // добавление middleware авторизации 


            app.MapControllers();

            app.Run();
        }
    }
}
