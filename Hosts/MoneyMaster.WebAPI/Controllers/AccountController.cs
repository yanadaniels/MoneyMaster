using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.WebAPI.Models.Account;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер счета
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(ILogger<AccountController> logger ,IAccountService accountService,IMapper mapper)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение объекта счета
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект счет по её идентификатору</remarks>
        /// <param name="id">Идентификатор счета</param>
        /// <response code="200">Получение объекта счета</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<AccountModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти счет по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<AccountModel>(account));
        }

        /// <summary>
        /// Получение всех счетов.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех счетов. 
        /// </remarks>
        /// <response code="200">Получение списка счетов</response>
        [HttpGet]
        [ProducesResponseType<ICollection<AccountModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await _accountService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<AccountModel>>(accounts));
        }
    }
}
