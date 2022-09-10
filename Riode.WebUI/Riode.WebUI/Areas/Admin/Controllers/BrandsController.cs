using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.BrandModule;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandsController : Controller
    {
        private readonly RiodeDbContext _db;
        private readonly IMediator _mediator;

        public BrandsController(RiodeDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        // GET: Admin/Brands
        public async Task<IActionResult> Index(BrandPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Brands/Details/5
        public async Task<IActionResult> Details(BrandSingleQuery query)
        {
            Brand brand = await _mediator.Send(query);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // GET: Admin/Brands/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Brands/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateCommand request)
        {
            int id = await _mediator.Send(request);
            if (id>0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/Brands/Edit/5
        public async Task<IActionResult> Edit(BrandSingleQuery query)
        {
            Brand brand = await _mediator.Send(query);
            if (brand == null)
            {
                return NotFound();
            }
            BrandViewModel vm = new BrandViewModel();
            {
                vm.Id = brand.Id;
                vm.Name = brand.Name;
                vm.Description = brand.Description;
            }
            return View(vm);

    }

        // POST: Admin/Brands/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandEditCommand request)
        {
             int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);


        }

       [HttpPost]
        public async Task<IActionResult> Delete(BrandRemoveCommand request)
        {
            var response = await _mediator.Send(request);
            return Json(response);
           
        }

    
    }
}
