using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.WebAPI.Models.Category;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер категорий
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CategoryController : ControllerBase
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

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
        /// <param name="cancellationToken">Токен отмены</param>
        /// <response code="200">Получение объекта категории</response>
        /// <response code="404">Не удалось найти категорию по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<CategoryModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryService.GetByIdAsync(id, cancellationToken);

            if (category == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти категорию по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<CategoryModel>(category));
        }

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех категорий. 
        /// </remarks>
        /// <response code="200">Получение списка всех категорий</response>
        [HttpGet]
        [ProducesResponseType<ICollection<CategoryModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<CategoryModel>>(categories));
        }
    }
}
