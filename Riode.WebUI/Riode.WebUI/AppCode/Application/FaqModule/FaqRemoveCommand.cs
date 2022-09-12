using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.AppCode.Infrastructure;
using Riode.WebUI.Models.DAL;

namespace Riode.WebUI.AppCode.Application.FaqModule
{
    public class FaqRemoveCommand: IRequest<CommandJsonResponse>
    {
        public int? Id { get; set; }
        public class FaqRemoveCommandHandler : IRequestHandler<FaqRemoveCommand, CommandJsonResponse>
        {
            private readonly RiodeDbContext _db;
            public FaqRemoveCommandHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<CommandJsonResponse> Handle(FaqRemoveCommand request, CancellationToken cancellationToken)
            {
                var response = new CommandJsonResponse();
                if (request.Id == null || request.Id < 1)
                {
                    response.Error = true;
                    response.Message = "Melumat tamligi qorunmayib";
                    goto end;
                }
                var faq = await _db.Faqs.FirstOrDefaultAsync(m => m.Id == request.Id && m.DeletedByUserId == null);
                if (faq == null)
                {
                    response.Error = true;
                    response.Message = "Melumat movcud deyil";
                    goto end;
                }
                faq.DeletedByUserId = 1;
                faq.DeletedDate = DateTime.Now;
                await _db.SaveChangesAsync(cancellationToken);
                response.Error = false;
                response.Message = "Ugurlu Emeliyyat";
            end:
                return response;
            }


        }
    }
}
