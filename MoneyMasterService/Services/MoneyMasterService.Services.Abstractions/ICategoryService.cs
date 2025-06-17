using MoneyMasterService.Services.Contracts.Account;
using MoneyMasterService.Services.Contracts.Category;

namespace MoneyMasterService.Services.Abstractions
{
    /// <summary>Интерфейс сервиса работы с категориями</summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Получить категорию по Id
        /// </summary>
        /// <param name="id"> Идентификатор. </param>
        /// <returns> ДТО категории. </returns>
        Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Получить список категорий.
        /// </summary>
        /// <returns> Список DTO категорий. </returns>
        Task<ICollection<CategoryDto>> GetAllAsync();

        /// <summary>
        /// Добавить новую категорию
        /// </summary>
        /// <param name="newCategoryDto"> Принимает DTO в качестве нового счёта </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> DTO счёта </returns>
        Task<CategoryDto> AddAsync(CreatingCategoryDto newCategoryDto, CancellationToken cancellationToken);

        /// <summary>
        /// Редактировать категорию
        /// </summary>
        /// <param name="updateCategoryDto"> Принимает DTO в качестве обновленного счёта</param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns></returns>
        Task<UpdatingCategoryDto?> UpdateAsync(UpdatingCategoryDto updateCategoryDto, CancellationToken cancellationToken);

        /// <summary>
        /// Удалить категорию по id
        /// </summary>
        /// <param name="id"> Идентификатор </param>
        /// <param name="cancellationToken"> Токен отмены </param>
        /// <returns> Подтверждение удаления </returns>
        Task<CategoryDto?> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
