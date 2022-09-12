using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.AppCode.Application.ProductColorModule
{
    public class ProductColorRemoveCommand : IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }
        public class ProductColorRemoveCommandHandler : IRequestHandler<ProductColorRemoveCommand, CommandJsonResponse>
        {
            private readonly RiodeDbContext _db;
            public ProductColorRemoveCommandHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<CommandJsonResponse> Handle(ProductColorRemoveCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();
                if (request.Id == null || request.Id < 1)
                {
                    response.Error = true;
                    response.Message = "Melumat tamligi qorunmayib";
                    goto end;
                }
                var productColor = await _db.ProductColors.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null);
                if (productColor == null)
                {
                    response.Error = true;
                    response.Message = "Melumat movcud deyil";
                    goto end;
                }
                productColor.DeletedByUserId = 1;
                productColor.DeletedDate = DateTime.Now;
                await _db.SaveChangesAsync(cancellationToken);
                response.Error = false;
                response.Message = "Ugurlu Emeliyyat";
            end:
                return response;
            }


        }
    }
}
