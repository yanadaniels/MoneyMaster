using MoneyMasterService.Domain.Entities.Enums;

namespace MoneyMasterService.WebAPI.Models.Category
{
    /// <summary>Модель категорий</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class CategoryModelResponse<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Тип категории</summary>
        public CategoryType CategoryType {get; set;}

        /// <summary></summary>
        public bool IsSystem { get; set; }

        /// <summary>Признак того что сущность удалена</summary>
        public bool IsDeleted { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель категорий</summary>
    public class CategoryModelResponse : CategoryModelResponse<Guid>;
}
