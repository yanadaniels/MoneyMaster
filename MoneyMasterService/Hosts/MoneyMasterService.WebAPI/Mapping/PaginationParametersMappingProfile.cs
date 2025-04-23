using AutoMapper;
using MoneyMaster.Common;
using MoneyMasterService.WebAPI.Models.Common;

namespace MoneyMasterService.WebAPI.Mapping
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
