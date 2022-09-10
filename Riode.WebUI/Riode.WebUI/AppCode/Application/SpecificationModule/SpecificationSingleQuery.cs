using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.SpecificationModule
{
    public class SpecificationSingleQuery: IRequest<Specification>
    {
        public int? Id { get; set; }
        public class SpecificationSingleQueryHandler : IRequestHandler<SpecificationSingleQuery, Specification>
        {
            private readonly RiodeDbContext _db;
            public SpecificationSingleQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<Specification> Handle(SpecificationSingleQuery request, CancellationToken cancellationToken)
            {
                if(request.Id == null || request.Id<1)
                {
                    return null;
                }
                var specification = await _db.Specifications.FirstOrDefaultAsync(m => m.Id == request.Id);

                return specification;
            }
        }
    } 
}
