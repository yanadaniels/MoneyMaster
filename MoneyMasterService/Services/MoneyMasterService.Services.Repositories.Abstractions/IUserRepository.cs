using MoneyMaster.Common.Interfaces.Repositories;
using MoneyMasterService.Domain.Entities;

namespace MoneyMasterService.Services.Repositories.Abstractions
{
    /// <summary>
    /// Репозиторий работы с пользователями.
    /// </summary>
    public interface IUserRepository : IRepository<User, Guid>
    {
        /// <summary>Существует ли в репозитории указанная сущность</summary>
        /// <param name="item">Проверяемая сущность</param>
        /// <param name="Cancel">Признак отмены асинхронной операции</param>
        /// <returns>Истина, если указанная сущность существует в репозитории</returns>
        Task<bool> Exist(User item, CancellationToken Cancel = default);
    }
}
