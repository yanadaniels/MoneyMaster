using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Repositories;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using MoneyMasterService.Services.Repositories.Abstractions;

namespace MoneyMasterServiceService.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IUserRepository"/></summary>
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        /// <summary><inheritdoc cref="IUserRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public UserRepository(MoneyMasterServiceContext context) : base(context) { }

        public async Task<bool> Exist(User item, CancellationToken Cancel = default) =>
                await Context.Set<User>().AnyAsync(x => x.UserName == item.UserName ,Cancel) || await Context.Set<User>().AnyAsync(x => x.Email == item.Email, Cancel);


    }
}
