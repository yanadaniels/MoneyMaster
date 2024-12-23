using MoneyMaster.Domain.Entities;

namespace MoneyMaster.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с пользователями.
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
    }
}
