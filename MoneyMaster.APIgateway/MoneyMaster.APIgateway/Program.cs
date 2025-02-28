using MoneyMaster.Common.Extensions;
using System.Reflection;

namespace MoneyMaster.APIgateway
{
    /// <summary>
    /// Точка входа в API Gateway.
    /// Отвечает за настройку и запуск веб-приложения
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Главный методо который запускает API Gateway
        /// </summary>
        /// <param name="args">Аргумент командной строки</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName), includeControllerXmlComments: true);
                opt.SupportNonNullableReferenceTypes();
            });

            builder.Services.AddHttpClient("MoneyMasterService", client =>
            {
                client.BaseAddress = new Uri("http://moneymasterservice.webapi:8080/api/v1/");
            });

            builder.Services.AddHttpClient("IdentityService", client =>
            {
                client.BaseAddress = new Uri("http://identityservice.webapi:8080/api/v1/");
            });

            builder.Services.AddCustomJWTAuthentification();

            // Временно отключаем CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // Разрешает все домены
                          .AllowAnyMethod()  // Разрешает все методы
                          .AllowAnyHeader(); // Разрешает все заголовки
                });
            });

            var app = builder.Build();

            app.UseCors("AllowAll");

            // Временно отключает редирект в https
            //app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gateway v1");
                c.RoutePrefix = string.Empty;
            });

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
