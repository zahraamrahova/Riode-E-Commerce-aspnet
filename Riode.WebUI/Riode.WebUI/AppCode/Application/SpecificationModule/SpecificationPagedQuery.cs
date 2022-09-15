using MediatR;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using Riode.WebUI.Models.ViewModels;

namespace Riode.WebUI.AppCode.Application.SpecificationModule
{
    public class SpecificationPagedQuery : IRequest<PagedViewModel<Specification>>
    {
        int pageIndex;
        int pageSize;
        public int? Id { get; set; }
        public int PageIndex { get 
            {
                if (pageIndex > 0)
                    return pageIndex;
                return 1;

            }
            set
            {
                if (value > 0)
                    pageIndex = value;
                else
                {
                    pageIndex = 1;
                }
            }
        }
        public int PageSize
        {
            get
            {
                if (pageSize > 0)
                    return pageSize;
                return 10;

            }
            set
            {
                if (value > 0)
                    pageSize = value;
                else
                {
                    pageSize = 10;
                }
            }
        }

        public class SpecificationPagedQueryHandler : IRequestHandler<SpecificationPagedQuery, PagedViewModel<Specification>>
        {
            private readonly RiodeDbContext _db;
            public SpecificationPagedQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<PagedViewModel<Specification>> Handle(SpecificationPagedQuery request, CancellationToken cancellationToken)
            {
                var query = _db.Specifications.Where(b => b.DeletedByUserId == null);
                var pagedModel = new PagedViewModel<Specification>(query, request.PageIndex, request.PageSize);

                return pagedModel;
            }
        }
    } 
}
