using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.ProductSizeModule
{
    public class ProductSizeCreateCommand:IRequest<int>
    {
        [Required]
        public string Abbr { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public class ProductSizeCreateCommandHandler : IRequestHandler<ProductSizeCreateCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public ProductSizeCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }
            public async Task<int> Handle(ProductSizeCreateCommand request, CancellationToken cancellationToken)
            {
                if (_ctx.IsModelStateValid())
                {
                    ProductSize productSize = new ProductSize();
                    productSize.Abbr = request.Abbr;
                    productSize.Name = request.Name;
                    productSize.Description = request.Description;
                    _db.Add(productSize);
                    await _db.SaveChangesAsync(cancellationToken);
                    return productSize.Id;
                }
                return 0;

            }
        }
    }
}
