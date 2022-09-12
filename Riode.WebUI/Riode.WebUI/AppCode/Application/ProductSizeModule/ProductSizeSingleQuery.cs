using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.ProductSizeModule
{
    public class ProductSizeSingleQuery: IRequest<ProductSize>
    {
        public int? Id { get; set; }
        public class ProductSizeSingleQueryHandler : IRequestHandler<ProductSizeSingleQuery, ProductSize>
        {
            private readonly RiodeDbContext _db;
            public ProductSizeSingleQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<ProductSize> Handle(ProductSizeSingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 1)
                {
                    return null;
                }
                var productSize = await _db.ProductSizes.FirstOrDefaultAsync(m => m.Id == request.Id);

                return productSize;
            }
        }
    }
}
