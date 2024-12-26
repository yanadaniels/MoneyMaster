using AutoMapper;
using MoneyMaster.Services.Contracts.Report;
using MoneyMaster.WebAPI.Models.Report;

namespace MoneyMaster.WebAPI.Mapping
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
