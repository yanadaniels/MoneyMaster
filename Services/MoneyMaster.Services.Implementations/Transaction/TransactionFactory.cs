using MoneyMaster.Services.Contracts.Transaction;

namespace MoneyMaster.Services.Implementations.Transaction;

public static class TransactionFactory
{
    public static Domain.Entities.Entities.Transaction Create(CreatingTransactionRequest request)
    {
        return new Domain.Entities.Entities.Transaction
        {
            Id = Guid.NewGuid(),
            CreateAt = DateTime.UtcNow,
            Amount = request.Amount,
            CategoryId = request.CategoryId,
            Description = request.Description,
            IsDeleted = false,
            AccountId = request.AccountId
        };
    }
}