using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.Controllers
{
    public class BlogController : Controller
    {

        private readonly RiodeDbContext _db;
        public BlogController(RiodeDbContext db)
        {
            _db=db;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
           var posts= await _db.BlogPosts.Where(bp=> bp.DeletedByUser == null && bp.PublishedDate!=null).ToListAsync();
            return View(posts);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            ViewBag.Categories = _db.Categories.Include(c => c.Children).ThenInclude(c => c.Children).Where(d => d.DeletedByUserId == null && d.ParentId == null).ToList();

            var blogpost = await _db.BlogPosts.Include(bp=>bp.CreatedByUser).FirstOrDefaultAsync(bp => bp.Id == id && bp.DeletedByUser==null && bp.PublishedDate != null);
            if (blogpost == null)
            {
                return NotFound();
            }
            return View(blogpost);
        }

        public IActionResult Comment(int postId, string comment)
        {
            return View();
        }
    }
}
