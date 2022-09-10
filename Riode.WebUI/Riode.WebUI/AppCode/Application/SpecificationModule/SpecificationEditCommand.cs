using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.SpecificationModule
{
    public class SpecificationEditCommand: SpecificationViewModel, IRequest<int>
    {

        public class SpecificationEditCommandHandler :  IRequestHandler<SpecificationEditCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public SpecificationEditCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }           

            public async Task<int> Handle(SpecificationEditCommand request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 0)
                    return 0;
                var entity=  await _db.Specifications.FirstOrDefaultAsync(b => b.Id == request.Id && b.DeletedByUserId==null);
                if (entity == null)
                    return 0;
                if (_ctx.IsModelStateValid())
                {
                    entity.Name = request.Name;
                    await _db.SaveChangesAsync(cancellationToken);
                    return entity.Id;
                }
                return 0;
            }
        }
    }
}
