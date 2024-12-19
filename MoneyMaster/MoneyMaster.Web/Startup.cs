using MoneyMaster.DAL.Extensions;

namespace MoneyMaster.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        /// <summary>
        /// Метод вызывается средой ASP.NET.
        /// Используйте его для подключения сервисов приложения
        /// Документация:  https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {

           

            services.AddDatabase(_configuration.GetSection("Database"));


            services.AddControllersWithViews();
            services.AddRazorPages();


            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = "/Login";
                opt.AccessDeniedPath = "/AccessDenied";
            });

            services.AddSession();
        }



        /// <summary>
        /// Метод вызывается средой ASP.NET.
        /// Используйте его для настройки конвейера запросов
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Проверяем, не запущен ли проект в среде разработки
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            var cachePeriod = "0";
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={cachePeriod}");
                }
            });
         

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();


            //Добавляем компонент с настройкой маршрутов для главной страницы
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

        }
    }
}
