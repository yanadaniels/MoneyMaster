﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.Services.Contracts.Account;
using MoneyMasterService.WebAPI.Controllers.Base;
using MoneyMasterService.WebAPI.Models.Account;
using MoneyMasterService.WebAPI.Models.Common;

namespace MoneyMasterService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер счета
    /// </summary>
    [ApiController]
    [Route("api/v1/accounts/")]
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        /// <summary>
        /// Конструктор для инициализации контроллера аккаунтов.
        /// </summary>
        /// <param name="logger">Логгер для регистрации событий и ошибок.</param>
        /// <param name="accountService">Сервис для работы с аккаунтами.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IMapper mapper): base(mapper)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Получить счет.
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект счет по её идентификатору</remarks>
        /// <param name="id">Идентификатор счета</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает счет</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet("{id}")]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetByIdAsync(id, cancellationToken);

            if (account is null)
               return NotFound($"Не удалось найти счет по указанному идентификатору");

            return Ok(_mapper.Map<AccountModelResponse>(account));
        }

        /// <summary>
        /// Получить все счета111.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех счетов с пагинацией. 
        /// </remarks>
        /// <param name="parameters">Параметры пагинации и сортировки.</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список аккаунтов и информация о пагинации.</returns>
        /// <response code="200">Возвращает список счетов</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet("")]
        [ProducesResponseType<PaginationResponseModel<AccountModelResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        //[RequirePrivilege(Privileges.Administrator, Privileges.System)]
        public async Task<IActionResult> GetAll([FromQuery] 
            PaginationParametersModel parameters, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parameterDto = _mapper.Map<PaginationParameters>(parameters);

            var (listAccountDto, totalCount) = await _accountService.GetAllAsync(parameterDto, cancellationToken);
            var listAccountModel = _mapper.Map<List<AccountModelResponse>>(listAccountDto);
             
            return Ok(new PaginationResponseModel<AccountModelResponse>
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
        /// <param name="cancellationToken">Токен отмены</param>
        /// <returns>Список аккаунтов и информация о пагинации.</returns>
        /// <response code="200">Возвращает список удаленных счетов</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpGet("deleted")]
        [ProducesResponseType<PaginationResponseModel<AccountModelResponse>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllDeleted([FromQuery]
            PaginationParametersModel parameters, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var parameterDto = _mapper.Map<PaginationParameters>(parameters);
            var (listAccountDto, totalCount) = await _accountService.GetAllDeletedAsync(parameterDto, cancellationToken);
            var listAccountModel = _mapper.Map<List<AccountModelResponse>>(listAccountDto);

            return Ok(new PaginationResponseModel<AccountModelResponse>
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
        [HttpGet("create")]
        [ProducesResponseType<CreatingAccountInfoModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCreateInfo(CancellationToken cancellationToken)
        {
            var creatingAccountInfoDto = await _accountService.GetInfoNeedToCreateAccountAsync(cancellationToken);
            var creatingAccountInfoModel = _mapper.Map<CreatingAccountInfoModelResponse>(creatingAccountInfoDto);
            return Ok(creatingAccountInfoModel);
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
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatingAccountModelRequest model, CancellationToken cancellationToken)
        {
            if (model == null)
                return BadRequest("Отсутсвуют данные");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //model.UserId = UserId;
            var newAccountDto = _mapper.Map<CreatingAccountDto>(model);
            var accountDto = await _accountService.AddAsync(newAccountDto, cancellationToken);
            var accountModel = _mapper.Map<AccountModelResponse>(accountDto);

            var resourceUrl = Url.Action(nameof(Get), new { id = accountDto.Id });
            return Created(resourceUrl, accountModel);
        }

        /// <summary>
        /// Редактировать счет.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет редактировать данные счета.
        /// </remarks>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает отредактированный счет</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указаниег где данные были некорретны</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPut("{id}")]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdatingAccountModelRequest model, CancellationToken cancellationToken)
        {
            if (model == null)
                return BadRequest("Отсутсвуют данные");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedAccountDto = _mapper.Map<UpdatingAccountDto>(model);
            var updatingAccountDto = await _accountService.UpdateAsync(updatedAccountDto, cancellationToken);
            var AccountModel = _mapper.Map<AccountModelResponse>(updatingAccountDto);

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
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="204">При успешном удалении счета</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var deletedAccount = await _accountService.DeleteAsync(id, cancellationToken);

            if (deletedAccount == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось удалить счет по указанному идентификатору");

            return NoContent();
        }

        /// <summary>
        /// Восстанавить счет.
        /// </summary>
        /// <remarks>Данный метод позволяет восстановить по ID счет который помечен как удаленный</remarks>
        /// <param name="id">Идентификатор счета который нужно восстановить</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает восстановленный счет</response>
        /// <response code="404">Не удалось найти счет по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPost]
        [Route("{id}")]
        [ProducesResponseType<AccountModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Restore([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var restoredAccount = await _accountService.RestoreAsync(id, cancellationToken);

            if (restoredAccount == null)
                return NotFound($"Не удалось восстановить счет по указанному идентификатору");

            return Ok(_mapper.Map<AccountModelResponse>(restoredAccount));
        }

    }
}
