using AutoMapper;
using MoneyMaster.Services.Contracts.Category;
using MoneyMaster.WebAPI.Models.Category;

namespace MoneyMaster.WebAPI.Mapping
{
    /// <summary>Профиль автомаппера для сущности категорий</summary>
    public class CategoryModelMappingsProfile : Profile
    {
        /// <summary><inheritdoc cref="CategoryModelMappingsProfile"/> </summary>
        public CategoryModelMappingsProfile()
        {
            CreateMap<CategoryDto, CategoryModel>();
        }
    }
}
