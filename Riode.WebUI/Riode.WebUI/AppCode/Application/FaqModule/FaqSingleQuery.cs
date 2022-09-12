using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.FaqModule
{
    public class FaqSingleQuery: IRequest<Faq>
    {
        public int? Id { get; set; }
        public class FaqSingleQueryHandler : IRequestHandler<FaqSingleQuery, Faq>
        {
            private readonly RiodeDbContext _db;
            public FaqSingleQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<Faq> Handle(FaqSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 1)
                {
                    return null;
                }
                var faq = await _db.Faqs.FirstOrDefaultAsync(m => m.Id == request.Id);

                return faq;
            }
        }
    }
}
