using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.BrandModule
{
    public class BrandCreateCommand:IRequest<int>
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public class BrandCreateCommandHandler : IRequestHandler<BrandCreateCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public BrandCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }
            public async Task<int> Handle(BrandCreateCommand request, CancellationToken cancellationToken)
            {
                if (_ctx.IsModelStateValid())
                {
                    Brand brand = new Brand();
                    brand.Name = request.Name;
                    brand.Description = request.Description;
                    _db.Add(brand);
                    await _db.SaveChangesAsync(cancellationToken);
                    return brand.Id;
                }
                return 0;
              
            }
        }
    }
}
