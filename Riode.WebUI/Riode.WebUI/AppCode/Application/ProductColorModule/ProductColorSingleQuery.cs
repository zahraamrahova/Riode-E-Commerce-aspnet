using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.ProductColorModule
{
    public class ProductColorSingleQuery: IRequest<ProductColor>
    {
        public int? Id { get; set; }
        public class ProductColorSingleQueryHandler : IRequestHandler<ProductColorSingleQuery, ProductColor>
        {
            private readonly RiodeDbContext _db;
            public ProductColorSingleQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<ProductColor> Handle(ProductColorSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 1)
                {
                    return null;
                }
                var productColor = await _db.ProductColors.FirstOrDefaultAsync(m => m.Id == request.Id);

                return productColor;
            }
        }
    }
}
