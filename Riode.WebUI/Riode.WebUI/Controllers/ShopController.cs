using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using Riode.WebUI.Models.FormModels;
using Riode.WebUI.Models.ViewModels;

namespace Riode.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private readonly RiodeDbContext _db;
        public ShopController(RiodeDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {
            ShopFilterViewModel vm = new ShopFilterViewModel();
            vm.Brands = _db.Brands.Where(d => d.DeletedByUserId == null).ToList();
            vm.ProductColors = _db.ProductColors.Where(d => d.DeletedByUserId == null).ToList();
            vm.ProductSizes = _db.ProductSizes.Where(d => d.DeletedByUserId == null).ToList();
            vm.Categories = _db.Categories.Include(c => c.Children).ThenInclude(c => c.Children).Where(d => d.DeletedByUserId == null && d.ParentId == null).ToList();
            vm.Products = _db.Products.Include(b => b.Images.Where(i => i.IsMain == true)).Include(b => b.Brand).Where(p => p.DeletedByUserId == null).ToList();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Filter(ShopFilterFormModel model)
        {

            var query = _db.Products.Include(b => b.Images.Where(i => i.IsMain == true))
                .Include(b => b.ProductSizeColorCollection).Include(b => b.Brand).Where(p => p.DeletedByUserId == null).AsQueryable();
            if (model?.Brands?.Count() > 0)
            {
                query = query.Where(p => model.Brands.Contains(p.BrandId));
            }

            if (model?.Sizes?.Count() > 0)
            {
                query = query.Where(p => p.ProductSizeColorCollection.Any(pscc => model.Sizes.Contains(pscc.SizeId)));
            }

            if (model?.Colors?.Count() > 0)
            {
                query = query.Where(p => p.ProductSizeColorCollection.Any(pscc => model.Colors.Contains(pscc.ColorId)));
            }

            return PartialView("_ProductContainer", query.ToList());

            //return Json(new
            //{
            //    error = false,
            //    data = query.ToList()

            //});
        }
        public IActionResult Details(int id)
        {
            Product product = _db.Products.Include(p => p.Images).Include(p => p.SpecificationValues.Where(s => s.DeletedByUserId == null)).ThenInclude(s => s.Specification).FirstOrDefault(p => p.Id == id && p.DeletedByUserId == null);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
