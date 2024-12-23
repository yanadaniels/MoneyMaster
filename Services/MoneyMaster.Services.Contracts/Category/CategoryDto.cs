// Ignore Spelling: Dto

using MoneyMaster.Services.Contracts.Transaction;
using MoneyMaster.Services.Contracts.TransactionType;

namespace MoneyMaster.Services.Contracts.Category
{
    /// <summary>DTO категорий</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class CategoryDto<TKey>
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
        public required TransactionTypeDto TransactionType { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<TransactionDto>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class CategoryDto : CategoryDto<Guid>;
}
