using AutoMapper;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Services.Contracts.Category;

namespace MoneyMaster.Services.Implementations.Mapping
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
