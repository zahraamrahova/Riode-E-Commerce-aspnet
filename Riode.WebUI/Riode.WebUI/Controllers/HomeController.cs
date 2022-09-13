using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Riode.WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RiodeDbContext _db;
        private readonly IConfiguration configuration;

        public HomeController(ILogger<HomeController> logger, RiodeDbContext db, IConfiguration configuration )
        {
            _logger = logger;
            _db = db;
            this.configuration = configuration;
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
       [ValidateAntiForgeryToken]
        public IActionResult Subscribe([Bind("Email")] Subscribe model)
        {
            var current = _db.Subscribes.FirstOrDefault(s => s.Email.Equals(model.Email));
            if(current!=null && current.EmailConfirmed==true)
            {
                return Json(new
                {
                    error = true,
                    message = "This email alrease subscribed!"
                });
            }
            else if (current != null && (current.EmailConfirmed ??false == false))
            {
                return Json(new
                {
                    error = true,
                    message = "The confirmation of this email wasnt completed"
                });
            }
            if (ModelState.IsValid)
            {
                _db.Subscribes.Add(model);
                _db.SaveChanges();

                string token = $"subscribetoken-{model.Id}-{DateTime.Now:yyyyMMddHHmmss}";
                token = token.Encrypt();
                string path = $"{Request.Scheme}://{Request.Host}/subscribe-confirm?token={token}";
                var mailSended=configuration.SendEmail(model.Email, "Riode News Letter Subscribe", $"Please complete subscribetion with this <a href ={path}>link</a>");

                if (mailSended == false)
                {
                    _db.Database.RollbackTransaction();
                    return Json(new
                    {
                      
                        error = false,
                        message = "There a problem during send email. Please try again after 5 minute"
                    });
                }
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
        [HttpGet]
        [Route("subscribe-confirm")]
        public IActionResult SubscribeConfirm(string token)

        {
            token = token.Decrypt();
            Match match = Regex.Match(token, @"subscribetoken-(?<id>\d+)-(?<executeTimeStamp>\d{14})");
            if (match.Success)
            {
                int id = Convert.ToInt32(match.Groups["id"].Value);
                string executeTimeStamp= match.Groups["executeTimeStamp"].Value;

                var subscribe = _db.Subscribes.FirstOrDefault(s => s.Id == id && s.DeletedByUserId==null);
                if(subscribe == null)
                {
                    ViewBag.Message = Tuple.Create(true, "Token Error");
                    goto end;
                }
                if ((subscribe.EmailConfirmed ?? false) == true)
                {
                    ViewBag.Message = Tuple.Create(true, "This token already confirmed");
                    goto end;
                }

                subscribe.EmailConfirmed = true;
                subscribe.EmailConfirmedDate = DateTime.Now;
                _db.SaveChanges();
                ViewBag.Message = Tuple.Create(false, "Your Subscribtion alreadey confirmed");
            }
            else
            {
                ViewBag.Message = Tuple.Create(true, "Token Error");
                goto end;
            }
            end:
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
                return Json(new
                {
                    error=false,
                    message= "Your messaged accept, we will return as soon as possible!"
                }) ;
            }
            return Json(new
            {
                error = true,
                message = "Please check again!!"
            });
        }
        [HttpGet]
        public string Cevir()
        {


            string text = $"subscribetoken-38-{DateTime.Now:yyyyMMddHHmmss}";
            string result1 = text.Encrypt();
            string result2 = result1.Decrypt();
          

            return $"{result1} and {result2}";
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