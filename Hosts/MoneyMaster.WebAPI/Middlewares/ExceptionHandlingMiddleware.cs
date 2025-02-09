
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities.DomainExceptions;

namespace MoneyMaster.WebAPI.Middlewares
{
    /// <summary>
    /// Middleware для обработки исключений в конвейере обработки запросов.
    /// Возвращает клиенту корректный HTTP-ответ.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ExceptionHandlingMiddleware"/>.
        /// </summary>
        /// <param name="next">Делегат для передачи запроса следующему компоненту конвейера.</param>
        /// <param name="logger">Экземпляр логгера для регистрации ошибок.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Обрабатывает входящий запрос и перехватывает исключения.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Передать управление следующему middleware
            }
            catch (NotFoundException exception)
            {
                await HandleExceptionAsync(context, StatusCodes.Status404NotFound, exception.Message);
            }
            catch (InvalidOperationException exception)
            {
                await HandleExceptionAsync(context, StatusCodes.Status422UnprocessableEntity, exception.Message);
            }
            catch (DbUpdateException exception)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError,
                    "Ошибка БД. Проверьте данные и попробуйте снова.");
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Произошла неизвестная ошибка, пожалуйста попробуйте повторить запрос позже");
            }
        }

        /// <summary>
        /// Обрабатывает исключение и возвращает соответствующий HTTP-ответ с указанным кодом статуса и сообщением.
        /// </summary>
        /// <param name="context">Контекст HTTP-запроса.</param>
        /// <param name="statusCode">Код HTTP-статуса, который будет возвращён.</param>
        /// <param name="detail">Сообщение об ошибке, которое будет включено в ответ.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string detail)
        {
            context.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = GetTitleForStatusCode(statusCode),
                Detail = detail,
                Instance = context.Request.Path
            };

            return context.Response.WriteAsJsonAsync(problemDetails);
        }

        /// <summary>
        /// Возвращает заголовок для указанного HTTP-статуса.
        /// </summary>
        /// <param name="statusCode">Код HTTP-статуса.</param>
        /// <returns>Заголовок ошибки.</returns>
        private static string GetTitleForStatusCode(int statusCode) =>
            statusCode switch
            {
                StatusCodes.Status400BadRequest => "Некорректный запрос",
                StatusCodes.Status500InternalServerError => "Внутренняя ошибка сервера",
                _ => "Ошибка"
            };
    }
}
