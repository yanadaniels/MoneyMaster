using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.AccountType;
using MoneyMaster.Services.Implementations;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер типа аккаунта
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountTypeController : ControllerBase
    {
        private readonly ILogger<AccountTypeController> _logger;
        private readonly IAccountTypeService _accountTypeService;

        public AccountTypeController(ILogger<AccountTypeController> logger, IAccountTypeService accountTypeService)
        {
            _logger = logger;
            _accountTypeService = accountTypeService;
        }

        /// <summary>
        /// Получение объекта типа учетной записи
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект типа учетной записи по её идентификатору</remarks>
        /// <param name="id">Идентификатор типа учетной записи</param>
        /// <response code="200">Получение объекта типа учетной записи</response>
        /// <response code="404">Не удалось найти тип учетной записи по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<AccountTypeDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var accountType = await _accountTypeService.GetByIdAsync(id);

            if (accountType == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти тип учетной записи по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, accountType);
        }

        /// <summary>
        /// Получение всех типов учетной записи.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех типов учетных данных. 
        /// </remarks>
        /// <response code="200">Получение списка типов учетных данных</response>
        [HttpGet]
        [ProducesResponseType<ICollection<AccountTypeDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var accountTypes = await _accountTypeService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, accountTypes);
        }
    }
}
