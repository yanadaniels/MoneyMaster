namespace MoneyMasterService.Services.Abstractions.Transaction
{
    public interface IBalanceChanger
    {
        public Task ChangeBalanceWithRollbackAsync(
            Domain.Entities.Transaction transaction,
            Func<Task> rollbackFunction,
            bool useReverseCategoryType,
            CancellationToken cancellationToken
        );
    }
}
