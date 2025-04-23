using Microsoft.AspNetCore.Mvc;
using MoneyMaster.APIgateway.Models.Transaction;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace MoneyMaster.APIgateway.Controllers
{
    /// <summary>
    /// Контроллер транзакций
    /// </summary>
    [ApiController]
    [Route("api/v1/transactions")]
    public class TransactionController : ControllerBase
    {
        private string route = "transactions";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор для инициализации контроллера транзакции.
        /// </summary>
        /// <param name="httpClientFactory">Фабрика http клиента</param>
        public TransactionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MoneyMasterService");
        }

        /// <summary>
        /// Получение объекта транзакции
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект транзакции по её идентификатору</remarks>
        /// <param name="id">Идентификатор транзакции</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Получение объекта транзакции</response>
        /// <response code="404">Не удалось найти транзакцию по указанному идентификатору</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionResponse>> GetTransaction([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}/{id}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получение всех транзакций.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех транзакций. 
        /// </remarks>
        /// <response code="200">Получение списка всех транзакций</response>
        [HttpGet]
        [ProducesResponseType<IReadOnlyCollection<TransactionResponse>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<TransactionResponse>> GetAllTransactions(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Создание транзакции.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет создать новую транзакцию. 
        /// </remarks>
        /// <response code="201">Транзакция успешно создана</response>
        [HttpPost]
        [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateTransaction([FromBody] CreatingTransactionRequest request,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{route}", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Изменение транкзакции.
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionResponse>> UpdateTransaction(
            [FromRoute] Guid id,
            [FromBody] UpdatingTransactionRequest request,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{route}/{id}", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Удаление транзакции.
        /// </summary>
        /// <param name="id">Идентификатор транзакции</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteTransaction([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.DeleteAsync($"{route}/{id}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Восстановление транзакции.
        /// </summary>
        [HttpPost("{id:guid}")]
        [ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionResponse>> RestoreTransaction([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{route}/{id}", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получить транзакции по id счета
        /// </summary>
        /// <param name="id">Индентификатор счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpGet("account/{id:guid}")]
        [ProducesResponseType<IReadOnlyCollection<TransactionResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionResponse>> GetTransactionByAccountIdAsync([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}/account/{id}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Создать перевод с одного счета на другой
        /// </summary>
        /// <param name="request">Модель для создания перевода</param>
        /// <param name="cancellationToken">Токен отмены</param>
        [HttpPost("transfer")]
        [ProducesResponseType<Guid>(StatusCodes.Status201Created)]
        public async Task<ActionResult<Guid>> CreateTransactionTransfer([FromBody] CreateTransactionTransferRequest request,
            CancellationToken cancellationToken)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{route}/transfer", content, cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }
    }
}
