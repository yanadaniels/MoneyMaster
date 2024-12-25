using Microsoft.EntityFrameworkCore;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Infrastructure.Repositories.Implementations.Base;
using MoneyMaster.Services.Repositories.Abstractions;
using System.Collections.Generic;

namespace MoneyMaster.Infrastructure.Repositories.Implementations.Repositories
{
    /// <summary><inheritdoc cref="IUserRepository"/></summary>
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        /// <summary><inheritdoc cref="IUserRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public UserRepository(MoneyMasterContext context) : base(context) { }

        public async Task<bool> Exist(User item, CancellationToken Cancel = default) =>
                await Context.Set<User>().AnyAsync(x => x.UserName == item.UserName ,Cancel) || await Context.Set<User>().AnyAsync(x => x.Email == item.Email, Cancel);


    }
}
