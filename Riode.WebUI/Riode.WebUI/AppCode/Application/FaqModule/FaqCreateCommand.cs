using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.FaqModule
{
    public class FaqCreateCommand: IRequest<int>
    {
        [Required]
        public string Question { get; set; }
        public string Answer { get; set; }

        public class FaqCreateCommandHandler : IRequestHandler<FaqCreateCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public FaqCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }
            public async Task<int> Handle(FaqCreateCommand request, CancellationToken cancellationToken)
            {
                if (_ctx.IsModelStateValid())
                {
                    Faq faq = new Faq();
                    faq.Question = request.Question;
                    faq.Answer = request.Answer;
                    _db.Add(faq);
                    await _db.SaveChangesAsync(cancellationToken);
                    return faq.Id;
                }
                return 0;

            }
        }
    }
}
