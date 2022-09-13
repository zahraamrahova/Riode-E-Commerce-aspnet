using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.CategoryModule;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly RiodeDbContext _db;
        private readonly IMediator _mediator;

        public CategoriesController(RiodeDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index(CategoryPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(CategorySingleQuery query)
        {
            Category category = await _mediator.Send(query);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Categories/Create
        public  IActionResult Create()
        {
            
            ViewBag.Parent = new SelectList(_db.Categories, "Name", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)
                return RedirectToAction(nameof(Index));
            ViewBag.Parent = new SelectList(_db.Categories, "Name", "Name", request.Name);
            return View(request);
        }

        // GET: Admin/Categories/Edit/5
        public async Task<IActionResult> Edit(CategorySingleQuery query)
        {
            Category category = await _mediator.Send(query);
            if (category == null)
            {
                return NotFound();
            }
            CategoryViewModel vm = new CategoryViewModel();
            {
                vm.Id = category.Id;
                vm.Name = category.Name;
                vm.Description = category.Description;
            }
            ViewBag.Parent = new SelectList(_db.Categories, "Name", "Name", category.Name);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            
            ViewBag.Parent = new SelectList(_db.Categories, "Name", "Name", request.Name);
            return View(request);
        }

        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(CategoryRemoveCommand request)
        {
            var response = await _mediator.Send(request);
            return Json(response);
        }
    }
}
