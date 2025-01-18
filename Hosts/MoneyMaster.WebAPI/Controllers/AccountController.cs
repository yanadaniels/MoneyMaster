using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.Common;
using MoneyMaster.WebAPI.Models.Account;
using MoneyMaster.WebAPI.Models.Common;

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

        /// <summary>
        /// Конструктор для инициализации контроллера аккаунтов.
        /// </summary>
        /// <param name="logger">Логгер для регистрации событий и ошибок.</param>
        /// <param name="accountService">Сервис для работы с аккаунтами.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMapper mapper)
        {
            _logger = logger;
            _accountService = accountService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить счет.
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект счет по её идентификатору</remarks>
        /// <param name="id">Идентификатор счета</param>
        /// <response code="200">Возвращает счет</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<AccountModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var account = await _accountService.GetByIdAsync(id);

            if (account == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти счет по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<AccountModel>(account));
        }

        /// <summary>
        /// Получить все счета.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех счетов с пагинацией. 
        /// </remarks>
        /// <param name="parameters">Параметры пагинации и сортировки.</param>
        /// <returns>Список аккаунтов и информация о пагинации.</returns>
        /// <response code="200">Возвращает список счетов</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet("paged")]
        [Produces("application/json")]
        [ProducesResponseType<PaginationResponseModel<AccountModel>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParametersModel parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parameterDto = _mapper.Map<PaginationParameters>(parameters);

            var (listAccountDto, totalCount) = await _accountService.GetAllAsync(parameterDto);
            var listAccountModel = _mapper.Map<List<AccountModel>>(listAccountDto);
             
            return Ok(new PaginationResponseModel<AccountModel>
            {
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                SortBy = parameters.SortBy,
                IsDescending = parameters.IsDescending,
                Data = listAccountModel
            });
        }

        /// <summary>
        /// Получить все удаленные счета.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех удаленных счетов с пагинацией. 
        /// </remarks>
        /// <param name="parameters">Параметры пагинации и сортировки.</param>
        /// <returns>Список аккаунтов и информация о пагинации.</returns>
        /// <response code="200">Возвращает список удаленных счетов</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet("paged")]
        [Produces("application/json")]
        [ProducesResponseType<PaginationResponseModel<AccountModel>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDeleted([FromQuery] PaginationParametersModel parameters)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parameterDto = _mapper.Map<PaginationParameters>(parameters);
            var (listAccountDto, totalCount) = await _accountService.GetAllDeletedAsync(parameterDto);
            var listAccountModel = _mapper.Map<List<AccountModel>>(listAccountDto);

            return Ok(new PaginationResponseModel<AccountModel>
            {
                TotalCount = totalCount,
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                SortBy = parameters.SortBy,
                IsDescending = parameters.IsDescending,
                Data = listAccountModel
            });
        }

        /// <summary>
        /// Получить всю необходимую информацию для создании счета.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить json со всеми необходимыми данными которые понадобяться при создании счёта
        /// </remarks>
        /// <response code="200">Возвращает данные необходимые в процессе создания нового счета</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet]
        [ProducesResponseType<CreatingAccountInfoModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCreateInfo()
        {
            var creatingAccountInfoDto = await _accountService.GetInfoNeedToCreateAccountAsync();
            var creatingAccountInfoModel = _mapper.Map<CreatingAccountInfoModel>(creatingAccountInfoDto);
            return StatusCode(StatusCodes.Status200OK, creatingAccountInfoModel);
        }

        /// <summary>
        /// Добавить новый счет.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет добавить новый счет. 
        /// </remarks>
        /// <response code="201">Возвращает добавленный счет</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPost]
        [ProducesResponseType<AccountModel>(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatingAccountModel model)
        {
            if (model == null)
                return BadRequest("Отсутсвуют данные");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newAccountDto = _mapper.Map<CreatingAccountDto>(model);
            var accountDto = await _accountService.AddAsync(newAccountDto);
            var accountModel = _mapper.Map<AccountModel>(accountDto);

            if (accountDto != null)
                return StatusCode(StatusCodes.Status201Created, accountModel);
            else
                return StatusCode(StatusCodes.Status500InternalServerError, "Не удается создать счёт из-за внутренней ошибки");
        }

        /// <summary>
        /// Редактировать счет.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет редактировать данные счета.
        /// </remarks>
        /// <param name="model"></param>
        /// <response code="200">Возвращает отредактированный счет</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPut]
        [ProducesResponseType<AccountModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdatingAccountModel model)
        {
            if (model == null)
                return BadRequest("Отсутсвуют данные");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedAccountDto = _mapper.Map<UpdatingAccountDto>(model);
            var updatingAccountDto = await _accountService.UpdateAsync(updatedAccountDto);
            var AccountModel = _mapper.Map<AccountModel>(updatingAccountDto);

            if (updatingAccountDto != null)
                return Ok(AccountModel);
            else
                return NotFound("Cчёт с указанным идентификатором не найден");
        }

        /// <summary>
        /// Удалить счет.
        /// </summary>
        /// <remarks>Данный метод позволяет пометить счет как удаленный по её ID</remarks>
        /// <param name="id">Идентификатор удаляемого счета</param>
        /// <response code="200">Возвращает удаленный счет</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType<AccountModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deletedAccount = await _accountService.DeleteAsync(id);

            if (deletedAccount == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось удалить счет по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<AccountModel>(deletedAccount));
        }

        /// <summary>
        /// Восстанавить счет.
        /// </summary>
        /// <remarks>Данный метод позволяет восстановить по ID счет который помечен как удаленный</remarks>
        /// <param name="id">Идентификатор счета который нужно восстановить</param>
        /// <response code="200">Возвращает восстановленный счет</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType<AccountModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var restoredAccount = await _accountService.RestoreAsync(id);

            if (restoredAccount == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось восстановить счет по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<AccountModel>(restoredAccount));
        }

    }
}
