using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.BrandModule;
using Riode.WebUI.AppCode.Application.SpecificationModule;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using Riode.WebUI.Models.ViewModels;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecificationsController : Controller
    {
        private readonly RiodeDbContext _db;
        private readonly IMediator _mediator;

        public SpecificationsController(RiodeDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        // GET: Admin/Specifications
        public async Task<IActionResult> Index(SpecificationPagedQuery query)
        {
            
            var response = await _mediator.Send(query);
              return View(response);
                        
        }

        // GET: Admin/Specifications/Details/5
        public async Task<IActionResult> Details(SpecificationSingleQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // GET: Admin/Specifications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Specifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecificationCreateCommand request)
        {
            int id = await _mediator.Send(request);
            if (id>0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/Specifications/Edit/5
        public async Task<IActionResult> Edit(SpecificationSingleQuery query)
        {
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            SpecificationViewModel vm = new SpecificationViewModel();
            {
                vm.Id = result.Id;
                vm.Name = result.Name;
              
            }
            return View(vm);

    }

        // POST: Admin/Specifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SpecificationEditCommand request)
        {
             int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);


        }

       [HttpPost]
        public async Task<IActionResult> Delete(SpecificationRemoveCommand request)
        {
            var response = await _mediator.Send(request);
            return Json(response);
           
        }

    
    }
}
