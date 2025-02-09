using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Category;

namespace MoneyMasterService.Services.Implementations.Mapping
{
    /// <summary>Профиль автомаппера для сущности категорий</summary>
    public class CategoryMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="CategoryMappingsProfile"/> </summary>
        public CategoryMappingsProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}
