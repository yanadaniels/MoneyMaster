// Ignore Spelling: Validator

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.APIgateway.Models.Account;
using MoneyMaster.APIgateway.Models.User;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace IdentityService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    [ApiController]
    [Route("/api/v1/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private string route = "users";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор для инициализации контроллера пользователей.
        /// </summary>
        /// <param name="httpClientFactory">Фабрика http клиента</param>
        public UserController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("IdentityService");
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Модель для создания пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <remarks>
        /// Данный метод позволяет создать нового пользователя
        /// </remarks>
        /// <response code="201">Новый пользователь успешно создан</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        /// <response code="409">Ошибки при указании данных для создания пользователя</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] CreatingUserModelRequest model, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{route}", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Авторизоваться
        /// </summary>
        /// <param name="authorizationModel">Данные необходимые для авторизации</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        [HttpPost("login")]
        [ProducesResponseType<UserJwtTokenResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Token([FromBody] UserAuthorizeModelRequest authorizationModel, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(authorizationModel), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{route}/login", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <remarks>Данный метод позволяет выйти из системы</remarks>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="204">При успешном выходе из системы</response>
        /// <response code="400">Если произошла ошибка при выходе</response>
        [HttpPost("logout/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<ActionResult> Logout([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{route}/logout/{id}");

            if (Request.Headers.TryGetValue("Authorization", out var accessToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString().Replace("Bearer ", ""));
            }

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Обновить Access и Refresh токен
        /// </summary>
        /// <param name="oldRefreshToken">Объект с данными для обновления Access токена</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        [HttpPost("refresh/{id}")]
        [ProducesResponseType<UserRefreshJwtTokenResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<UserRefreshJwtTokenResponse>> RefreshToken([FromBody] RefreshTokenRequest oldRefreshToken, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(oldRefreshToken), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, $"{route}/refresh/{oldRefreshToken.Id}");
            requestMessage.Content = content;

            if (Request.Headers.TryGetValue("Authorization", out var accessToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString().Replace("Bearer ", ""));
            }

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="updateUser">Json c обновленными свойствами</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        /// <response code="401">Если нет авторизации</response>
        [HttpPatch("{id}")]
        [ProducesResponseType<UserModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<UserModelResponse>> Update(Guid id, [FromBody] UserUpdateModelRequest updateUser, CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updateUser), Encoding.UTF8, "application/json");
            var requestMessage = new HttpRequestMessage(HttpMethod.Patch, $"{route}/{updateUser.Id}");
            requestMessage.Content = content;

            if (Request.Headers.TryGetValue("Authorization", out var accessToken))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken.ToString().Replace("Bearer ", ""));
            }

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает информацию о счете</response>
        /// <response code="404">Не найден счет</response>
        /// <response code="500">Ошибка сервера</response>
        //[HttpGet("{id}")]
        //[ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //[Authorize]
        //public async Task<IActionResult> GetAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var response = await _httpClient.GetAsync($"{route}/{id}", cancellationToken);
        //    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        //}
    }
}
