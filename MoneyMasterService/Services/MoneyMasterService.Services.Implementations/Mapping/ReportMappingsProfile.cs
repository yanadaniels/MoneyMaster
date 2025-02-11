using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Report;

namespace MoneyMasterService.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности отчета</summary>
    public class ReportMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="ReportMappingsProfile"/> </summary>
        public ReportMappingsProfile()
        {
            CreateMap<Report, ReportDto>();
        }
    }
}
