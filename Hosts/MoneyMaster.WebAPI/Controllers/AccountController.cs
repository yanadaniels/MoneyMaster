using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Account;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер аккаунта
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(ILogger<AccountController> logger ,IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Получение объекта аккаунта
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект аккаунта по её идентификатору</remarks>
        /// <param name="id">Идентификатор аккаунта</param>
        /// <response code="200">Получение объекта аккаунта</response>
        /// <response code="404">Не удалось найти аккаунт по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<AccountDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти аккаунт по идентификатору");

            return StatusCode(StatusCodes.Status200OK, account);
        }

        /// <summary>
        /// Получение всех аккаунтов.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех аккаунтов. 
        /// </remarks>
        /// <response code="200">Получение списка аккаунтов</response>
        [HttpGet]
        [ProducesResponseType<ICollection<AccountDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, accounts);
        }
    }
}
