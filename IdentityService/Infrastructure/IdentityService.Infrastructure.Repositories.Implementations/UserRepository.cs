﻿using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.EntityFramework.Context;
using IdentityService.Services.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common.Extensions;
using MoneyMaster.Common.Repositories;

namespace IdentityService.Infrastructure.Repositories.Implementations
{
    /// <summary><inheritdoc cref="IUserRepository"/></summary>
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        /// <summary><inheritdoc cref="IUserRepository"/></summary>
        /// <param name="context">Контекст БД</param>
        public UserRepository(IdentityContext context) : base(context) { }

        //TODO: Пароль нужно зашифровать
        public async Task<User?> AuthorizeUserAsync(string email, string password, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return null;

            var entity = await Context.Set<User>().FirstOrDefaultAsync(a => a.Email == email, cancellationToken);
            if (entity != null && UserHelper.VerifyHash(password, entity.PasswordHash))
            {
                return entity;
            }
            return null;
        }

        public async Task<bool> Exist(User item, CancellationToken Cancel = default) =>
                await Context.Set<User>().AnyAsync(x => x.UserName == item.UserName, Cancel) || await Context.Set<User>().AnyAsync(x => x.Email == item.Email, Cancel);


    }
}
