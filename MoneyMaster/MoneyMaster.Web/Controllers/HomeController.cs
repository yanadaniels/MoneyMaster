using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyMaster.DAL.Context;
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
