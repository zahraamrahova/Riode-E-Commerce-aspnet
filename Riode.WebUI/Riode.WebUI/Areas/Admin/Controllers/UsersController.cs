using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [Authorize(Policy = "admin.users.index")]
        public async Task<IActionResult> Index()
        {
            var data = await _db.Users.ToListAsync();
            return View(data);
        }
        [Authorize(Policy = "admin.users.details")]
        public async Task<IActionResult> Details(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(d=>d.Id==id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles= await (from r in _db.Roles join
               ur in _db.UserRoles on new
              { RoleId = r.Id, UserId = user.Id }
              equals new { ur.RoleId, ur.UserId }  into lJoin
             from lj in lJoin.DefaultIfEmpty()
             select Tuple.Create(r.Id, r.Name, lj != null)).ToListAsync();

            ViewBag.Principals =  (from p in Program.principals
                                        join uc in _db.UserClaims on new { ClaimValue = "1", ClaimType = p, UserId = user.Id } equals new
                                        { uc.ClaimValue, uc.ClaimType, uc.UserId } into lJoin
                                        from lj in lJoin.DefaultIfEmpty()
                                        select Tuple.Create(p, lj != null)).ToList();
            return View(user);
        }
    }
}
