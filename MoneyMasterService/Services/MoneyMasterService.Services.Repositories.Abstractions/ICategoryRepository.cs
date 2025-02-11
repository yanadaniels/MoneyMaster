using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с категориями.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
