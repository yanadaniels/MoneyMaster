namespace MoneyMaster.Services.Abstractions.Transaction;

public interface IBalanceChanger
{
    public Task ChangeBalanceWithRollbackAsync(
        Domain.Entities.Entities.Transaction transaction,
        Func<Task> rollbackFunction,
        bool useReverseCategoryType,
        CancellationToken cancellationToken
    );
}