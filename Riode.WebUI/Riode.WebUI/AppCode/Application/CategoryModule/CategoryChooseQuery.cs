using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.CategoryModule
{  
        public class CategoryChooseQuery : IRequest<List<Category>>
        {
           
            public class CategoryChooseQueryHandler : IRequestHandler<CategoryChooseQuery, List<Category>>
            {
                private readonly RiodeDbContext _db;
                public CategoryChooseQueryHandler(RiodeDbContext db)
                {
                    _db = db;
                }
                public async Task<List<Category>> Handle(CategoryChooseQuery request, CancellationToken cancellationToken)
                {
                  
                    var category = await _db.Categories.Include(c => c.Parent).ToListAsync();

                    return category;
                }
            }
       }   
}
