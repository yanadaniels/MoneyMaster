using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.WebAPI.Models.AccountType;

namespace MoneyMasterService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер типа счета
    /// </summary>
    [ApiController]
    [Route("/api/v1/accountType")]
    public class AccountTypeController : ControllerBase
    {
        private readonly ILogger<AccountTypeController> _logger;
        private readonly IAccountTypeService _accountTypeService;
        private readonly IMapper _mapper;

        public AccountTypeController(ILogger<AccountTypeController> logger, IAccountTypeService accountTypeService, IMapper mapper)
        {
            _logger = logger;
            _accountTypeService = accountTypeService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение объекта типа счета
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект типа счета по её идентификатору</remarks>
        /// <param name="id">Идентификатор типа счета</param>
        /// <response code="200">Получение объекта типа счета</response>
        /// <response code="404">Не удалось найти тип счета по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<AccountTypeModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var accountType = await _accountTypeService.GetByIdAsync(id);

            if (accountType == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти тип счета по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<AccountTypeModel>(accountType));
        }

        /// <summary>
        /// Получение всех типов счетов.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех типов счетов. 
        /// </remarks>
        /// <response code="200">Получение списка типов счетов</response>
        [HttpGet]
        [ProducesResponseType<ICollection<AccountTypeModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var accountTypes = await _accountTypeService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<AccountTypeModel>>(accountTypes) );
        }
    }
}
