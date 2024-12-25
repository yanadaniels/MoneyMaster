using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Report;

namespace MoneyMaster.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер отчетов
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IReportService _reportService;

        public ReportController(ILogger<ReportController> logger, IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        /// <summary>
        /// Получение объекта отчета
        /// </summary>
        /// <remarks>Данный метод позволяет получить объект отчета по её идентификатору</remarks>
        /// <param name="id">Идентификатор отчета</param>
        /// <response code="200">Получение объекта отчета</response>
        /// <response code="404">Не удалось найти отчет по указанному идентификатору</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType<ReportDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var report = await _reportService.GetByIdAsync(id);

            if (report == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти отчет по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, report);
        }

        /// <summary>
        /// Получение всех отчетов.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех отчетов. 
        /// </remarks>
        /// <response code="200">Получение списка всех отчетов</response>
        [HttpGet]
        [ProducesResponseType<ICollection<ReportDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, reports);
        }
    }
}
