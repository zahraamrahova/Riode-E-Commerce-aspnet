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
            var faqs = _db.Faqs.Where(f => f.DeletedByUserId == null).ToList();
            return View(faqs);
        }
        [HttpGet]
        public IActionResult Contact()
        {

            ViewBag.Time = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss ffffff");
            return View();
        }
        [HttpPost]
       // [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")] Subscribe model)
        {
            if (!ModelState.IsValid)
            {
                _db.Subscribes.Add(model);
                _db.SaveChanges();
                return Json(new
                {
                    error = false,
                    message = "Your query accepted succesufylly.Please confirmed your email"
                });
            }
            return Json(new
            {
                error=true,
                message= "There are problem. Please try after 5 minutes again"
            });
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
                 // ModelState.Clear();
                //ViewBag.Message = "Your message have been send to support succesufully";
                //return View();
                return Json(new
                {
                    error=false,
                    message= "Your messaged accept, we will return as soon as possible!"
                }) ;
            }
            //return View(model);
            return Json(new
            {
                error = true,
                message = "Please check again!!"
            });
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