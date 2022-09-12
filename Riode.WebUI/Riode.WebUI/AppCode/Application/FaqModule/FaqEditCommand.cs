using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.AppCode.Application.FaqModule
{
    public class FaqEditCommand: FaqViewModel, IRequest<int>
    {
        public class FaqEditCommandHandler : IRequestHandler<FaqEditCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public FaqEditCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }

            public async Task<int> Handle(FaqEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 0)
                    return 0;
                var entity = await _db.Faqs.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId == null);
                if (entity == null)
                    return 0;
                if (_ctx.IsModelStateValid())
                {
                    entity.Question = request.Question;
                    entity.Answer = request.Answer;
                    await _db.SaveChangesAsync(cancellationToken);
                    return entity.Id;
                }
                return 0;
            }
        }
    }
}
