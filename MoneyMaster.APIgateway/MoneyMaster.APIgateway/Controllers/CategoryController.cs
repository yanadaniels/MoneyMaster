using Microsoft.AspNetCore.Mvc;
using MoneyMaster.APIgateway.Models.Category;

namespace MoneyMaster.APIgateway.Controllers
{
    /// <summary>
    /// Контроллер транзакций
    /// </summary>
    [ApiController]
    [Route("api/v1/categories")]
    public class CategoryController : ControllerBase
    {
        private string route = "categories";
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор для инициализации контроллера категории.
        /// </summary>
        /// <param name="httpClientFactory">Фабрика http клиента</param>
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MoneyMasterService");
        }

        /// <summary>
        /// Получение объекта категорий
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект категории по её идентификатору</remarks>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="cancellationToken">Токен отмены операции</param>
        /// <response code="200">Получение объекта категории</response>
        /// <response code="404">Не удалось найти категорию по указанному идентификатору</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType<CategoryModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryModelResponse>> GetCategory([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}/{id}",cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        /// <summary>
        /// Получение всех категории.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех категории. 
        /// </remarks>
        /// <response code="200">Получение списка всех категории</response>
        [HttpGet]
        [ProducesResponseType<IReadOnlyCollection<CategoryModelResponse>>(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryModelResponse>> GetAllTransactions(CancellationToken cancellationToken)
        {
            var response = await _httpClient.GetAsync($"{route}", cancellationToken);
            return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        }

        ///// <summary>
        ///// Создание транзакции.
        ///// </summary>
        ///// <remarks>
        ///// Данный метод позволяет создать новую транзакцию. 
        ///// </remarks>
        ///// <response code="201">Транзакция успешно создана</response>
        //[HttpPost]
        //[ProducesResponseType<Guid>(StatusCodes.Status201Created)]
        //public async Task<ActionResult<Guid>> CreateTransaction([FromBody] CreatingTransactionRequest request,
        //    CancellationToken cancellationToken)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync($"{route}", content, cancellationToken);
        //    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        //}

        ///// <summary>
        ///// Изменение транкзакции.
        ///// </summary>
        //[HttpPut("{id:guid}")]
        //[ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<TransactionResponse>> UpdateTransaction(
        //    [FromRoute] Guid id,
        //    [FromBody] UpdatingTransactionRequest request,
        //    CancellationToken cancellationToken)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PutAsync($"{route}/{id}", content, cancellationToken);
        //    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        //}

        ///// <summary>
        ///// Удаление транзакции.
        ///// </summary>
        ///// <param name="id">Идентификатор транзакции</param>
        ///// <param name="cancellationToken">Токен отмены операции</param>
        //[HttpDelete("{id:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult> DeleteTransaction([FromRoute] Guid id, CancellationToken cancellationToken)
        //{
        //    var response = await _httpClient.DeleteAsync($"{route}/{id}", cancellationToken);
        //    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        //}

        ///// <summary>
        ///// Восстановление транзакции.
        ///// </summary>
        //[HttpPost("{id:guid}")]
        //[ProducesResponseType<TransactionResponse>(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<TransactionResponse>> RestoreTransaction([FromRoute] Guid id,
        //    CancellationToken cancellationToken)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync($"{route}/{id}", content, cancellationToken);
        //    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync(cancellationToken));
        //}
    }
}
