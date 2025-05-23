﻿using AutoMapper;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Domain.Entities.Enums;
using MoneyMasterService.Services.Abstractions;
using MoneyMasterService.Services.Contracts.Category;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterService.Services.Implementations
{
    /// <summary>Сервис работы с категориями</summary>
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        /// <summary><inheritdoc cref="CategoryService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="categoryRepository">Репозиторий</param>
        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ICollection<CategoryDto>> GetAllAsync()
        {
            ICollection<Category> entities = _categoryRepository.GetAll().ToList();
            var categoryType = Enum.GetNames(typeof(CategoryType)).ToList();
            return _mapper.Map<ICollection<Category>, ICollection<CategoryDto>>(entities);
        }

        public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetAsync(id, cancellationToken);
            return _mapper.Map<Category, CategoryDto>(category);
        }
    }
}
