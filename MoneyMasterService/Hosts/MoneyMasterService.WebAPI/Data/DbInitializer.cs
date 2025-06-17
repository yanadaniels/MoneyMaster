using Microsoft.EntityFrameworkCore;
using MoneyMasterService.Domain.Entities;
using MoneyMasterService.Domain.Entities.Enums;
using MoneyMasterService.Infrastructure.EntityFramework.Context;
using System.Data;

namespace MoneyMasterService.WebAPI.Data
{
    public class DbInitializer
    {
        private readonly MoneyMasterServiceContext _db;

        public DbInitializer(MoneyMasterServiceContext db)
        {
            _db = db;
        }

        public async Task InitializeAsync()
        {
            //Если он не существует БД, никаких действий не выполняется. Если она существует, база данных удаляется.
            await _db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //Прекращаем отслеживание всех отслеживаемых в настоящее время сущностей.
            //_db.ChangeTracker.Clear();
            //Мигрируем БД
            await _db.Database.MigrateAsync().ConfigureAwait(false);
            await InitialDB();
        }

        private async Task InitialDB()
        {
            List<AccountType> accountTypes = new List<AccountType>()
            {
            new AccountType() { Name = "Банковская карта" },
            new AccountType() { Name = "Счет в банке" },
            new AccountType() { Name = "Наличные" },
            };

            await _db.Set<AccountType>().AddRangeAsync(accountTypes);

            

            List<Category> categories = new List<Category>()
            {
                new Category() { Name = "Зарплата", CategoryType = CategoryType.Revenue, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Подработка" , CategoryType = CategoryType.Revenue, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Продажа", CategoryType = CategoryType.Revenue, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Перевод", CategoryType = CategoryType.Revenue, CreateAt = DateTime.Now, IsSystem = true},

                new Category() { Name = "Еда", CategoryType = CategoryType.Expenses, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Транспорт", CategoryType = CategoryType.Expenses, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Спорт", CategoryType = CategoryType.Expenses, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Одежда", CategoryType = CategoryType.Expenses, CreateAt = DateTime.Now, IsSystem = true},
                new Category() { Name = "Перевод", CategoryType = CategoryType.Expenses, CreateAt = DateTime.Now, IsSystem = true},

            };
            await _db.Set<Category>().AddRangeAsync(categories);

            _db.SaveChanges();


            //User user1 = new User() { UserName = "Петр", Email = "Peter@Gmail.com", PasswordHash = "GGGG", CreateAt = DateTime.Now };
            //User user2 = new User() { UserName = "Иван", Email = "Ivan@Gmail.com", PasswordHash = "Ivan", CreateAt = DateTime.Now };
            //User user3 = new User() { UserName = "Вася", Email = "Basia@Gmail.com", PasswordHash = "Basia", CreateAt = DateTime.Now };
            //UserSetting userSetting1 = new UserSetting() { Language = "ru" };
            //UserSetting userSetting2 = new UserSetting() { Language = "eng" };
            //UserSetting userSetting3 = new UserSetting() { Language = "ru" };
            //user1.UserSetting = userSetting1;
            //user2.UserSetting = userSetting2;
            //user3.UserSetting = userSetting3;

            //await _db.Set<User>().AddAsync(user1);
            //await _db.Set<User>().AddAsync(user2);
            //await _db.Set<User>().AddAsync(user3);

            //List<Account> accounts = new List<Account>()
            //{
            //    new Account() { Name = "Тинькофф", User = user1 , AccountType = accountTypes.FirstOrDefault(x=>x.Name == "Банковская карта")  },
            //    new Account() { Name = "Наличные", User = user1 , AccountType = accountTypes.FirstOrDefault(x=>x.Name == "Наличные")  },

            //     new Account() { Name = "Сбер", User = user2 , AccountType = accountTypes.FirstOrDefault(x=>x.Name == "Банковская карта")  },
            //     new Account() { Name = "Сбережения", User = user2 , AccountType = accountTypes.FirstOrDefault(x=>x.Name == "Наличные")  },

            //     new Account() { Name = "Совкомбанк", User = user3 , AccountType = accountTypes.FirstOrDefault(x=>x.Name == "Банковская карта")  },
            //     new Account() { Name = "Сбережения", User = user3 , AccountType = accountTypes.FirstOrDefault(x=>x.Name == "Счет в банке")  },

            //};
            //await _db.Set<Account>().AddRangeAsync(accounts);


            //Transaction transaction1 = new Transaction()
            //{
            //    Account = accounts.FirstOrDefault(x => x.User.UserName == "Петр"),
            //    Category = categories.FirstOrDefault(x => x.Name == "Зарплата"),
            //    TransactionType = categories.FirstOrDefault(x => x.Name == "Зарплата").TransactionType
            //};
            //Transaction transaction2 = new Transaction() { 
            //    Account = accounts.FirstOrDefault(x => x.User.UserName == "Петр"), 
            //    Category = categories.FirstOrDefault(x => x.Name == "Еда"), 
            //    TransactionType = categories.FirstOrDefault(x => x.Name == "Еда").TransactionType };
            //Transaction transaction3 = new Transaction() {
            //    Account = accounts.FirstOrDefault(x => x.User.UserName == "Петр"),
            //    Category = categories.FirstOrDefault(x => x.Name == "Спорт"),
            //    TransactionType = categories.FirstOrDefault(x => x.Name == "Спорт").TransactionType
            //};

            //Transaction transaction4 = new Transaction() { 
            //    Account = accounts.FirstOrDefault(x => x.User.UserName == "Иван"), 
            //    Category  = categories.FirstOrDefault(x => x.Name == "Зарплата"), 
            //    TransactionType = categories.FirstOrDefault(x => x.Name == "Зарплата").TransactionType
            //};
            //Transaction transaction5 = new Transaction()
            //{
            //    Account = accounts.FirstOrDefault(x => x.User.UserName == "Иван"),
            //    Category = categories.FirstOrDefault(x => x.Name == "Одежда"),
            //    TransactionType = categories.FirstOrDefault(x => x.Name == "Одежда").TransactionType
            //};

            //Transaction transaction6 = new Transaction() { 
            //    Account = accounts.FirstOrDefault(x => x.User.UserName == "Вася"), 
            //    Category  = categories.FirstOrDefault(x => x.Name == "Зарплата"), 
            //    TransactionType =categories.FirstOrDefault(x => x.Name == "Зарплата").TransactionType
            //};

            //await _db.Set<Transaction>().AddAsync(transaction1);
            //await _db.Set<Transaction>().AddAsync(transaction2);
            //await _db.Set<Transaction>().AddAsync(transaction3);
            //await _db.Set<Transaction>().AddAsync(transaction4);
            //await _db.Set<Transaction>().AddAsync(transaction5);
            //await _db.Set<Transaction>().AddAsync(transaction6);

            _db.SaveChanges();
        }
    }
}
