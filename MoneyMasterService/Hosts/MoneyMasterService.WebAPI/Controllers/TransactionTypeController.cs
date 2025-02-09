using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.WebAPI.Models.TransactionType;

namespace MoneyMasterService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер типов транзакций
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ILogger<TransactionTypeController> _logger;
        private readonly ITransactionTypeService _transactionTypeService;
        private readonly IMapper _mapper;

        public TransactionTypeController(ILogger<TransactionTypeController> logger, ITransactionTypeService transactionTypeService, IMapper mapper)
        {
            _logger = logger;
            _transactionTypeService = transactionTypeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение объекта типа транзакции
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект тип транзакции по её идентификатору</remarks>
        /// <param name="id">Идентификатор типа транзакции</param>
        /// <response code="200">Получение объекта типа транзакции</response>
        /// <response code="404">Не удалось найти тип транзакции по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<TransactionTypeModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var transactionType = await _transactionTypeService.GetByIdAsync(id);

            if (transactionType == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти тип транзакции по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<TransactionTypeModel>(transactionType));
        }

        /// <summary>
        /// Получение всех типов транзакций.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех типов транзакций. 
        /// </remarks>
        /// <response code="200">Получение списка всех типов транзакций</response>
        [HttpGet]
        [ProducesResponseType<ICollection<TransactionTypeModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
           var transactionTypes = await _transactionTypeService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<TransactionTypeModel>>(transactionTypes));
        }
    }
}
