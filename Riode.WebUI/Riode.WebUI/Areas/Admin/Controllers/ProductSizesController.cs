using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.ProductSizeModule;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductSizesController : Controller
    {
        private readonly RiodeDbContext _db;
        private readonly IMediator _mediator;

        public ProductSizesController(RiodeDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        // GET: Admin/ProductSizes
        [Authorize(Policy = "admin.productsizes.index")]
        public async Task<IActionResult> Index(ProductSizePagedQuery query)
        {
            var response = await _mediator.Send(query);
            return View(response);
        }

        // GET: Admin/ProductSizes/Details/5
        [Authorize(Policy = "admin.productsizes.details")]
        public async Task<IActionResult> Details(ProductSizeSingleQuery query)
        {
            ProductSize productSize = await _mediator.Send(query);
            if (productSize == null)
            {
                return NotFound();
            }

            return View(productSize);
        }

        // GET: Admin/ProductSizes/Create
        [Authorize(Policy = "admin.productsizes.create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductSizes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.productsizes.create")]
        public async Task<IActionResult> Create(ProductSizeCreateCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/ProductSizes/Edit/5
        [Authorize(Policy = "admin.productsizes.edit")]
        public async Task<IActionResult> Edit(ProductSizeSingleQuery query)
        {
            ProductSize productSize = await _mediator.Send(query);
            if (productSize == null)
            {
                return NotFound();
            }
            ProductSizeViewModel vm = new ProductSizeViewModel();
            {
                vm.Id = productSize.Id;
                vm.Abbr = productSize.Abbr;
                vm.Name = productSize.Name;
                vm.Description = productSize.Description;
            }
            return View(vm);
        }

        // POST: Admin/ProductSizes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "admin.productsizes.edit")]
        public async Task<IActionResult> Edit(ProductSizeEditCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/ProductSizes/Delete/5
        [Authorize(Policy = "admin.productsizes.delete")]
        public async Task<IActionResult> Delete(ProductSizeRemoveCommand request)
        {
            var response = await _mediator.Send(request);
            return Json(response);
        }

    }
}
