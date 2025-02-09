using MoneyMaster.Services.Contracts.Category;

namespace MoneyMaster.Services.Abstractions
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
    }
}
