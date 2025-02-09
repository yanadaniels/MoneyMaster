using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.WebAPI.Models.Transaction;

namespace MoneyMasterService.WebAPI.Controllers
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
        private readonly IMapper _mapper;

        public TransactionController(ILogger<TransactionController> logger, ITransactionService transactionService,IMapper mapper)
        {
            _logger = logger;
            _transactionService = transactionService;
            _mapper = mapper;
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
        [ProducesResponseType<TransactionModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);

            if (transaction == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти транзакцию по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<TransactionModel>(transaction));
        }

        /// <summary>
        /// Получение всех транзакций.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех транзакций. 
        /// </remarks>
        /// <response code="200">Получение списка всех транзакций</response>
        [HttpGet]
        [ProducesResponseType<ICollection<TransactionModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<TransactionModel>>(transactions));
        }
    }
}
