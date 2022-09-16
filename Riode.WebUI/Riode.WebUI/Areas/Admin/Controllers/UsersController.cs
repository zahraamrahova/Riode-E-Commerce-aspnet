using Microsoft.AspNetCore.Mvc;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly RiodeDbContext _db;
        public UsersController(RiodeDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
