using System.ComponentModel.DataAnnotations;

namespace MoneyMaster.Services.Contracts.Transaction;

/// <summary>Модель создания транзакции </summary>
/// <typeparam name="TKey">Тип первичного ключа</typeparam>
public record CreatingTransactionRequest<TKey>
{
    /// <summary>Количество</summary>
    [Required]
    [Range(0, int.MaxValue)]
    public required decimal Amount { get; init; }

    /// <summary>Идентификатор категории</summary>
    [Required]
    public required TKey CategoryId { get; init; }

    /// <summary>Описание</summary>
    public string? Description { get; init; }

    /// <summary>Идентификатор счета</summary>
    [Required]
    public required TKey AccountId { get; init; }
}

/// <summary>DTO транзакции</summary>
public record CreatingTransactionRequest: CreatingTransactionRequest<Guid>;