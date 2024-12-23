using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с Аккаунтом.
    /// </summary>
    public interface IAccountRepository:IRepository<Account,Guid>
    {
    }
}
