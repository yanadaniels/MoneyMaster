using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Services.Contracts.Transaction;

namespace MoneyMasterService.Services.Implementations
{
    public static class TransactionFactory
    {
        public static Transaction Create(CreatingTransactionRequest request)
        {
            return new Transaction
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
}
