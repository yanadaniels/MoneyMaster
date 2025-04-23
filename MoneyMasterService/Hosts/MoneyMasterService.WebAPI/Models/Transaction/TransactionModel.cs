using MoneyMasterService.WebAPI.Models.Account;
using MoneyMasterService.WebAPI.Models.Category;
using MoneyMasterService.WebAPI.Models.TransactionType;

namespace MoneyMasterService.WebAPI.Models.Transaction
{
    /// <summary>Модель транзакции</summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public class TransactionModel<TKey>
    {
        /// <summary>Первичный ключ </summary>
        public TKey? Id { get; set; }

        /// <summary>Количество</summary>
        public decimal Amount { get; set; }

        /// <summary>Идентификатор категории</summary>
        public TKey? CategoryId { get; set; }

        /// <summary>Модель категории</summary>
        public required CategoryModelResponse Category { get; set; }

        /// <summary>Описание</summary>
        public string? Description { get; set; }

        /// <summary>Идентификатор типа транзакции</summary>
        public TKey? TransactionTypeId { get; set; }

        /// <summary>Тип транзакции</summary>
        public required TransactionTypeModel TransactionType { get; set; }

        /// <summary>Идентификатор счета</summary>
        public TKey? AccountId { get; set; }

        /// <summary>Счет</summary>
        public required AccountModelResponse Account { get; set; }

        /// <summary>Время</summary>
        public DateTime CreateAt { get; set; }
    }

    /// <summary>Модель транзакции</summary>
    public class TransactionModel : TransactionModel<Guid>;
}
