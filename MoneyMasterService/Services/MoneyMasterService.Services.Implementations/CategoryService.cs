using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CategoryService> _logger;

        /// <summary><inheritdoc cref="CategoryService"/> </summary>
        /// <param name="mapper">Маппер</param>
        /// <param name="categoryRepository">Репозиторий</param>
        public CategoryService(IMapper mapper, ICategoryRepository categoryRepository, ILogger<CategoryService> logger)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ICollection<CategoryDto>> GetAllAsync()
        {
            ICollection<Category> entities = await _categoryRepository.GetAll().ToListAsync();
            var categoryType = Enum.GetNames(typeof(CategoryType)).ToList();
            return _mapper.Map<ICollection<Category>, ICollection<CategoryDto>>(entities);
        }

        public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _categoryRepository.GetAsync(id, cancellationToken);
                return category is null ? null : _mapper.Map<Category, CategoryDto>(category);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при доступе к базе данных во время получения категории по ID: {id}.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {GetByIdAsync}.", nameof(GetByIdAsync));
                throw;
            }
        }

        public async Task<CategoryDto> AddAsync(CreatingCategoryDto newCategoryDto, CancellationToken cancellationToken)
        {
            if (newCategoryDto is null)
            {
                _logger.LogError($"Попытка передать null в аргумент 'newCategoryDto' при создании нового счета: {newCategoryDto}");
                throw new ArgumentNullException(nameof(newCategoryDto), "Переданный объект account не может быть null.");
            }

            var newCategory = _mapper.Map<Category>(newCategoryDto);

            try
            {
                var createdCategory = await _categoryRepository.AddAsync(newCategory);
                await _categoryRepository.SaveChangesAsync();

                return _mapper.Map<CategoryDto>(createdCategory);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при сохранении категории в базу данных.");
                throw new InvalidOperationException("Не удалось создать новую категорию. Проверьте данные и попробуйте снова.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {AddAsync}.", nameof(AddAsync));
                throw;
            }
        }

        public async Task<UpdatingCategoryDto?> UpdateAsync(UpdatingCategoryDto updateCategoryDto, CancellationToken cancellationToken)
        {
            var updateCategory = _mapper.Map<Category>(updateCategoryDto);

            try
            {
                _categoryRepository.Update(updateCategory);
                await _categoryRepository.SaveChangesAsync();
                return updateCategoryDto;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении категории в базе данных");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {UpdateAsync}.", nameof(UpdateAsync));
                throw;
            }
        }

        public async Task<CategoryDto?> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var deletedCategory = await _categoryRepository.SoftDeleteAsync(id, cancellationToken);
                return deletedCategory is null ? null : _mapper.Map<CategoryDto>(deletedCategory);

            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Мягкое удаление не поддерживается для типа {typeof(Category).Name} in {nameof(DeleteAsync)}.");
                throw;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"Ошибка при доступе к базе данных во время удаления категории");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошла непредвиденная ошибка в методе {DeleteAsync}.", nameof(DeleteAsync));
                throw;
            }
        }
    }
}
