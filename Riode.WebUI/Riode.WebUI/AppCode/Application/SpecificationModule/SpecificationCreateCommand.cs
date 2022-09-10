using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.SpecificationModule
{
    public class SpecificationCreateCommand:IRequest<int>
    {
        [Required]
        public string Name { get; set; }
        public class SpecificationCreateCommandHandler : IRequestHandler<SpecificationCreateCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public SpecificationCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }
            public async Task<int> Handle(SpecificationCreateCommand request, CancellationToken cancellationToken)
            {
                if (_ctx.IsModelStateValid())
                {
                    Specification specification = new Specification();
                    specification.Name = request.Name;
                    _db.Add(specification);
                    await _db.SaveChangesAsync(cancellationToken);
                    return specification.Id;
                }
                return 0;
              
            }
        }
    }
}
