using MoneyMaster.WebAPI.Middlewares;

namespace MoneyMaster.WebAPI.Extensions
{
    /// <summary>
    /// Содержит методы расширения для добавления пользовательских Middleware в конвейер обработки запросов.
    /// </summary>
    public static class CustomMiddlewareExtensions
    {
        /// <summary>
        /// Добавляет пользовательский Middleware для обработки исключений в конвейер обработки запросов.
        /// </summary>
        /// <param name="builder">Построитель приложения для настройки Middleware.</param>
        /// <returns>Обновленный построитель приложения.</returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
