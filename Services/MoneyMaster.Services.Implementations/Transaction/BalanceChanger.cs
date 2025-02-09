using MoneyMaster.Domain.Entities.DomainExceptions;
using MoneyMaster.Domain.Entities.Enums;
using MoneyMaster.Services.Abstractions;
using MoneyMaster.Services.Abstractions.Transaction;
using MoneyMaster.Services.Contracts.Category;

namespace MoneyMaster.Services.Implementations.Transaction;

public class BalanceChanger : IBalanceChanger
{
    //TODO реализовать кэш
    private readonly ICategoryService _categoryService;
    private readonly IAccountService _accountService;

    public BalanceChanger(ICategoryService categoryService, IAccountService accountService)
    {
        _categoryService = categoryService;
        _accountService = accountService;
    }

    public async Task ChangeBalanceWithRollbackAsync(
        Domain.Entities.Entities.Transaction transaction,
        Func<Task> rollbackFunction,
        bool useReverseCategoryType,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var category = await GetCategoryByIdAsync(transaction.CategoryId, cancellationToken);

            var categoryType = useReverseCategoryType
                ? GetReverseCategoryType(category.CategoryType)
                : category.CategoryType;

            await ChangeBalanceAsync(categoryType, transaction.AccountId, transaction.Amount,
                cancellationToken);
        }
        catch (Exception exception)
        {
            await rollbackFunction.Invoke();

            throw new BalanceChangeException(exception);
        }
    }

    private async Task<CategoryDto> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetByIdAsync(id, cancellationToken);

        if (category == null)
        {
            throw new CategoryNotFoundException();
        }

        return category;
    }

    private async Task ChangeBalanceAsync(CategoryType categoryType, Guid accountId, decimal amount,
        CancellationToken cancellationToken)
    {
        if (categoryType == CategoryType.Revenue)
        {
            await _accountService.IncreaseBalanceAsync(accountId, amount, cancellationToken);
        }
        else
        {
            await _accountService.DecreaseBalanceAsync(accountId, amount, cancellationToken);
        }
    }

    private static CategoryType GetReverseCategoryType(CategoryType categoryType) =>
        categoryType == CategoryType.Revenue ? CategoryType.Expenses : CategoryType.Revenue;
}