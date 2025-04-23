using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.Infrastructure.EntityFramework;
using IdentityService.Infrastructure.Repositories.Implementations.Service;
using IdentityService.Services.Implementations.Service;
using IdentityService.WebAPI.Data;
using IdentityService.WebAPI.Extensions;
using IdentityService.WebAPI.Validation.User;
using MoneyMaster.Common.Extensions;
using System.Reflection;

namespace IdentityService.WebAPI
{
    /// <summary>
    /// Основной класс приложения IdentityService.
    /// Отвечает за инициализацию и запуск веб-приложения.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// Конфигурирует сервисы, middleware и запускает веб-приложение.
        /// </summary>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services
                  //.AddMapper()
                  //.AddModelMapper()
                  .AddIdentityDatabase(builder.Configuration.GetSection("Database"))
                  .AddRepositories()
                  .AddServices()
                  .AddValidation();

            builder.Services.AddTransient<DbInitializer>().BuildServiceProvider().CreateAsyncScope().ServiceProvider.GetRequiredService<DbInitializer>().InitializeAsync().Wait();

            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<CreatingUserValidation>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserAuthorizeValidation>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserUpdateValidation>();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName), includeControllerXmlComments: true);
                opt.SupportNonNullableReferenceTypes();
            });

            //Добавляем авторизацию
            builder.Services.AddCustomJWTAuthentification();

            //Временно отключаем CORS
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

            //Временно отключаем CORS
            app.UseCors("AllowAll");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API IdentityService v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            //app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
