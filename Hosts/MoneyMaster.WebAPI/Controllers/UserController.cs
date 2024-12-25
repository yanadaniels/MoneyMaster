﻿using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.User;

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

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
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
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти пользователя по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, user);
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
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, users);
        }
    }
}