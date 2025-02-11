using AutoMapper;
using MoneyMasterService.Services.Contracts.Report;
using MoneyMasterService.WebAPI.Models.Report;

namespace MoneyMasterService.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности отчета</summary>
    public class ReportModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="ReportModelMappingsProfile"/> </summary>
        public ReportModelMappingsProfile()
        {
            CreateMap<ReportDto, ReportModel>();
        }
    }
}
