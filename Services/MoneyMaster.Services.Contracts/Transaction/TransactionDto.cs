// Ignore Spelling: Dto

using MoneyMaster.Services.Contracts.Account;
using MoneyMaster.Services.Contracts.Category;
using MoneyMaster.Services.Contracts.TransactionType;

namespace MoneyMaster.Services.Contracts.Transaction
{
    /// <summary>DTO транзакции</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class TransactionDto<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Количество</summary>
        public decimal Amount { get; set; }

        /// <summary>Идентификатор категории</summary>
        public TKey? CategoryId { get; set; }

        public required CategoryDto Category { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Идентификатор типа транзакции</summary>
        public TKey? TransactionTypeId { get; set; }

        /// <summary>Тип транзакции</summary>
        public required TransactionTypeDto TransactionType { get; set; }

        /// <summary>Идентификатор аккаунта</summary>
        public TKey? AccountId { get; set; }

        /// <summary>Аккаунт</summary>
        public required AccountDto Account { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>DTO транзакции</summary>
    public class TransactionDto: TransactionDto<Guid>;
}
