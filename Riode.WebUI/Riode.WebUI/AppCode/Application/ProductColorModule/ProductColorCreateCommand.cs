using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.ProductColorModule
{
    public class ProductColorCreateCommand: IRequest<int>
    {
        [Required]
        public string HexCode { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public class ProductColorCreateCommandHandler : IRequestHandler<ProductColorCreateCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public ProductColorCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }
            public async Task<int> Handle(ProductColorCreateCommand request, CancellationToken cancellationToken)
            {
                if (_ctx.IsModelStateValid())
                {
                    ProductColor productColor = new ProductColor();
                    productColor.HexCode = request.HexCode;
                    productColor.Name = request.Name;
                    productColor.Description = request.Description;
                    _db.Add(productColor);
                    await _db.SaveChangesAsync(cancellationToken);
                    return productColor.Id;
                }
                return 0;

            }
        }
    }
}
