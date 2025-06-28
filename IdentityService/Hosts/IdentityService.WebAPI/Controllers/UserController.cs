using AutoMapper;
using IdentityService.Services.Abstractions;
using IdentityService.Services.Contracts.User;
using IdentityService.WebAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    [ApiController]
    [Route("/api/v1/users/")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор контроллера пользователя
        /// </summary>
        /// <param name="logger">Логгер</param>
        /// <param name="userService">Сервис пользователя</param>
        /// <param name="mapper">Маппер</param>
        public UserController(
            ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        /// <remarks>Данный метод позволяет получить пользователя по её идентификатору</remarks>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="404">Не удалось найти пользователя по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<UserModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var user = await _userService.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти пользователя по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<UserModelResponse>(user));
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет создать нового пользователя
        /// </remarks>
        /// <response code="201">Новый пользователь успешно создан</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        /// <response code="409">Ошибки при указании данных для создания пользователя</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] 
            CreatingUserModelRequest model, 
            CancellationToken cancellationToken)
        {
            var result = await _userService.AddAsync(_mapper.Map<CreatingUserDto>(model), cancellationToken);
            if (result is null)
                return Conflict($"Конфликт: элемент с именем = {model.UserName} или с email = {model.Email} уже существует");

            return Created("/login", new
            {
                Message = "Вы успешно зарегестрировались. Пожалуйста перейдите на страницу авторизации",
                LoginUrl = "/login"
            });
        }

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех пользователей. 
        /// </remarks>
        /// <response code="200">Получение списка пользователей</response>
        [HttpGet]
        [ProducesResponseType<ICollection<UserModelResponse>>(StatusCodes.Status200OK)]
        //[RequirePrivilege(Privileges.Administrator, Privileges.System)]
        [Authorize]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var users = await _userService.GetAllAsync(cancellationToken);

            return Ok(_mapper.Map<ICollection<UserModelResponse>>(users));
        }

        /// <summary>
        /// Авторизоваться
        /// </summary>
        /// <param name="user"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        [HttpPost("login")]
        [ProducesResponseType<UserJwtTokenResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<UserJwtTokenResponse>> Login([FromBody] 
            UserAuthorizeModelRequest user,
            CancellationToken cancellationToken)
        {
            var userJwtTokenDto = await _userService.SignIn(_mapper.Map<UserAuthorizeDto>(user), cancellationToken);
            if (userJwtTokenDto == null)
                return Unauthorized(new { errorText = "Неверное имя пользователя или пароль" });

            var response = _mapper.Map<UserJwtTokenResponse>(userJwtTokenDto);

            return Ok(response);
        }

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <remarks>Данный метод позволяет выйти из системы</remarks>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="204">При успешном выходе из системы</response>
        /// <response code="400">Если произошла ошибка при выходе</response>
        [HttpPost("logout/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<ActionResult> Logout([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var isLogout = await _userService.SignOut(id, cancellationToken);
            if (!isLogout)
                return BadRequest(new { errorText = "Не удалось разлогиниться. Пользователь не найден или произошла ошибка." });

            return NoContent();
        }


        /// <summary>
        /// Обновить Access и Refresh токен
        /// </summary>
        /// <param name="oldRefreshToken">Объект с данными для обновления Access токена</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        [HttpPost("refresh/{id}")]
        [ProducesResponseType<UserRefreshJwtTokenResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public async Task<ActionResult<UserRefreshJwtTokenResponse>> RefreshToken([FromBody] 
            RefreshTokenRequest oldRefreshToken, 
            CancellationToken cancellationToken)
        {
            var userRefreshTokenDto = _mapper.Map<RefreshTokenRequest, UserRefreshTokenDto>(oldRefreshToken);
            var userJwtTokenDto = await _userService.RefreshToken(userRefreshTokenDto, cancellationToken);

            if (userJwtTokenDto == null)
                return BadRequest(new { errorText = "Неверное имя пользователя или пароль или устарел Refresh токен" });

            var response = _mapper.Map<UserRefreshJwtTokenResponse>(userJwtTokenDto);

            return Ok(response);
        }

        /// <summary>
        /// Обновить данные пользователя
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <param name="updateUser">Json c обновленными свойствами</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        /// <response code="401">Если нет авторизации</response>
        [HttpPatch("{id}")]
        [ProducesResponseType<UserModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]
        public async Task<ActionResult<UserModelResponse>> Update(Guid id, [FromBody] 
            UserUpdateModelRequest updateUser, 
            CancellationToken cancellationToken)
        {
            var updateUserDto = _mapper.Map<UserUpdateDto>(updateUser);
            var updatedUser = await _userService.UpdateAsync(updateUserDto, cancellationToken);

            if (updatedUser == null)
                return NotFound(new { errorText = "Пользователь не найден" });

            return Ok(_mapper.Map<UserModelResponse>(updatedUser));
        }

    }
}
