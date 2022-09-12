using MediatR;
using Riode.WebUI.Models.DAL;
using Riode.WebUI.Models.Entities;
using Riode.WebUI.Models.ViewModels;

namespace Riode.WebUI.AppCode.Application.ProductSizeModule
{
    public class ProductSizePagedQuery :IRequest<PagedViewModel<ProductSize>>
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
                pageIndex = 1;
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
                pageSize = 10;
            }
        }

        public class ProductSizePagedQueryHandler : IRequestHandler<ProductSizePagedQuery, PagedViewModel<ProductSize>>
        {
            private readonly RiodeDbContext _db;
            public ProductSizePagedQueryHandler(RiodeDbContext db)
            {
                _db = db;
            }
            public async Task<PagedViewModel<ProductSize>> Handle(ProductSizePagedQuery request, CancellationToken cancellationToken)
            {
                var query = _db.ProductSizes.Where(b => b.DeletedByUserId == null);
                var pagedModel = new PagedViewModel<ProductSize>(query, request.PageIndex, request.PageSize);

                return pagedModel;
            }
        }
    }
}
