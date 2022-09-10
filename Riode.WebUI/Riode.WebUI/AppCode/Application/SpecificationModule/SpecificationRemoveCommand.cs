using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.AppCode.Application.SpecificationModule
{
    public class SpecificationRemoveCommand: IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }
        public class SpecificationRemoveCommandHandler : IRequestHandler<SpecificationRemoveCommand, CommandJsonResponse>
        {
            private readonly RiodeDbContext _db;
            public SpecificationRemoveCommandHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<CommandJsonResponse> Handle(SpecificationRemoveCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();
                if (request.Id == null || request.Id < 1)
                {
                    response.Error = true;
                    response.Message = "Melumat tamligi qorunmayib";
                    goto end ;
                }
                var brand = await _db.Specifications.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId==null);
                if (brand==null)
                {
                    response.Error = true;
                    response.Message = "Melumat movcud deyil";
                    goto end;
                }
                brand.DeletedByUserId = 1;
                brand.DeletedDate = DateTime.Now;
                await _db.SaveChangesAsync(cancellationToken);
                response.Error = false;
                response.Message = "Ugurlu Emeliyyat";
            end:
                return response;
            }

           
        }
    }

   
}
