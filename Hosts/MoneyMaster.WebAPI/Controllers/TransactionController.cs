using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Report;
using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.Services.Implementations;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер транзакций
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ILogger<TransactionController> _logger;
        private readonly ITransactionService _transactionService;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService)
        {
            _logger = logger;
            _transactionService = transactionService;
        }

        /// <summary>
        /// Получение объекта транзакции
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект транзакции по её идентификатору</remarks>
        /// <param name="id">Идентификатор транзакции</param>
        /// <response code="200">Получение объекта транзакции</response>
        /// <response code="404">Не удалось найти транзакцию по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<TransactionDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            if (transaction == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти транзакцию по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, transaction);
        }

        /// <summary>
        /// Получение всех транзакций.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех транзакций. 
        /// </remarks>
        /// <response code="200">Получение списка всех транзакций</response>
        [HttpGet]
        [ProducesResponseType<ICollection<TransactionDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, transactions);
        }
    }
}
