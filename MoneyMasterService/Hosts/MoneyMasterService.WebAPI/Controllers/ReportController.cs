using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.WebAPI.Models.Report;

namespace MoneyMasterService.WebAPI.Controllers
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
        private readonly IMapper _mapper;

        public ReportController(ILogger<ReportController> logger, IReportService reportService,IMapper mapper)
        {
            _logger = logger;
            _reportService = reportService;
            _mapper = mapper;
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
        [ProducesResponseType<ReportModel>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            var report = await _reportService.GetByIdAsync(id);

            if (report == null)
                return StatusCode(StatusCodes.Status404NotFound, $"Не удалось найти отчет по указанному идентификатору");

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ReportModel>(report));
        }

        /// <summary>
        /// Получение всех отчетов.
        /// </summary>
        /// <remarks>
        /// Данный метод позволяет получить список всех отчетов. 
        /// </remarks>
        /// <response code="200">Получение списка всех отчетов</response>
        [HttpGet]
        [ProducesResponseType<ICollection<ReportModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK, _mapper.Map<ICollection<ReportModel>>(reports) );
        }
    }
}
