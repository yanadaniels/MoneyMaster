using MoneyMasterService.Domain.Entities.Enums;

namespace MoneyMasterService.Services.Contracts.Category
{
    /// <summary>Модель создания категории</summary>
    public class CreatingCategoryDto
    {
        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Тип категории</summary>
        public required CategoryType CategoryType { get; set; }
    }

}
