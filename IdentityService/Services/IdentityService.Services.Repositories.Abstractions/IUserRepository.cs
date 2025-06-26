using IdentityService.Domain.Entities;
using MoneyMaster.Common.Interfaces.Repositories;

namespace IdentityService.Services.Repositories.Abstractions
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

        /// <summary>
        /// Авторизация пользователя по имени и паролю
        /// </summary>
        /// <param name="userName">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public Task<User?> AuthorizeUserAsync(string email, string password, CancellationToken cancellationToken);
    }
}
