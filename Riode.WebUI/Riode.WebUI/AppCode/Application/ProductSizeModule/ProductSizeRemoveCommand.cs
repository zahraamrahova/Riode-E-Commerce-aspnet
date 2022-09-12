using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.AppCode.Application.ProductSizeModule
{
    public class ProductSizeRemoveCommand: IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }
        public class ProductSizeRemoveCommandHandler : IRequestHandler<ProductSizeRemoveCommand, CommandJsonResponse>
        {
            private readonly RiodeDbContext _db;
            public ProductSizeRemoveCommandHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<CommandJsonResponse> Handle(ProductSizeRemoveCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();
                if (request.Id == null || request.Id < 1)
                {
                    response.Error = true;
                    response.Message = "Melumat tamligi qorunmayib";
                    goto end;
                }
                var productSize = await _db.ProductSizes.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null);
                if (productSize == null)
                {
                    response.Error = true;
                    response.Message = "Melumat movcud deyil";
                    goto end;
                }
                productSize.DeletedByUserId = 1;
                productSize.DeletedDate = DateTime.Now;
                await _db.SaveChangesAsync(cancellationToken);
                response.Error = false;
                response.Message = "Ugurlu Emeliyyat";
            end:
                return response;
            }


        }
    }
}
