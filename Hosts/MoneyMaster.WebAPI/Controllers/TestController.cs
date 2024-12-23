using Microsoft.AspNetCore.Mvc;
using MoneyMaster.Domain.Entities.Entities;
using MoneyMaster.Domain.Entities;
using MoneyMaster.Infrastructure.EntityFramework.Context;
using MoneyMaster.Services.Repositories.Abstractions;
using MoneyMaster.Services.Abstractions;

namespace MoneyMaster.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;

        public TestController(ILogger<TestController> logger, IUnitOfWork unitOfWork, IUserService userService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userService = userService;
        }
        [HttpGet]
       // [Route("/[controller]/[action]/{id}")]
        public async Task<IActionResult> Index()
        {
            User user1 = new User() { UserName = "Петр", Email = "Peter@Gmail.com", PasswordHash = "GGGG", CreateAt = DateTime.Now };
            User user2 = new User() { UserName = "Иван", Email = "Ivan@Gmail.com", PasswordHash = "Ivan", CreateAt = DateTime.Now };
            User user3 = new User() { UserName = "Вася", Email = "Basia@Gmail.com", PasswordHash = "Basia", CreateAt = DateTime.Now };

            UserSetting userSetting1 = new UserSetting() { Language = "ru" };
            UserSetting userSetting2 = new UserSetting() { Language = "eng" };
            UserSetting userSetting3 = new UserSetting() { Language = "ru" };
            user1.UserSetting = userSetting1;
            user2.UserSetting = userSetting2;
            user3.UserSetting = userSetting3;

            _unitOfWork.UserRepository.Add(user1);
            _unitOfWork.UserRepository.Add(user2);
            _unitOfWork.UserRepository.Add(user3);
            _unitOfWork.SaveChangesAsync();


            AccountType accountType1 = new AccountType() { Name = "User" };
            AccountType accountType2 = new AccountType() { Name = "Admin" };

            _unitOfWork.AccountTypeRepository.Add(accountType1);
            _unitOfWork.AccountTypeRepository.Add(accountType2);
            _unitOfWork.SaveChangesAsync();


            Account account1 = new Account() { Name = "Петр", AccountType = accountType1, User = user1 };

            Account account2 = new Account() { Name = "Иван", AccountType = accountType1, User = user2 };

            Account account3 = new Account() { Name = "Вася", AccountType = accountType2, User = user3 };

            _unitOfWork.AccountRepository.Add(account1);
            _unitOfWork.AccountRepository.Add(account2);
            _unitOfWork.AccountRepository.Add(account3);
            _unitOfWork.SaveChangesAsync();


            TransactionType transactionType1 = new TransactionType() { Name = "Приход", CreateAt = DateTime.Now };
            TransactionType transactionType2 = new TransactionType() { Name = "Расход", CreateAt = DateTime.Now };
            TransactionType transactionType3 = new TransactionType() { Name = "Перевод", CreateAt = DateTime.Now };
            _unitOfWork.TransactionTypeRepository.Add(transactionType1);
            _unitOfWork.TransactionTypeRepository.Add(transactionType2);
            _unitOfWork.TransactionTypeRepository.Add(transactionType3);
            _unitOfWork.SaveChangesAsync();

            Category category1 = new Category() { Name = "Зарплата", TransactionType = transactionType1 };
            Category category2 = new Category() { Name = "Покупка", TransactionType = transactionType2 };
            Category category3 = new Category() { Name = "ЖКХ", TransactionType = transactionType3 };

            _unitOfWork.CategoryRepository.Add(category1);
            _unitOfWork.CategoryRepository.Add(category2);
            _unitOfWork.CategoryRepository.Add(category3);
            _unitOfWork.SaveChangesAsync();

            Transaction transaction1 = new Transaction() { Account = account1, Category = category1, TransactionType = category1.TransactionType };
            Transaction transaction2 = new Transaction() { Account = account1, Category = category2, TransactionType = category2.TransactionType };
            Transaction transaction3 = new Transaction() { Account = account1, Category = category3, TransactionType = category3.TransactionType };

            Transaction transaction4 = new Transaction() { Account = account2, Category = category1, TransactionType = category1.TransactionType };
            Transaction transaction5 = new Transaction() { Account = account2, Category = category3, TransactionType = category3.TransactionType };


            Transaction transaction6 = new Transaction() { Account = account3, Category = category1, TransactionType = category1.TransactionType };

            _unitOfWork.TransactionRepository.Add(transaction1);
            _unitOfWork.TransactionRepository.Add(transaction2);
            _unitOfWork.TransactionRepository.Add(transaction3);
            _unitOfWork.TransactionRepository.Add(transaction4);
            _unitOfWork.TransactionRepository.Add(transaction5);
            _unitOfWork.TransactionRepository.Add(transaction6);
            _unitOfWork.SaveChangesAsync();

            var accountType = _unitOfWork.AccountTypeRepository.GetAll().ToList();

            var users = _unitOfWork.UserRepository.GetAll().ToList();

            var category = _unitOfWork.CategoryRepository.GetAll().ToList();

            var dtoUser = await _userService.GetAllAsync();

            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
