using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.DAL.Context;
using MoneyMaster.DAL.Entities;

using MoneyMaster.Web.Models;
using System.Diagnostics;

namespace MoneyMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MoneyMasterContext _db;

        public HomeController(ILogger<HomeController> logger, MoneyMasterContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            using (var db = _db) 
            {
               

               User user1 = new User() { UserName = "Петр", Email="Peter@Gmail.com", PasswordHash = "GGGG",CreateAt = DateTime.Now};
               User user2 = new User() { UserName = "Иван", Email = "Ivan@Gmail.com", PasswordHash = "Ivan", CreateAt = DateTime.Now };
               User user3 = new User() { UserName = "Вася", Email = "Basia@Gmail.com", PasswordHash = "Basia", CreateAt = DateTime.Now };

                UserSetting userSetting1 = new UserSetting() {  Language = "ru"};
                UserSetting userSetting2 = new UserSetting() {  Language = "eng"};
                UserSetting userSetting3 = new UserSetting() { Language = "ru"};
                user1.UserSetting = userSetting1;
                user2.UserSetting = userSetting2;
                user3.UserSetting = userSetting3;

                db.Set<User>().Add(user1);
                db.Set<User>().Add(user2);
                db.Set<User>().Add(user3);
                db.SaveChanges();

                AccountType accountType1 = new AccountType() { Name = "User" };
                AccountType accountType2 = new AccountType() {Name = "Admin" };

                db.Set<AccountType>().Add(accountType1);
                db.Set<AccountType>().Add(accountType2);
                db.SaveChanges();

                Account account1 = new Account() {  Name = "Петр" , AccountType= accountType1, User = user1 };

                Account account2 = new Account() {  Name = "Иван", AccountType = accountType1, User = user2 };

                Account account3 = new Account() { Name = "Вася", AccountType = accountType2, User = user3 };

                db.Set<Account>().Add(account1);
                db.Set<Account>().Add(account2);
                db.Set<Account>().Add(account3);
                db.SaveChanges();


                TransactionType transactionType1 = new TransactionType() { Name = "Приход" , CreateAt = DateTime.Now};
                TransactionType transactionType2 = new TransactionType() { Name = "Расход", CreateAt = DateTime.Now };
                TransactionType transactionType3 = new TransactionType() { Name = "Перевод", CreateAt = DateTime.Now };
                db.Set<TransactionType>().Add(transactionType1);
                db.Set<TransactionType>().Add(transactionType2);
                db.Set<TransactionType>().Add(transactionType3);
                db.SaveChanges();

                Category category1 = new Category() { Name = "Зарплата", TransactionType = transactionType1 };
                Category category2 = new Category() { Name = "Покупка", TransactionType = transactionType2 };
                Category category3 = new Category() { Name = "ЖКХ", TransactionType = transactionType3 };

                db.Set<Category>().Add(category1);
                db.Set<Category>().Add(category2);
                db.Set<Category>().Add(category3);

                db.SaveChanges();

                Transaction transaction1 = new Transaction() { Account = account1, Category = category1, TransactionType = category1.TransactionType };
                Transaction transaction2 = new Transaction() { Account = account1, Category = category2, TransactionType = category2.TransactionType };
                Transaction transaction3 = new Transaction() { Account = account1, Category = category3, TransactionType = category3.TransactionType };

                Transaction transaction4 = new Transaction() { Account = account2, Category = category1, TransactionType = category1.TransactionType };
                Transaction transaction5 = new Transaction() { Account = account2, Category = category3, TransactionType = category3.TransactionType };


                Transaction transaction6 = new Transaction() { Account = account3, Category = category1, TransactionType = category1.TransactionType };

                db.Set<Transaction>().Add(transaction1);
                db.Set<Transaction>().Add(transaction2);
                db.Set<Transaction>().Add(transaction3);
                db.Set<Transaction>().Add(transaction4);
                db.Set<Transaction>().Add(transaction5);
                db.Set<Transaction>().Add(transaction6);

                db.SaveChanges();

                var accountType= db.Set<AccountType>().ToList();

                var users= db.Set<User>().ToList();

                var category= db.Set<Category>().ToList();
            }


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
