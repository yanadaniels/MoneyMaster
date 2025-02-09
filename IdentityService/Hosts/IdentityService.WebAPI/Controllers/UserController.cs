using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common.Options;
using MoneyMaster.Common;
using System.Security.Claims;
using IdentityService.Services.Abstractions;
using AutoMapper;
using IdentityService.Services.Contracts.User;
using IdentityService.WebAPI.Infrastructure;

namespace IdentityService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly AuthOptions _authOptions;

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper, AuthOptions authOptions)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _authOptions = authOptions;
        }

        /// <summary>
        /// Получение объекта пользователя
        /// </summary>
        /// <remarks>Данный метод позволяет получить пользователя по её идентификатору</remarks>
        /// <param name="id">Идентификатор пользователя</param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="404">Не удалось найти пользователя по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти пользователя по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<UserDto>(user));
        }


        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет создать нового пользователя
        /// </remarks>
        /// <response code="201">Новый пользователь успешно создан</response>
        /// <response code="409">Ошибки при указании данных для создания пользователя</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatingUserDto model)
        {
            var result = await _userService.AddAsync(model);
            if (result is null)
                return StatusCode(StatusCodes.Status409Conflict, $"Конфликт: элемент с именем = {model.UserName} или с email = {model.Email} уже существует");
            return StatusCode(StatusCodes.Status201Created, $"Пользователь успешно создан.");
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех пользователей. 
        /// </remarks>
        /// <response code="200">Получение списка пользователей</response>
        [HttpGet]
        [ProducesResponseType<ICollection<UserDto>>(StatusCodes.Status200OK)]
        [RequirePrivilege(Privileges.Administrator, Privileges.System)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<UserDto>>(users));
        }

        /// <summary>
        /// Авторизованный пользователь, без каких-либо ограничений
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("authorize")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Token([FromBody] UserAuthorizeDto user)
        {
            var identity = await GetIdentity(user);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Неверное имя пользователя или пароль." });
            }

            var encodedJwt = TokenProducer.GetJWTToken(identity.Claims, _authOptions);

            var response = new
            {
                token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(UserAuthorizeDto user)
        {
            var person = await _userService.AuthorizeUser(user);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role),
                    new Claim("ID", person.Id.ToString()),
                    new Claim("NameTelegram", person.TelegramUserName?.ToString()?? ""),
                    new Claim("Role", person.Role),
                    new Claim("EMail",person.Email)
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
