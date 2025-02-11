// Ignore Spelling: Validator

using AutoMapper;
using FluentValidation;
using IdentityService.Services.Abstractions;
using IdentityService.Services.Contracts.User;
using IdentityService.WebAPI.Infrastructure;
using IdentityService.WebAPI.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Common;
using MoneyMaster.Common.Options;
using System.Security.Claims;

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
        private readonly IValidator<CreatingUserModel> _creatingValidator;
        private readonly IValidator<UserAuthorizeModel> _authorizeValidator;

        public UserController(ILogger<UserController> logger,
            IUserService userService,
            IMapper mapper, 
            AuthOptions authOptions, 
            IValidator<CreatingUserModel> creatingValidator,
            IValidator<UserAuthorizeModel> authorizeValidator)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
            _authOptions = authOptions;
            _creatingValidator = creatingValidator;
            _authorizeValidator = authorizeValidator;
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
        [ProducesResponseType<UserModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[Authorize]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти пользователя по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<UserModel>(user));
        }


        /// <summary>
        /// Создание пользователя
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
        public async Task<IActionResult> Create([FromBody] CreatingUserModel model)
        {
            var validationResult = await _creatingValidator.ValidateAsync(model);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Aggregate("Validation error: ", (a, b) => $"{a} {b};"));

            var result = await _userService.AddAsync(_mapper.Map<CreatingUserDto>(model));
            if (result is null)
                return Conflict($"Конфликт: элемент с именем = {model.UserName} или с email = {model.Email} уже существует");
            var userModel = _mapper.Map<CreatingUserModel>(result);

            var resourceUrl = Url.Action(nameof(Get), new { id = userModel.Id });
            return Created(resourceUrl, userModel);
        }

        /// <summary>
        /// Получение всех пользователей.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех пользователей. 
        /// </remarks>
        /// <response code="200">Получение списка пользователей</response>
        [HttpGet]
        [ProducesResponseType<ICollection<UserModel>>(StatusCodes.Status200OK)]
        //[RequirePrivilege(Privileges.Administrator, Privileges.System)]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return Ok(_mapper.Map<ICollection<UserModel>>(users));
        }

        /// <summary>
        /// Авторизованный пользователь, без каких-либо ограничений
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Получение объекта пользователя</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        [HttpPost("authorize")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Token([FromBody] UserAuthorizeModel user)
        {
            var validationResult = await _authorizeValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Aggregate("Validation error: ", (a, b) => $"{a} {b};"));

            var identity = await GetIdentity(_mapper.Map<UserAuthorizeDto>(user));
            if (identity == null)
                return BadRequest(new { errorText = "Неверное имя пользователя или пароль." });


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
