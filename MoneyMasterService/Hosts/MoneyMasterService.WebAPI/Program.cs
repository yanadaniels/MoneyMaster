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

            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xml = $"{Assembly.GetAssembly(typeof(UserDto)).GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName), includeControllerXmlComments: true);
                //opt.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xml));
                opt.SupportNonNullableReferenceTypes();
            });

            //��������� �����������
            builder.Services.AddCustomJWTAuthentification();


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()  // ��������� ��� ������
                          .AllowAnyMethod()  // ��������� ��� ������
                          .AllowAnyHeader(); // ��������� ��� ���������
                });
            });

            var app = builder.Build();

            app.UseCors("AllowAll");

            using (var scope = app.Services.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
                await dbInitializer.InitializeAsync(); // ���������� �������������� ��
            }

            app.UseExceptionHandlingMiddleware();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                //app.UseSwaggerUI();
                app.UseSwaggerUI(c =>
                {
                    //o.InjectStylesheet("/css/swagger-custom.css");
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API MoneyMasterService v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            //app.UseHttpsRedirection();

            //app.UseAuthentication();   // ���������� middleware �������������� 

            app.UseAuthorization();   // ���������� middleware ����������� 

            app.MapControllers();

            await app.RunAsync();
        }
    }
}
