using AutoMapper;
using MoneyMasterService.Services.Contracts.Category;
using MoneyMasterService.WebAPI.Models.Category;

namespace MoneyMasterService.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности категорий</summary>
    public class CategoryModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="CategoryModelMappingsProfile"/> </summary>
        public CategoryModelMappingsProfile()
        {
            CreateMap<CategoryDto, CategoryModelResponse>();

            CreateMap<CreatingCategoryModelRequest, CreatingCategoryDto>();

            CreateMap<CreatingCategoryDto, CategoryModelResponse>();

            CreateMap<UpdatingCategoryDto, CategoryModelResponse>();

            CreateMap<UpdatingCategoryModelRequest, UpdatingCategoryDto>();
        }
    }
}
