using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Account;
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
            CreateMap<UpdatingCategoryDto, Category>()
                .ForMember(category => category.CreateAt, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<CreatingCategoryDto, Category>()
                .ForMember(category => category.Id, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(category => category.IsDeleted, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(category => category.Transactions, memberConfiguration => memberConfiguration.Ignore())
                .ForMember(category => category.CreateAt, opt => opt.MapFrom(src=> DateTime.UtcNow))
                ;
        }
    }
}
