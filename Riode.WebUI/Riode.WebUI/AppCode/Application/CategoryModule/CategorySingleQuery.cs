using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.CategoryModule
{
    public class CategorySingleQuery: IRequest<Category>
    {
        public int? Id { get; set; }
        public class CategorySingleQueryHandler : IRequestHandler<CategorySingleQuery, Category>
        {
            private readonly RiodeDbContext _db;
            public CategorySingleQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<Category> Handle(CategorySingleQuery request, CancellationToken cancellationToken)
            {
                if (request.Id == null || request.Id < 1)
                {
                    return null;
                }
                var category = await _db.Categories.Include(c => c.Parent).FirstOrDefaultAsync(m => m.Id == request.Id);

                return category;
            }
        }
    }
}
