using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.Services.Contracts.Category;
using MoneyMasterService.WebAPI.Models.Category;

namespace MoneyMasterService.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер категорий
    /// </summary>
    [ApiController]
    [Route("api/v1/categories/")]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор для инициализации контроллера категорий.
        /// </summary>
        /// <param name="logger">Логгер для регистрации событий и ошибок.</param>
        /// <param name="categoryService">Сервис для работы с аккаунтами.</param>
        /// <param name="mapper">Объект для маппинга между сущностями и DTO.</param>
        public CategoryController(ILogger<CategoryController> logger, ICategoryService categoryService, IMapper mapper)
        {
            _logger = logger;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение объекта категорий
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект категорий по её идентификатору</remarks>
        /// <param name="id">Идентификатор категории</param>
        /// <response code="200">Получение объекта категории</response>
        /// <response code="404">Не удалось найти категорию по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<CategoryModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);

            if (category == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти категорию по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<CategoryModelResponse>(category));
        }

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех категорий. 
        /// </remarks>
        /// <response code="200">Получение списка всех категорий</response>
        [HttpGet]
        [ProducesResponseType<ICollection<CategoryModelResponse>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<CategoryModelResponse>>(categories));
        }

        /// <summary>
        /// Добавить новую категорию.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет добавить новую категорию. 
        /// </remarks>
        /// <response code="201">Возвращает добавленную категорию</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPost]
        [ProducesResponseType<CategoryModelResponse>(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreatingCategoryModelRequest model, CancellationToken cancellationToken)
        {
            if (model == null)
                return BadRequest("Отсутствуют данные");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //model.UserId = UserId;
            var newCategoryDto = _mapper.Map<CreatingCategoryDto>(model);
            var categoryDto = await _categoryService.AddAsync(newCategoryDto, cancellationToken);
            var categoryModel = _mapper.Map<CategoryModelResponse>(categoryDto);

            var resourceUrl = Url.Action(nameof(Get), new { id = categoryDto.Id });
            return Created(resourceUrl, categoryModel);
        }


        /// <summary>
        /// Редактировать категорию.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет редактировать данные категории.
        /// </remarks>
        /// <param name="model"></param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Возвращает отредактированную категорию</response>
        /// <response code="400">Неверные данные, возвращается ValidationProblemDetails с указанием где данные были некорректны</response>
        /// <response code="404">Не удалось найти категорию по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpPut("{id}")]
        [ProducesResponseType<CategoryModelResponse>(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdatingCategoryModelRequest model, CancellationToken cancellationToken)
        {
            if (model == null)
                return BadRequest("Отсутствуют данные");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedCategoryDto = _mapper.Map<UpdatingCategoryDto>(model);
            var updatingCategoryDto = await _categoryService.UpdateAsync(updatedCategoryDto, cancellationToken);
            var сategoryModel = _mapper.Map<CategoryModelResponse>(updatingCategoryDto);

            if (updatingCategoryDto != null)
                return Ok(сategoryModel);
            else
                return NotFound("Категория с указанным идентификатором не найдена");
        }

        /// <summary>
        /// Удалить категорию.
        /// </summary>
        /// <remarks>Данный метод позволяет пометить категорию как удаленный по её ID</remarks>
        /// <param name="id">Идентификатор удаляемой категории</param>
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="204">При успешном удалении категории</response>
        /// <response code="404">Не удалось найти категорию по указанному идентификатору</response>
        /// <response code="500">Внутренняя ошибка сервера, возвращает ProblemDetails</response>
        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var deletedCategory = await _categoryService.DeleteAsync(id, cancellationToken);

            if (deletedCategory == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось удалить категорию по указанному идентификатору");

            return NoContent();
        }
    }
}
