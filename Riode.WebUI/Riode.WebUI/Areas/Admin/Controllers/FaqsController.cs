using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Application.FaqModule;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FaqsController : Controller
    {
        private readonly RiodeDbContext _db;
        private readonly IMediator _mediator;

        public FaqsController(RiodeDbContext db, IMediator mediator)
        {
            _db = db;
            _mediator = mediator;
        }

        // GET: Admin/Faqs
        public async Task<IActionResult> Index(FaqPagedQuery query)
        {
            var response = await _mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Faqs/Details/5
        public async Task<IActionResult> Details(FaqSingleQuery query)
        {
            Faq faq = await _mediator.Send(query);
            if (faq == null)
            {
                return NotFound();
            }

            return View(faq);
        }

        // GET: Admin/Faqs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Faqs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaqCreateCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/Faqs/Edit/5
        public async Task<IActionResult> Edit(FaqSingleQuery query)
        {
            Faq faq = await _mediator.Send(query);
            if (faq == null)
            {
                return NotFound();
            }
            FaqViewModel vm = new FaqViewModel();
            {
                vm.Id = faq.Id;
                vm.Question = faq.Question;
                vm.Answer = faq.Answer;
            }
            return View(vm);
        }

        // POST: Admin/Faqs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FaqEditCommand request)
        {
            int id = await _mediator.Send(request);
            if (id > 0)

                return RedirectToAction(nameof(Index));
            return View(request);
        }

        // GET: Admin/Faqs/Delete/5
        public async Task<IActionResult> Delete(FaqRemoveCommand request)
        {
            var response = await _mediator.Send(request);
            return Json(response);
        }
    }
}
