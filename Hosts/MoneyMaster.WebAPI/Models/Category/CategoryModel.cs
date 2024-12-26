using MoneyMaster.WebAPI.Models.Transaction;
using MoneyMaster.WebAPI.Models.TransactionType;

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

        /// <summary>Тип транзакции</summary>
        public required TransactionTypeModel TransactionType { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<TransactionModel>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель категорий</summary>
    public class CategoryModel : CategoryModel<Guid>;
}
