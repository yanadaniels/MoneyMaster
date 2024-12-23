using MoneyMaster.Services.Contracts.Category;
using MoneyMaster.Services.Contracts.Transaction;

namespace MoneyMaster.Services.Contracts.TransactionType
{
    /// <summary>DTO типа транзакции</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class TransactionTypeDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Имя</summary>
        public required string Name { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Коллекция категорий</summary>
        public ICollection<CategoryDto>? Categories { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<TransactionDto>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary><inheritdoc/></summary>
    public class TransactionTypeDto: TransactionTypeDto<Guid>;
}
