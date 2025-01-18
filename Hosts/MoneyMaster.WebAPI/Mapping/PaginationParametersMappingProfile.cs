using AutoMapper;
using MoneyMaster.Services.Contracts.Common;
using MoneyMaster.WebAPI.Models.Common;

namespace MoneyMaster.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для параметров пагинации.</summary>
    public class PaginationParametersMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="PaginationParametersMappingsProfile"/> </summary>
        public PaginationParametersMappingsProfile()
        {
            CreateMap<PaginationParametersModel, PaginationParameters>();
        }
    }
}
