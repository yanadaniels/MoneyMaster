﻿using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.Report;
using MoneyMaster.Services.Contracts.User;

namespace MoneyMaster.Services.Implementations.Mapping
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
