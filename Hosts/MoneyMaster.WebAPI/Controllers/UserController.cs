using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.User;
using MoneyMaster.WebAPI.Models.User;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер пользователя
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(ILogger<UserController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _userService = userService;
            _mapper = mapper;
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
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти пользователя по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<UserModel>(user) );
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
        public async Task<IActionResult> Create([FromBody] CreatingUserModel model)
        {
            var t = _mapper.Map<CreatingUserDto>(model);
            var result = await _userService.AddAsync(_mapper.Map<CreatingUserDto>(model));
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
        [ProducesResponseType<ICollection<UserModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<UserModel>>(users));
        }
    }
}
