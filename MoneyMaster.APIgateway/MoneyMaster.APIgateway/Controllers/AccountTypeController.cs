using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.APIgateway.Models.AccountType;

namespace APIGateway.Controllers
{
    /// <summary>
    /// Контроллер счетов
    /// </summary>
    [ApiController]
    [Route("/api/v1/accountType")]
    public class AccountTypeController : ControllerBase
    {
        private string route = "accountType";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор для инициализации контроллера аккаунтов.
        /// </summary>
        /// <param name="httpClientFactory">Фабрика http клиента</param>
        public AccountTypeController(IHttpClientFactory httpClientFactory)
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
        [ProducesResponseType<AccountTypeModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAccountType([FromRoute] Guid id, CancellationToken cancellationToken)
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
        [ProducesResponseType<ICollection<AccountTypeModel>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> GetAllAccountType(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

    }
}
