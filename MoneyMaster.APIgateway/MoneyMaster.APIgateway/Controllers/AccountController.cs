using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.APIgateway.Models.Account;
using MoneyMaster.Common.Models;
using Newtonsoft.Json;
using System.Text;

namespace APIGateway.Controllers
{
    /// <summary>
    /// Контроллер счетов
    /// </summary>
    [ApiController]
    [Route("/api/v1/accounts")]
    public class AccountController : ControllerBase
    {
        private string route = "accounts";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор для инициализации контроллера аккаунтов.
        /// </summary>
        /// <param name="httpClientFactory">Фабрика http клиента</param>
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MoneyMasterService");
        }

        /// <summary>
        /// Получить счет по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает информацию о счете</response>
        /// <response code="404">Не найден счет</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("{id}")]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}/{id}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получить все счета.
        /// </summary>
        /// <response code="200">Возвращает список счетов</response>
        /// <response code="400">Неверные данные запроса</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet]
        [ProducesResponseType(typeof(PaginationResponseModel<AccountModelResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAllAccounts([FromQuery] PaginationParametersModel parameters, 
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}?{parameters.ToQueryString()}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получить информацию для создания нового счета.
        /// </summary>
        /// <response code="200">Возвращает информацию для создания счета</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpGet("create")]
        [ProducesResponseType<CreatingAccountInfoModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAccountCreateInfo(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}/create", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Создать новый счет.
        /// </summary>
        /// <param name="model">Модель для создания счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="201">Возвращает созданный счет</response>
        /// <response code="400">Неверные данные запроса</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPost]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateAccount([FromBody] CreatingAccountModelRequest model, 
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{route}", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Обновить данные счета.
        /// </summary>
        /// <param name="id">Идентификатор счета для обновления</param>
        /// <param name="model">Модель для обновления данных счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает обновленный счет</response>
        /// <response code="400">Неверные данные запроса</response>
        /// <response code="404">Не найден счет</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpPut("{id}")]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id, [FromBody] UpdatingAccountModelRequest model, 
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{route}/{id}", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Удалить счет по ID.
        /// </summary>
        /// <param name="id">Идентификатор счета для удаления</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="204">Счет удален</response>
        /// <response code="404">Не найден счет</response>
        /// <response code="500">Ошибка сервера</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.DeleteAsync($"{route}/{id}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получить все удаленные счета.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех удаленных счетов с пагинацией. 
        /// </remarks>
        /// <param name="parameters">Параметры пагинации и сортировки.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список аккаунтов и информация о пагинации.</returns>
        /// <response code="200">Возвращает список удаленных счетов</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet("deleted")]
        [ProducesResponseType<PaginationResponseModel<AccountModelResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAllDeleted([FromQuery] PaginationParametersModel parameters,
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}/deleted?{parameters.ToQueryString()}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Восстанавить счет.
        /// </summary>
        /// <remarks>Данный метод позволяет восстановить по ID счет который помечен как удаленный</remarks>
        /// <param name="id">Идентификатор счета который нужно восстановить</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает восстановленный счет</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> Restore([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.PostAsync($"{route}/{id}", null, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}
