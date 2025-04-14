using MoneyMaster.Common.Extensions;
using System.Reflection;

namespace MoneyMaster.APIgateway
{
    /// <summary>
    /// ����� ����� � API Gateway.
    /// �������� �� ��������� � ������ ���-����������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ������� ������ ������� ��������� API Gateway
        /// </summary>
        /// <param name="args">�������� ��������� ������</param>
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

            // �������� ��������� CORS
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

            // �������� ��������� �������� � https
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
