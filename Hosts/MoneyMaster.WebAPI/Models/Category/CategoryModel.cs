using MoneyMaster.Domain.Entities.Enums;
using MoneyMaster.Services.Contracts.Transaction;

namespace MoneyMaster.WebAPI.Models.Category
{
    /// <summary>Модель категорий</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class CategoryModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Иконка</summary>
        public string? Icon { get; set; }

        /// <summary>Идентификатор типа транзакции</summary>
        public TKey? TransactionTypeId { get; set; }

        /// <summary>Тип категории</summary>
        public required CategoryType CategoryType { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<TransactionResponse>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель категорий</summary>
    public class CategoryModel : CategoryModel<Guid>;
}
