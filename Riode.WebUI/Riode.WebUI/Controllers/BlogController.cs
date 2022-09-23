using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
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
        [HttpPost]
        public async Task<IActionResult> PostComment(int? commentId, int postId, string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
            {
                return Json(new
                {
                    error=true,
                    message="Comment cannot be empty!"
                });
            }

            if (postId <1)
            {
                return Json(new
                {
                    error = true,
                    message = "This post is not avaliable!"
                });
            }
            var post = await _db.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == postId);
            if (post==null)
            {
                return Json(new
                {
                    error = true,
                    message = "This post is not avaliable!"
                });
            }
            var commentModel = new BlogPostComment
            {
                BlogPostId = postId,
                Comment = comment,
                CreatedByUserId = User.GetCurrentUserId()
            };

            if (commentId.HasValue && await _db.BlogPostComments.AnyAsync(c => c.Id == commentId))
            { commentModel.ParentId = commentId; }
           
            _db.BlogPostComments.Add(commentModel);

             await _db.SaveChangesAsync();
            commentModel = await _db.BlogPostComments
                .Include(u=>u.CreatedByUser)
                .Include(p=>p.Parent)
                .FirstOrDefaultAsync(c => c.Id == commentModel.Id);
            return PartialView("_Comment",commentModel);
        }
    }
}
