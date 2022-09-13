using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.AppCode.Application.CategoryModule
{
    public class CategoryRemoveCommand: IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }
        public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand, CommandJsonResponse>
        {
            private readonly RiodeDbContext _db;
            public CategoryRemoveCommandHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<CommandJsonResponse> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();
                if (request.Id == null || request.Id < 1)
                {
                    response.Error = true;
                    response.Message = "Melumat tamligi qorunmayib";
                    goto end;
                }
                var category = await _db.Categories.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null);
                if (category == null)
                {
                    response.Error = true;
                    response.Message = "Melumat movcud deyil";
                    goto end;
                }
                category.DeletedByUserId = 1;
                category.DeletedDate = DateTime.Now;
                await _db.SaveChangesAsync(cancellationToken);
                response.Error = false;
                response.Message = "Ugurlu Emeliyyat";
            end:
                return response;
            }


        }
    }
}
