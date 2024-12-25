using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с счетами.
    /// </summary>
    public interface IAccountRepository:IRepository<Account,Guid>
    {
    }
}
