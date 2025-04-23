using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с типом счета.
    /// </summary>
    public interface IAccountTypeRepository : IRepository<AccountType, Guid>
    {
    }
}
