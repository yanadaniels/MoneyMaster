using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common;
using MoneyMaster.Common.Extensions;

namespace IdentityService.WebAPI.Data
{
    public class DbInitializer
    {
        private readonly IdentityContext _db;

        public DbInitializer(IdentityContext db)
        {
            _db = db;
        }

        public async Task InitializeAsync()
        {
            //Если он не существует БД, никаких действий не выполняется. Если она существует, база данных удаляется.
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //Прекращаем отслеживание всех отслеживаемых в настоящее время сущностей.
            //_db.ChangeTracker.Clear();
            //await _db.Database.EnsureCreatedAsync().ConfigureAwait(false);
            //Мигрируем БД
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            await InitialDB();
        }

        private async Task InitialDB()
        {



            User user1 = new User() { UserName = "Петр", Email = "Peter@Gmail.com", PasswordHash = UserHelper.GenerateHash("GGGG"), Role = nameof(Privileges.Administrator), CreateAt = DateTime.Now };
            User user2 = new User() { UserName = "Иван", Email = "Ivan@Gmail.com", PasswordHash = UserHelper.GenerateHash("Ivan"), Role = nameof(Privileges.System), CreateAt = DateTime.Now };
            User user3 = new User() { UserName = "Вася", Email = "Basia@Gmail.com", PasswordHash = UserHelper.GenerateHash("Basia"), Role = nameof(Privileges.User), CreateAt = DateTime.Now };

            await _db.Set<User>().AddAsync(user1);
            await _db.Set<User>().AddAsync(user2);
            await _db.Set<User>().AddAsync(user3);

            _db.SaveChanges();
        }
    }
}
