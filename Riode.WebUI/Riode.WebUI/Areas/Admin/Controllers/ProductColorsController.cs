using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.ProductColorModule;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductColorsController : Controller
    {
        private readonly RiodeDbContext _db;
        private readonly IMediator _mediator;

        public ProductColorsController(RiodeDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        // GET: Admin/ProductColors
        public async Task<IActionResult> Index(ProductColorPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return View(response);
        }

        // GET: Admin/ProductColors/Details/5
        public async Task<IActionResult> Details(ProductColorSingleQuery query)
        {
            ProductColor productColor = await _mediator.Send(query);
            if (productColor == null)
            {
                return NotFound();
            }

            return View(productColor);
        }

        // GET: Admin/ProductColors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ProductColors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductColorCreateCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/ProductColors/Edit/5
        public async Task<IActionResult> Edit(ProductColorSingleQuery query)
        {
            ProductColor productColor = await _mediator.Send(query);
            if (productColor == null)
            {
                return NotFound();
            }
            ProductColorViewModel vm = new ProductColorViewModel();
            {
                vm.Id = productColor.Id;
                vm.HexCode = productColor.HexCode;
                vm.Name = productColor.Name;
                vm.Description = productColor.Description;
            }
            return View(vm);
        }

        // POST: Admin/ProductColors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductColorEditCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);

        }

        // GET: Admin/ProductColors/Delete/5
        public async Task<IActionResult> Delete(ProductColorRemoveCommand request)
        {
            var response = await _mediator.Send(request);
            return Json(response);
        }

    }
}
