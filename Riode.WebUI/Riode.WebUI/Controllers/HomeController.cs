using Microsoft.AspNetCore.Mvc;
using Riode.WebUI.Models;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.Diagnostics;

namespace Riode.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RiodeDbContext _db;

        public HomeController(ILogger<HomeController> logger, RiodeDbContext db )
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Fags()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Contact()
        {

            ViewBag.Time = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ffffff");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult Contact(ContactPost model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Time = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ffffff");
                _db.ContactPosts.Add(model);
                _db.SaveChanges();
                ModelState.Clear();
                ViewBag.Message = "Your message have been send to support succesufully";
                return View();
            }
            return View(model);
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