using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с счетами.
    /// </summary>
    public interface IAccountRepository:IRepository<Account,Guid>
    {
    }
}
