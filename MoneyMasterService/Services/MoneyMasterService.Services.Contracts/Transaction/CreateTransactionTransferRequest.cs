using System.ComponentModel.DataAnnotations;

namespace MoneyMasterService.Services.Contracts.Transaction
{
    /// <summary>Модель создания транзакции для перевода </summary>
    /// <typeparam name="TKey">Тип первичного ключа</typeparam>
    public record CreateTransactionTransferRequest<TKey>
    {
        /// <summary>Количество</summary>
        [Required]
        [Range(0, int.MaxValue)]
        public required decimal Amount { get; init; }

        /// <summary>Идентификатор счета списания</summary>
        [Required]
        public required TKey FromAccountId { get; init; }

        /// <summary>Идентификатор категории счета списания</summary>
        [Required]
        public required TKey FromCategoryId { get; init; }

        /// <summary>Идентификатор счета зачисления</summary>
        [Required]
        public required TKey ToAccountId { get; init; }

        /// <summary>Идентификатор категории счета зачисления</summary>
        [Required]
        public required TKey ToCategoryId { get; init; }

        /// <summary>Описание</summary>
        public string? Description { get; init; }
    }

    /// <summary>DTO транзакции перевода</summary>
    public record CreateTransactionTransferRequest : CreateTransactionTransferRequest<Guid>;
}
