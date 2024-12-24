using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.User;
using MoneyMaster.Services.Contracts.UserSetting;

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

        /// <summary><inheritdoc cref="UserSettingController"/></summary>
        /// <param name="logger"></param>
        /// <param name="userSettingService"></param>
        public UserSettingController(ILogger<UserSettingController> logger, IUserSettingService userSettingService)
        {
            _logger = logger;
            _userSettingService = userSettingService;
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
        [ProducesResponseType<UserSettingDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var userSetting = await _userSettingService.GetByIdAsync(id);

            if (userSetting == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Настройки пользователя не найдены");

            return StatusCode(StatusCodes.Status200OK, userSetting);
        }

        /// <summary>
        /// Получение всех настроек пользователей.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех настроек пользователей. 
        /// </remarks>
        /// <response code="200">Получение списка настроек пользователей</response>
        [HttpGet]
        [ProducesResponseType<ICollection<UserSettingDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var userSettings = await _userSettingService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, userSettings);
        }
    }
}
