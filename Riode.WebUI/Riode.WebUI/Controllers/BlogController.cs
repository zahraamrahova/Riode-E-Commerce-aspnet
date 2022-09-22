using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.ViewModels;

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

            var vm = new BlogPostSingleModel();
            vm.Categories = _db.Categories.Include(c => c.Children).ThenInclude(c => c.Children).Where(d => d.DeletedByUserId == null && d.ParentId == null).ToList();

            vm.Post = await _db.BlogPosts
                .Include(bp=>bp.CreatedByUser)
                .Include(bp=>bp.Comments).ThenInclude(bp=>bp.CreatedByUser)
                 .Include(bp => bp.Comments).ThenInclude(bp => bp.Children)
                .FirstOrDefaultAsync(bp => bp.Id == id && bp.DeletedByUser==null && bp.PublishedDate != null);
            //vm.Comments = _db.BlogPostComments.Where(bpc => bpc.CreatedByUserId == null && bpc.BlogPostId == vm.Post.Id).ToList();
            return View(vm);
        }

        public IActionResult Comment(int postId, string comment)
        {
            return View();
        }
    }
}
