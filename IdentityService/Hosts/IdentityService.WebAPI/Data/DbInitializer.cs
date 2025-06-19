using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.Common;
using MoneyMaster.Common.Extensions;

namespace IdentityService.WebAPI.Data
{
    /// <summary>
    /// Класс для инициализации базы данных IdentityService.
    /// Отвечает за применение миграций и добавление начальных данных.
    /// </summary>
    public class DbInitializer
    {
        private readonly IdentityContext _db;

        /// <summary>
        /// Создает новый экземпляр инициализатора базы данных.
        /// </summary>
        /// <param name="db">Контекст базы данных IdentityService.</param>
        public DbInitializer(IdentityContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Выполняет инициализацию базы данных.
        /// Удаляет существующую базу данных, применяет миграции и загружает начальные данные.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
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

        /// <summary>
        /// Добавляет в базу данных начальные учетные записи пользователей.
        /// </summary>
        /// <returns>Асинхронная задача.</returns>
        private async Task InitialDB()
        {



            User user1 = new User() { UserName = "Петр", Email = "Peter@Gmail.com", PasswordHash = UserHelper.GenerateHash("!gGGGG!1"), Role = nameof(Privileges.Administrator), CreateAt = DateTime.UtcNow };
            User user2 = new User() { UserName = "Иван", Email = "Ivan@Gmail.com", PasswordHash = UserHelper.GenerateHash("Ivan"), Role = nameof(Privileges.System), CreateAt = DateTime.UtcNow };
            User user3 = new User() { UserName = "Вася", Email = "Basia@Gmail.com", PasswordHash = UserHelper.GenerateHash("Basia"), Role = nameof(Privileges.User), CreateAt = DateTime.UtcNow };

            await _db.Set<User>().AddAsync(user1);
            await _db.Set<User>().AddAsync(user2);
            await _db.Set<User>().AddAsync(user3);

            _db.SaveChanges();
        }
    }
}
