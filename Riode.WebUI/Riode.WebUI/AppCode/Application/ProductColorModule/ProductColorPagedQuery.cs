using MediatR;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using Riode.WebUI.Models.ViewModels;

namespace Riode.WebUI.AppCode.Application.ProductColorModule
{
    public class ProductColorPagedQuery: IRequest<PagedViewModel<ProductColor>>
    {

        int pageIndex;
        int pageSize;
        public int? Id { get; set; }
        public int PageIndex
        {
            get
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
                else { pageSize = 10; }
               
            }
        }

        public class ProductColorPagedQueryHandler : IRequestHandler<ProductColorPagedQuery, PagedViewModel<ProductColor>>
        {
            private readonly RiodeDbContext _db;
            public ProductColorPagedQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<PagedViewModel<ProductColor>> Handle(ProductColorPagedQuery request, CancellationToken cancellationToken)
            {
                var query = _db.ProductColors.Where(b => b.DeletedByUserId == null);
                var pagedModel = new PagedViewModel<ProductColor>(query, request.PageIndex, request.PageSize);

                return pagedModel;
            }
        }
    }
}
