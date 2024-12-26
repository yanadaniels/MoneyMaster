using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.WebAPI.Models.UserSetting;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер настроек пользователя
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserSettingController : ControllerBase
    {
        private readonly ILogger<UserSettingController> _logger;
        private readonly IUserSettingService _userSettingService;
        private readonly IMapper _mapper;

        /// <summary><inheritdoc cref="UserSettingController"/></summary>
        /// <param name="logger"></param>
        /// <param name="userSettingService"></param>
        public UserSettingController(ILogger<UserSettingController> logger, IUserSettingService userSettingService, IMapper mapper)
        {
            _logger = logger;
            _userSettingService = userSettingService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение объекта настроек пользователя
        /// </summary>
        /// <remarks>Данный метод позволяет получить настройки пользователя по её идентификатору</remarks>
        /// <param name="id">Идентификатор настроек пользователя</param>
        /// <response code="200">Получение объекта настроек пользователя</response>
        /// <response code="404">Не удалось найти настройки пользователя по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<UserSettingModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var userSetting = await _userSettingService.GetByIdAsync(id);

            if (userSetting == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Настройки пользователя не найдены");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<UserSettingModel>(userSetting) );
        }

        /// <summary>
        /// Получение всех настроек пользователей.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех настроек пользователей. 
        /// </remarks>
        /// <response code="200">Получение списка настроек пользователей</response>
        [HttpGet]
        [ProducesResponseType<ICollection<UserSettingModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var userSettings = await _userSettingService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<UserSettingModel>>(userSettings));
        }
    }
}
