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
               

               User user1 = new User() { Id= Guid.CreateVersion7(DateTimeOffset.Now), UserName = "Петр", Email="Peter@Gmail.com", PasswordHash = "GGGG",CreateAt = DateTime.Now};
               User user2 = new User() { Id = Guid.CreateVersion7(DateTimeOffset.Now), UserName = "Иван", Email = "Ivan@Gmail.com", PasswordHash = "Ivan", CreateAt = DateTime.Now };
               User user3 = new User() { Id = Guid.CreateVersion7(DateTimeOffset.Now), UserName = "Вася", Email = "Basia@Gmail.com", PasswordHash = "Basia", CreateAt = DateTime.Now };

                UserSetting userSetting1 = new UserSetting() { Id = Guid.CreateVersion7(DateTimeOffset.Now), Language = "ru", UserId = user1.Id };
                UserSetting userSetting2 = new UserSetting() { Id = Guid.CreateVersion7(DateTimeOffset.Now), Language = "eng", UserId = user2.Id };
                UserSetting userSetting3 = new UserSetting() { Id = Guid.CreateVersion7(DateTimeOffset.Now), Language = "ru", UserId = user3.Id };
                user1.Setting = userSetting1;
                user2.Setting = userSetting2;
                user3.Setting = userSetting3;

                db.Set<User>().Add(user1);
                db.Set<User>().Add(user2);
                db.Set<User>().Add(user3);
                db.SaveChanges();

                AccountType accountType1 = new AccountType() { Id = Guid.NewGuid(), Name = "User" };
                AccountType accountType2 = new AccountType() { Id = Guid.NewGuid(), Name = "Admin" };

                db.Set<AccountType>().Add(accountType1);
                db.Set<AccountType>().Add(accountType2);
                db.SaveChanges();

                Account account1 = new Account() { Id = Guid.NewGuid() , Name = "Петр" , AccountType= accountType1, User = user1 };

                Account account2 = new Account() { Id = Guid.NewGuid(), Name = "Иван", AccountType = accountType1, User = user2 };

                Account account3 = new Account() { Id = Guid.NewGuid(), Name = "Вася", AccountType = accountType2, User = user3 };


                var users= db.Set<User>().ToList();
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
