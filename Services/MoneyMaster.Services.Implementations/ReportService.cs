using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Contracts.Report;
using MoneyMaster.Services.Repositories.Abstractions;

namespace MoneyMaster.Services.Implementations
{
    /// <summary>Сервис работы с отчетами</summary>
    public class ReportService : IReportService
    {
        private readonly IMapper _mapper;
        private readonly IReportRepository _reportRepository;

        /// <summary><inheritdoc cref="ReportService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="reportRepository">Репозиторий</param>
        public ReportService(IMapper mapper, IReportRepository reportRepository)
        {
            _mapper = mapper;
            _reportRepository = reportRepository;
        }

        public async Task<ICollection<ReportDto>> GetAllAsync()
        {
            ICollection<Report> entities = _reportRepository.GetAll().ToList();
            return _mapper.Map<ICollection<Report>, ICollection<ReportDto>>(entities);
        }

        public async Task<ReportDto> GetByIdAsync(Guid id)
        {
            var report = await _reportRepository.GetAsync(id, CancellationToken.None);
            return _mapper.Map<Report, ReportDto>(report);
        }
    }
}
