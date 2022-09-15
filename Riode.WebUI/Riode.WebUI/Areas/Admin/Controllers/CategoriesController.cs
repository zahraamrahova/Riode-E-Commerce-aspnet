using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(Policy = "admin.categories.index")]
        // GET: Admin/Categories
        public async Task<IActionResult> Index(CategoryPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return View(response);
        }
        //[Authorize(Policy = "admin.categories.details")]
        // GET: Admin/Categories/Details/5
        public async Task<IActionResult> Details(CategorySingleQuery query)
        {
            if (query?.Id == null)
                return NotFound();
            Category category = await _mediator.Send(query);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
        //[Authorize(Policy = "admin.categories.create")]
        // GET: Admin/Categories/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ParentId = new SelectList(await _mediator.Send(new CategoryChooseQuery()), "Id", "Name");
            return View();
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "admin.categories.create")]
        public async Task<IActionResult> Create(CategoryCreateCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ParentId = new SelectList(await _mediator.Send(new CategoryChooseQuery()), "Id", "Name", command.ParentId);
            return View(command);
        }

        // GET: Admin/Categories/Edit/5
        //[Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit(CategorySingleQuery query)
        {
            if (query?.Id == null)
                return NotFound();
            Category category = await _mediator.Send(query);
            if (category == null)
            {
                return NotFound();
            }

            ViewBag.ParentId = new SelectList(await _mediator.Send(new CategoryChooseQuery()), "Id", "Name", category.ParentId);
            return View(category);
        }

        // POST: Admin/Categories/Edit/5      
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Policy = "admin.categories.edit")]
        public async Task<IActionResult> Edit([FromRoute] int id, CategoryEditCommand command)
        {
            if (id != command.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ParentId = new SelectList(await _mediator.Send(new CategoryChooseQuery()), "Id", "Name", command.ParentId);
            return View(command);
        }

        //[Authorize(Policy = "admin.categories.delete")]
        // GET: Admin/Categories/Delete/5
        public async Task<IActionResult> Delete(CategoryRemoveCommand command)
        {
            if (command?.Id == null)
                return NotFound();

            var response = await _mediator.Send(command);
            if (response == null)
                return NotFound();
            return Json(response);
        }
    }
}
