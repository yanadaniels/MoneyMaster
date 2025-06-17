using MoneyMasterService.Domain.Entities.Enums;

namespace MoneyMasterService.Services.Contracts.Category
{
    public class UpdatingCategoryDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Тип категории</summary>
        public required CategoryType CategoryType { get; set; }

    }

    /// <summary>DTO категорий</summary>
    public class UpdatingCategoryDto : UpdatingCategoryDto<Guid>;
}
