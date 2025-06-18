using MoneyMasterService.Domain.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace MoneyMasterService.WebAPI.Models.Category
{
    /// <summary>Модель создания категории </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class CreatingCategoryModelRequest<TKey>
    {
        /// <summary>Имя</summary>
        [Required(ErrorMessage = "Название категории обязательно.")]
        [StringLength(50, ErrorMessage = "Название категории не может быть длиннее 50 символов.")]
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Тип категории</summary>
        [EnumDataType(typeof(CategoryType), ErrorMessage = "Выберите тип категории")]
        public required CategoryType CategoryType { get; set; }
    }

    /// <summary>Модель создания категории </summary>
    public class CreatingCategoryModelRequest:CreatingCategoryModelRequest<Guid> { }
}
