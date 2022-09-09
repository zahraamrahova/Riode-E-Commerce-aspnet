using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.BrandModule
{
    public class BrandSingleQuery: IRequest<Brand>
    {
        public int? Id { get; set; }
        public class BrandSingleQueryHandler : IRequestHandler<BrandSingleQuery, Brand>
        {
            private readonly RiodeDbContext _db;
            public BrandSingleQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<Brand> Handle(BrandSingleQuery request, CancellationToken cancellationToken)
            {
                if(request.Id == null || request.Id<1)
                {
                    return null;
                }
                var brand = await _db.Brands.FirstOrDefaultAsync(m => m.Id == request.Id);

                return brand;
            }
        }
    } 
}
