using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с категориями.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
    }
}
