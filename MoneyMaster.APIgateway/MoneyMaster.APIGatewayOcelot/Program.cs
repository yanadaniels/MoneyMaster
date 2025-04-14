using MoneyMaster.Common.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace MoneyMaster.APIGatewayOcelot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            builder.Services.AddCustomJWTAuthentification();
            builder.Services.AddAuthorization();
            builder.Services.AddOcelot(builder.Configuration);
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                                  builder =>
                                  {
                                      builder.AllowAnyOrigin()
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();    //SetIsOriginAllowed(x=>true).AllowCredentials()
                                  });
            });

            var app = builder.Build();
            app.UseCors("AllowAll");
            await app.UseOcelot();
            //app.UseSwagger();
            //app.UseSwaggerUI();

            app.Run();
        }
    }
}
