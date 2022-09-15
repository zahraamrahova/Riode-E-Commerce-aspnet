using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Riode.WebUI.AppCode.Extensions;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Riode.WebUI.AppCode.Application.CategoryModule
{
    public class CategoryCreateCommand: IRequest<int>
    {
      
        public int? ParentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, int>
        {
            private readonly RiodeDbContext _db;
            private readonly IActionContextAccessor _ctx;
            public CategoryCreateCommandHandler(RiodeDbContext db, IActionContextAccessor ctx)
            {
                _db = db;
                _ctx = ctx;
            }
            public async Task<int> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                if (_ctx.IsModelStateValid())
                {
                    Category category = new Category();
                    category.ParentId = request.ParentId;
                    category.Name = request.Name;
                    category.Description = request.Description;
                    _db.Add(category);
                    await _db.SaveChangesAsync(cancellationToken);
                    return category.Id;
                }
                return 0;

            }
        }
    }
}
