// Ignore Spelling: Dto

using MoneyMasterService.Domain.Entities.Enums;
using MoneyMasterService.Services.Contracts.Transaction;
using MoneyMasterService.Services.Contracts.TransactionType;

namespace MoneyMasterService.Services.Contracts.Category
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

        /// <summary>Тип категории</summary>
        public required CategoryType CategoryType { get; set; }

        /// <summary>Коллекция транзакций</summary>
        public ICollection<TransactionResponse>? Transactions { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO категорий</summary>
    public class CategoryDto : CategoryDto<Guid>;
}
